using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DatabaseComparer.Common;
using DatabaseComparer.UIControl;

namespace DatabaseComparer
{
    public partial class FrmDifferentList : Form
    {
        private ContextMenuStrip MenuStrip;
        public TreeViewEx TvSource;
        public TreeViewEx TvDestination;

        public TreeView CurrentTV;

        #region FrmDifferentList
        public FrmDifferentList()
        {
            InitializeComponent();
            Core.DifferentForm = this;
            MenuStrip = new ContextMenuStrip();
            var item2 = new ToolStripMenuItem()
            {
                Name = "CreateField",
                Text = "创建字段 Sql"
            };
            MenuStrip.Items.Add(item2);
            MenuStrip.ItemClicked += new ToolStripItemClickedEventHandler(MenuStrip_ItemClicked);
            MenuStrip.Opening += new CancelEventHandler(MenuStrip_Opening);
        }

        void MenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (CurrentTV != null && CurrentTV.SelectedNode != null && CurrentTV.SelectedNode.Parent != null)
            {
                this.MenuStrip.Items[0].Enabled = true;
            }
            else
            {
                this.MenuStrip.Items[0].Enabled = false;
            }
        }

        void MenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string conn = string.Empty;
            TreeView tvAnother;
            Dictionary<string, List<DBTable>> table = null;
            switch (CurrentTV.Name)
            {
                case "TvSource":
                    tvAnother = this.TvDestination;
                    conn = Data.SourceConnectionString;
                    table = Data.DestTable;
                    break;
                default:
                    tvAnother = this.TvSource;
                    conn = Data.DestConnectionString;
                    table = Data.SourceTable;
                    break;
            }
            if (string.IsNullOrEmpty(CurrentTV.SelectedNode.Parent.Text.Trim()) || string.IsNullOrEmpty(tvAnother.Nodes[CurrentTV.SelectedNode.Parent.Index].Text.Trim()))
            {
                MessageBox.Show("请先创建表！");
                return;
            }

            if (string.IsNullOrEmpty(tvAnother.Nodes[CurrentTV.SelectedNode.Parent.Index].Nodes[CurrentTV.SelectedNode.Index].Text.Trim()))
            {
                MessageBox.Show("另一边不存在该字段！");
                return;
            }

            var sql = Core.CreateFieldSql(tvAnother.Nodes[CurrentTV.SelectedNode.Parent.Index].Text.Trim(), tvAnother.Nodes[CurrentTV.SelectedNode.Parent.Index].Nodes[CurrentTV.SelectedNode.Index].Text.Trim(), conn, table);

            if (!string.IsNullOrEmpty(sql))
            {
                Clipboard.SetDataObject(sql);
                MessageBox.Show("已复制到剪贴板");
            }
        }
        #endregion

        #region FrmDifferentList_Load
        private void FrmDifferentList_Load(object sender, EventArgs e)
        {
            Core.LoadDBTable(this.gBoxSource, Data.SourceConnectionString, Data.SourceDataBase, Data.SourceTable);
            Core.LoadDBTable(this.gBoxDest, Data.DestConnectionString, Data.DestDataBase, Data.DestTable);

            Core.LoadTableField(Data.SourceConnectionString, Data.SourceTable, pBarSource);
            Core.LoadTableField(Data.DestConnectionString, Data.DestTable, pBarDest);

            #region 创建TreeView
            TvSource = new TreeViewEx();
            TvSource.Name = "TvSource";
            TvDestination = new TreeViewEx();
            TvDestination.Name = "TvDestination";

            this.gBoxSource.Controls.Add(TvSource);
            this.gBoxDest.Controls.Add(TvDestination);

            this.TvSource.Dock = DockStyle.Fill;
            this.TvDestination.Dock = DockStyle.Fill;
            this.TvSource.AfterExpand += new TreeViewEventHandler(TreeView_AfterExpand);
            this.TvDestination.AfterExpand += new TreeViewEventHandler(TreeView_AfterExpand);
            this.TvSource.AfterCollapse += new TreeViewEventHandler(TreeView_AfterCollapse);
            this.TvDestination.AfterCollapse += new TreeViewEventHandler(TreeView_AfterCollapse);

            this.TvSource.BeforeSelect += new TreeViewCancelEventHandler(TreeView_BeforeSelect);
            this.TvDestination.BeforeSelect += new TreeViewCancelEventHandler(TreeView_BeforeSelect);
            this.TvSource.AfterSelect += new TreeViewEventHandler(TreeView_AfterSelect);
            this.TvDestination.AfterSelect += new TreeViewEventHandler(TreeView_AfterSelect);

            this.TvSource.NodeMouseClick += new TreeNodeMouseClickEventHandler(TreeView_NodeMouseClick);
            this.TvDestination.NodeMouseClick += new TreeNodeMouseClickEventHandler(TreeView_NodeMouseClick);

            TvSource.ContextMenuStrip = this.MenuStrip;
            TvDestination.ContextMenuStrip = this.MenuStrip;
            #endregion

            Core.DataBaseCompare(this.pBarSource, this.pBarDest);
        }
        #endregion

        #region 处理在树节点上右击
        private void TreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            CurrentTV = sender as TreeView;
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

            if (tv.Name == "TvSource")
            {
                tvAnother = this.gBoxDest.Controls.Find("TvDestination", true).FirstOrDefault() as TreeView;
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
                tvAnother = this.gBoxDest.Controls.Find("TvDestination", true).FirstOrDefault() as TreeView;
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
                tvAnother = this.gBoxDest.Controls.Find("TvDestination", true).FirstOrDefault() as TreeView;
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
            Application.ExitThread();
        }
        #endregion
    }
}
