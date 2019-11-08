namespace FreehandQuery
{
    partial class FreehandQueryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FreehandQueryForm));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btNew = new System.Windows.Forms.Button();
            this.btExport = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            this.btLoad = new System.Windows.Forms.Button();
            this.btRun = new System.Windows.Forms.Button();
            this.btnCloseScript = new System.Windows.Forms.Button();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.addTableName = new System.Windows.Forms.CheckBox();
            this.btRefreshTableList = new System.Windows.Forms.Button();
            this.tvTables = new System.Windows.Forms.TreeView();
            this.sqlCommandMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectTop1000ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scriptTableAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cREATEToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dROPToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.sELECTToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iNSERTToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uPDATEToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dELETEToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tcSQLDocuments = new System.Windows.Forms.TabControl();
            this.newQuery = new System.Windows.Forms.TabPage();
            this.QueryEditor = new FastColoredTextBoxNS.FastColoredTextBox();
            this.tcQueryResults = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.QueryResult = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.queryMessage = new System.Windows.Forms.TextBox();
            this.lbFilename = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.sqlCommandMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tcSQLDocuments.SuspendLayout();
            this.newQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.QueryEditor)).BeginInit();
            this.tcQueryResults.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.QueryResult)).BeginInit();
            this.SuspendLayout();
            // 
            // btNew
            // 
            this.btNew.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btNew.BackgroundImage")));
            this.btNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btNew.Location = new System.Drawing.Point(26, 8);
            this.btNew.Name = "btNew";
            this.btNew.Size = new System.Drawing.Size(38, 33);
            this.btNew.TabIndex = 8;
            this.toolTip1.SetToolTip(this.btNew, "New SQL Query (Ctrl+N)");
            this.btNew.UseVisualStyleBackColor = true;
            this.btNew.Click += new System.EventHandler(this.btNew_Click);
            // 
            // btExport
            // 
            this.btExport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btExport.BackgroundImage")));
            this.btExport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btExport.Location = new System.Drawing.Point(227, 7);
            this.btExport.Name = "btExport";
            this.btExport.Size = new System.Drawing.Size(38, 34);
            this.btExport.TabIndex = 6;
            this.toolTip1.SetToolTip(this.btExport, "Export to Excel (F6 , Ctrl+E)");
            this.btExport.UseVisualStyleBackColor = true;
            this.btExport.Click += new System.EventHandler(this.btExport_Click);
            // 
            // btSave
            // 
            this.btSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btSave.BackgroundImage")));
            this.btSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btSave.Location = new System.Drawing.Point(116, 7);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(38, 34);
            this.btSave.TabIndex = 5;
            this.toolTip1.SetToolTip(this.btSave, "Save SQL Query (F2 , Ctrl+S)");
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btLoad
            // 
            this.btLoad.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btLoad.BackgroundImage")));
            this.btLoad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btLoad.Location = new System.Drawing.Point(72, 8);
            this.btLoad.Name = "btLoad";
            this.btLoad.Size = new System.Drawing.Size(38, 33);
            this.btLoad.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btLoad, "Open SQL Query (Ctrl+L , Ctrl+O)");
            this.btLoad.UseVisualStyleBackColor = true;
            this.btLoad.Click += new System.EventHandler(this.btLoad_Click);
            // 
            // btRun
            // 
            this.btRun.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btRun.BackgroundImage")));
            this.btRun.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btRun.Location = new System.Drawing.Point(160, 7);
            this.btRun.Name = "btRun";
            this.btRun.Size = new System.Drawing.Size(38, 34);
            this.btRun.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btRun, "Execute Query (F5 , Ctrl+R)");
            this.btRun.UseVisualStyleBackColor = true;
            this.btRun.Click += new System.EventHandler(this.btRun_Click);
            // 
            // btnCloseScript
            // 
            this.btnCloseScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseScript.BackgroundImage = global::this.Resources.Remove_16;
            this.btnCloseScript.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCloseScript.Location = new System.Drawing.Point(899, 7);
            this.btnCloseScript.Name = "btnCloseScript";
            this.btnCloseScript.Size = new System.Drawing.Size(38, 34);
            this.btnCloseScript.TabIndex = 19;
            this.toolTip1.SetToolTip(this.btnCloseScript, "Would you like to save before closing?");
            this.btnCloseScript.UseVisualStyleBackColor = true;
            this.btnCloseScript.Click += new System.EventHandler(this.btnCloseScript_Click);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 631);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(961, 33);
            this.pnlBottom.TabIndex = 18;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnCancel.Location = new System.Drawing.Point(879, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.Location = new System.Drawing.Point(0, 42);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            this.splitContainer2.Panel1.Controls.Add(this.addTableName);
            this.splitContainer2.Panel1.Controls.Add(this.btRefreshTableList);
            this.splitContainer2.Panel1.Controls.Add(this.tvTables);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer2.Size = new System.Drawing.Size(961, 500);
            this.splitContainer2.SplitterDistance = 265;
            this.splitContainer2.TabIndex = 17;
            this.splitContainer2.Paint += new System.Windows.Forms.PaintEventHandler(this.TablesAndColumns_Paint);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "Tables";
            // 
            // addTableName
            // 
            this.addTableName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addTableName.AutoSize = true;
            this.addTableName.Location = new System.Drawing.Point(100, 7);
            this.addTableName.Name = "addTableName";
            this.addTableName.Size = new System.Drawing.Size(116, 19);
            this.addTableName.TabIndex = 15;
            this.addTableName.Text = "Add Table name";
            this.addTableName.UseVisualStyleBackColor = true;
            // 
            // btRefreshTableList
            // 
            this.btRefreshTableList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btRefreshTableList.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btRefreshTableList.BackgroundImage")));
            this.btRefreshTableList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btRefreshTableList.Location = new System.Drawing.Point(220, 3);
            this.btRefreshTableList.Name = "btRefreshTableList";
            this.btRefreshTableList.Size = new System.Drawing.Size(28, 25);
            this.btRefreshTableList.TabIndex = 17;
            this.btRefreshTableList.UseVisualStyleBackColor = true;
            this.btRefreshTableList.Click += new System.EventHandler(this.btRefreshTableList_Click);
            // 
            // tvTables
            // 
            this.tvTables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvTables.ContextMenuStrip = this.sqlCommandMenu;
            this.tvTables.Location = new System.Drawing.Point(12, 32);
            this.tvTables.Name = "tvTables";
            this.tvTables.PathSeparator = ".";
            this.tvTables.ShowNodeToolTips = true;
            this.tvTables.Size = new System.Drawing.Size(250, 465);
            this.tvTables.TabIndex = 18;
            this.tvTables.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvTables_ItemDrag);
            this.tvTables.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvTables_NodeMouseClick);
            this.tvTables.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvTables_NodeMouseDoubleClick);
            this.tvTables.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tvTables_KeyDown);
            // 
            // sqlCommandMenu
            // 
            this.sqlCommandMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectTop1000ToolStripMenuItem,
            this.scriptTableAsToolStripMenuItem});
            this.sqlCommandMenu.Name = "sqlCommandMenu";
            this.sqlCommandMenu.Size = new System.Drawing.Size(187, 48);
            this.sqlCommandMenu.Opening += new System.ComponentModel.CancelEventHandler(this.sqlCommandMenu_Opening);
            // 
            // selectTop1000ToolStripMenuItem
            // 
            this.selectTop1000ToolStripMenuItem.Name = "selectTop1000ToolStripMenuItem";
            this.selectTop1000ToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.selectTop1000ToolStripMenuItem.Text = "Select Top 1000 Rows";
            this.selectTop1000ToolStripMenuItem.Click += new System.EventHandler(this.selectTop1000ToolStripMenuItem_Click);
            // 
            // scriptTableAsToolStripMenuItem
            // 
            this.scriptTableAsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cREATEToToolStripMenuItem,
            this.dROPToToolStripMenuItem,
            this.toolStripSeparator1,
            this.sELECTToToolStripMenuItem,
            this.iNSERTToToolStripMenuItem,
            this.uPDATEToToolStripMenuItem,
            this.dELETEToToolStripMenuItem});
            this.scriptTableAsToolStripMenuItem.Name = "scriptTableAsToolStripMenuItem";
            this.scriptTableAsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.scriptTableAsToolStripMenuItem.Text = "Script Table";
            // 
            // cREATEToToolStripMenuItem
            // 
            this.cREATEToToolStripMenuItem.Name = "cREATEToToolStripMenuItem";
            this.cREATEToToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.cREATEToToolStripMenuItem.Text = "CREATE Script";
            this.cREATEToToolStripMenuItem.Click += new System.EventHandler(this.cREATEToToolStripMenuItem_Click);
            // 
            // dROPToToolStripMenuItem
            // 
            this.dROPToToolStripMenuItem.Name = "dROPToToolStripMenuItem";
            this.dROPToToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.dROPToToolStripMenuItem.Text = "DROP Script";
            this.dROPToToolStripMenuItem.Click += new System.EventHandler(this.dROPToToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(147, 6);
            // 
            // sELECTToToolStripMenuItem
            // 
            this.sELECTToToolStripMenuItem.Name = "sELECTToToolStripMenuItem";
            this.sELECTToToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.sELECTToToolStripMenuItem.Text = "SELECT Script";
            this.sELECTToToolStripMenuItem.Click += new System.EventHandler(this.sELECTToToolStripMenuItem_Click);
            // 
            // iNSERTToToolStripMenuItem
            // 
            this.iNSERTToToolStripMenuItem.Name = "iNSERTToToolStripMenuItem";
            this.iNSERTToToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.iNSERTToToolStripMenuItem.Text = "INSERT Script";
            this.iNSERTToToolStripMenuItem.Click += new System.EventHandler(this.iNSERTToToolStripMenuItem_Click);
            // 
            // uPDATEToToolStripMenuItem
            // 
            this.uPDATEToToolStripMenuItem.Name = "uPDATEToToolStripMenuItem";
            this.uPDATEToToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.uPDATEToToolStripMenuItem.Text = "UPDATE Script";
            this.uPDATEToToolStripMenuItem.Click += new System.EventHandler(this.uPDATEToToolStripMenuItem_Click);
            // 
            // dELETEToToolStripMenuItem
            // 
            this.dELETEToToolStripMenuItem.Name = "dELETEToToolStripMenuItem";
            this.dELETEToToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.dELETEToToolStripMenuItem.Text = "DELETE Script";
            this.dELETEToToolStripMenuItem.Click += new System.EventHandler(this.dELETEToToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tcSQLDocuments);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tcQueryResults);
            this.splitContainer1.Size = new System.Drawing.Size(689, 497);
            this.splitContainer1.SplitterDistance = 213;
            this.splitContainer1.TabIndex = 2;
            this.splitContainer1.Paint += new System.Windows.Forms.PaintEventHandler(this.TablesAndColumns_Paint);
            // 
            // tcSQLDocuments
            // 
            this.tcSQLDocuments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcSQLDocuments.Controls.Add(this.newQuery);
            this.tcSQLDocuments.Location = new System.Drawing.Point(3, 5);
            this.tcSQLDocuments.Name = "tcSQLDocuments";
            this.tcSQLDocuments.SelectedIndex = 0;
            this.tcSQLDocuments.Size = new System.Drawing.Size(665, 206);
            this.tcSQLDocuments.TabIndex = 1;
            this.tcSQLDocuments.SelectedIndexChanged += new System.EventHandler(this.tcSQLDocuments_SelectedIndexChanged);
            this.tcSQLDocuments.Click += new System.EventHandler(this.tcSQLDocuments_Click);
            // 
            // newQuery
            // 
            this.newQuery.Controls.Add(this.QueryEditor);
            this.newQuery.Location = new System.Drawing.Point(4, 24);
            this.newQuery.Name = "newQuery";
            this.newQuery.Padding = new System.Windows.Forms.Padding(3);
            this.newQuery.Size = new System.Drawing.Size(657, 178);
            this.newQuery.TabIndex = 1;
            this.newQuery.Text = "NewQuery.sql";
            this.newQuery.UseVisualStyleBackColor = true;
            // 
            // QueryEditor
            // 
            this.QueryEditor.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.QueryEditor.AutoIndentCharsPatterns = "";
            this.QueryEditor.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.QueryEditor.BackBrush = null;
            this.QueryEditor.CharHeight = 14;
            this.QueryEditor.CharWidth = 8;
            this.QueryEditor.CommentPrefix = "--";
            this.QueryEditor.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.QueryEditor.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.QueryEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QueryEditor.IsReplaceMode = false;
            this.QueryEditor.Language = FastColoredTextBoxNS.Language.SQL;
            this.QueryEditor.LeftBracket = '(';
            this.QueryEditor.Location = new System.Drawing.Point(3, 3);
            this.QueryEditor.Name = "QueryEditor";
            this.QueryEditor.Paddings = new System.Windows.Forms.Padding(0);
            this.QueryEditor.RightBracket = ')';
            this.QueryEditor.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.QueryEditor.ShowFoldingLines = true;
            this.QueryEditor.Size = new System.Drawing.Size(651, 172);
            this.QueryEditor.TabIndex = 1;
            this.QueryEditor.VirtualSpace = true;
            this.QueryEditor.Zoom = 100;
            // 
            // tcQueryResults
            // 
            this.tcQueryResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcQueryResults.Controls.Add(this.tabPage1);
            this.tcQueryResults.Location = new System.Drawing.Point(3, 0);
            this.tcQueryResults.Name = "tcQueryResults";
            this.tcQueryResults.SelectedIndex = 0;
            this.tcQueryResults.Size = new System.Drawing.Size(665, 280);
            this.tcQueryResults.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this.QueryResult);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(657, 252);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Result";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // QueryResult
            // 
            this.QueryResult.AllowUserToAddRows = false;
            this.QueryResult.AllowUserToDeleteRows = false;
            this.QueryResult.AllowUserToOrderColumns = true;
            this.QueryResult.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.QueryResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.QueryResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QueryResult.Location = new System.Drawing.Point(3, 3);
            this.QueryResult.Name = "QueryResult";
            this.QueryResult.ReadOnly = true;
            this.QueryResult.RowHeadersVisible = false;
            this.QueryResult.Size = new System.Drawing.Size(651, 246);
            this.QueryResult.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 545);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "Message Box";
            // 
            // queryMessage
            // 
            this.queryMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.queryMessage.BackColor = System.Drawing.SystemColors.Control;
            this.queryMessage.ForeColor = System.Drawing.Color.Red;
            this.queryMessage.Location = new System.Drawing.Point(12, 564);
            this.queryMessage.Multiline = true;
            this.queryMessage.Name = "queryMessage";
            this.queryMessage.ReadOnly = true;
            this.queryMessage.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.queryMessage.Size = new System.Drawing.Size(937, 60);
            this.queryMessage.TabIndex = 9;
            // 
            // lbFilename
            // 
            this.lbFilename.AutoSize = true;
            this.lbFilename.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbFilename.Location = new System.Drawing.Point(289, 16);
            this.lbFilename.Name = "lbFilename";
            this.lbFilename.Size = new System.Drawing.Size(85, 17);
            this.lbFilename.TabIndex = 7;
            this.lbFilename.Text = "NewQuery.sql";
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(961, 664);
            this.shapeContainer1.TabIndex = 16;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape1
            // 
            this.lineShape1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 11;
            this.lineShape1.X2 = 952;
            this.lineShape1.Y1 = 42;
            this.lineShape1.Y2 = 42;
            // 
            // FreehandQueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(961, 664);
            this.Controls.Add(this.btnCloseScript);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.queryMessage);
            this.Controls.Add(this.btNew);
            this.Controls.Add(this.lbFilename);
            this.Controls.Add(this.btExport);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.btLoad);
            this.Controls.Add(this.btRun);
            this.Controls.Add(this.shapeContainer1);
            this.KeyPreview = true;
            this.Name = "FreehandQueryForm";
            this.Text = "Freehand Query";
            this.Load += new System.EventHandler(this.FreehandQueryForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FreehandQueryForm_KeyUp);
            this.pnlBottom.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.sqlCommandMenu.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tcSQLDocuments.ResumeLayout(false);
            this.newQuery.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.QueryEditor)).EndInit();
            this.tcQueryResults.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.QueryResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView QueryResult;
        //        private System.Windows.Forms.RichTextBox NewQueryEditor;
        private System.Windows.Forms.Button btRun;
        private System.Windows.Forms.Button btLoad;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btExport;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lbFilename;
        private System.Windows.Forms.Button btNew;
        private System.Windows.Forms.TextBox queryMessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private System.Windows.Forms.CheckBox addTableName;
        private System.Windows.Forms.Button btRefreshTableList;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabControl tcQueryResults;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TreeView tvTables;
        private System.Windows.Forms.ContextMenuStrip sqlCommandMenu;
        private System.Windows.Forms.ToolStripMenuItem selectTop1000ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scriptTableAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dROPToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cREATEToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sELECTToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iNSERTToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uPDATEToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dELETEToToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TabControl tcSQLDocuments;
        private System.Windows.Forms.TabPage newQuery;
        private System.Windows.Forms.Button btnCloseScript;
        private FastColoredTextBoxNS.FastColoredTextBox QueryEditor;
    }
}