using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;//如果是.net2.0环境，不仅仅需要在这里using，还要在项目中右键引用 
using MySql.Data.MySqlClient;// 这里需要引用Connector Net 6.5.4 里面的  MySql.Data.dll

namespace DatabaseComparer.DbHelper
{
    /// <summary>基于MySQL的数据层基类,可以传入新的连接字符串，如果为null，则使用web.config里面指定的字符串
    /// 实际上，MySql.Data.dll里面也有一个自带的 MySqlHelper帮助类，我这里取一样的名字是覆盖了它自带的帮助类（自带的差的离谱··）
    /// </summary>
    public class MySqlHelper : ISqlHelper
    {
        #region 是否连接
        public bool IsConnect(string connectionString)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    return true;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        #endregion

        #region PrepareCommand -> Command预处理,会判断conn的连接字符串是否已经指定，如果没有指定则使用默认web.config里面的字符串
        /// <summary>Command预处理
        /// </summary>
        /// <param name="conn">MySqlConnection对象</param>
        /// <param name="trans">MySqlTransaction对象，可为null</param>
        /// <param name="cmd">MySqlCommand对象</param>
        /// <param name="cmdType">CommandType，存储过程或命令行</param>
        /// <param name="cmdText">SQL语句或存储过程名</param>
        /// <param name="cmdParms">MySqlCommand参数数组，可为null</param>
        private static void PrepareCommand(MySqlConnection conn, MySqlTransaction trans, MySqlCommand cmd, CommandType cmdType, string cmdText, MySqlParameter[] cmdParms)
        {
            //到底什么时候需要conn.open呢？哪些操作需要open呢？
            //如果是通过cmd执行 ExecuteNonQuery，ExecuteScalar,ExecuteReader (也就是 类似于cmd.ExecuteNonQuery) 的时候，是需要open的
            //如果是通过MySqlDataAdapter da = new MySqlDataAdapter(cmd); 来执行的时候，是不需要open的，那么这个时候conn就一直开着吗？会浪费？
            //不会浪费，因为通过SqlDataAdapter的时候，我们都用using来封装了conn的 => using (MySqlConnection conn = new MySqlConnection(connectionString))
            //那么，没有封装using的 ExecuteReader 呢？也是一直开着？放心，我们有用到 MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); 
            //当 dr 关闭的时候,conn也会跟着关闭了(如果前台调用是用dr.Read()来获取值，则需要dr.Close来关闭dr，如果是gridview之类的控件绑定， 则不用dr.Close了)  

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (MySqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
        #endregion

        #region ExecuteNonQuery->执行ExecuteNonQuery，用来增加,删除，修改
        /// <summary>执行ExecuteNonQuery 
        /// </summary>
        /// <param name="connectionString">连接字符串，可为null，如果为null则使用web.config的值，不为null则使用传入的值</param>
        /// <param name="trans">MySqlTransaction,事务，可为null</param>
        /// <param name="cmdType">命令类型（存储过程或SQL语句）</param>
        /// <param name="cmdText">SQL语句或存储过程名</param>
        /// <param name="cmdParms">MySqlCommand参数数组</param>
        /// <returns>返回受引响的记录行数</returns>
        public static int ExecuteNonQuery(string connectionString, MySqlTransaction trans, CommandType cmdType, string cmdText, params MySqlParameter[] cmdParms)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    PrepareCommand(trans.Connection, trans, cmd, cmdType, cmdText, cmdParms);
                    int val = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return val;
                }
            }
        }
        #endregion

        #region ExecuteScalar ->执行命令，返回第一行第一列的值
        /// <summary>
        /// 执行命令，返回第一行第一列的值
        /// </summary>
        /// <param name="connectionString">连接字符串，可为null，如果为null则使用web.config的值，不为null则使用传入的值</param>
        /// <param name="cmdType">命令类型（存储过程或SQL语句）</param>
        /// <param name="cmdText">SQL语句或存储过程名</param>
        /// <param name="cmdParms">MySqlCommand参数数组</param>
        /// <returns>返回Object对象</returns>
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] cmdParms)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    PrepareCommand(conn, null, cmd, cmdType, cmdText, cmdParms);
                    object val = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    return val;
                }
            }
        }
        #endregion

        #region ExecuteReader ->执行命令或存储过程，返回MySqlDataReader对象
        /// <summary>
        /// 执行命令或存储过程，返回MySqlDataReader对象
        /// 注意MySqlDataReader对象,在前台使用完最好是先弄一个dr等于这里的返回值，
        /// 如果前台调用是用的dr.Read(),然后读取的值，则之后需要对dr进行dr.Close以关闭dr,进而释放MySqlConnection资源
        /// 如果是绑定了gridview之类的控件，则不用dr.Close(绑定控件帮我们已经close了)
        /// </summary>
        /// <param name="connectionString">连接字符串，可为null，如果为null则使用web.config的值，不为null则使用传入的值</param>
        /// <param name="cmdType">命令类型（存储过程或SQL语句）</param>
        /// <param name="cmdText">SQL语句或存储过程名</param>
        /// <param name="cmdParms">MySqlCommand参数数组</param>
        /// <returns></returns>
        public static IDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] cmdParms)
        {
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection conn = new MySqlConnection(connectionString);

            try
            {
                PrepareCommand(conn, null, cmd, cmdType, cmdText, cmdParms);
                MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return dr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }
        #endregion

        #region ExecuteDataSet->执行命令或存储过程，返回DataSet对象
        /// <summary>执行命令或存储过程，返回DataSet对象
        /// 
        /// </summary>
        /// <param name="connectionString">连接字符串，可为null，如果为null则使用web.config的值，不为null则使用传入的值</param>
        /// <param name="cmdType">命令类型(存储过程或SQL语句)</param>
        /// <param name="cmdText">SQL语句或存储过程名</param>
        /// <param name="cmdParms">MySqlCommand参数数组(可为null值)</param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] cmdParms)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    PrepareCommand(conn, null, cmd, cmdType, cmdText, cmdParms);
                    //SqlDataAdapter，离线应用，不需要用conn.open(), 它把这部分功能给封装到自己内部了，不需要你来显式的去调用, 它直接将数据fill到dataset中.
                    //但是我们这里用 PrepareCommand 的时候不是还是帮我们open了么？不就浪费了么？没事，因为 conn被我们用 using 了，等下语句结束的时候，会释放的
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    cmd.Parameters.Clear();
                    return ds;
                }
            }
        }
        #endregion

        #region ExecuteDataTable->执行执行命令或存储过程，返回一个DataTable

        /// <summary>执行命令或存储过程，返回一个DataTable
        /// 
        /// </summary>
        /// <param name="connectionString">连接字符串，可为null，如果为null则使用web.config的值，不为null则使用传入的值</param>
        /// <param name="cmdType">CommandType.Text或者是CommandType.StoredProcedure</param>
        /// <param name="cmdText">SQL语句或存储过程名</param>
        /// <param name="cmdParms">MySqlCommand参数数组(可为null值)</param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] cmdParms)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    PrepareCommand(conn, null, cmd, cmdType, cmdText, cmdParms);

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    cmd.Parameters.Clear();
                }
            }
            return dt;
        }
        #endregion

        public DataTable ExecuteDataTable(string connectionString, CommandType cmdType, string cmdText)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = cmdText;
                        cmd.CommandType = CommandType.Text;
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        da.Fill(dt);
                        cmd.Parameters.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return dt;
        }
    }
}
