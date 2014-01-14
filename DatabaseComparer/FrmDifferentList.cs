using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using DatabaseComparer.Common;
using System.Threading;

namespace DatabaseComparer
{
    public partial class FrmDifferentList : Form
    {
        public TreeViewEx TvSource;
        public TreeViewEx TvDest;

        public FrmDifferentList()
        {
            InitializeComponent();
        }

        #region FrmDifferentList_Load
        private void FrmDifferentList_Load(object sender, EventArgs e)
        {
            #region 取Source里的所有表
            string mysqlServer = "show tables";
            string sqlServer = "SELECT name FROM sysobjects WHERE xtype='U' order by name";
            DataTable sourceDT = null;
            if (Data.IsMySql(Data.SourceConnectionString))
            {
                this.gBoxSource.Text += " [" + Data.SourceDataBase + "]";
                sourceDT = new MySqlHelper().ExecuteDataTable(Data.SourceConnectionString, CommandType.Text, mysqlServer, null);
            }
            else
            {
                this.gBoxSource.Text += " [" + Data.SourceDataBase + "]";
                sourceDT = new SqlServerHelper().ExecuteDataTable(Data.SourceConnectionString, CommandType.Text, sqlServer, null);
            }

            foreach (var t in sourceDT.AsEnumerable().ToList())
            {
                Data.SourceTable.Add(t.ItemArray[0].ToString(), new List<DBTable>());
            }
            #endregion

            #region 取Destination里的所有表
            DataTable destDT = null;
            if (Data.IsMySql(Data.DestConnectionString))
            {
                this.gBoxDest.Text += " [" + Data.DestDataBase + "]";
                destDT = new MySqlHelper().ExecuteDataTable(Data.DestConnectionString, CommandType.Text, mysqlServer, null);
            }
            else
            {
                this.gBoxDest.Text += " [" + Data.DestDataBase + "]";
                destDT = new SqlServerHelper().ExecuteDataTable(Data.DestConnectionString, CommandType.Text, sqlServer, null);
            }
            foreach (var t in destDT.AsEnumerable().ToList())
            {
                Data.DestTable.Add(t.ItemArray[0].ToString(), new List<DBTable>());
            }
            #endregion

            LoadTableField(Data.SourceConnectionString, Data.SourceTable, pBarSource);
            LoadTableField(Data.DestConnectionString, Data.DestTable, pBarDest);

            #region 创建TreeView
            TvSource = new TreeViewEx();
            TvSource.Name = "TvSource";
            TvDest = new TreeViewEx();
            TvDest.Name = "TvDest";

            this.gBoxSource.Controls.Add(TvSource);
            this.gBoxDest.Controls.Add(TvDest);

            this.TvSource.Dock = DockStyle.Fill;
            this.TvDest.Dock = DockStyle.Fill;
            this.TvSource.AfterExpand += new TreeViewEventHandler(TreeView_AfterExpand);
            this.TvDest.AfterExpand += new TreeViewEventHandler(TreeView_AfterExpand);
            this.TvSource.AfterCollapse += new TreeViewEventHandler(TreeView_AfterCollapse);
            this.TvDest.AfterCollapse += new TreeViewEventHandler(TreeView_AfterCollapse);

            this.TvSource.BeforeSelect += new TreeViewCancelEventHandler(TreeView_BeforeSelect);
            this.TvDest.BeforeSelect += new TreeViewCancelEventHandler(TreeView_BeforeSelect);
            this.TvSource.AfterSelect += new TreeViewEventHandler(TreeView_AfterSelect);
            this.TvDest.AfterSelect += new TreeViewEventHandler(TreeView_AfterSelect);
            #endregion

            DataBaseCompare();
        }

        #endregion

        #region 载入每个表的字段
        private void LoadTableField(string conn, Dictionary<string, List<DBTable>> list, ProgressBar pBar)
        {
            try
            {
                pBar.Minimum = 0;
                pBar.Maximum = list.Count;

                Thread thread = new System.Threading.Thread(delegate()
                {
                    int current = 0;
                    foreach (var t in list)
                    {
                        this.BeginInvoke(new MethodInvoker(delegate
                        {
                            pBar.Value = current;
                        }));

                        string sql = string.Empty;
                        if (Data.IsMySql(conn))
                        {
                            #region MySql数据库读取每张表的字段
                            sql = "SHOW FULL COLUMNS FROM " + t.Key;
                            DataTable dt = new MySqlHelper().ExecuteDataTable(conn, CommandType.Text, sql, null);
                            foreach (var field in dt.AsEnumerable().ToList())
                            {
                                DBTable item = new DBTable();
                                item.Field = field[0].ToString();
                                int loc = field[1].ToString().IndexOf("(");
                                if (loc > 0)
                                {
                                    item.Type = field[1].ToString().Substring(0, loc);
                                    item.Length = field[1].ToString().Substring(loc + 1).TrimEnd(')').Uint();
                                }
                                else
                                {
                                    item.Type = field[1].ToString();
                                }
                                list[t.Key].Add(item);
                            }
                            #endregion
                        }
                        else
                        {
                            #region SqlServer数据库读取每张表的字段
                            sql = "SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('" + t.Key + "') ;";
                            DataTable dt = new SqlServerHelper().ExecuteDataTable(conn, CommandType.Text, sql, null);
                            foreach (var field in dt.AsEnumerable().ToList())
                            {
                                DBTable item = new DBTable();
                                item.Field = field["name"].ToString();
                                list[t.Key].Add(item);
                            }

                            foreach (var field in list[t.Key])
                            {
                                sql = "select data_type,character_maximum_length from INFORMATION_SCHEMA.COLUMNS IC where TABLE_NAME = '" + t.Key + "' and COLUMN_NAME = '" + field.Field + "'";
                                DataTable temp = new SqlServerHelper().ExecuteDataTable(conn, CommandType.Text, sql, null);
                                if (temp.Rows.Count > 0)
                                {
                                    field.Type = temp.Rows[0][0].ToString();
                                    field.Length = temp.Rows[0][1].ToString().Uint();
                                }
                            }
                            #endregion
                        }
                        current++;
                    }
                    this.BeginInvoke(new MethodInvoker(delegate
                       {
                           pBar.Visible = false;
                       }));
                });
                thread.IsBackground = true;
                thread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 这个线程用于比较两个库的不同
        private void DataBaseCompare()
        {
            try
            {
                Thread thread = new System.Threading.Thread(delegate()
                       {
                           #region 判断进度条是否完成
                           while (true)
                           {
                               if (this.pBarSource.Visible == true || this.pBarDest.Visible == true)
                               {
                                   Thread.Sleep(1000);
                                   continue;
                               }
                               else
                               {
                                   break;
                               }
                           }
                           #endregion

                           List<string> HandledTakble = new List<string>();

                           #region 先循环Source，后循环Dest
                           ShowCompareResult(Data.SourceTable, this.TvSource, Data.DestTable, this.TvDest, HandledTakble);
                           #endregion

                           #region 先循环Dest，后循环Source
                           ShowCompareResult(Data.DestTable, this.TvDest, Data.SourceTable, this.TvSource, HandledTakble);
                           #endregion
                       });
                thread.IsBackground = true;
                thread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 显示比较的树形结果
        private void ShowCompareResult(Dictionary<string, List<DBTable>> source, TreeView tSource, Dictionary<string, List<DBTable>> dest, TreeView tDest, List<string> HandledTable)
        {
            foreach (var stable in source)
            {

                var dtable = dest.Where(p => p.Key.ToLower() == stable.Key.ToLower()).Select(p => p.Value);
                if (dtable != null && dtable.Count() > 0)
                {
                    if (!HandledTable.Contains(stable.Key.ToLower()))
                    {
                        HandledTable.Add(stable.Key.ToLower());
                        HandleDifference(stable, tSource, dtable.FirstOrDefault().ToList(), tDest);
                    }
                }
                else
                {
                    List<TreeNode> array = new List<TreeNode>();
                    List<TreeNode> arrayEmpty = new List<TreeNode>(); //添加一个没有数据的TreeNode对应

                    for (int i = 0; i < stable.Value.Count; i++)
                    {
                        string text = stable.Value[i].Field + " " + stable.Value[i].Type;
                        array.Add(new TreeNode(text));
                        arrayEmpty.Add(new TreeNode(string.Empty.PadRight(text.Length, ' ')));
                    }
                    TreeNode treeNode = new TreeNode(stable.Key, array.ToArray());
                    TreeNode treeNodeEmpty = new TreeNode(string.Empty.PadLeft(stable.Key.Length, ' '), arrayEmpty.ToArray());
                    this.BeginInvoke(new MethodInvoker(delegate
                      {
                          tSource.Nodes.Add(treeNode);
                          tDest.Nodes.Add(treeNodeEmpty);
                      }));
                }
            }
        }
        #endregion

        #region  处理两个库中每一张表不同的结构
        private void HandleDifference(KeyValuePair<string, List<DBTable>> list, TreeView tSource, List<DBTable> anotherList, TreeView tDest)
        {
            List<TreeNode> array = new List<TreeNode>();
            List<TreeNode> arrayAnother = new List<TreeNode>(); //添加一个没有数据的TreeNode对应

            List<string> HandledField = new List<string>();

            CompareTableField(list.Value, anotherList, array, arrayAnother, HandledField);
            CompareTableField(anotherList, list.Value, arrayAnother, array, HandledField);

            TreeNode treeNode = new TreeNode(list.Key, array.ToArray());
            TreeNode treeNodeEmpty = new TreeNode(list.Key, arrayAnother.ToArray());
            if (array.Count > 0 && arrayAnother.Count > 0)
            {
                this.BeginInvoke(new MethodInvoker(delegate
                {
                    tSource.Nodes.Add(treeNode);
                    tDest.Nodes.Add(treeNodeEmpty);
                }));
            }
        }
        #endregion

        #region  遍历表字段
        private void CompareTableField(List<DBTable> list, List<DBTable> anotherList, List<TreeNode> array, List<TreeNode> arrayAnother, List<string> HandledField)
        {
            foreach (var one in list)
            {
                bool flag = false;

                if (HandledField.Contains(one.Field.ToLower()))
                {
                    continue;
                }
                HandledField.Add(one.Field.ToLower());

                #region 比较字段
                foreach (var another in anotherList)
                {
                    if (one.Field.ToLower() == another.Field.ToLower())//字段名相同
                    {
                        if (one.ToString() == another.ToString()) //字段类型和长度完全相同
                        {
                            flag = true;
                            break;
                        }

                        if (Data.IsIgnoreType && Data.IsIgnoreLength) //忽略类型和长度
                        {
                            flag = true;
                            break;
                        }

                        if (Data.IsIgnoreType) //忽略类型
                        {
                            if ((one.Field + one.Length).ToLower() == (another.Field + another.Length).ToLower())
                            {
                                flag = true;
                                break;
                            }
                        }

                        if (Data.IsIgnoreLength) //忽略长度
                        {
                            if ((one.Field + one.Type).ToLower() == (another.Field + another.Type).ToLower())
                            {
                                flag = true;
                                break;
                            }
                        }

                        //字段类型或是长度不相同
                        flag = true;
                        array.Add(new TreeNode(one.ToString()));
                        arrayAnother.Add(new TreeNode(another.ToString()));
                    }
                }
                #endregion
                if (!flag)
                {
                    string text = one.ToString();
                    array.Add(new TreeNode(text));
                    arrayAnother.Add(new TreeNode(string.Empty.PadRight(text.Length, ' ')));
                }
            }
        }
        #endregion

        #region TreeView同步折叠和展开处理、节点选中前后处理
        void TreeView_AfterExpand(object sender, TreeViewEventArgs e)
        {
            TreeView_ExpandCollapse(sender, e, 0);
        }

        void TreeView_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            TreeView_ExpandCollapse(sender, e, 1);
        }

        void TreeView_ExpandCollapse(object sender, TreeViewEventArgs e, int type)
        {
            TreeView tv = sender as TreeView;
            TreeView tvAnother = null;
            int cIndex = e.Node.Index;

            //tv.Nodes[cIndex].EnsureVisible();
            if (tv.Name == "TvSource")
            {
                tvAnother = this.gBoxDest.Controls.Find("TvDest", true).FirstOrDefault() as TreeView;
            }
            else
            {
                tvAnother = this.gBoxSource.Controls.Find("TvSource", true).FirstOrDefault() as TreeView;
            }
            switch (type)
            {
                case 0: tvAnother.Nodes[cIndex].Expand(); break;
                case 1: tvAnother.Nodes[cIndex].Collapse(); break;
            }
            //tvAnother.Nodes[cIndex].EnsureVisible();
        }

        void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeView tv = sender as TreeView;
            TreeView tvAnother = null;
            int parent = -1;
            if (e.Node.Parent != null)
            {
                parent = e.Node.Parent.Index;
            }
            int cIndex = e.Node.Index;

            if (tv.Name == "TvSource")
            {
                tvAnother = this.gBoxDest.Controls.Find("TvDest", true).FirstOrDefault() as TreeView;
            }
            else
            {
                tvAnother = this.gBoxSource.Controls.Find("TvSource", true).FirstOrDefault() as TreeView;
            }
            for (int i = 0; i < tvAnother.Nodes.Count; i++) //把所有的节点去掉颜色
            {
                tvAnother.Nodes[i].BackColor = Color.White;
                for (int j = 0; j < tvAnother.Nodes[i].Nodes.Count; j++)
                {
                    tvAnother.Nodes[i].Nodes[j].BackColor = Color.White;
                }
            }
            if (parent != -1)
            {
                tv.Nodes[parent].EnsureVisible();
                tvAnother.Nodes[parent].Nodes[cIndex].EnsureVisible();
                tvAnother.Nodes[parent].Nodes[cIndex].BackColor = Color.SkyBlue; //选中子节点
            }
            else
            {
                tv.Nodes[cIndex].EnsureVisible();
                tvAnother.Nodes[cIndex].EnsureVisible();
                tvAnother.Nodes[cIndex].BackColor = Color.SkyBlue; //选中父节点
            }
        }

        void TreeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            TreeView tv = sender as TreeView;
            TreeView tvAnother = null;
            if (tv.Name == "TvSource")
            {
                tvAnother = this.gBoxDest.Controls.Find("TvDest", true).FirstOrDefault() as TreeView;
            }
            else
            {
                tvAnother = this.gBoxSource.Controls.Find("TvSource", true).FirstOrDefault() as TreeView;
            }
            for (int i = 0; i < tv.Nodes.Count; i++) //把所有的节点去掉颜色
            {
                tv.Nodes[i].BackColor = Color.White;
                for (int j = 0; j < tvAnother.Nodes[i].Nodes.Count; j++)
                {
                    tv.Nodes[i].Nodes[j].BackColor = Color.White;
                }
            }
            for (int i = 0; i < tvAnother.Nodes.Count; i++) //把所有的节点去掉颜色
            {
                tvAnother.Nodes[i].BackColor = Color.White;
                for (int j = 0; j < tvAnother.Nodes[i].Nodes.Count; j++)
                {
                    tvAnother.Nodes[i].Nodes[j].BackColor = Color.White;
                }
            }
        }
        #endregion

        #region 字段搜索
        private void FieldSearch_Click(object sender, EventArgs e)
        {
            Dictionary<string, List<DBTable>> dict = null;
            TextBox txtField = null;
            TextBox txtSearchResult = null;

            Button btn = sender as Button;
            if (btn != null && btn.Name.Contains("Source"))
            {
                dict = Data.SourceTable;
                txtField = this.txtSource;
                txtSearchResult = this.txtSourceResult;
            }
            else
            {
                dict = Data.DestTable;
                txtField = this.txtDest;
                txtSearchResult = this.txtDestResult;
            }

            if (string.IsNullOrEmpty(txtField.Text))
            {
                txtSearchResult.Text = "搜索关键字不能为空";
                return;
            }
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(txtField.Text))
            {
                foreach (var table in dict.Keys)
                {
                    foreach (var t in dict[table])
                    {
                        if (t.Field.ToLower().Contains(txtField.Text.ToLower()))
                        {
                            sb.AppendLine(table + " " + t.Field);
                        }
                    }
                }
            }
            if (sb.Length < 1)
            {
                txtSearchResult.Text = "没有符合条件的字段";
            }
            else
            {
                txtSearchResult.Text = sb.ToString();
            }
        }
        #endregion

        #region 窗体关闭时
        private void FrmDifferentList_FormClosing(object sender, FormClosingEventArgs e)
        {
            UIControlHelper.ApplicationExit();
        }
        #endregion


    }
}
