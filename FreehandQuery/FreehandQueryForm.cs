using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FastColoredTextBoxNS;

//Vazric 09/16/2014
namespace FreehandQuery
{
    public partial class FreehandQueryForm : Form
    {
        /// <summary>
        /// Maximum length of binary data to display (display is truncated after this length)
        /// </summary>
        private const int maxBinaryDisplayString = 8000;
        private FastColoredTextBox QueryEditor2 = new FastColoredTextBox();
        private bool loading = false;
        private bool textChanged = false;

        string serverName, databaseName, userName, password;

        public FreehandQueryForm()
        {
            InitializeComponent();
        }

        public bool ExecuteQuery(string queryString)
        {
            bool successQuery = true;
            bool nonQuery = true;
            bool waitForCommentEnd = false;
            //IDataReader rdrData;
            DataSet ds = new DataSet();
            try
            {
                using (var uow = new DBContext())
                {
                    using (IDbCommand command = new IDbCommand())
                    {
                        command.CommandText = queryString;
                        //command.CommandTimeout = 120;
                        command.Connection = new SqlConnection($"Data Source={serverName};Initial Catalog={databaseName};Persist Security Info=True;User ID={userName};Password={password};Pooling=False; Connection Timeout = 120;packet size= 32767");
                        // command.Connection.Open();
                        uow.PrepareCommand(command);
                        List<string> sqllines = queryString.Split(Environment.NewLine.ToCharArray()).ToList();
                        foreach (string line in sqllines)
                        {
                            if (string.IsNullOrWhiteSpace(line))
                                continue;
                            if (waitForCommentEnd)
                            {
                                if (line.Trim().Substring(0, 2).ToLower().Equals("*/") || line.Trim().Substring(line.Trim().Length - 2, 2).ToLower().Equals("*/"))
                                    waitForCommentEnd = false;
                                continue;
                            }
                            if (line.Trim().Substring(0, 6).ToLower().Equals("select") || line.Trim().Substring(0, 4).ToLower().Equals("exec"))
                            {
                                if (line.Trim().ToLower().IndexOf("@") > 0)
                                {
                                    if (line.Trim().ToLower().IndexOf(" ", line.Trim().ToLower().IndexOf("@")) > 0)
                                        if (line.Trim().ToLower().Substring(line.Trim().ToLower().IndexOf(" ", line.Trim().ToLower().IndexOf("@"))).Trim().StartsWith("="))
                                            continue;
                                    //break;
                                }
                                nonQuery = false;
                                break;
                            }
                            if (line.Trim().Substring(0, 2).ToLower().Equals("/*") && line.Trim().Substring(line.Trim().Length - 2, 2).ToLower().Equals("*/"))
                                continue;
                            if (line.Trim().Substring(0, 2).ToLower().Equals("/*") && !line.Trim().Substring(line.Trim().Length - 2, 2).ToLower().Equals("*/"))
                            {
                                waitForCommentEnd = true;
                                continue;
                            }
                            if (line.Trim().Substring(0, 7).ToLower().Equals("declare") || line.Trim().Substring(0, 3).ToLower().Equals("set"))
                            {
                                continue;
                            }
                            if (!line.Trim().Substring(0, 2).ToLower().Equals("--"))
                            {
                                nonQuery = true;
                                break;
                            }
                        }
                        queryMessage.Text = "";
                        QueryResult.Columns.Clear();
                        if (nonQuery)
                        {
                            int nra = command.ExecuteNonQuery();
                            queryMessage.ForeColor = Color.Green;
                            queryMessage.Text = "Number of rows affected. (" + nra + ")" + Environment.NewLine;
                        }
                        else
                        {
                            //remove all the tabresults but the first
                            int _tabCount = tcQueryResults.TabPages.Count;
                            for (int i = 1; i < _tabCount; i++)
                                tcQueryResults.TabPages.RemoveAt(tcQueryResults.TabPages.Count - 1);
                            //rdrData = command.ExecuteReader();
                            SqlDataAdapter da = new SqlDataAdapter((SqlCommand)command);
                            //da.RowUpdated += new SqlRowUpdatedEventHandler((object sender, SqlRowUpdatedEventArgs e) => queryMessage.Text += "rows affected "+e.RecordsAffected);
                            IDataAdapter ida = da;
                            queryMessage.Text = "";

                            ida.Fill(ds);

                            //ds.Load(rdrData, LoadOption.PreserveChanges, "data");
                            QueryResult.Columns.Clear();
                            QueryResult.AutoGenerateColumns = true;
                            //Vazric (11-11-14) fixing the issue with binery datatype like timestamp and hash
                            QueryResult.DataSource = FixBinaryColumnsForDisplay(ds.Tables[0]);
                            //QueryResult.DataSource = ds.Tables[0];
                            //setting size of columns
                            List<int> Width = new List<int>();
                            QueryResult.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                            foreach (DataGridViewColumn column in QueryResult.Columns)
                            {
                                Width.Add(column.Width > 200 ? 200 : column.Width);
                            }
                            QueryResult.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                            for (int index = 0; index < QueryResult.Columns.Count; index++)
                                QueryResult.Columns[index].Width = Width[index];

                            tcQueryResults.TabPages[0].Text = "Result1";
                            queryMessage.ForeColor = Color.Green;
                            queryMessage.Text = "Total records for Result1 = (" + QueryResult.RowCount + ")" + Environment.NewLine;

                            //if there is other results
                            for (int index = 1; index < ds.Tables.Count; index++)
                            {
                                DataGridView newQuery = new DataGridView();
                                tcQueryResults.TabPages.Add("Result" + (index + 1));
                                tcQueryResults.TabPages[index].Controls.Add(newQuery);

                                newQuery.RowHeadersVisible = false;
                                newQuery.ReadOnly = true;
                                newQuery.AllowUserToAddRows = false;
                                newQuery.AllowUserToDeleteRows = false;
                                newQuery.Dock = DockStyle.Fill;
                                newQuery.Columns.Clear();
                                newQuery.AutoGenerateColumns = true;
                                newQuery.DataSource = ds.Tables[index];
                                newQuery.SelectionMode = DataGridViewSelectionMode.CellSelect;
                                newQuery.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
                                //setting size of columns
                                Width.Clear();
                                newQuery.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                                foreach (DataGridViewColumn column in newQuery.Columns)
                                {
                                    Width.Add(column.Width > 200 ? 200 : column.Width);
                                }
                                newQuery.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                                for (int Cindex = 0; Cindex < newQuery.Columns.Count; Cindex++)
                                    newQuery.Columns[Cindex].Width = Width[Cindex];

                                queryMessage.ForeColor = Color.Green;
                                queryMessage.Text += "Total records for Result" + (index + 1) + " = (" + newQuery.RowCount + ")" + Environment.NewLine;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                queryMessage.Text = ex.Message;
                queryMessage.ForeColor = Color.Red;
                successQuery = false;
            }

            return successQuery;
        }

        /// <summary>
        /// Converts binary data column to a string equivalent, including handling of null columns
        /// </summary>
        /// <param name="hexBuilder">String builder pre-allocated for maximum space needed</param>
        /// <param name="columnValue">Column value, expected to be of type byte []</param>
        /// <returns>String representation of column value</returns>
        private string BinaryDataColumnToString(StringBuilder hexBuilder, object columnValue)
        {
            const string hexChars = "0123456789ABCDEF";
            if (columnValue == DBNull.Value)
            {
                // Return special "(null)" value here for null column values
                return "(null)";
            }
            else
            {
                // Otherwise return hex representation
                byte[] byteArray = (byte[])columnValue;
                int displayLength = (byteArray.Length > maxBinaryDisplayString) ? maxBinaryDisplayString : byteArray.Length;
                hexBuilder.Length = 0;
                hexBuilder.Append("0x");
                for (int i = 0; i < displayLength; i++)
                {
                    hexBuilder.Append(hexChars[(int)byteArray[i] >> 4]);
                    hexBuilder.Append(hexChars[(int)byteArray[i] % 0x10]);
                }
                return hexBuilder.ToString();
            }
        }

        private void btExport_Click(object sender, EventArgs e)
        {

            if (QueryResult.RowCount == 0)
            {
                MessageBox.Show("There is no result to export." + Environment.NewLine + "Please run the query to get result and try again.");
                return;
            }
            DataGridView tempDGV = (DataGridView)tcQueryResults.TabPages[tcQueryResults.SelectedIndex].Controls[0];
            if (tempDGV.Columns[0].HeaderText.Equals("ID"))
                tempDGV.Columns[0].HeaderText = "  " + tempDGV.Columns[0].HeaderText;
            tempDGV.SelectAll();
            DataObject dataObj = tempDGV.GetClipboardContent();

            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);

            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Excel Workbook (*.xlsx)|*.xlsx|Excel 97-2003 (*.xls)|*.xls|Text (Tab delimited) (*.txt)|*.txt|CSV (Comma delimited) (*.csv)|*.csv|Web Page (*.html)|*.html|Single File Web Page (*.mht)|*.mht|XML Data(*.xml)|*.xml"; ;
            fileDialog.FileName ="Results "+DateTime.Now.ToString("yyyy-MM-dd")+".xlsx";
            if (!string.IsNullOrWhiteSpace(lbFilename.Text))
                fileDialog.FileName = lbFilename.Text.Substring(0, lbFilename.Text.IndexOf(".")) + ".xlsx";
            fileDialog.FileName = fileDialog.FileName.Replace("]", "");

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(fileDialog.FileName.Substring(fileDialog.FileName.LastIndexOf("\\") + 2, fileDialog.FileName.IndexOf(".") - 1 - fileDialog.FileName.LastIndexOf("\\")).Trim()))
                {
                    MessageBox.Show("Warrning: Can not save the file, please try again and select a valid filename.");
                    tempDGV.CurrentCell = null;
                    return;
                }
                string FileName = fileDialog.FileName.Trim();
                if (File.Exists(FileName))
                    File.Delete(FileName);
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();

                Microsoft.Office.Interop.Excel.Workbook wb = excelApp.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets.Item[1];
                try
                {
                    ws.Name = "Freehand";

                    Microsoft.Office.Interop.Excel.Range CR = (Microsoft.Office.Interop.Excel.Range)ws.Cells[1, 1];

                    CR.Select();
                    ws.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
                    ws.get_Range(ws.Cells[1, 1], ws.Cells[1, QueryResult.Columns.Count + 1]).Interior.Color = Color.LightGray;
                    ws.get_Range(ws.Cells[1, 1], ws.Cells[1, QueryResult.Columns.Count + 1]).Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].Weight = 2;
                    ws.get_Range(ws.Cells[1, 1], ws.Cells[1, QueryResult.Columns.Count + 1]).Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].Color = Color.Black;
                    ws.Columns.AutoFit();
                    //vv 20161229 de-6375
                    //                    object misValue = System.Reflection.Missing.Value;
                    if (FileName.RightTrim(3).Equals("xls"))
                        wb.SaveAs(FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel8);
                    else if (FileName.RightTrim(3).Equals("txt"))
                        wb.SaveAs(FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlTextWindows);
                    else if (FileName.RightTrim(4).Equals("html"))
                        wb.SaveAs(FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlHtml);
                    else if (FileName.RightTrim(3).Equals("mht"))
                        wb.SaveAs(FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWebArchive);
                    else if (FileName.RightTrim(3).Equals("xml"))
                        wb.SaveAs(FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlXMLSpreadsheet);
                    else if (FileName.RightTrim(3).Equals("csv"))
                        wb.SaveAs(FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlCSV);
                    else
                        wb.SaveAs(FileName);
                    wb.Close(false);
                    excelApp.Workbooks.Close();
                    excelApp.Quit();
                    Marshal.ReleaseComObject(wb);
                    Marshal.ReleaseComObject(ws);
                    Marshal.ReleaseComObject(excelApp);
                    ws = null;
                    wb = null;
                    excelApp = null;
                    GC.Collect();
                    MessageBox.Show("Queried data exported successfully. " + Environment.NewLine + FileName);
                    queryMessage.ForeColor = Color.Green;
                    queryMessage.Text = "Queried data exported successfully. " + Environment.NewLine + FileName;
                }
                catch (Exception e1)
                {
                    wb.Close();
                    excelApp.Workbooks.Close();
                    excelApp.Quit();
                    Marshal.ReleaseComObject(wb);
                    Marshal.ReleaseComObject(ws);
                    Marshal.ReleaseComObject(excelApp);
                    ws = null;
                    wb = null;
                    excelApp = null;
                    GC.Collect();
                    MessageBox.Show("There was an error in exporting the data." + Environment.NewLine + e1.Message);
                }
            }

            tempDGV.CurrentCell = null;
        }

        private void btLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "SQL file (*.sql)|*.sql|txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                btNew_Click(sender, e);
                QueryEditor2 = (FastColoredTextBox)tcSQLDocuments.SelectedTab.Controls[0];
                QueryEditor2.Text = "";
                QueryEditor2.SelectionColor = Color.Black;
                string fileText = System.IO.File.ReadAllText(fileDialog.FileName);
                fileText = fileText.Replace("\t", "     ").Replace("\r\n", "\n").Replace("\r", "\n");
                fileText = fileText.Replace("(", " ( ").Replace(")", " ) ").Replace(",", " , ").Replace("=", " = ");
                QueryEditor2.Text = fileText;
                //vv 20161229 de-6375
                //                int st = 0;
                loading = true;
                //QueryEditorColorCheck();
                //if (fileText.Length > 0)
                //{
                //    //do
                //    //{
                //    if (fileText.IndexOf(" ", st + 1) > fileText.IndexOf("\n", st + 1) && fileText.IndexOf("\n", st + 1) > -1)
                //        st = fileText.IndexOf("\n", st + 1);
                //    else
                //        st = fileText.IndexOf(" ", st + 1);
                //    if (st == -1)
                //        st = fileText.Length;
                //    QueryEditor.SelectionStart = st;

                //    Regex regExp = new Regex("\\bbelse\\b|\\bfor\\b|\\bavg\\b|\\bcount\\b|\\bfirst\\b|\\blast\\b|\\bmax\\b|\\bmin\\b|\\bsum\\b|\\bucase\\b|\\blcasse\\b|\\bmid\\b|\\blen\\b|\\bround\\b|\\bnow\\b|\\bformat\\b|\\binteger\\b|\\breal\\b|\\bsmallint\\b|\\bfloat\\b|\\btype\\b|\\bfunction\\b|\\bvalue\\b|\\bdates\\b|\\bviews\\b|\\bauto\\b|\\bincrement\\b|\\bindex\\b|\\balter\\b|\\bdefault\\b|\\bcheck\\b|\\bcreate\\b|\\bforeign\\b|\\btable\\b|\\bprimary\\b|\\bkey\\b|\\bunique\\b|\\bleft\\b|\\bright\\b|\\bfull\\b|\\bunion\\b|\\binner\\b|\\binjection\\b|\\border\\b|\\bby\\b|\\binto\\b|\\bdistinct\\b|\\bupdate\\b|\\bdelete\\b|\\bdrop\\b|\\bset\\b|\\bgo\\b|\\bdeclare\\b|\\balter\\b|\\btable\\b|\\bif\\b|\\bbegin\\b|\\bend\\b|\\badd\\b|\\bvarchar\\b|\\bint\\b|\\bstring\\b|\\bnvarchar\\b|\\bchar\\b|\\bcreate\\b|\\bindex\\b|\\bvalues\\b|\\binsert\\b|\\binto\\b|\\bfrom\\b|\\bselect\\b|\\bjoin\\b|\\bwhere\\b|\\bhaving\\b|\\bon\\b|\\btop\\b|\\bfirst\\b");
                //    var rmatches = regExp.Matches(QueryEditor.Text.ToLower());
                //    foreach (Match match in rmatches)
                //    {
                //        QueryEditor.Select(match.Index, match.Length);
                //        QueryEditor.SelectionColor = Color.Blue;
                //        QueryEditor.SelectedText = QueryEditor.SelectedText.ToUpper();
                //    }
                //    regExp = new Regex("(['])(?:(?=(\\\\?))\\2.)*?\\1");
                //    rmatches = regExp.Matches(QueryEditor.Text);
                //    foreach (Match match in rmatches)
                //    {
                //        QueryEditor.Select(match.Index, match.Length);
                //        QueryEditor.SelectionColor = Color.Red;
                //    }
                //    regExp = new Regex("\\b-\\b|\\b+\\b|\\b*\\b|\\b(\\b|\\b)\\b|\\b,\\b|\\b=\\b|\\bis\\b|\\bor\\b|\\band\\b|\\bnot\\b|\\bin\\b|\\bexist\\b|\\bnull\\b|\\blike\\b|\\bexists\\b");
                //    rmatches = regExp.Matches(QueryEditor.Text);
                //    foreach (Match match in rmatches)
                //    {
                //        QueryEditor.Select(match.Index, match.Length);
                //        QueryEditor.SelectionColor = Color.Gray;
                //    }
                //    regExp = new Regex("--[^\\n\\r]+");
                //    rmatches = regExp.Matches(QueryEditor.Text);
                //    foreach (Match match in rmatches)
                //    {
                //        QueryEditor.Select(match.Index, match.Length);
                //        QueryEditor.SelectionColor = Color.Green;
                //    }


                //    //    QueryEditorColorCheck();
                //    //} while (fileText.Length > st);
                //}

                //LB 11/21/2017 DE-8898
                fileDialog.FileName = Path.GetFileNameWithoutExtension(fileDialog.FileName) + ".sql";

                lbFilename.Text = fileDialog.FileName;
                tcSQLDocuments.SelectedTab.Text = lbFilename.Text;

                // 20170123 JH DE-7146 If Associate clicks on Cancel, do not clear the previous results.
                loading = false;
                queryMessage.Text = "";
                QueryResult.Columns.Clear();

                //vv 20170112 de-7057
                if (tcSQLDocuments.TabCount > 0)
                {
                    btnCloseScript.Visible = true;
                    btSave.Visible = true;
                    btRun.Visible = true;
                }
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btNew_Click(object sender, EventArgs e)
        {
            AddNewQuery();
            //vv 20170112 de-7057
            if (tcSQLDocuments.TabCount > 0)
            {
                btnCloseScript.Visible = true;
                btSave.Visible = true;
                btRun.Visible = true;
            }
        }
        private void AddNewQuery(string queryName = "NewQuery")
        {
            tcSQLDocuments.TabPages.Add(queryName + ".sql");
            lbFilename.Text = queryName + ".sql";
            FastColoredTextBox newQueryEditor = new FastColoredTextBox();
            newQueryEditor.Dock = DockStyle.Fill;
            int index = tcSQLDocuments.TabPages.Count - 1;
            tcSQLDocuments.TabPages[index].Controls.Add(newQueryEditor);
            tcSQLDocuments.SelectedIndex = index;
            newQueryEditor.Language = Language.SQL;
            //newQueryEditor.TextChanged += QueryEditor_TextChanged;
            //newQueryEditor.SelectionChanged += QueryEditor_TextChanged;
            QueryEditor2 = newQueryEditor;
            return;

            //if (!string.IsNullOrWhiteSpace(QueryEditor.Text) && textChanged)
            //{
            //    var result = MessageBox.Show(this, "Would you like to save your query before creating new query?", "New Query", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
            //    if (result == System.Windows.Forms.DialogResult.Yes)
            //        btSave_Click(this, new EventArgs());
            //    else if (result == System.Windows.Forms.DialogResult.Cancel)
            //        return;
            //}
            ////remove all the tabresults but the first
            //int _tabCount = tcQueryResults.TabPages.Count;
            //for (int i = 1; i < _tabCount; i++)
            //    tcQueryResults.TabPages.RemoveAt(tcQueryResults.TabPages.Count - 1);

            //queryMessage.Text = "";
            //QueryResult.Columns.Clear();
            //lbFilename.Text = queryName + ".sql";
            //QueryEditor.Text = "";

        }

        private void btRefreshTableList_Click(object sender, EventArgs e)
        {
            tvTables.Nodes.Clear();
            FreehandQueryForm_Load(this, new EventArgs());
        }

        private void btRun_Click(object sender, EventArgs e)
        {
            if (tcSQLDocuments.SelectedTab == null)
                return;
            QueryEditor2 = (FastColoredTextBox)tcSQLDocuments.SelectedTab.Controls[0];
            try
            {
                string execText = QueryEditor2.Text;
                if (QueryEditor2.SelectedText.Length > 1)
                    execText = QueryEditor2.SelectedText;
                if (ExecuteQuery(execText))
                    MessageBox.Show("Query executed successfully.", "Freehand Query", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    //vv 20190814 de-13285 removing ! mark
                    MessageBox.Show(this, "An error occurred while executing the query." + Environment.NewLine + "Please check the detail message", "Freehand Query", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception err)
            {
        
                MessageBox.Show(err.Message);
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (tcSQLDocuments.SelectedTab == null)
                return;
            QueryEditor2 = (FastColoredTextBox)tcSQLDocuments.SelectedTab.Controls[0];
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.FileName = lbFilename.Text;
            fileDialog.Filter = "SQL file (*.sql)|*.sql|txt files (*.txt)|*.txt";
            if (fileDialog.FileName.IndexOf(".sql") < 0 && fileDialog.FileName.IndexOf(".txt") < 0)
                fileDialog.FileName += ".sql";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string FileName = fileDialog.FileName.Trim();
                System.IO.File.WriteAllText(FileName, QueryEditor2.Text);
                MessageBox.Show("File successfully saved.", "Freehand Query", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //LB 11/21/2017 DE-8898
                lbFilename.Text = Path.GetFileNameWithoutExtension(fileDialog.FileName) + ".sql";
            }
            textChanged = false;

            //LB 11/21/2017 DE-8898
            tcSQLDocuments.SelectedTab.Text = Path.GetFileNameWithoutExtension(lbFilename.Text) + ".sql";
        }

        /// <summary>
        /// Accepts datatable and converts all binary columns into textual representation of a binary column
        /// For use when display binary columns in a DataGridView
        /// </summary>
        /// <param name="t">Input data table</param>
        /// <returns>Updated data table, with binary columns replaced</returns>
        private DataTable FixBinaryColumnsForDisplay(DataTable t)
        {
            List<string> binaryColumnNames = t.Columns.Cast<DataColumn>().Where(col => col.DataType.Equals(typeof(byte[]))).Select(col => col.ColumnName).ToList();
            foreach (string binaryColumnName in binaryColumnNames)
            {
                // Create temporary column to copy over data
                string tempColumnName = "C" + Guid.NewGuid().ToString();
                t.Columns.Add(new DataColumn(tempColumnName, typeof(string)));
                t.Columns[tempColumnName].SetOrdinal(t.Columns[binaryColumnName].Ordinal);

                // Replace values in every row
                StringBuilder hexBuilder = new StringBuilder(maxBinaryDisplayString * 2 + 2);
                foreach (DataRow r in t.Rows)
                {
                    r[tempColumnName] = BinaryDataColumnToString(hexBuilder, r[binaryColumnName]);
                }

                t.Columns.Remove(binaryColumnName);
                t.Columns[tempColumnName].ColumnName = binaryColumnName;
            }
            return t;
        }

        private void FreehandQueryForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5 || (e.Control == true && e.KeyCode == Keys.R))
                btRun_Click(sender, new EventArgs());
            else if (e.KeyCode == Keys.F2 || (e.Control == true && e.KeyCode == Keys.S))
                btSave_Click(sender, new EventArgs());
            else if (e.KeyCode == Keys.F6 || (e.Control == true && e.KeyCode == Keys.E))
                btExport_Click(sender, new EventArgs());
            else if (e.Control == true && (e.KeyCode == Keys.L || e.KeyCode == Keys.O))
                btLoad_Click(sender, new EventArgs());
            else if (e.Control == true && e.KeyCode == Keys.N)
                btNew_Click(sender, new EventArgs());
        }

        private void FreehandQueryForm_Load(object sender, EventArgs e)
        {
            string ConnectionString = "";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    DataTable schema = connection.GetSchema("Tables");

                    //databaseName = schema.Rows[0].ToString();
                    foreach (DataRow row in schema.Rows)
                    {
                        //TableList.Items.Add(row[2].ToString());
                        DataTable ColumnsSchema = connection.GetSchema("Columns", new[] { null, null, row[2].ToString(), null });
                        TreeNode[] nodeArray = new TreeNode[ColumnsSchema.Rows.Count];
                        for (int index = 0; index < ColumnsSchema.Rows.Count; index++)
                        {
                            // ColumnList.Items.Add(row2[3].ToString());
                            nodeArray[index] = new TreeNode(ColumnsSchema.Rows[index][3].ToString());
                            //string test = "";
                            //for(int i =0;i<21;i++)
                            //test += ColumnsSchema.Columns[i].ColumnName +" = "+ColumnsSchema.Rows[index][i] + "\n";
                            nodeArray[index].ToolTipText = ColumnsSchema.Rows[index][2].ToString() + "." + ColumnsSchema.Rows[index][3].ToString() + " (" + ColumnsSchema.Rows[index][7].ToString() + " " + ColumnsSchema.Rows[index][8].ToString() + ") " + ColumnsSchema.Rows[index][6].ToString().Replace("NO", "NOT").Replace("YES", "") + " NULL";
                        }

                        TreeNode mainNode = new TreeNode(row[2].ToString(), nodeArray);
                        mainNode.ToolTipText = row[0].ToString() + "." + row[1].ToString() + "." + row[2].ToString();
                        tvTables.Nodes.Add(mainNode);
                    }
                    tvTables.Sorted = true;
                }
            
        }
        private int preLine = -1;
        //private void QueryEditor_TextChanged(object sender, EventArgs e)
        //{
        //    textChanged = true;
        //    if (QueryEditor2.GetLineFromCharIndex(QueryEditor2.SelectionStart) != preLine && !loading)
        //    {
        //        preLine = QueryEditor2.GetLineFromCharIndex(QueryEditor2.SelectionStart);
        //        QueryEditorColorCheck(true);
        //    }
        //}

        //private void QueryEditorColorCheck(bool lineOnly = false)
        //{
        //    int curentStart = QueryEditor2.SelectionStart;
        //    loading = true;
        //    if (QueryEditor2.Text.Length > 0)
        //    {
        //        int firstVisibleChar = QueryEditor2.GetCharIndexFromPosition(new Point(0, 0));
        //        int lineIndex = QueryEditor2.GetLineFromCharIndex(firstVisibleChar);
        //        int startLine = lineIndex + 1;
        //        int endLine = lineIndex + 17;

        //        if (lineOnly)
        //        {
        //            startLine = QueryEditor2.GetLineFromCharIndex(QueryEditor2.SelectionStart);
        //            endLine = startLine + 1;
        //        }
        //        if (startLine < 0)
        //            startLine = 0;
        //        if (endLine > QueryEditor2.Lines.Count())
        //            endLine = QueryEditor2.Lines.Count();
        //        for (int l = startLine; l < endLine; l++)
        //        {

        //            int startindex = QueryEditor2.GetFirstCharIndexFromLine(l);
        //            QueryEditor2.Select(startindex, QueryEditor2.Lines[l].Length);
        //            QueryEditor2.SelectionColor = Color.Black;
        //            Regex regExp = new Regex("\\buse\\b|\\bmoney\\b|\\bbit\\b|\\bdatetime\\b|\\bxml\\b|\\bbelse\\b|\\bfor\\b|\\bavg\\b|\\bcount\\b|\\bfirst\\b|\\blast\\b|\\bmax\\b|\\bmin\\b|\\bsum\\b|\\bucase\\b|\\blcasse\\b|\\bmid\\b|\\blen\\b|\\bround\\b|\\bnow\\b|\\bformat\\b|\\binteger\\b|\\breal\\b|\\bsmallint\\b|\\bfloat\\b|\\btype\\b|\\bfunction\\b|\\bvalue\\b|\\bdates\\b|\\bviews\\b|\\bauto\\b|\\bincrement\\b|\\bindex\\b|\\balter\\b|\\bdefault\\b|\\bcheck\\b|\\bcreate\\b|\\bforeign\\b|\\btable\\b|\\bprimary\\b|\\bkey\\b|\\bunique\\b|\\bleft\\b|\\bright\\b|\\bfull\\b|\\bunion\\b|\\binner\\b|\\binjection\\b|\\border\\b|\\bby\\b|\\binto\\b|\\bdistinct\\b|\\bupdate\\b|\\bdelete\\b|\\bdrop\\b|\\bset\\b|\\bgo\\b|\\bdeclare\\b|\\balter\\b|\\btable\\b|\\bif\\b|\\bbegin\\b|\\bend\\b|\\badd\\b|\\bvarchar\\b|\\bint\\b|\\bstring\\b|\\bnvarchar\\b|\\bchar\\b|\\bcreate\\b|\\bindex\\b|\\bvalues\\b|\\binsert\\b|\\binto\\b|\\bfrom\\b|\\bselect\\b|\\bjoin\\b|\\bwhere\\b|\\bhaving\\b|\\bon\\b|\\btop\\b|\\bfirst\\b");
        //            var rmatches = regExp.Matches(QueryEditor2.Lines[l].ToLower());
        //            foreach (Match match in rmatches)
        //            {
        //                QueryEditor2.Select(match.Index + startindex, match.Length);
        //                QueryEditor2.SelectionColor = Color.Blue;
        //                QueryEditor2.SelectedText = QueryEditor2.SelectedText.ToUpper();
        //            }
        //            regExp = new Regex("(['])(?:(?=(\\\\?))\\2.)*?\\1");
        //            rmatches = regExp.Matches(QueryEditor2.Lines[l]);
        //            foreach (Match match in rmatches)
        //            {

        //                QueryEditor2.Select(match.Index + startindex, match.Length);
        //                QueryEditor2.SelectionColor = Color.Red;
        //            }
        //            regExp = new Regex("\\b-\\b|\\b+\\b|\\b*\\b|\\b(\\b|\\b)\\b|\\b,\\b|\\b=\\b|\\bis\\b|\\bor\\b|\\band\\b|\\bnot\\b|\\bin\\b|\\bexist\\b|\\bnull\\b|\\blike\\b|\\bexists\\b");
        //            rmatches = regExp.Matches(QueryEditor2.Lines[l]);
        //            foreach (Match match in rmatches)
        //            {
        //                QueryEditor2.Select(match.Index + startindex, match.Length);
        //                QueryEditor2.SelectionColor = Color.Gray;
        //            }
        //            regExp = new Regex("--[^\\n\\r]+");
        //            rmatches = regExp.Matches(QueryEditor2.Lines[l]);
        //            foreach (Match match in rmatches)
        //            {
        //                QueryEditor2.Select(match.Index + startindex, match.Length);
        //                QueryEditor2.SelectionColor = Color.Green;
        //            }
        //            //regExp = new Regex("\\*^*\\+");
        //            //rmatches = regExp.Matches(QueryEditor.Lines[l]);
        //            //foreach (Match match in rmatches)
        //            //{
        //            //    QueryEditor.Select(match.Index + startindex, match.Length);
        //            //    QueryEditor.SelectionColor = Color.Green;
        //            //}

        //        }
        //    }
        //    QueryEditor2.SelectionStart = curentStart;
        //    QueryEditor2.SelectionLength = 0;
        //    loading = false;
        //}
        //private void QueryEditorColorCheck_old()
        //{

        //    string keyword;
        //    int curentStart = QueryEditor2.SelectionStart;
        //    int st = 0;
        //    int rempoint = 0;
        //    bool comment = false;

        //    if (QueryEditor2.SelectionStart > 0)
        //    {
        //        rempoint = QueryEditor2.Text.LastIndexOf("--", QueryEditor2.SelectionStart - 1);
        //        if (rempoint > 0 && QueryEditor2.Text.LastIndexOf("\n", QueryEditor2.SelectionStart - 1) < rempoint)
        //            comment = true;
        //        if (!comment)
        //        {
        //            rempoint = QueryEditor2.Text.LastIndexOf("/*", QueryEditor2.SelectionStart - 1);
        //            if (rempoint > 0 && QueryEditor2.Text.LastIndexOf("*/", QueryEditor2.SelectionStart - 1) < rempoint)
        //                comment = true;
        //        }
        //        st = QueryEditor2.Text.LastIndexOf(" ", QueryEditor2.SelectionStart - 1) + 1;
        //        if (st < QueryEditor2.Text.LastIndexOf("\n", QueryEditor2.SelectionStart - 1) + 1)
        //            st = QueryEditor2.Text.LastIndexOf("\n", QueryEditor2.SelectionStart - 1) + 1;
        //    }
        //    int ed = QueryEditor2.Text.IndexOf(" ", QueryEditor2.SelectionStart);
        //    if (QueryEditor2.Text.IndexOf("\n", QueryEditor2.SelectionStart) > -1 && QueryEditor2.Text.IndexOf("\n", QueryEditor2.SelectionStart) < ed || ed == -1)
        //        ed = QueryEditor2.Text.IndexOf("\n", QueryEditor2.SelectionStart);
        //    if (st == -1)
        //        st = 0;
        //    if (ed == -1)
        //        ed = QueryEditor2.Text.Length;
        //    if (ed < st)
        //        ed = st;
        //    keyword = QueryEditor2.Text.Substring(st, ed - st);

        //    QueryEditor2.SelectionStart = st;
        //    QueryEditor2.SelectionLength = ed - st;
        //    if (keyword.Length > 0)
        //        if (comment)
        //        {
        //            QueryEditor2.SelectionColor = Color.Green;
        //            if (keyword.Trim().Length > 1)
        //                if (keyword.Trim().Substring(0, 2) == "--" || keyword.Trim().Substring(0, 2) == "/*")
        //                {
        //                    ed = QueryEditor2.Text.IndexOf("\n", QueryEditor2.SelectionStart);
        //                    if (ed == -1)
        //                        ed = QueryEditor2.Text.Length;
        //                    QueryEditor2.SelectionLength = ed - st;
        //                    QueryEditor2.SelectionColor = Color.Green;
        //                }
        //                else if (keyword.Trim().Substring(keyword.Length - 2) == "*/")
        //                {
        //                    ed = QueryEditor2.Text.IndexOf("/*", QueryEditor2.SelectionStart);
        //                    if (ed > -1)
        //                    {
        //                        QueryEditor2.SelectionStart = ed;
        //                        QueryEditor2.SelectionLength = st - ed;
        //                    }

        //                    QueryEditor2.SelectionColor = Color.Green;
        //                }
        //        }
        //        else if (keyword[0] == '\'' && keyword[keyword.Length - 1] == '\'')
        //        {
        //            QueryEditor2.SelectionColor = Color.Red;
        //        }
        //        else if (" avg count first last max min sum ucase lcasse mid len round now format now integer real smallint float type function value dates views auto increment index alter default check create foreign table primary key unique left right full union inner injection order by into distinct update delete drop set go declare alter table if begin end add varchar int string nvarchar char create index values insert into from select join where having on top first ".Contains(" " + keyword.Trim().ToLower() + " "))
        //        {
        //            QueryEditor2.SelectionColor = Color.Blue;
        //            QueryEditor2.SelectedText = keyword.ToUpper();
        //        }
        //        else if (" - + * ( ) , = is or and not in exist null like exists ".Contains(" " + keyword.Trim().ToLower() + " "))
        //        {
        //            QueryEditor2.SelectionColor = Color.Gray;
        //            QueryEditor2.SelectedText = keyword.ToUpper();
        //        }
        //        else
        //        {
        //            QueryEditor2.SelectionColor = Color.Black;
        //            if (keyword.Trim().Length > 1)
        //            {
        //                if (keyword.Trim().Substring(0, 2) == "--" || keyword.Trim().Substring(0, 2) == "/*")
        //                {
        //                    ed = QueryEditor2.Text.IndexOf("\n", QueryEditor2.SelectionStart);
        //                    if (ed == -1)
        //                        ed = QueryEditor2.Text.Length;
        //                    QueryEditor2.SelectionLength = ed - st;
        //                    QueryEditor2.SelectionColor = Color.Green;
        //                }
        //                if (keyword.Trim().Substring(keyword.Length - 2) == "*/")
        //                {
        //                    st = QueryEditor2.Text.LastIndexOf("/*", QueryEditor2.SelectionStart);
        //                    if (st > -1)
        //                    {
        //                        QueryEditor2.SelectionStart = st;
        //                        QueryEditor2.SelectionLength = ed - st;
        //                    }

        //                    QueryEditor2.SelectionColor = Color.Green;
        //                }
        //            }
        //        }

        //    //reseting the selected area
        //    QueryEditor2.SelectionStart = curentStart;
        //    QueryEditor2.SelectionLength = 0;
        //}

        private void sqlCommandMenu_Opening(object sender, CancelEventArgs e)
        {
            if (tvTables.SelectedNode != null && tvTables.SelectedNode.Parent != null)
                e.Cancel = true;
        }

        private void TablesAndColumns_Paint(object sender, PaintEventArgs e)
        {
            SplitContainer s = sender as SplitContainer;
            if (s != null)
            {
                if (s.Orientation == Orientation.Vertical)
                {
                    int top = (s.Height / 2) - 30;
                    int bottom = (s.Height / 2) + 30;
                    int left = s.SplitterDistance;
                    int right = left + s.SplitterWidth - 1;
                    e.Graphics.DrawLine(Pens.White, left, top, left, bottom);
                    e.Graphics.DrawLine(Pens.Silver, left + (s.SplitterWidth / 2) - 1, top, left + (s.SplitterWidth / 2) - 1, bottom);
                    e.Graphics.DrawLine(Pens.DarkGray, left + (s.SplitterWidth / 2), top, left + (s.SplitterWidth / 2), bottom);
                    e.Graphics.DrawLine(Pens.White, right, top, right, bottom);
                }
                else
                {
                    int left = (s.Width / 2) - 30;
                    int right = left + 60;
                    int top = s.SplitterDistance;
                    int bottom = top + s.SplitterWidth - 1;
                    //e.Graphics.DrawLine(Pens.Gray, left, top, right, top);
                    e.Graphics.DrawLine(Pens.White, left, top, right, top);
                    e.Graphics.DrawLine(Pens.DarkGray, left, top + (s.SplitterWidth / 2) - 1, right, top + (s.SplitterWidth / 2) - 1);
                    e.Graphics.DrawLine(Pens.Silver, left, top + (s.SplitterWidth / 2), right, top + (s.SplitterWidth / 2));
                    e.Graphics.DrawLine(Pens.White, left, bottom, right, bottom);
                }
            }
        }

        private void tcSQLDocuments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcSQLDocuments.SelectedTab == null)
                return;
            lbFilename.Text = tcSQLDocuments.SelectedTab.Text;
            QueryEditor2 = tcSQLDocuments.SelectedTab.Controls[0] as FastColoredTextBox;
        }

        private void tvTables_ItemDrag(object sender, ItemDragEventArgs e)
        {
            string text = ((TreeNode)e.Item).Text;
            if (addTableName.Checked && ((TreeNode)e.Item).Parent != null)
                text = ((TreeNode)e.Item).Parent.Text + "." + text;
            tvTables.DoDragDrop(text, DragDropEffects.All);
        }

        private void tvTables_KeyDown(object sender, KeyEventArgs e)
        {
            //Vazric (01-05-15) adding column name with ',' at the end when Control + Enter is pressed
            if (e.KeyCode == Keys.Enter)
            {
                string text = tvTables.SelectedNode.Text;
                if ((addTableName.Checked || ModifierKeys.HasFlag(Keys.Alt)) && tvTables.SelectedNode.Parent.Parent != null)
                    text = tvTables.SelectedNode.Parent.Text + "." + text;
                if (ModifierKeys.HasFlag(Keys.Control))
                {
                    text += ",";
                }
                if (ModifierKeys.HasFlag(Keys.Shift))
                {
                    text = ", " + text;
                }
                QueryEditor2.SelectedText = " " + text;
            }
        }

        private void tvTables_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //vv 20180116 de-9550
            if (e.Button != MouseButtons.Right)
            {
                if (e.Node.Parent != null)
                    //toolTip1.Show("Double click or Drag and Drop to insert the Column name into the editor.Hold Alt to add Table name befroe. Hold Ctrl to add comma after and hold Shift to insert comma before." + Environment.NewLine + e.Node.Text.ToString(), this, e.Location.X + splitContainer2.Location.X + 10, e.Location.Y + splitContainer2.Location.Y + 54, 2000);
                    //LB DE-8898 solution #1 
                    toolTip1.Show("Double click or Drag & Drop to insert the column name into the editor. To add a table name, hold <Alt>. To add a comma after the column name, hold <Ctrl>. To add a comma before the column name, hold <Shift>." + Environment.NewLine + e.Node.Text.ToString(), this, e.Location.X + splitContainer2.Location.X + 10, e.Location.Y + splitContainer2.Location.Y + 54, 2000);

                else
                    toolTip1.Show("Double click or Drag and Drop to insert the Table name into the editor. Hold Ctrl to add 'FROM' before. Hold Alt to Insert Select All for Table" + Environment.NewLine + e.Node.Text.ToString(), this, e.Location.X + splitContainer2.Location.X + 10, e.Location.Y + splitContainer2.Location.Y + 54, 2000);
            }
            //vv 20180116 de-9550
            tvTables.SelectedNode = e.Node;
        }

        private void tvTables_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //vv 20180116 de-9550
            tvTables.SelectedNode = e.Node;

            if (e.Node.Parent != null)
            {
                string text = (e.Node).Text;
                if ((addTableName.Checked || ModifierKeys.HasFlag(Keys.Alt)) && (e.Node).Parent != null)
                    text = (e.Node).Parent.Text + "." + text;
                if (ModifierKeys.HasFlag(Keys.Control))
                {
                    text += ",";
                }
                if (ModifierKeys.HasFlag(Keys.Shift))
                {
                    text = ", " + text;
                }

                QueryEditor2.SelectedText = " " + text;
            }
            else if (e.Node != null)
            {
                string text = (e.Node).Text;
                if (ModifierKeys.HasFlag(Keys.Control))
                {
                    text = " FROM " + text;
                }
                else if (ModifierKeys.HasFlag(Keys.Alt))
                {
                    text = "\r\nSELECT * FROM " + text + "\r\n";
                }
                QueryEditor2.SelectedText = " " + text;
            }
        }

        private void selectTop1000ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string tableName = tvTables.SelectedNode.ToolTipText.Replace(".", "].[");
            //vv 20170123 de-7143
            string queryName = "Select "+tvTables.SelectedNode.ToolTipText.Substring(tvTables.SelectedNode.ToolTipText.LastIndexOf(".")+1);
            AddNewQuery(queryName);
            string queryStr = "SELECT TOP 1000\r\n";
            foreach (TreeNode node in tvTables.SelectedNode.Nodes)
            {
                queryStr += "\t";
                if (tvTables.SelectedNode.Nodes.IndexOf(node) > 0)
                    queryStr += ",";
                queryStr += "[" + node.Text + "]\r\n";

            }

            queryStr += "FROM [" + tableName + "]";
            QueryEditor2.Text = queryStr;
            btRun_Click(sender, e);
        }

        private void tcSQLDocuments_Click(object sender, EventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Control))
            {
                if (!string.IsNullOrWhiteSpace(QueryEditor2.Text) && textChanged)
                {
                    var result = MessageBox.Show(this, "Would you like to save your query before closing it?", "Close Query", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                        btSave_Click(this, new EventArgs());
                    else if (result == System.Windows.Forms.DialogResult.Cancel)
                        return;
                }
            }
        }

        private void cREATEToToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string tableName = tvTables.SelectedNode.ToolTipText.Replace(".", "].[");
            string queryName = "Create "+tvTables.SelectedNode.ToolTipText.Substring(tvTables.SelectedNode.ToolTipText.LastIndexOf(".") + 1);
            AddNewQuery(queryName);
            string queryStr = "SET ANSI_NULLS ON\r\n";
            queryStr += "GO\r\n";
            queryStr += "SET QUOTED_IDENTIFIER ON\r\n";
            queryStr += "GO\r\n";
            queryStr += "SET ANSI_PADDING ON\r\n";
            queryStr += "CREATE TABLE [" + tableName + "]\r\n";
            foreach (TreeNode node in tvTables.SelectedNode.Nodes)
            {
                queryStr += "\t";
                if (tvTables.SelectedNode.Nodes.IndexOf(node) > 0)
                    queryStr += ",";
                else
                    queryStr += "(";

                queryStr += "[" + node.Text + "] [" + node.ToolTipText.Substring(node.ToolTipText.IndexOf("("), node.ToolTipText.IndexOf(")") + 1 - node.ToolTipText.IndexOf("(")) + "] " + node.ToolTipText.Substring(node.ToolTipText.IndexOf(")") + 1) + "\r\n";

            }

            QueryEditor2.Text = queryStr;
            //QueryEditorColorCheck();
        }

        private void sELECTToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string tableName = tvTables.SelectedNode.ToolTipText.Replace(".", "].[");
            string queryName = "Select "+tvTables.SelectedNode.ToolTipText.Substring(tvTables.SelectedNode.ToolTipText.LastIndexOf(".") + 1);
            AddNewQuery(queryName);
            string queryStr = "SELECT \r\n";
            foreach (TreeNode node in tvTables.SelectedNode.Nodes)
            {
                queryStr += "\t";
                if (tvTables.SelectedNode.Nodes.IndexOf(node) > 0)
                    queryStr += ",";
                queryStr += "[" + node.Text + "]\r\n";

            }

            queryStr += "FROM [" + tableName + "]";
            QueryEditor2.Text = queryStr;
            //QueryEditorColorCheck();
        }

        private void dELETEToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string tableName = tvTables.SelectedNode.ToolTipText.Replace(".", "].[");
            string queryName = "Delete " +tvTables.SelectedNode.ToolTipText.Substring(tvTables.SelectedNode.ToolTipText.LastIndexOf(".") + 1);
            AddNewQuery(queryName);
            string queryStr = "DELETE FROM [" + tableName + "]\r\n";
            queryStr += "\tWHERE<Search Conditions,,>\r\n";
            QueryEditor2.Text = queryStr;
            //QueryEditorColorCheck();
        }

        private void dROPToToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string tableName = tvTables.SelectedNode.ToolTipText.Replace(".", "].[");
            string queryName = "Drop "+tvTables.SelectedNode.ToolTipText.Substring(tvTables.SelectedNode.ToolTipText.LastIndexOf(".") + 1);
            AddNewQuery(queryName);
            string queryStr = "DROP TABLE [" + tableName + "]\r\n";
            QueryEditor2.Text = queryStr;
            //QueryEditorColorCheck();
        }

        private void uPDATEToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string tableName = tvTables.SelectedNode.ToolTipText.Replace(".", "].[");
            string queryName = "Update "+tvTables.SelectedNode.ToolTipText.Substring(tvTables.SelectedNode.ToolTipText.LastIndexOf(".") + 1);
            AddNewQuery(queryName);
            string queryStr = "";
            queryStr = "";
            queryStr += "UPDATE [" + tableName + "]\r\n";
            foreach (TreeNode node in tvTables.SelectedNode.Nodes)
            {
                queryStr += "\t";
                if (tvTables.SelectedNode.Nodes.IndexOf(node) > 0)
                    queryStr += ",";
                else
                    queryStr += "SET ";
                queryStr += "[" + node.Text + "] = <" + node.Text + ", " + node.ToolTipText.Substring(node.ToolTipText.IndexOf("(")) + ",>\r\n";

            }

            queryStr += " WHERE <Search Conditions,,>";
            QueryEditor2.Text = queryStr;
            //QueryEditorColorCheck();
        }

        private void iNSERTToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string tableName = tvTables.SelectedNode.ToolTipText.Replace(".", "].[");
            string queryName = "Insert "+tvTables.SelectedNode.ToolTipText.Substring(tvTables.SelectedNode.ToolTipText.LastIndexOf(".") + 1);
            AddNewQuery(queryName);
            string queryStr = "";
            queryStr = "";
            queryStr += "INSERT INTO [" + tableName + "]\r\n";
            foreach (TreeNode node in tvTables.SelectedNode.Nodes)
            {
                queryStr += "\t";
                if (tvTables.SelectedNode.Nodes.IndexOf(node) > 0)
                    queryStr += ",";
                else
                    queryStr += "(";
                queryStr += "[" + node.Text + "]\r\n";

            }
            queryStr += ")\r\n";
            queryStr += "VALUES";
            foreach (TreeNode node in tvTables.SelectedNode.Nodes)
            {
                queryStr += "\t";
                if (tvTables.SelectedNode.Nodes.IndexOf(node) > 0)
                    queryStr += ",";
                else
                    queryStr += "(";

                queryStr += "<" + node.Text + ", " + node.ToolTipText.Substring(node.ToolTipText.IndexOf("(")) + ",>\r\n";

            }
            queryStr += ")\r\n";
            QueryEditor2.Text = queryStr;
            //QueryEditorColorCheck();
        }

        private void btnCloseScript_Click(object sender, EventArgs e)
        {
            //vv 20170112 de-7057
            if (tcSQLDocuments.SelectedTab != null)
            {
                var result = MessageBox.Show("Would you like to save and close this tab?", "Freehand Query", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                    btSave_Click(this, null);
                else if (result == DialogResult.Cancel)
                    return;
                tcSQLDocuments.TabPages.RemoveAt(tcSQLDocuments.SelectedIndex);

                //LB 11/21/2017 DE-8898
                if(tcSQLDocuments.TabPages.Count ==0)
                AddNewQuery();
            }
            //vv 20170112 de-7057
            if (tcSQLDocuments.TabCount == 0)
            {
                btnCloseScript.Visible = false;
                btSave.Visible = false;
                btRun.Visible = false;
                lbFilename.Text = "";
            }
        }

    }
}