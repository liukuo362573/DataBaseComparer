using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;
using System.IO;

namespace DatabaseComparer.Common
{
    public class Data
    {
        public static string XmlRoot { get { return "Connect"; } }
        public const string XmlSqlServer = "SqlServer";
        public const string XmlMysql = "Mysql";
        public const string Type = "type";

        public static bool IsIgnoreType { get; set; }//是否忽略数据库类型
        public static bool IsIgnoreLength { get; set; }//是否忽略数据库长度
        public static int SourceType { get { return 0; } }
        public static int DestinationType { get { return 1; } }

        //下拉框数据库连接
        internal static List<XElement> DataBaseList = null;

        #region 静态构造函数
        static Data()
        {
            XmlHelper.SettingXml = Environment.CurrentDirectory + @"\Data\DB.xml";

            if (!Directory.Exists(Path.GetDirectoryName(XmlHelper.SettingXml)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(XmlHelper.SettingXml));
            }
            if (!File.Exists(XmlHelper.SettingXml))
            {
                XElement xe = new XElement("Root", "");
                xe.Save(XmlHelper.SettingXml);
            }
            XDocument xdoc = XDocument.Load(XmlHelper.SettingXml);
            DataBaseList = (from s in xdoc.Descendants(XmlRoot) select s).ToList();
        }
        #endregion

        internal static string SourceDataBase { get; set; } //Source数据库
        internal static string DestDataBase { get; set; } //Dest数据库

        internal static string SourceConnectionString { get; set; } //Source数据库连接
        internal static string DestConnectionString { get; set; } //Dest数据库连接

        public static string Connecting { get { return "正在连接..."; } }
        public static string ConnectSuccess { get { return "连接成功"; } }
        public static string ConnectionFail { get { return "连接失败"; } }

        //保存Source库中的表和字段
        internal static Dictionary<string, List<DBTable>> SourceTable = new Dictionary<string, List<DBTable>>();
        //保存Dest库中的表和字段
        internal static Dictionary<string, List<DBTable>> DestTable = new Dictionary<string, List<DBTable>>();

        #region IsMySql
        public static bool IsMySql(string conn)
        {
            if (conn.Contains("Uid"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

    }
}
