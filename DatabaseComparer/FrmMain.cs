using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using DatabaseComparer.Common;

namespace DatabaseComparer
{
    public partial class FrmMain : Form
    {
        public bool OpenCompare { get; set; }

        public FrmMain()
        {
            InitializeComponent();
        }

        #region 载入
        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.lblSTip.Text = string.Empty;
            this.lblSTip.ForeColor = Color.Red;

            this.lblDTip.Text = string.Empty;
            this.lblDTip.ForeColor = Color.Red;

            this.ckBoxIgnoreLength.Checked = true;
            this.ckBoxIgnoreType.Checked = true;

            LoadDataBase(0);
            LoadDataBase(1);
        }
        #endregion

        #region 下一步
        private void btnNext_Click(object sender, EventArgs e)
        {
            bool bs = false;
            bool ds = false;
            ISqlHelper sqlHelper = null;
            string sqlgetdb = string.Empty;

            #region Source
            if (!string.IsNullOrEmpty(cmboxSourceServer.Text) && !string.IsNullOrEmpty(this.cmboxSDatabase.Text))
            {
                this.lblSTip.Text = Data.Connecting;

                string connSource = string.Empty;
                sqlHelper = GetConnectionStr(0, ref connSource, ref sqlgetdb); //得到连接字符串和对应的数据库实例

                var sourceThread = new System.Threading.Thread(delegate()
                {
                    bs = sqlHelper.IsConnect(connSource);
                    if (!bs)
                    {
                        this.BeginInvoke(new MethodInvoker(delegate
                        {
                            this.lblSTip.Text = Data.ConnectionFail;
                        }));
                    }
                    else
                    {
                        Data.SourceConnectionString = connSource;
                        this.BeginInvoke(new MethodInvoker(delegate
                        {
                            Data.SourceDataBase = this.cmboxSDatabase.Text;
                            this.lblSTip.Text = Data.ConnectSuccess;
                            new XmlHelper().SaveSetting(this.gBoxSource, this.pSourceType, this.cmboxSourceServer, "Source");
                            if (bs && ds)
                            {
                                SaveConnect();
                            }
                        }));
                    }
                });
                sourceThread.IsBackground = true;
                sourceThread.Start();
            }
            #endregion

            #region Destination
            if (!string.IsNullOrEmpty(this.cmboxDestServer.Text) && !string.IsNullOrEmpty(this.cmboxDDatabase.Text))
            {
                this.lblDTip.Text = Data.Connecting;

                string connDest = string.Empty;
                sqlHelper = GetConnectionStr(1, ref connDest, ref sqlgetdb); //得到连接字符串和对应的数据库实例

                var destThread = new System.Threading.Thread(delegate()
                {
                    ds = sqlHelper.IsConnect(connDest);
                    if (!ds)
                    {
                        this.BeginInvoke(new MethodInvoker(delegate
                        {
                            this.lblDTip.Text = Data.ConnectionFail;
                        }));
                    }
                    else
                    {
                        Data.DestConnectionString = connDest;
                        this.BeginInvoke(new MethodInvoker(delegate
                        {
                            Data.DestDataBase = this.cmboxDDatabase.Text;
                            this.lblDTip.Text = Data.ConnectSuccess;
                            new XmlHelper().SaveSetting(this.gBoxDest, this.pDestType, this.cmboxDestServer,
                                "Destination");
                            if (bs && ds)
                            {
                                SaveConnect();
                            }
                        }));
                    }
                });
                destThread.IsBackground = true;
                destThread.Start();
            }
            #endregion
        }
        #endregion

        #region  数据库连接是否成功

        private void SaveConnect()
        {
            if (!OpenCompare)
            {
                Data.IsIgnoreLength = this.ckBoxIgnoreLength.Checked;
                Data.IsIgnoreType = this.ckBoxIgnoreType.Checked;
                var diff = new FrmDifferentList();
                diff.Show();
                this.Hide();
                OpenCompare = true;
            }
        }

        #endregion

        #region 加载默认的服务器
        public void LoadDataBase(int type)
        {
            List<XNode> list = null;
            switch (type)
            {
                case 0: list = Data.SourceList.SelectMany(p => p.Elements(this.cmboxSourceServer.Name)).Select(p => p.FirstNode).ToList(); break;
                case 1: list = Data.DestinationList.SelectMany(p => p.Elements(this.cmboxDestServer.Name)).Select(p => p.FirstNode).ToList(); break;
            }
            if (list != null)
            {
                foreach (var server in list)
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = server.ToString();
                    item.Value = server.ToString();
                    switch (type)
                    {
                        case 0:
                            this.cmboxSourceServer.Items.Add(item);
                            this.cmboxSourceServer.SelectedIndex = 0;
                            break;
                        case 1:
                            this.cmboxDestServer.Items.Add(item);
                            this.cmboxDestServer.SelectedIndex = 0;
                            break;
                    }
                }
            }
        }
        #endregion

        #region 下拉框值变化
        private void cmboxSourceServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cbox = sender as ComboBox;
            if (cbox != null && cbox.SelectedIndex >= 0)
            {
                string currentServer = string.Empty;
                XElement xelement = null;
                Panel panel = null;
                GroupBox gBox = null;

                if (cbox.Name == this.cmboxSourceServer.Name)
                {
                    gBox = this.gBoxSource;
                    currentServer = this.cmboxSourceServer.SelectedItem.ToString();
                    xelement = Data.SourceList.Where(p => p.Element(this.cmboxSourceServer.Name).Value == currentServer).FirstOrDefault();
                    panel = this.pSourceType;
                }
                else if (cbox.Name == this.cmboxDestServer.Name)
                {
                    gBox = this.gBoxDest;
                    currentServer = this.cmboxDestServer.SelectedItem.ToString();
                    xelement = Data.DestinationList.Where(p => p.Element(this.cmboxDestServer.Name).Value == currentServer).FirstOrDefault();
                    panel = this.pDestType;
                }

                if (xelement != null)
                {
                    #region 给界面上控件赋值
                    foreach (var ele in xelement.Elements())
                    {
                        string value = ele.Value;
                        string name = ele.Name.LocalName;

                        foreach (Control control in gBox.Controls)
                        {
                            if (control.Name == name)
                            {
                                control.Text = value;
                            }
                        }

                        foreach (Control control in gBox.Controls)
                        {
                            if (control.Name == name)
                            {
                                control.Text = value;
                            }
                        }
                    }

                    var dbType = xelement.Element("DBType");
                    foreach (Control c in panel.Controls)
                    {
                        RadioButton radio = c as RadioButton;
                        if (radio != null && radio.Text == dbType.Value)
                        {
                            radio.Checked = true;
                        }
                    }
                    #endregion
                }
            }
        }
        #endregion

        #region 得到连接字符串对应的数据库
        private ISqlHelper GetConnectionStr(int type, ref string conn, ref  string sql)
        {
            ISqlHelper sqlHelper = null;
            switch (type)
            {
                case 0:
                    var radio = this.pSourceType.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);

                    string source = string.Empty;
                    switch (radio.Text)
                    {
                        case "Mysql": sqlHelper = new MySqlHelper();
                            conn = "Server=" + this.cmboxSourceServer.Text.Trim() + ";Database=" + this.cmboxSDatabase.Text.Trim() + ";Uid=" + this.txtSUserId.Text.Trim() + ";Pwd=" + this.txtSPassword.Text + ";charset=utf8;Port=" + this.txtSPort.Text.Trim();
                            sql = "  SELECT schema_name FROM INFORMATION_SCHEMA.SCHEMATA order by schema_name ";
                            break;

                        case "Sql Server": sqlHelper = new SqlServerHelper();
                            conn = "Server=" + this.cmboxSourceServer.Text.Trim() + "," + this.txtSPort.Text.Trim() + ";Initial Catalog=" + this.cmboxSDatabase.Text.Trim() + ";User ID=" + this.txtSUserId.Text.Trim() + ";Password=" + this.txtSPassword.Text;
                            sql = " SELECT  name FROM sys.databases order by name ";
                            break;
                    }
                    break;

                case 1:
                    radio = this.pDestType.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
                    string dest = string.Empty;
                    switch (radio.Text)
                    {
                        case "Mysql": sqlHelper = new MySqlHelper();
                            conn = "Server=" + this.cmboxDestServer.Text.Trim() + ";Database=" + this.cmboxDDatabase.Text + ";Uid=" + this.txtDUserId.Text.Trim() + ";Pwd=" + this.txtDPassword.Text + ";charset=utf8;Port=" + this.txtDPort.Text.Trim();
                            sql = "  SELECT schema_name FROM INFORMATION_SCHEMA.SCHEMATA order by schema_name ";
                            break;

                        case "Sql Server": sqlHelper = new SqlServerHelper();
                            conn = "Server=" + this.cmboxDestServer.Text + "," + this.txtDPort.Text.Trim() + ";Initial Catalog=" + this.cmboxDDatabase.Text.Trim() + ";User ID=" + this.txtDUserId.Text.Trim() + ";Password=" + this.txtDPassword.Text;
                            sql = " SELECT  name FROM sys.databases order by name ";
                            break;
                    }
                    break;
            }
            return sqlHelper;
        }
        private void cmboxDatabase_Click(object sender, EventArgs e)
        {
            var cmBox = sender as ComboBox;
            string conn = string.Empty;
            string sqlgetdb = string.Empty;
            ISqlHelper sqlHelper = null;

            if (cmBox.Name == "cmboxSDatabase")
            {
                sqlHelper = GetConnectionStr(0, ref conn, ref sqlgetdb);
                if (Data.SourceConnectionString != conn)
                {
                    DataTable temp = sqlHelper.ExecuteDataTable(conn, CommandType.Text, sqlgetdb);
                    if (temp != null)
                    {
                        for (int i = 0; i < temp.Rows.Count; i++)
                        {
                            ComboboxItem item = new ComboboxItem();
                            item.Text = temp.Rows[i][0].ToString();
                            item.Value = temp.Rows[i][0].ToString();
                            this.cmboxSDatabase.Items.Add(item);
                        }
                        if (this.cmboxSDatabase.Items.Count > 0)
                        {
                            this.cmboxSDatabase.SelectedItem = 0;
                        }
                    }
                    else
                    {
                        this.lblSTip.Text = Data.ConnectionFail;
                    }
                }
            }
            else
            {
                sqlHelper = GetConnectionStr(1, ref conn, ref sqlgetdb);
                if (Data.SourceConnectionString != conn)
                {

                    DataTable temp = sqlHelper.ExecuteDataTable(conn, CommandType.Text, sqlgetdb);
                    if (temp != null)
                    {
                        for (int i = 0; i < temp.Rows.Count; i++)
                        {
                            ComboboxItem item = new ComboboxItem();
                            item.Text = temp.Rows[i][0].ToString();
                            item.Value = temp.Rows[i][0].ToString();
                            this.cmboxDDatabase.Items.Add(item);
                        }
                        if (this.cmboxDDatabase.Items.Count > 0)
                        {
                            this.cmboxDDatabase.SelectedItem = 0;
                        }
                    }
                    else
                    {
                        this.lblDTip.Text = Data.ConnectionFail;
                    }
                }
            }
        }
        #endregion
    }
}
