using System;
using System.Data;
using System.Xml;
using System.Collections;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace DatabaseComparer.DbHelper
{
    public class OracleHelper : ISqlHelper
    {
        #region 是否连接
        public bool IsConnect(string connectionString)
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
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

        #region ExecuteDataTable
        public DataTable ExecuteDataTable(string connectionString, CommandType cmdType, string cmdText)
        {
            DataTable dt = new DataTable();
            try
            {
                using (OracleConnection conn = new OracleConnection(connectionString))
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        PrepareCommand(conn, null, cmd, cmdType, cmdText, null);
                        OracleDataAdapter da = new OracleDataAdapter(cmd);
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
        private void PrepareCommand(OracleConnection conn, OracleTransaction trans, OracleCommand cmd, CommandType cmdType, string cmdText,
             OracleParameter[] cmdParms)
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
                foreach (OracleParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
        #endregion
    }
}
