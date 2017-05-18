using Microsoft.Data.ConnectionUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DBUsageInspector
{
    public partial class main : Form
    {
        private FileService codeFileService;
        private Neo4jService neo4jService;
        private FileService sqlScriptFileService;
        private SqlServerService sqlServerService;

        public main()
        {
            InitializeComponent();

            codeFileService = new FileService();
            sqlScriptFileService = new FileService();
            sqlServerService = new SqlServerService();
            neo4jService = new Neo4jService();

            LoadConfig();

            // Load reference objects if the save file exists
            LoadReferenceObjects();
        }

        private void clearReferenceList_Click(object sender, EventArgs e)
        {
            referenceList.Items.Clear();
            writeToNeo4j.Enabled = false;
        }

        private void codePath_Click(object sender, EventArgs e)
        {
            DialogResult result = filePathDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                codePath.Text = filePathDialog.SelectedPath;
            }
        }

        private void codePath_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(codePath.Text))
            {
                HashSet<string> codeExtensions = new HashSet<string>();
                codeExtensions.Add(".vb");
                codeExtensions.Add(".cs");

                codeFileService.RootPath = codePath.Text;
                codeFileService.Extensions = codeExtensions;
            }
        }

        private void compareLists_Click(object sender, EventArgs e)
        {
            int notOnServerCount = 0;
            int notInScriptCount = 0;

            // Script on Server
            foreach (ListViewItem sqlScriptObject in sqlScriptObjectList.Items)
            {
                bool matchFound = false;

                foreach (ListViewItem sqlServerObject in sqlServerObjectList.Items)
                {
                    if (sqlScriptObject.Text == sqlServerObject.Text)
                    {
                        matchFound = true;
                        break;
                    }
                }

                if (!matchFound)
                {
                    notOnServerCount++;
                    sqlScriptObject.BackColor = Color.Red;
                }
            }

            // Server in Script
            foreach (ListViewItem sqlServerObject in sqlServerObjectList.Items)
            {
                bool matchFound = false;

                foreach (ListViewItem sqlScriptObject in sqlServerObjectList.Items)
                {
                    if (sqlServerObject.Text == sqlScriptObject.Text)
                    {
                        matchFound = true;
                        break;
                    }
                }

                if (!matchFound)
                {
                    notInScriptCount++;
                    sqlServerObject.BackColor = Color.Red;
                }
            }

            sqlScriptNotOnServerCount.Text = notOnServerCount + " not on server";
            sqlScriptNotOnServerCount.Visible = true;
            sqlServerNotInScriptsCount.Text = notInScriptCount + " not in scripts";
            sqlServerNotInScriptsCount.Visible = true;
        }

        private void getCodeReferencesToSqlServerObjects_Click(object sender, EventArgs e)
        {
            PopulateReferences("CODE");
        }

        private void getSQLScriptObjects_Click(object sender, EventArgs e)
        {
            sqlScriptObjectList.Items.Clear();

            foreach (ReferenceObject referenceObject in sqlScriptFileService.GetSqlScriptObjects())
            {
                string[] item = new string[3];

                item[0] = referenceObject.Schema;
                item[1] = referenceObject.Name;
                item[2] = referenceObject.Type;

                sqlScriptObjectList.Items.Add(new ListViewItem(item));
            }

            sqlScriptObjectCount.Text = sqlScriptObjectList.Items.Count + " Objects";

            if (sqlScriptObjectList.Items.Count > 0 && sqlServerObjectList.Items.Count > 0)
            {
                compareLists.Enabled = true;
            }
        }

        private void getSQLServerObjects_Click(object sender, EventArgs e)
        {
            sqlServerObjectList.Items.Clear();

            foreach (ReferenceObject referenceObject in sqlServerService.GetObjects())
            {
                string[] item = new string[3];

                item[0] = referenceObject.Schema;
                item[1] = referenceObject.Name;
                item[2] = referenceObject.Type;

                sqlServerObjectList.Items.Add(new ListViewItem(item));
            }

            sqlServerObjectCount.Text = sqlServerObjectList.Items.Count + " Objects";

            if (sqlServerObjectList.Items.Count > 0)
            {
                if (sqlScriptObjectList.Items.Count > 0)
                {
                    compareLists.Enabled = true;
                }

                getCodeReferencesToSqlServerObjects.Enabled = true;
                getSQLServerReferences.Enabled = true;
            }
        }

        private void getSQLServerReferences_Click(object sender, EventArgs e)
        {
            PopulateReferences("SQL");
        }

        private bool LoadConfig()
        {
            IDictionary<string, string> settings = ConfigService.GetConfigSettings();

            if (settings.Count > 0)
            {
                codePath.Text = settings["codePath"];
                sqlScriptPath.Text = settings["sqlScriptPath"];
                sqlServerConnectionString.Text = settings["sqlServerConnectionString"];
                neo4jUrl.Text = settings["neo4jUrl"];
                neo4jUsername.Text = settings["neo4jUsername"];
                neo4jPassword.Text = settings["neo4jPassword"];

                return true;
            }

            return false;
        }

        private void LoadReferenceObjects()
        {
            FileService fileService = new FileService("./", new HashSet<string>());

            foreach (KeyValuePair<ReferenceObject, ReferenceObject> reference in fileService.LoadReferenceObjects())
            {
                string[] item = new string[7];

                item[0] = reference.Key.Schema;
                item[1] = reference.Key.Name;
                item[2] = reference.Key.Type;
                item[3] = reference.Key.Relationship;
                item[4] = reference.Value.Schema;
                item[5] = reference.Value.Name;
                item[6] = reference.Value.Type;

                referenceList.Items.Add(new ListViewItem(item));
            }

            if (referenceList.Items.Count > 0)
            {
                writeToNeo4j.Enabled = true;
            }
        }

        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveConfig();
        }

        private void neo4jPassword_TextChanged(object sender, EventArgs e)
        {
            neo4jService.Password = neo4jPassword.Text;
        }

        private void neo4jUrl_TextChanged(object sender, EventArgs e)
        {
            neo4jService.Url = neo4jUrl.Text;
        }

        private void neo4jUsername_TextChanged(object sender, EventArgs e)
        {
            neo4jService.Username = neo4jUsername.Text;
        }

        private void PopulateReferences(string referenceSource)
        {
            IDictionary<ReferenceObject, ReferenceObject> references = new Dictionary<ReferenceObject, ReferenceObject>();
            string referenceSourceDeclaration = string.Empty;

            DateTime startTime = DateTime.Now;

            //referenceList.Items.Clear();
            // Bundle SQL Server Objects from the list
            List<ReferenceObject> sqlServerObjects = new List<ReferenceObject>();

            foreach (ListViewItem item in sqlServerObjectList.Items)
            {
                sqlServerObjects.Add(new ReferenceObject(item.SubItems[1].Text, item.SubItems[2].Text, string.Empty, item.SubItems[0].Text));
            }

            // Determine the type of references we are retrieving
            if (referenceSource == "CODE")
            {
                references = codeFileService.GetReferencesToSqlServerObjects(sqlServerObjects);
                referenceSourceDeclaration = "Retrieving Code references";
            }
            else if (referenceSource == "SQL")
            {
                references = sqlServerService.GetReferences(sqlServerObjects);
                referenceSourceDeclaration = "Retrieving SQL references";
            }

            foreach (KeyValuePair<ReferenceObject, ReferenceObject> reference in references)
            {
                string[] item = new string[7];

                item[0] = reference.Key.Schema;
                item[1] = reference.Key.Name;
                item[2] = reference.Key.Type;
                item[3] = reference.Key.Relationship;
                item[4] = reference.Value.Schema;
                item[5] = reference.Value.Name;
                item[6] = reference.Value.Type;

                referenceList.Items.Add(new ListViewItem(item));
            }

            referenceList.Text = referenceList.Items.Count + " Objects";

            if (referenceList.Items.Count > 0)
            {
                writeToNeo4j.Enabled = true;
            }

            DateTime endTime = DateTime.Now;
            TimeSpan timeSpan = (endTime - startTime);

            MessageBox.Show(referenceSourceDeclaration + " took " + timeSpan.Hours.ToString() + " hours and " + timeSpan.Minutes.ToString() + " minutes and " + timeSpan.Seconds.ToString() + " seconds for " + references.Count + " references");
        }

        private void SaveConfig()
        {
            IDictionary<string, string> settings = new Dictionary<string, string>();

            settings.Add("codePath", codePath.Text);
            settings.Add("sqlScriptPath", sqlScriptPath.Text);
            settings.Add("sqlServerConnectionString", sqlServerConnectionString.Text);
            settings.Add("neo4jUrl", neo4jUrl.Text);
            settings.Add("neo4jUsername", neo4jUsername.Text);
            settings.Add("neo4jPassword", neo4jPassword.Text);

            ConfigService.SaveConfig(settings);
        }

        private void sqlScriptPath_Click(object sender, EventArgs e)
        {
            DialogResult result = filePathDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                sqlScriptPath.Text = filePathDialog.SelectedPath;
            }
        }

        private void sqlScriptPath_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(sqlScriptPath.Text))
            {
                HashSet<string> sqlScriptExtensions = new HashSet<string>();
                sqlScriptExtensions.Add(".sql");

                sqlScriptFileService.RootPath = sqlScriptPath.Text;
                sqlScriptFileService.Extensions = sqlScriptExtensions;

                getSQLScriptObjects.Enabled = true;
            }
        }

        private void sqlServerConnectionString_Click(object sender, EventArgs e)
        {
            using (var dialog = new DataConnectionDialog())
            {
                dialog.DataSources.Add(DataSource.SqlDataSource);

                DialogResult result = DataConnectionDialog.Show(dialog);

                if (result == DialogResult.OK)
                {
                    sqlServerConnectionString.Text = dialog.ConnectionString;
                }
            }
        }

        private void sqlServerConnectionString_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(sqlServerConnectionString.Text))
            {
                sqlServerService.ConnectionString = sqlServerConnectionString.Text;

                getSQLServerObjects.Enabled = true;
            }
        }

        private void writeToNeo4j_Click(object sender, EventArgs e)
        {
            DateTime startTime = DateTime.Now;

            List<ReferenceObject> nodes = new List<ReferenceObject>();

            // Add CODE nodes (Only those with references are necessary)
            foreach (ListViewItem item in referenceList.Items)
            {
                if (item.SubItems[2].Text == "CODE")
                {
                    bool addNewNode = true;
                    ReferenceObject newNode = new ReferenceObject(item.SubItems[1].Text, item.SubItems[2].Text, item.SubItems[3].Text, item.SubItems[0].Text);

                    foreach (ReferenceObject existingNode in nodes)
                    {
                        if (existingNode.Name == newNode.Name) // only add the reference object once to avoid duplicates
                        {
                            addNewNode = false;
                            break;
                        }
                    }

                    if (addNewNode)
                        nodes.Add(newNode);
                }
            }

            // Add SQL Server Objects (All existing objects on SQL Server are necessary)
            foreach (ListViewItem item in sqlServerObjectList.Items)
            {
                nodes.Add(new ReferenceObject(item.SubItems[1].Text, item.SubItems[2].Text, string.Empty, item.SubItems[0].Text));
            }

            // Create Relationships
            Dictionary<ReferenceObject, ReferenceObject> relationships = new Dictionary<ReferenceObject, ReferenceObject>();

            foreach (ListViewItem item in referenceList.Items)
            {
                ReferenceObject referrer = new ReferenceObject(item.SubItems[1].Text, item.SubItems[2].Text, item.SubItems[3].Text, item.SubItems[0].Text);
                ReferenceObject referree = new ReferenceObject(item.SubItems[5].Text, item.SubItems[6].Text, string.Empty, item.SubItems[4].Text);

                relationships.Add(referrer, referree);
            }

            // Before sending everything to Neo4j, save a file containing the references
            FileService fileService = new FileService("./", new HashSet<string>());
            fileService.SaveReferenceObjects(relationships);

            neo4jService.CreateObjects(nodes);
            neo4jService.CreateRelationships(relationships);

            DateTime endTime = DateTime.Now;
            TimeSpan timeSpan = (endTime - startTime);

            MessageBox.Show("writeToNeo4j_Click took " + timeSpan.Hours.ToString() + " hours and " + timeSpan.Minutes.ToString() + " minutes and " + timeSpan.Seconds.ToString() + " seconds for " + referenceList.Items.Count + " references");
        }
    }
}