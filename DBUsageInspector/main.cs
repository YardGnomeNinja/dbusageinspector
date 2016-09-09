using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.ConnectionUI;
using System.Diagnostics;

namespace DBUsageInspector
{
    public partial class main : Form
    {
        private FileService codeFileService;
        private FileService sqlScriptFileService;
        private SqlServerService sqlServerService;
        private Neo4jService neo4jService;

        public main()
        {
            InitializeComponent();

            HashSet<string> codeExtensions = new HashSet<string>();
            codeExtensions.Add(".vb");
            codeExtensions.Add(".cs");

            HashSet<string> sqlScriptExtensions = new HashSet<string>();
            sqlScriptExtensions.Add(".sql");

            codeFileService = new FileService(codePath.Text, codeExtensions);
            sqlScriptFileService = new FileService(sqlScriptPath.Text, sqlScriptExtensions);
            sqlServerService = new SqlServerService(sqlServerConnectionString.Text);
            neo4jService = new Neo4jService(neo4jUrl.Text, neo4jUsername.Text, neo4jPassword.Text);

            // Load reference objects if the save file exists
            LoadReferenceObjects();
        }

        private void sqlScriptPath_Click(object sender, EventArgs e)
        {
            DialogResult result = sqlScriptPathDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                sqlScriptPath.Text = sqlScriptPathDialog.SelectedPath;
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

        private void sqlScriptPath_TextChanged(object sender, EventArgs e)
        {
            sqlScriptFileService.RootPath = sqlScriptPath.Text;
        }

        private void sqlServerConnectionString_TextChanged(object sender, EventArgs e)
        {
            sqlServerService.ConnectionString = sqlServerConnectionString.Text;
        }

        private void neo4jUrl_TextChanged(object sender, EventArgs e)
        {
            neo4jService.Url = neo4jUrl.Text;
        }

        private void neo4jUsername_TextChanged(object sender, EventArgs e)
        {
            neo4jService.Username = neo4jUsername.Text;
        }

        private void neo4jPassword_TextChanged(object sender, EventArgs e)
        {
            neo4jService.Password = neo4jPassword.Text;
        }

        private void getSQLScriptObjects_Click(object sender, EventArgs e)
        {
            sqlScriptObjectList.Items.Clear();

            foreach (ReferenceObject referenceObject in sqlScriptFileService.GetSqlScriptObjects())
            {
                string[] item = new string[2];

                item[0] = referenceObject.Name;
                item[1] = referenceObject.Type;

                sqlScriptObjectList.Items.Add(new ListViewItem(item));
            }

            sqlScriptObjectCount.Text = sqlScriptObjectList.Items.Count + " Objects";
        }

        private void getSQLServerObjects_Click(object sender, EventArgs e)
        {
            sqlServerObjectList.Items.Clear();

            foreach (ReferenceObject referenceObject in sqlServerService.GetObjects())
            {
                string[] item = new string[2];

                item[0] = referenceObject.Name;
                item[1] = referenceObject.Type;

                sqlServerObjectList.Items.Add(new ListViewItem(item));
            }

            sqlServerObjectCount.Text = sqlServerObjectList.Items.Count + " Objects";

            if (sqlServerObjectList.Items.Count > 0)
            {
                getCodeReferencesToSqlServerObjects.Enabled = true;
                getSQLServerReferences.Enabled = true;
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

        private void getSQLServerReferences_Click(object sender, EventArgs e)
        {
            PopulateReferences("SQL");
        }

        private void getCodeReferencesToSqlServerObjects_Click(object sender, EventArgs e)
        {
            PopulateReferences("CODE");
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
                sqlServerObjects.Add(new ReferenceObject(item.SubItems[0].Text, item.SubItems[1].Text, string.Empty));
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
                string[] item = new string[5];

                item[0] = reference.Key.Name;
                item[1] = reference.Key.Type;
                item[2] = reference.Key.Relationship;
                item[3] = reference.Value.Name;
                item[4] = reference.Value.Type;

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

        private void writeToNeo4j_Click(object sender, EventArgs e)
        {
            DateTime startTime = DateTime.Now;

            List<ReferenceObject> newReferenceObjects = new List<ReferenceObject>();

            // Add CODE nodes (Only those with references are necessary)
            foreach (ListViewItem item in referenceList.Items)
            {
                if (item.SubItems[1].Text == "CODE")
                {
                    bool addNewReferenceObject = true;
                    ReferenceObject newReferenceObject = new ReferenceObject(item.SubItems[0].Text, item.SubItems[1].Text, item.SubItems[2].Text);

                    foreach (ReferenceObject existingObject in newReferenceObjects)
                    {
                        if (existingObject.Name == newReferenceObject.Name) // only add the reference object once to avoid duplicates
                        {
                            addNewReferenceObject = false;
                            break;
                        }
                    }

                    if (addNewReferenceObject)
                        newReferenceObjects.Add(newReferenceObject);
                }
            }

            // Add SQL Server Objects (All existing objects on SQL Server are necessary)
            foreach (ListViewItem item in sqlServerObjectList.Items)
            {
                newReferenceObjects.Add(new ReferenceObject(item.SubItems[0].Text, item.SubItems[1].Text, string.Empty));
            }

            neo4jService.CreateObjects(newReferenceObjects);

            // Create Relationships
            Dictionary<ReferenceObject, ReferenceObject> referenceObjects = new Dictionary<ReferenceObject, ReferenceObject>();

            foreach (ListViewItem item in referenceList.Items)
            {
                ReferenceObject referrer = new ReferenceObject(item.SubItems[0].Text, item.SubItems[1].Text, item.SubItems[2].Text);
                ReferenceObject referree = new ReferenceObject(item.SubItems[3].Text, item.SubItems[4].Text, string.Empty);

                referenceObjects.Add(referrer, referree);
            }

            // Before sending everything to Neo4j, save a file containing the references
            FileService fileService = new FileService("./", new HashSet<string>());
            fileService.SaveReferenceObjects(referenceObjects);

            neo4jService.CreateRelationships(referenceObjects);

            DateTime endTime = DateTime.Now;
            TimeSpan timeSpan = (startTime - endTime);

            MessageBox.Show("writeToNeo4j_Click took " + timeSpan.Hours.ToString() + " hours and " + timeSpan.Minutes.ToString() + " minutes and " + timeSpan.Seconds.ToString() + " seconds for " + referenceList.Items.Count + " references");
        }

        private void LoadReferenceObjects()
        {
            FileService fileService = new FileService("./", new HashSet<string>());

            foreach (KeyValuePair<ReferenceObject, ReferenceObject> reference in fileService.LoadReferenceObjects())
            {
                string[] item = new string[5];

                item[0] = reference.Key.Name;
                item[1] = reference.Key.Type;
                item[2] = reference.Key.Relationship;
                item[3] = reference.Value.Name;
                item[4] = reference.Value.Type;

                referenceList.Items.Add(new ListViewItem(item));
            }
        }
    }
}
