namespace DBUsageInspector
{
    partial class main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.filePathDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.sqlScriptPathLabel = new System.Windows.Forms.Label();
            this.sqlScriptPath = new System.Windows.Forms.TextBox();
            this.mainTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.sqlServerConnectionString = new System.Windows.Forms.TextBox();
            this.neo4jUrl = new System.Windows.Forms.TextBox();
            this.neo4jUsername = new System.Windows.Forms.TextBox();
            this.neo4jPassword = new System.Windows.Forms.TextBox();
            this.codePath = new System.Windows.Forms.TextBox();
            this.sqlServerConnectionStringLabel = new System.Windows.Forms.Label();
            this.neo4jUrlLabel = new System.Windows.Forms.Label();
            this.neo4jUsernameLabel = new System.Windows.Forms.Label();
            this.neo4jPasswordLabel = new System.Windows.Forms.Label();
            this.compareLists = new System.Windows.Forms.Button();
            this.getSQLServerReferences = new System.Windows.Forms.Button();
            this.referenceList = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.writeToNeo4j = new System.Windows.Forms.Button();
            this.sqlServerObjectPanel = new System.Windows.Forms.Panel();
            this.sqlServerNotInScriptsCount = new System.Windows.Forms.Label();
            this.sqlServerObjectCount = new System.Windows.Forms.Label();
            this.getSQLServerObjects = new System.Windows.Forms.Button();
            this.sqlServerObjectList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sqlScriptObjectPanel = new System.Windows.Forms.Panel();
            this.sqlScriptNotOnServerCount = new System.Windows.Forms.Label();
            this.sqlScriptObjectCount = new System.Windows.Forms.Label();
            this.getSQLScriptObjects = new System.Windows.Forms.Button();
            this.sqlScriptObjectList = new System.Windows.Forms.ListView();
            this.ScriptName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ScriptType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.codePathLabel = new System.Windows.Forms.Label();
            this.getCodeReferencesToSqlServerObjects = new System.Windows.Forms.Button();
            this.clearReferenceList = new System.Windows.Forms.Button();
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sqlServerObjectPanel.SuspendLayout();
            this.sqlScriptObjectPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // filePathDialog
            // 
            this.filePathDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.filePathDialog.ShowNewFolderButton = false;
            // 
            // sqlScriptPathLabel
            // 
            this.sqlScriptPathLabel.AutoSize = true;
            this.sqlScriptPathLabel.Location = new System.Drawing.Point(101, 44);
            this.sqlScriptPathLabel.Name = "sqlScriptPathLabel";
            this.sqlScriptPathLabel.Size = new System.Drawing.Size(109, 17);
            this.sqlScriptPathLabel.TabIndex = 0;
            this.sqlScriptPathLabel.Text = "SQL Script Path";
            // 
            // sqlScriptPath
            // 
            this.sqlScriptPath.Location = new System.Drawing.Point(217, 41);
            this.sqlScriptPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sqlScriptPath.Name = "sqlScriptPath";
            this.sqlScriptPath.Size = new System.Drawing.Size(849, 22);
            this.sqlScriptPath.TabIndex = 1;
            this.mainTooltip.SetToolTip(this.sqlScriptPath, "The top folder containing database project scripts. This folder and its decendent" +
        "s will be searched for *.sql files.");
            this.sqlScriptPath.Click += new System.EventHandler(this.sqlScriptPath_Click);
            this.sqlScriptPath.TextChanged += new System.EventHandler(this.sqlScriptPath_TextChanged);
            // 
            // mainTooltip
            // 
            this.mainTooltip.AutoPopDelay = 10000;
            this.mainTooltip.InitialDelay = 500;
            this.mainTooltip.IsBalloon = true;
            this.mainTooltip.ReshowDelay = 100;
            // 
            // sqlServerConnectionString
            // 
            this.sqlServerConnectionString.Location = new System.Drawing.Point(217, 69);
            this.sqlServerConnectionString.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sqlServerConnectionString.Name = "sqlServerConnectionString";
            this.sqlServerConnectionString.Size = new System.Drawing.Size(849, 22);
            this.sqlServerConnectionString.TabIndex = 3;
            this.mainTooltip.SetToolTip(this.sqlServerConnectionString, "The SQL Server connection string for the database being examined.");
            this.sqlServerConnectionString.Click += new System.EventHandler(this.sqlServerConnectionString_Click);
            this.sqlServerConnectionString.TextChanged += new System.EventHandler(this.sqlServerConnectionString_TextChanged);
            // 
            // neo4jUrl
            // 
            this.neo4jUrl.Location = new System.Drawing.Point(217, 98);
            this.neo4jUrl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.neo4jUrl.Name = "neo4jUrl";
            this.neo4jUrl.Size = new System.Drawing.Size(849, 22);
            this.neo4jUrl.TabIndex = 5;
            this.mainTooltip.SetToolTip(this.neo4jUrl, "The URL to an instance of Neo4j.");
            this.neo4jUrl.TextChanged += new System.EventHandler(this.neo4jUrl_TextChanged);
            // 
            // neo4jUsername
            // 
            this.neo4jUsername.Location = new System.Drawing.Point(217, 129);
            this.neo4jUsername.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.neo4jUsername.Name = "neo4jUsername";
            this.neo4jUsername.Size = new System.Drawing.Size(169, 22);
            this.neo4jUsername.TabIndex = 6;
            this.mainTooltip.SetToolTip(this.neo4jUsername, "Your Neo4j username.");
            this.neo4jUsername.TextChanged += new System.EventHandler(this.neo4jUsername_TextChanged);
            // 
            // neo4jPassword
            // 
            this.neo4jPassword.Location = new System.Drawing.Point(217, 158);
            this.neo4jPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.neo4jPassword.Name = "neo4jPassword";
            this.neo4jPassword.Size = new System.Drawing.Size(169, 22);
            this.neo4jPassword.TabIndex = 7;
            this.mainTooltip.SetToolTip(this.neo4jPassword, "Your Neo4j password.");
            this.neo4jPassword.TextChanged += new System.EventHandler(this.neo4jPassword_TextChanged);
            // 
            // codePath
            // 
            this.codePath.Location = new System.Drawing.Point(217, 12);
            this.codePath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.codePath.Name = "codePath";
            this.codePath.Size = new System.Drawing.Size(849, 22);
            this.codePath.TabIndex = 28;
            this.mainTooltip.SetToolTip(this.codePath, "The top folder containing project code. This folder and its decendents will be se" +
        "arched for *.vb, *.cs, files.");
            this.codePath.Click += new System.EventHandler(this.codePath_Click);
            this.codePath.TextChanged += new System.EventHandler(this.codePath_TextChanged);
            // 
            // sqlServerConnectionStringLabel
            // 
            this.sqlServerConnectionStringLabel.AutoSize = true;
            this.sqlServerConnectionStringLabel.Location = new System.Drawing.Point(13, 71);
            this.sqlServerConnectionStringLabel.Name = "sqlServerConnectionStringLabel";
            this.sqlServerConnectionStringLabel.Size = new System.Drawing.Size(198, 17);
            this.sqlServerConnectionStringLabel.TabIndex = 2;
            this.sqlServerConnectionStringLabel.Text = "SQL Server Connection String";
            // 
            // neo4jUrlLabel
            // 
            this.neo4jUrlLabel.AutoSize = true;
            this.neo4jUrlLabel.Location = new System.Drawing.Point(133, 102);
            this.neo4jUrlLabel.Name = "neo4jUrlLabel";
            this.neo4jUrlLabel.Size = new System.Drawing.Size(77, 17);
            this.neo4jUrlLabel.TabIndex = 4;
            this.neo4jUrlLabel.Text = "Neo4j URL";
            // 
            // neo4jUsernameLabel
            // 
            this.neo4jUsernameLabel.AutoSize = true;
            this.neo4jUsernameLabel.Location = new System.Drawing.Point(97, 132);
            this.neo4jUsernameLabel.Name = "neo4jUsernameLabel";
            this.neo4jUsernameLabel.Size = new System.Drawing.Size(114, 17);
            this.neo4jUsernameLabel.TabIndex = 8;
            this.neo4jUsernameLabel.Text = "Neo4j Username";
            // 
            // neo4jPasswordLabel
            // 
            this.neo4jPasswordLabel.AutoSize = true;
            this.neo4jPasswordLabel.Location = new System.Drawing.Point(101, 161);
            this.neo4jPasswordLabel.Name = "neo4jPasswordLabel";
            this.neo4jPasswordLabel.Size = new System.Drawing.Size(110, 17);
            this.neo4jPasswordLabel.TabIndex = 9;
            this.neo4jPasswordLabel.Text = "Neo4j Password";
            // 
            // compareLists
            // 
            this.compareLists.Enabled = false;
            this.compareLists.Location = new System.Drawing.Point(16, 442);
            this.compareLists.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.compareLists.Name = "compareLists";
            this.compareLists.Size = new System.Drawing.Size(1051, 27);
            this.compareLists.TabIndex = 21;
            this.compareLists.Text = "Compare Lists";
            this.compareLists.UseVisualStyleBackColor = true;
            this.compareLists.Click += new System.EventHandler(this.compareLists_Click);
            // 
            // getSQLServerReferences
            // 
            this.getSQLServerReferences.Enabled = false;
            this.getSQLServerReferences.Location = new System.Drawing.Point(16, 750);
            this.getSQLServerReferences.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.getSQLServerReferences.Name = "getSQLServerReferences";
            this.getSQLServerReferences.Size = new System.Drawing.Size(1051, 23);
            this.getSQLServerReferences.TabIndex = 23;
            this.getSQLServerReferences.Text = "Get SQL Server References";
            this.getSQLServerReferences.UseVisualStyleBackColor = true;
            this.getSQLServerReferences.Click += new System.EventHandler(this.getSQLServerReferences_Click);
            // 
            // referenceList
            // 
            this.referenceList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader8,
            this.columnHeader9});
            this.referenceList.Location = new System.Drawing.Point(15, 492);
            this.referenceList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.referenceList.Name = "referenceList";
            this.referenceList.Size = new System.Drawing.Size(1051, 191);
            this.referenceList.TabIndex = 22;
            this.referenceList.UseCompatibleStateImageBehavior = false;
            this.referenceList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Schema";
            this.columnHeader5.Width = 100;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Referencer";
            this.columnHeader6.Width = 200;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Type";
            this.columnHeader7.Width = 120;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Relationship";
            this.columnHeader3.Width = 150;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Schema";
            this.columnHeader4.Width = 100;
            // 
            // writeToNeo4j
            // 
            this.writeToNeo4j.Enabled = false;
            this.writeToNeo4j.Location = new System.Drawing.Point(16, 779);
            this.writeToNeo4j.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.writeToNeo4j.Name = "writeToNeo4j";
            this.writeToNeo4j.Size = new System.Drawing.Size(1051, 23);
            this.writeToNeo4j.TabIndex = 25;
            this.writeToNeo4j.Text = "Write to Neo4j";
            this.writeToNeo4j.UseVisualStyleBackColor = true;
            this.writeToNeo4j.Click += new System.EventHandler(this.writeToNeo4j_Click);
            // 
            // sqlServerObjectPanel
            // 
            this.sqlServerObjectPanel.Controls.Add(this.sqlServerNotInScriptsCount);
            this.sqlServerObjectPanel.Controls.Add(this.sqlServerObjectCount);
            this.sqlServerObjectPanel.Controls.Add(this.getSQLServerObjects);
            this.sqlServerObjectPanel.Controls.Add(this.sqlServerObjectList);
            this.sqlServerObjectPanel.Location = new System.Drawing.Point(549, 186);
            this.sqlServerObjectPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sqlServerObjectPanel.Name = "sqlServerObjectPanel";
            this.sqlServerObjectPanel.Size = new System.Drawing.Size(517, 250);
            this.sqlServerObjectPanel.TabIndex = 26;
            // 
            // sqlServerNotInScriptsCount
            // 
            this.sqlServerNotInScriptsCount.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.sqlServerNotInScriptsCount.Location = new System.Drawing.Point(367, 217);
            this.sqlServerNotInScriptsCount.Name = "sqlServerNotInScriptsCount";
            this.sqlServerNotInScriptsCount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.sqlServerNotInScriptsCount.Size = new System.Drawing.Size(148, 17);
            this.sqlServerNotInScriptsCount.TabIndex = 22;
            this.sqlServerNotInScriptsCount.Text = "0 not in scripts";
            this.sqlServerNotInScriptsCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.sqlServerNotInScriptsCount.Visible = false;
            // 
            // sqlServerObjectCount
            // 
            this.sqlServerObjectCount.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.sqlServerObjectCount.Location = new System.Drawing.Point(367, 199);
            this.sqlServerObjectCount.Name = "sqlServerObjectCount";
            this.sqlServerObjectCount.Size = new System.Drawing.Size(148, 17);
            this.sqlServerObjectCount.TabIndex = 21;
            this.sqlServerObjectCount.Text = "0 Objects";
            this.sqlServerObjectCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // getSQLServerObjects
            // 
            this.getSQLServerObjects.Enabled = false;
            this.getSQLServerObjects.Location = new System.Drawing.Point(180, 199);
            this.getSQLServerObjects.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.getSQLServerObjects.Name = "getSQLServerObjects";
            this.getSQLServerObjects.Size = new System.Drawing.Size(181, 39);
            this.getSQLServerObjects.TabIndex = 17;
            this.getSQLServerObjects.Text = "Get SQL Server Objects";
            this.getSQLServerObjects.UseVisualStyleBackColor = true;
            this.getSQLServerObjects.Click += new System.EventHandler(this.getSQLServerObjects_Click);
            // 
            // sqlServerObjectList
            // 
            this.sqlServerObjectList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader10});
            this.sqlServerObjectList.Location = new System.Drawing.Point(3, 2);
            this.sqlServerObjectList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sqlServerObjectList.Name = "sqlServerObjectList";
            this.sqlServerObjectList.Size = new System.Drawing.Size(512, 191);
            this.sqlServerObjectList.TabIndex = 16;
            this.sqlServerObjectList.UseCompatibleStateImageBehavior = false;
            this.sqlServerObjectList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Schema";
            this.columnHeader1.Width = 75;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 200;
            // 
            // sqlScriptObjectPanel
            // 
            this.sqlScriptObjectPanel.Controls.Add(this.sqlScriptNotOnServerCount);
            this.sqlScriptObjectPanel.Controls.Add(this.sqlScriptObjectCount);
            this.sqlScriptObjectPanel.Controls.Add(this.getSQLScriptObjects);
            this.sqlScriptObjectPanel.Controls.Add(this.sqlScriptObjectList);
            this.sqlScriptObjectPanel.Location = new System.Drawing.Point(16, 186);
            this.sqlScriptObjectPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sqlScriptObjectPanel.Name = "sqlScriptObjectPanel";
            this.sqlScriptObjectPanel.Size = new System.Drawing.Size(517, 250);
            this.sqlScriptObjectPanel.TabIndex = 23;
            // 
            // sqlScriptNotOnServerCount
            // 
            this.sqlScriptNotOnServerCount.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.sqlScriptNotOnServerCount.Location = new System.Drawing.Point(367, 217);
            this.sqlScriptNotOnServerCount.Name = "sqlScriptNotOnServerCount";
            this.sqlScriptNotOnServerCount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.sqlScriptNotOnServerCount.Size = new System.Drawing.Size(148, 17);
            this.sqlScriptNotOnServerCount.TabIndex = 21;
            this.sqlScriptNotOnServerCount.Text = "0 not on server";
            this.sqlScriptNotOnServerCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.sqlScriptNotOnServerCount.Visible = false;
            // 
            // sqlScriptObjectCount
            // 
            this.sqlScriptObjectCount.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.sqlScriptObjectCount.Location = new System.Drawing.Point(367, 199);
            this.sqlScriptObjectCount.Name = "sqlScriptObjectCount";
            this.sqlScriptObjectCount.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.sqlScriptObjectCount.Size = new System.Drawing.Size(148, 17);
            this.sqlScriptObjectCount.TabIndex = 20;
            this.sqlScriptObjectCount.Text = "0 Objects";
            this.sqlScriptObjectCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // getSQLScriptObjects
            // 
            this.getSQLScriptObjects.Enabled = false;
            this.getSQLScriptObjects.Location = new System.Drawing.Point(167, 199);
            this.getSQLScriptObjects.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.getSQLScriptObjects.Name = "getSQLScriptObjects";
            this.getSQLScriptObjects.Size = new System.Drawing.Size(181, 39);
            this.getSQLScriptObjects.TabIndex = 16;
            this.getSQLScriptObjects.Text = "Get SQL Script Objects";
            this.getSQLScriptObjects.UseVisualStyleBackColor = true;
            this.getSQLScriptObjects.Click += new System.EventHandler(this.getSQLScriptObjects_Click);
            // 
            // sqlScriptObjectList
            // 
            this.sqlScriptObjectList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ScriptName,
            this.ScriptType});
            this.sqlScriptObjectList.Location = new System.Drawing.Point(3, 2);
            this.sqlScriptObjectList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sqlScriptObjectList.Name = "sqlScriptObjectList";
            this.sqlScriptObjectList.Size = new System.Drawing.Size(512, 191);
            this.sqlScriptObjectList.TabIndex = 14;
            this.sqlScriptObjectList.UseCompatibleStateImageBehavior = false;
            this.sqlScriptObjectList.View = System.Windows.Forms.View.Details;
            // 
            // ScriptName
            // 
            this.ScriptName.Text = "Name";
            this.ScriptName.Width = 250;
            // 
            // ScriptType
            // 
            this.ScriptType.Text = "Type";
            this.ScriptType.Width = 100;
            // 
            // codePathLabel
            // 
            this.codePathLabel.AutoSize = true;
            this.codePathLabel.Location = new System.Drawing.Point(133, 15);
            this.codePathLabel.Name = "codePathLabel";
            this.codePathLabel.Size = new System.Drawing.Size(74, 17);
            this.codePathLabel.TabIndex = 27;
            this.codePathLabel.Text = "Code Path";
            // 
            // getCodeReferencesToSqlServerObjects
            // 
            this.getCodeReferencesToSqlServerObjects.Enabled = false;
            this.getCodeReferencesToSqlServerObjects.Location = new System.Drawing.Point(16, 718);
            this.getCodeReferencesToSqlServerObjects.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.getCodeReferencesToSqlServerObjects.Name = "getCodeReferencesToSqlServerObjects";
            this.getCodeReferencesToSqlServerObjects.Size = new System.Drawing.Size(1051, 23);
            this.getCodeReferencesToSqlServerObjects.TabIndex = 29;
            this.getCodeReferencesToSqlServerObjects.Text = "Get Code References To SQL Server Objects";
            this.getCodeReferencesToSqlServerObjects.UseVisualStyleBackColor = true;
            this.getCodeReferencesToSqlServerObjects.Click += new System.EventHandler(this.getCodeReferencesToSqlServerObjects_Click);
            // 
            // clearReferenceList
            // 
            this.clearReferenceList.Location = new System.Drawing.Point(16, 689);
            this.clearReferenceList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.clearReferenceList.Name = "clearReferenceList";
            this.clearReferenceList.Size = new System.Drawing.Size(1051, 23);
            this.clearReferenceList.TabIndex = 30;
            this.clearReferenceList.Text = "Clear List";
            this.clearReferenceList.UseVisualStyleBackColor = true;
            this.clearReferenceList.Click += new System.EventHandler(this.clearReferenceList_Click);
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Referencee";
            this.columnHeader8.Width = 200;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Type";
            this.columnHeader9.Width = 120;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Type";
            this.columnHeader10.Width = 150;
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1079, 812);
            this.Controls.Add(this.clearReferenceList);
            this.Controls.Add(this.getCodeReferencesToSqlServerObjects);
            this.Controls.Add(this.codePath);
            this.Controls.Add(this.codePathLabel);
            this.Controls.Add(this.sqlScriptObjectPanel);
            this.Controls.Add(this.sqlServerObjectPanel);
            this.Controls.Add(this.writeToNeo4j);
            this.Controls.Add(this.getSQLServerReferences);
            this.Controls.Add(this.referenceList);
            this.Controls.Add(this.compareLists);
            this.Controls.Add(this.neo4jPasswordLabel);
            this.Controls.Add(this.neo4jUsernameLabel);
            this.Controls.Add(this.neo4jPassword);
            this.Controls.Add(this.neo4jUsername);
            this.Controls.Add(this.neo4jUrl);
            this.Controls.Add(this.neo4jUrlLabel);
            this.Controls.Add(this.sqlServerConnectionString);
            this.Controls.Add(this.sqlServerConnectionStringLabel);
            this.Controls.Add(this.sqlScriptPath);
            this.Controls.Add(this.sqlScriptPathLabel);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "main";
            this.Text = "GraphMyDB";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.main_FormClosing);
            this.sqlServerObjectPanel.ResumeLayout(false);
            this.sqlScriptObjectPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog filePathDialog;
        private System.Windows.Forms.Label sqlScriptPathLabel;
        private System.Windows.Forms.TextBox sqlScriptPath;
        private System.Windows.Forms.ToolTip mainTooltip;
        private System.Windows.Forms.Label sqlServerConnectionStringLabel;
        private System.Windows.Forms.TextBox sqlServerConnectionString;
        private System.Windows.Forms.Label neo4jUrlLabel;
        private System.Windows.Forms.TextBox neo4jUrl;
        private System.Windows.Forms.TextBox neo4jUsername;
        private System.Windows.Forms.TextBox neo4jPassword;
        private System.Windows.Forms.Label neo4jUsernameLabel;
        private System.Windows.Forms.Label neo4jPasswordLabel;
        private System.Windows.Forms.Button compareLists;
        private System.Windows.Forms.Button getSQLServerReferences;
        private System.Windows.Forms.ListView referenceList;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button writeToNeo4j;
        private System.Windows.Forms.Panel sqlServerObjectPanel;
        private System.Windows.Forms.Label sqlServerNotInScriptsCount;
        private System.Windows.Forms.Label sqlServerObjectCount;
        private System.Windows.Forms.Button getSQLServerObjects;
        private System.Windows.Forms.ListView sqlServerObjectList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Panel sqlScriptObjectPanel;
        private System.Windows.Forms.Label sqlScriptNotOnServerCount;
        private System.Windows.Forms.Label sqlScriptObjectCount;
        private System.Windows.Forms.Button getSQLScriptObjects;
        private System.Windows.Forms.ListView sqlScriptObjectList;
        private System.Windows.Forms.ColumnHeader ScriptName;
        private System.Windows.Forms.ColumnHeader ScriptType;
        private System.Windows.Forms.TextBox codePath;
        private System.Windows.Forms.Label codePathLabel;
        private System.Windows.Forms.Button getCodeReferencesToSqlServerObjects;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Button clearReferenceList;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
    }
}

