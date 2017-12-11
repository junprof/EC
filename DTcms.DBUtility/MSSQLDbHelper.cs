using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

namespace DTcms.DBUtility
{
    /// <summary>
    /// Oracle数据访问库,同DbHelperOra(连接字符串)
    /// 区别：使用oracle 官方访问类(微软推荐)
    /// </summary>
    public class MSSQLDbHelper
    {
        /// <summary>
        /// 业务逻辑库 数据访问
        /// </summary>
        public static MSSQLDbHelper Instance = new MSSQLDbHelper(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

		public string ConnectionString
        {
            get;
            private set;
        }

        public MSSQLDbHelper(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        #region NonQuery
        /// <summary>
        /// 非查询SQL语句执行
        /// </summary> 
        /// <param name="sqlText">sql语句</param> 
        /// <returns>影响行数</returns>
        public int ExecuteNonQuery(string sqlText, params SqlParameter[] cmdParameters)
        {
            return ExecuteNonQuery(CommandType.Text, sqlText, cmdParameters);
        }

        /// <summary>
        /// 非查询语句执行
        /// </summary> 
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">存储过程名orSQL</param> 
        /// <returns>影响行数</returns>
        public int ExecuteNonQuery(CommandType cmdType, string cmdText,
            params SqlParameter[] cmdParameters)
        {

            SqlCommand cmd = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, cmdParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }
        /// <summary>
        /// 非查询语句执行
        /// </summary> 
        /// <param name="cmd">命令</param>
        /// <returns>影响行数</returns>
        public int ExecuteNonQuery(SqlCommand cmd)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                cmd.Connection = connection;
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }
        ///// <summary>
        ///// 批量执行
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="sql"></param>
        ///// <param name="datalist"></param>
        ///// <returns></returns>
        //public int BatchExecute<T>(string sql, List<T> datalist) where T : new()
        //{
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandText = sql;
        //    cmd.CommandType = CommandType.Text;
        //    cmd.ArrayBindCount = datalist.Count;
        //    List<Columns> cols = OracleAccess.GetColumnsByEntity<T>();
        //    List<SqlParameter> paramslist = OracleAccess.Params(datalist, cols);
        //    paramslist.ForEach(p => cmd.Parameters.Add(p));
        //    return ExecuteNonQuery(cmd);
        //}
        //public int BatchExecute<T>(string sql, List<T> datalist, List<Columns> cols) where T : new()
        //{
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandText = sql;
        //    cmd.CommandType = CommandType.Text;
        //    cmd.ArrayBindCount = datalist.Count;
        //    List<SqlParameter> paramslist = OracleAccess.Params(datalist, cols);
        //    paramslist.ForEach(p => cmd.Parameters.Add(p));
        //    return ExecuteNonQuery(cmd);
        //}
        //public int BatchExecute<T>(string sql, List<T> datalist, List<Columns> cols,SqlTransaction oTrans) where T : new()
        //{
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandText = sql;
        //    cmd.CommandType = CommandType.Text;
        //    cmd.Transaction = oTrans;
        //    cmd.ArrayBindCount = datalist.Count;
        //    List<SqlParameter> paramslist = OracleAccess.Params(datalist, cols);
        //    paramslist.ForEach(p => cmd.Parameters.Add(p));
        //    return ExecuteNonQuery(cmd);
        //}






        /// <summary>
        /// 非查询语句执行
        /// </summary> 
        /// <param name="cmdType">命令类型</param>
        /// <param name="sqlText">SQL</param>
        /// <param name="cmdParameters">oracle 参数</param>
        /// <returns>影响行数</returns>
        public int ExecuteNonQuery(SqlTransaction myTrans, string sqlText,
            params SqlParameter[] cmdParameters)
        {
            return ExecuteNonQuery(myTrans, CommandType.Text, sqlText, cmdParameters);
        }

        /// <summary>
        /// 对已存在的时候执行非查询语句
        /// </summary>
        /// <remarks>
        /// e.g.: int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "PublishOrders", new SqlParameter(":prodid", 24));
        /// </remarks>
        /// <param name="myTrans">已存在的事务</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">存储过程名orSQL</param>
        /// <param name="cmdParameters">参数</param>
        /// <returns>影响行数</returns>
        public int ExecuteNonQuery(SqlTransaction myTrans, CommandType cmdType,
            string cmdText, params SqlParameter[] cmdParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, myTrans.Connection, myTrans, cmdType, cmdText, cmdParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }
        #endregion

        #region Reader

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">存储过程名orSQL</param>
        /// <param name="cmdParameters">oracle 参数</param>
        /// <returns></returns>
        public SqlDataReader ExecuteReader(CommandType cmdType, string cmdText,
            params SqlParameter[] cmdParameters)
        {

            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(ConnectionString);

            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParameters);

                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;

            }
            catch
            {
                conn.Close();
                throw;
            }
        }
        public SqlDataAdapter ExecuteAdapter(CommandType cmdType, string cmdText,
            params SqlParameter[] cmdParameters)
        {

            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParameters);
                SqlDataAdapter rdr = new SqlDataAdapter(cmd);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }
        #endregion

        #region Scaler
        /// <summary>
        /// 返回第一行第一列数据
        /// </summary>
        /// <param name="sqlText">SQL</param> 
        /// <returns>Convert.To{Type}</returns>
        public object ExecuteScalar(string sqlText, params SqlParameter[] cmdParameters)
        {
            return ExecuteScalar(CommandType.Text, sqlText, cmdParameters);
        }
        /// <summary>
        /// 返回第一行第一列数据
        /// </summary> 
        /// <param name="connString">连接字符串</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">存储过程名orSQL</param>
        /// <param name="cmdParameters">oracle 参数</param>
        /// <returns>Convert.To{Type}</returns>
        public object ExecuteScalar(CommandType cmdType, string cmdText,
            params SqlParameter[] cmdParameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParameters);

                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }


        ///	<summary>
        /// 返回第一行第一列数据
        ///	</summary>
        /// <param name="sqlText">SQL</param> 
        /// <returns>Convert.To{Type}</returns>
        public object ExecuteScalar(SqlTransaction myTrans, string sqlText,
            params SqlParameter[] cmdParameters)
        {
            return ExecuteScalar(myTrans, CommandType.Text, sqlText, cmdParameters);
        }

        ///	<summary>
        /// 返回第一行第一列数据
        ///	</summary>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">存储过程名orSQL</param>
        /// <param name="cmdParameters">oracle 参数</param>
        /// <returns>Convert.To{Type}</returns>
        public object ExecuteScalar(SqlTransaction myTrans, CommandType cmdType,
            string cmdText, params SqlParameter[] cmdParameters)
        {
            if (myTrans == null)
                throw new ArgumentNullException("transaction");
            if (myTrans != null && myTrans.Connection == null)
                throw new ArgumentException("The transaction was rollbacked	or commited, please provide an open transaction.", "transaction");

            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, myTrans.Connection, myTrans, cmdType, cmdText, cmdParameters);

            object retval = cmd.ExecuteScalar();

            cmd.Parameters.Clear();
            return retval;
        }
        #endregion

        #region Scaler -- Int
        /// <summary>
        /// 返回第一行第一列int数据
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="sqlText">CommandType.Text</param>
        /// <param name="cmdParameters"></param>
        /// <returns></returns>
        public int ExecuteInt(string sqlText, params SqlParameter[] cmdParameters)
        {
            return Convert.ToInt32(ExecuteScalar(sqlText, cmdParameters));
        }


        /// <summary>
        /// 返回第一行第一列int数据
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="sqlText">CommandType.Text</param>
        /// <param name="cmdParameters"></param>
        /// <returns></returns>
        public int ExecuteInt(SqlTransaction myTrans, string sqlText,
            params SqlParameter[] cmdParameters)
        {
            return Convert.ToInt32(ExecuteScalar(myTrans, CommandType.Text, sqlText, cmdParameters));
        }
        #endregion

        #region DataTable
        public DataTable ExecuteTable(string cmdText, params SqlParameter[] cmdParameters)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                OpenConnection(conn);
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, cmdParameters);
                Stopwatch sw = new Stopwatch();
                sw.Start();
                DataTable dt = new DataTable();
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch(SqlException ex)
                {
                    throw ex;
                }
                catch(Exception)
                {
                    SqlDataAdapter da = ExecuteAdapter(CommandType.Text, cmdText, cmdParameters);
                    da.Fill(dt);
                }
                conn.Close();
                cmd.Parameters.Clear();
                sw.Stop();
                if (sw.ElapsedMilliseconds > 3000)
                {
                }
                return dt;
            }
        }
        static int reconnect = 0;
        private static void OpenConnection(SqlConnection conn)
        {
            try {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
            }catch(SqlException)
            {
                SqlConnection.ClearAllPools();
                if (reconnect < 2)
                {
                    OpenConnection(conn);
                    reconnect++;
                }
                else
                {
                    reconnect = 0;
                }
            }
        }

        public DataTable ExecuteTable(CommandType cmdType, string cmdText,
            params SqlParameter[] cmdParameters)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                OpenConnection(conn);
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParameters);
                Stopwatch sw = new Stopwatch();
                sw.Start();
                DataTable dt = new DataTable();
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch (Exception)
                {
                    SqlDataAdapter da = ExecuteAdapter(cmdType, cmdText, cmdParameters);
                    da.Fill(dt);
                }
                conn.Close();
                cmd.Parameters.Clear();
                sw.Stop();
                if (sw.ElapsedMilliseconds > 3000)
                {
                }
                return dt;
            }
        }

        public DataTable ExecuteTable(CommandType cmdType, string cmdText, SqlCommand cmd)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                OpenConnection(conn);
                cmd.Connection = conn;
                cmd.CommandText = cmdText;
                cmd.CommandType = cmdType;
                DataTable dt = new DataTable();
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch (Exception)
                {   
                    List<SqlParameter> cmdparams = new List<SqlParameter>();
                    var penumer = cmd.Parameters.GetEnumerator();
                    while (penumer.MoveNext())
                    {
                        cmdparams.Add(penumer.Current as SqlParameter);
                    }
                    SqlDataAdapter da = ExecuteAdapter(cmdType, cmdText, cmdparams.ToArray());
                    da.Fill(dt);
                }
                conn.Close();
                sw.Stop();
                if (sw.ElapsedMilliseconds > 3000)
                {
                }
                return dt;
            }
        }


        public DataTable ExecuteTable(SqlTransaction myTrans, CommandType cmdType,
            string cmdText, params SqlParameter[] cmdParameters)
        {
            if (myTrans == null)
                throw new ArgumentNullException("transaction");
            if (myTrans != null && myTrans.Connection == null)
                throw new ArgumentException("The transaction was rollbacked	or commited, please	provide	an open	transaction.", "transaction");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, myTrans.Connection, myTrans,
                cmdType, cmdText, cmdParameters);
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception)
            {
                SqlDataAdapter da = ExecuteAdapter(cmdType, cmdText, cmdParameters);
                da.Fill(dt);
            }

            sw.Stop();
            if (sw.ElapsedMilliseconds > 3000)
            {
            }
            cmd.Parameters.Clear();
            return dt;
        }

        #endregion

        #region DataSet
        /// <summary>
        /// 未测试
        /// </summary>
        public DataSet ExecuteDataSet(string cmdText, params SqlParameter[] cmdParameters)
        {
            return ExecuteDataSet(CommandType.Text, cmdText, cmdParameters);
        }
        /// <summary>
        /// 未测试
        /// </summary>
        public DataSet ExecuteDataSet(CommandType cmdType, string cmdText,
            params SqlParameter[] cmdParameters)
        {

            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParameters);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                try
                {
                    adapter.FillSchema(ds, SchemaType.Source);

                    // Fill data
                    adapter.Fill(ds);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    cmd.Parameters.Clear();
                    // Dispose SqlCommand
                    cmd.Dispose();
                }
                return ds;
            }
        }
        #endregion

        #region PrepareCommand
        private void PrepareCommand(SqlCommand cmd, SqlConnection conn,
            SqlTransaction trans, CommandType cmdType,
            string cmdText, SqlParameter[] cmdParms)
        {
            OpenConnection(conn);
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = cmdType;//cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                {
                    if ((parm.Direction == ParameterDirection.InputOutput || parm.Direction == ParameterDirection.Input) &&
                           (parm.Value == null))
                    {
                        parm.Value = DBNull.Value;
                    }
                    if (!cmd.Parameters.Contains(parm.ParameterName))
                        cmd.Parameters.Add(parm);
                }
                
            }

            //Utility.Log.Error(cmd.CommandText);
        }
        

        #endregion
    }
}