using System;
using System.Collections;
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
using DatabaseComparer.DbHelper;

namespace DatabaseComparer
{
    public partial class FrmMain : Form
    {
        public bool OpenCompare { get; set; }
        public Hashtable HT;//两个连接都成功了，才能下一步

        public FrmMain()
        {
            InitializeComponent();
            HT = new Hashtable();
            HT[Data.SourceType] = 0;
            HT[Data.DestinationType] = 0;
        }

        #region 载入
        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.dbConnectSource.SetGroupBoxText("源头");
            this.dbConnectSource.LoadDataBase(0);
            this.dbConnectDestination.SetGroupBoxText("目标");
            this.dbConnectDestination.LoadDataBase(1);

            this.dbConnectSource.ConnectCallBack = SaveConnect;
            this.dbConnectDestination.ConnectCallBack = SaveConnect;

            this.ckBoxIgnoreType.Checked = true;
            this.ckBoxIgnoreLength.Checked = true;
        }
        #endregion

        #region 下一步
        private void btnNext_Click(object sender, EventArgs e)
        {
            this.dbConnectSource.TestConnect(HT, Data.SourceType);
            this.dbConnectDestination.TestConnect(HT, Data.DestinationType);
        }
        #endregion

        #region  数据库连接是否成功
        private void SaveConnect(Hashtable ht)
        {
            if (!OpenCompare && !ht.ContainsValue(0))
            {
                Data.SourceConnectionString = this.dbConnectSource.ConnectString;
                Data.DestConnectionString = this.dbConnectDestination.ConnectString;

                Data.SourceDataBase = Data.SourceConnectionString.Split(';')[0].Split('=')[1] + " " + Data.SourceConnectionString.Split(';')[1].Split('=')[1];
                Data.DestDataBase = Data.DestConnectionString.Split(';')[0].Split('=')[1] + " " + Data.DestConnectionString.Split(';')[1].Split('=')[1];

                Data.IsIgnoreLength = this.ckBoxIgnoreLength.Checked;
                Data.IsIgnoreType = this.ckBoxIgnoreType.Checked;
                var diff = new FrmDifferentList();
                diff.Show();
                this.Hide();
                OpenCompare = true;
            }
        }
        #endregion
    }
}
