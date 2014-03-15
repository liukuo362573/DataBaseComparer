using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace DatabaseComparer.DbHelper
{
    /// <summary>基于MySQL的数据层基类,可以传入新的连接字符串，如果为null，则使用web.config里面指定的字符串
    /// 实际上，MySql.Data.dll里面也有一个自带的 MySqlHelper帮助类，我这里取一样的名字是覆盖了它自带的帮助类（自带的差的离谱··）
    /// </summary>
    public class SqlServerHelper : ISqlHelper
    {
        #region 是否连接
        public bool IsConnect(string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
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
        private void PrepareCommand(SqlConnection conn, SqlTransaction trans, SqlCommand cmd, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
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
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
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
        public DataTable ExecuteDataTable(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    PrepareCommand(conn, null, cmd, cmdType, cmdText, cmdParms);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
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
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        PrepareCommand(conn, null, cmd, cmdType, cmdText, null);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
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
