using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using DatabaseComparer.DbHelper;

namespace DatabaseComparer.Common
{
    public class Core
    {
        public static FrmDifferentList DifferentForm { get; set; }

        static string MySqlServer = "show tables";
        static string SqlServer = "SELECT name FROM sysobjects WHERE xtype='U' order by name";

        #region 这个线程用于比较两个库的不同
        public static void DataBaseCompare(ProgressBar pBarSource, ProgressBar pBarDest)
        {
            try
            {
                Thread thread = new System.Threading.Thread(delegate()
                {
                    #region 判断进度条是否完成
                    while (true)
                    {
                        if (pBarSource.Visible == true || pBarDest.Visible == true)
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
                    ShowCompareResult(Data.SourceTable, DifferentForm.TvSource, Data.DestTable, DifferentForm.TvDestination, HandledTakble);
                    #endregion

                    #region 先循环Dest，后循环Source
                    ShowCompareResult(Data.DestTable, DifferentForm.TvDestination, Data.SourceTable, DifferentForm.TvSource, HandledTakble);
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
        static void ShowCompareResult(Dictionary<string, List<DBTable>> source, TreeView tSource, Dictionary<string, List<DBTable>> dest, TreeView tDest, List<string> HandledTable)
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
                        string text = stable.Value[i].Field + " " + stable.Value[i].Type + "(" + stable.Value[i].Length + ")";
                        array.Add(new TreeNode(text));
                        arrayEmpty.Add(new TreeNode(string.Empty.PadRight(text.Length, ' ')));
                    }
                    TreeNode treeNode = new TreeNode(stable.Key, array.ToArray());
                    TreeNode treeNodeEmpty = new TreeNode(string.Empty.PadLeft(stable.Key.Length, ' '), arrayEmpty.ToArray());
                    DifferentForm.BeginInvoke(new MethodInvoker(delegate
                    {
                        tSource.Nodes.Add(treeNode);
                        tDest.Nodes.Add(treeNodeEmpty);
                    }));
                }
            }
        }
        #endregion

        #region  处理两个库中每一张表不同的结构
        static void HandleDifference(KeyValuePair<string, List<DBTable>> list, TreeView tSource, List<DBTable> anotherList, TreeView tDest)
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
                DifferentForm.BeginInvoke(new MethodInvoker(delegate
                {
                    tSource.Nodes.Add(treeNode);
                    tDest.Nodes.Add(treeNodeEmpty);
                }));
            }
        }
        #endregion

        #region 读取全部表
        public static void LoadDBTable(GroupBox gBox, string conn, string db, Dictionary<string, List<DBTable>> data)
        {
            DataTable dt = null;
            if (Core.IsMySql(conn))
            {
                gBox.Text += " [" + db + "]";
                dt = new MySqlHelper().ExecuteDataTable(conn, CommandType.Text, MySqlServer, null);
            }
            else
            {
                gBox.Text += " [" + db + "]";
                dt = new SqlServerHelper().ExecuteDataTable(conn, CommandType.Text, SqlServer, null);
            }
            foreach (var t in dt.AsEnumerable().ToList())
            {
                data.Add(t.ItemArray[0].ToString(), new List<DBTable>());
            }
        }

        #endregion

        #region  比较表字段
        static void CompareTableField(List<DBTable> list, List<DBTable> anotherList, List<TreeNode> array, List<TreeNode> arrayAnother, List<string> HandledField)
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

        #region 载入每个表的字段
        public static void LoadTableField(string conn, Dictionary<string, List<DBTable>> list, ProgressBar pBar)
        {
            try
            {
                pBar.Minimum = 0;
                pBar.Maximum = list.Count;

                Thread thread = new Thread(delegate()
                {
                    int current = 0;
                    foreach (var t in list)
                    {
                        DifferentForm.BeginInvoke(new MethodInvoker(delegate
                        {
                            pBar.Value = current;
                        }));

                        string sql = string.Empty;
                        if (Core.IsMySql(conn))
                        {
                            #region MySql数据库读取每张表的字段

                            sql = "SHOW FULL COLUMNS FROM " + t.Key;
                            DataTable dt = new MySqlHelper().ExecuteDataTable(conn, CommandType.Text, sql, null);
                            foreach (var field in dt.AsEnumerable().ToList())
                            {
                                DBTable item = new DBTable();
                                item.Field = field["Field"].Ustring();
                                int loc = field["Type"].Ustring().IndexOf("(");
                                if (loc > 0)
                                {
                                    item.Type = field["Type"].ToString().Substring(0, loc);
                                    item.Length = field["Type"].ToString().Substring(loc + 1).TrimEnd(')').Uint();
                                }
                                else
                                {
                                    item.Type = field["Type"].ToString();
                                }
                                item.CanNull = (field["Null"].Ustring().ToLower() == "yes" ? true : false);
                                item.Comment = field["Comment"].Ustring();
                                list[t.Key].Add(item);
                            }

                            #endregion
                        }
                        else
                        {
                            #region SqlServer数据库读取每张表的字段

                            sql = @"SELECT table_name,
                                        column_name,
                                        ISNULL(column_default,'') AS column_default,
                                        is_nullable,
                                        data_type,
                                        ISNULL(ISNULL(ISNULL(character_maximum_length,numeric_precision),datetime_precision),1) AS column_length
                                        FROM information_schema.columns
                                        WHERE NOT table_name IN('sysdiagrams','dtproperties') and table_name='" + t.Key +
                                  "'";

                            DataTable dt = new SqlServerHelper().ExecuteDataTable(conn, CommandType.Text, sql, null);
                            foreach (var field in dt.AsEnumerable().ToList())
                            {
                                DBTable item = new DBTable();
                                item.Field = field["column_name"].ToString();
                                item.Type = field["data_type"].ToString();
                                item.Length = field["column_length"].Uint();
                                item.CanNull = (field["is_nullable"].Ustring().ToLower() == "yes" ? true : false);
                                list[t.Key].Add(item);
                            }

                            #endregion
                        }
                        current++;
                    }
                    DifferentForm.BeginInvoke(new MethodInvoker(delegate
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

        #region 创建字段脚本
        public static string CreateFieldSql(string tableName, string filed, string conn, Dictionary<string, List<DBTable>> table)
        {
            string sql = string.Empty;
            string ok = string.Empty;
            string filedName = filed.Split(' ')[0];
            var t = table.Where(p => p.Key.ToLower() == tableName.ToLower()).FirstOrDefault();
            var one = t.Value.Where(p => p.Field.ToLower() == filedName.ToLower().Trim()).FirstOrDefault();

            if (Core.IsMySql(conn))
            {
                if (Data.MySqlFiledType.Contains(one.Type.ToLower()))
                {
                    ok += " `" + one.Field + "` " + one.Type + "(" + one.Length.ToString() + ")";
                }
                else
                {
                    ok += " `" + one.Field + "` " + one.Type;
                }

                if (one.CanNull)
                {
                    ok += " Null ";
                }
                else
                {
                    ok += " Not Null ";
                }
                sql = "ALTER TABLE " + "`" + tableName + "`" + " Add Column " + ok + ";";
            }
            else
            {
                if (Data.SqlFiledType.Contains(one.Type.ToLower()))
                {
                    ok += "[" + one.Field + "]" + one.Type + "(" + one.Length.ToString() + ")";
                }
                else
                {
                    ok += "[" + one.Field + "] " + one.Type;
                }

                if (one.CanNull)
                {
                    ok += " Null ";
                }
                else
                {
                    ok += " Not Null ";
                }
                sql = "ALTER TABLE " + "[" + tableName + "]" + " Add " + ok + ";";
            }
            return sql;
        }
        #endregion

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
