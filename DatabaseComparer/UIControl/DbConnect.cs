using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using DatabaseComparer.Common;
using DatabaseComparer.DbHelper;

namespace DatabaseComparer.UIControl
{
    public partial class DbConnect : UserControl
    {
        public string ConnectString { get; set; }
        public Action<Hashtable> ConnectCallBack;//连接成功的委托
        private int _type { get; set; } //用来标示是源数据库还是目标数据库

        public DbConnect()
        {
            InitializeComponent();
        }

        private void DbConnect_Load(object sender, EventArgs e)
        {
            this.lblTip.Text = string.Empty;
            this.rBtnMysql.Text = Data.XmlMysql;
            this.rBtnSqlServer.Text = Data.XmlSqlServer;
            this.rBtnSqlServer.Checked = true;
            rBtn_Click(this.rBtnSqlServer, null);
        }

        #region 修改GroupBox上的文字
        public void SetGroupBoxText(string text)
        {
            this.gBox.Text = text;
        }
        #endregion

        #region 数据库连接
        public void TestConnect(Hashtable ht, int type)
        {
            ISqlHelper sqlHelper = null;
            string sqlgetdb = string.Empty;

            if (!string.IsNullOrEmpty(cmboxServer.Text) && !string.IsNullOrEmpty(this.cmboxDatabase.Text)
                && !string.IsNullOrEmpty(this.txtUserId.Text) && !string.IsNullOrEmpty(this.txtPassword.Text)
                && !string.IsNullOrEmpty(this.txtPort.Text))
            {
                this.lblTip.ForeColor = Color.Indigo;
                this.lblTip.Text = Data.Connecting;
                string connSource = string.Empty;
                sqlHelper = GetConnectionStr(0, ref connSource, ref sqlgetdb); //得到连接字符串和对应的数据库实例

                #region 开一个线程

                var sourceThread = new System.Threading.Thread(delegate()
                {
                    if (!sqlHelper.IsConnect(connSource))
                    {
                        this.BeginInvoke(new MethodInvoker(delegate
                        {
                            this.lblTip.ForeColor = Color.Red;
                            this.lblTip.Text = Data.ConnectionFail;
                        }));
                        if (ConnectCallBack != null)
                        {
                            ConnectCallBack(ht);
                        }
                    }
                    else
                    {
                        ConnectString = connSource;
                        this.BeginInvoke(new MethodInvoker(delegate
                        {
                            this.lblTip.ForeColor = Color.Green;
                            this.lblTip.Text = Data.ConnectSuccess;

                            new XmlHelper().SaveSetting(this.gBox, this.pDbType, this.cmboxServer, type);
                            if (ConnectCallBack != null)
                            {
                                ht[type] = 1;
                                ConnectCallBack(ht);
                            }
                        }));
                    }
                });
                sourceThread.IsBackground = true;
                sourceThread.Start();

                #endregion
            }
            else
            {
                this.lblTip.ForeColor = Color.Red;
                this.lblTip.Text = "请输入连接字符串";
            }
        }
        #endregion

        #region 加载默认的服务器
        public void LoadDataBase(int type)
        {
            bool selected = false;
            _type = type;
            List<XNode> list = Data.DataBaseList.Where(p => p.Attribute(Data.Type).Value == type.ToString()).
                SelectMany(p => p.Elements(this.cmboxServer.Name)).
                Select(p => p.FirstNode).ToList();

            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    var item = new ComboboxItem();
                    item.Text = list[i].ToString();
                    item.Value = list[i].ToString();
                    this.cmboxServer.Items.Add(item);

                    if (list[i].Parent.Parent.Attribute(Data.Selected) != null && list[i].Parent.Parent.Attribute(Data.Selected).Value == "1")
                    {
                        selected = true;
                        this.cmboxServer.SelectedIndex = i;
                    }
                }
            }
            if (!selected && this.cmboxServer.Items.Count > 0)
            {
                this.cmboxServer.SelectedIndex = 0;
            }
        }
        #endregion

        #region 下拉框值变化
        private void cmboxServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cbox = sender as ComboBox;
            if (cbox != null && cbox.SelectedIndex >= 0)
            {
                string currentServer = string.Empty;
                XElement xelement = null;
                Panel panel = this.pDbType;
                GroupBox gBox = this.gBox;
                currentServer = this.cmboxServer.SelectedItem.ToString();
                xelement = Data.DataBaseList.Where(p => p.Element(this.cmboxServer.Name).Value == currentServer && p.Attribute(Data.Type).Value == _type.ToString()).FirstOrDefault();

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
                        var radio = c as RadioButton;
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
            var radio = this.pDbType.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);

            switch (radio.Text)
            {
                case Data.XmlMysql:
                    sqlHelper = new MySqlHelper();
                    conn = "Server=" + this.cmboxServer.Text.Trim() + ";Database=" +
                           this.cmboxDatabase.Text.Trim() + ";Uid=" + this.txtUserId.Text.Trim() + ";Pwd=" +
                           this.txtPassword.Text + ";charset=utf8;Port=" + this.txtPort.Text.Trim();
                    sql = "  SELECT schema_name FROM INFORMATION_SCHEMA.SCHEMATA order by schema_name ";
                    break;

                case Data.XmlSqlServer:
                    sqlHelper = new SqlServerHelper();
                    conn = "Server=" + this.cmboxServer.Text.Trim() + "," + (this.cmboxServer.Text.Contains(",") ? "" : this.txtPort.Text.Trim()) +
                           ";Initial Catalog=" + this.cmboxDatabase.Text.Trim() + ";User ID=" +
                           this.txtUserId.Text.Trim() + ";Password=" + this.txtPassword.Text;
                    sql = " SELECT  name FROM sys.databases order by name ";
                    break;
            }
            return sqlHelper;
        }
        private void cmboxDatabase_Click(object sender, EventArgs e)
        {
            string conn = string.Empty;
            string sqlgetdb = string.Empty;
            ISqlHelper sqlHelper = null;

            if (string.IsNullOrEmpty(this.txtUserId.Text) || string.IsNullOrEmpty(this.txtPassword.Text) ||
                string.IsNullOrEmpty(this.txtPort.Text) || string.IsNullOrEmpty(this.cmboxServer.Text))
            {
                this.lblTip.ForeColor = Color.Red;
                this.lblTip.Text = "请输入连接字符串";
                return;
            }

            sqlHelper = GetConnectionStr(0, ref conn, ref sqlgetdb);
            if (ConnectString != conn)
            {
                ConnectString = string.Empty;
                this.lblTip.ForeColor = Color.Indigo;
                this.lblTip.Text = Data.Connecting;
                this.cmboxDatabase.Items.Clear();
                var th = new System.Threading.Thread(delegate()
                {
                    if (sqlHelper.IsConnect(conn))
                    {
                        ConnectString = conn;
                        DataTable temp = sqlHelper.ExecuteDataTable(conn, CommandType.Text, sqlgetdb);
                        this.BeginInvoke(new MethodInvoker(delegate
                        {
                            this.lblTip.ForeColor = Color.Green;
                            this.lblTip.Text = Data.ConnectSuccess;

                            if (temp != null)
                            {
                                for (int i = 0; i < temp.Rows.Count; i++)
                                {
                                    ComboboxItem item = new ComboboxItem();
                                    item.Text = temp.Rows[i][0].ToString();
                                    item.Value = temp.Rows[i][0].ToString();
                                    this.cmboxDatabase.Items.Add(item);
                                }
                                if (this.cmboxDatabase.Items.Count > 0)
                                {
                                    this.cmboxDatabase.SelectedItem = 0;
                                }
                            }
                        }));
                    }
                    else
                    {
                        this.BeginInvoke(new MethodInvoker(delegate
                        {
                            this.lblTip.ForeColor = Color.Red;
                            this.lblTip.Text = Data.ConnectionFail;
                        }));
                    }
                });
                th.IsBackground = true;
                th.Start();
            }
        }
        #endregion

        #region 数据库种类切换
        private void rBtn_Click(object sender, EventArgs e)
        {
            RadioButton rbtn = (RadioButton)sender;
            switch (rbtn.Text)
            {
                case Data.XmlMysql:
                    if (string.IsNullOrEmpty(this.txtPort.Text) || this.txtPort.Text == "1433")
                    {
                        this.txtPort.Text = "3306";
                    }
                    break;

                case Data.XmlSqlServer:
                    if (string.IsNullOrEmpty(this.txtPort.Text) || this.txtPort.Text == "3306")
                    {
                        this.txtPort.Text = "1433";
                    }
                    break;

            }
        }
        #endregion
    }
}
