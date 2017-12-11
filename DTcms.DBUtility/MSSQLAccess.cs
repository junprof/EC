/**********************************************************************************
 * File name: ODBSystem.cs
 * Author: Allen Wong  Version: 1.0.0   Date: 2017/3/16
 * Desc:  Data access layer assistance
 *    
 * ================================================================================
 * Change History
 * ================================================================================
 *  		Date:		Author:     Type:   	Description:  
 *  		2017/3/16	Allen Wong	[NEW]       
 *  	 
 * ================================================================================
**********************************************************************************/

using DTcms.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace DTcms.DBUtility
{
    public class Columns
    {
        public string COLUMN_NAME { get; set; }
        public string DATA_TYPE { get; set; }
        public string DATA_LENGTH { get; set; }
    }

    public class DataColumnAttribute : Attribute
    {
        
    }
    /// <summary>
    /// MSSQL 数据源层
    ///     To be updated...
    /// </summary>
    public class MSSQLAccess
    {

        static string[] DataType = new string[] { "String", "Byte", "Boolean", "Int16", "Int32", "Int64", "Double", "DateTime", "Decimal", "Float","Guid" };
        /// <summary>
        /// 根据数据库表名获取列集合
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="tablename">表名</param>
        /// <returns></returns>
        public static List<Columns> GetColumns(string connectionString, string tablename)
        {
            List<Columns> columns = new List<Columns>();
            string sql = "SELECT COLUMN_NAME,DATA_TYPE,DATA_LENGTH FROM USER_TAB_COLUMNS WHERE TABLE_NAME = :TABLENAME ORDER BY COLUMN_ID";
            var tb = new MSSQLDbHelper(connectionString).ExecuteTable(sql, new SqlParameter("TABLENAME", tablename));
            if (tb.Rows.Count > 0)
            {
                columns= (from DataRow dr in tb.Rows select new DBRowConvertor(dr).ConvertToEntity<Columns>()).ToList(); 
            }
            return columns;
        }
        /// <summary>
        /// 根据数据库表名获取列集合
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <returns></returns>
        public static List<Columns> GetColumns(string tablename)
        {
            List<Columns> columns = new List<Columns>();
            string sql = "SELECT COLUMN_NAME,DATA_TYPE,DATA_LENGTH FROM USER_TAB_COLUMNS WHERE TABLE_NAME = :TABLENAME ORDER BY COLUMN_ID";
            var tb =MSSQLDbHelper.Instance.ExecuteTable(sql, new SqlParameter("TABLENAME", tablename));
            if (tb.Rows.Count > 0)
            {
                columns = (from DataRow dr in tb.Rows select new DBRowConvertor(dr).ConvertToEntity<Columns>()).ToList();
            }
            return columns;
        }
        /// <summary>
        /// SELECT SQL
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="wheresql">where条件</param>
        /// <returns></returns>
        public static string GetSelectSQL( string tablename, string wheresql = "")
        {
            List<Columns> columns = GetColumns(tablename);
            string sql = $"SELECT {(columns.Count == 0 ? "1" : string.Join(",", columns.Select(p => p.COLUMN_NAME)))} FROM {tablename} ";
            if (!string.IsNullOrEmpty(wheresql))
                sql += "WHERE " + wheresql;
            return sql;
        }
        /// <summary>
        /// SELECT SQL
        /// </summary>
        /// <param name="columns">列</param>
        /// <param name="tablename">表名</param>
        /// <param name="wheresql">where条件</param>
        /// <returns></returns>
        public static string GetSelectSQL(List<Columns> columns, string tablename, string wheresql = "")
        {
            string sql = $"SELECT {(columns.Count==0?"1": string.Join(",", columns.Select(p => p.COLUMN_NAME)))} FROM {tablename} ";
            if (!string.IsNullOrEmpty(wheresql))
                sql += "WHERE " + wheresql;
            return sql;
        }
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <typeparam name="T">指定类型</typeparam>
        /// <param name="tablename">表名</param>
        /// <param name="wheresql">where条件（直接拼接条件）</param>
        /// <returns></returns>
        public static List<T> GetData<T>(string tablename,string wheresql="") where T:new ()
        {
            var dt = MSSQLDbHelper.Instance.ExecuteTable(GetSelectSQL(tablename, wheresql));
            return (from DataRow dr in dt.Rows select new DBRowConvertor(dr).ConvertToEntity<T>()).ToList();
        }
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <typeparam name="T">指定类型</typeparam>
        /// <param name="columns">指定字段</param>
        /// <param name="tablename">表名</param>
        /// <param name="wheresql">where条件（直接拼接条件）</param>
        /// <returns></returns>
        public static List<T> GetData<T>(List<Columns> columns,string tablename, string wheresql = "") where T : new()
        {
            if (columns.Count == 0) { return null; }
            var dt = MSSQLDbHelper.Instance.ExecuteTable(GetSelectSQL(columns,tablename, wheresql));
            return (from DataRow dr in dt.Rows select new DBRowConvertor(dr).ConvertToEntity<T>()).ToList();
        }
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <typeparam name="T">指定类型</typeparam>
        /// <param name="columns">指定字段</param>
        /// <param name="tablename">表名</param>
        /// <param name="wheresql">where条件（使用Oracle参数化）</param>
        /// <param name="cmdParameters">参数</param>
        /// <returns></returns>
        public static List<T> GetData<T>(List<Columns> columns, string tablename, string wheresql = "", params SqlParameter[] cmdParameters) where T : new()
        {
            if (columns.Count == 0) { return null; }
            var dt = MSSQLDbHelper.Instance.ExecuteTable(GetSelectSQL(columns, tablename, wheresql),cmdParameters);
            return (from DataRow dr in dt.Rows select new DBRowConvertor(dr).ConvertToEntity<T>()).ToList();
        }
        /// <summary>
        /// 根据实体类获取字段数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tablename">表名</param>
        /// <param name="wheresql">简易条件</param>
        /// <returns></returns>
        public static List<T> GetData_EntityColumns<T>(string tablename, string wheresql = "",SqlTransaction myTrans=null) where T : new()
        {
            List<Columns> columns = GetColumnsByEntity<T>();
            DataTable dt = null;
            if(myTrans==null)
             dt = MSSQLDbHelper.Instance.ExecuteTable(GetSelectSQL(columns,tablename, wheresql));
            else
                dt = MSSQLDbHelper.Instance.ExecuteTable(myTrans, CommandType.Text, GetSelectSQL(columns, tablename, wheresql));
            return (from DataRow dr in dt.Rows select new DBRowConvertor(dr).ConvertToEntity<T>()).ToList();
        }
        /// <summary>
        /// 根据实体类获取字段数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tablename">表名</param>
        /// <param name="wheresql">条件</param>
        /// <param name="cmdParameters">参数</param>
        /// <returns></returns>
        public static List<T> GetData_EntityColumns<T>(string tablename, string wheresql = "", params SqlParameter[] cmdParameters) where T : new()
        {
            List<Columns> columns = GetColumnsByEntity<T>();
            var dt = MSSQLDbHelper.Instance.ExecuteTable(GetSelectSQL(columns, tablename, wheresql),cmdParameters);
            return (from DataRow dr in dt.Rows select new DBRowConvertor(dr).ConvertToEntity<T>()).ToList();
        }
        /// <summary>
        /// 获取Insert SQL
        /// </summary>
        /// <param name="columns">自定义列</param>
        /// <param name="tablename">表名</param>
        /// <returns>插入sql语句</returns>
        public static string GetInsertSQL(List<Columns> columns, string tablename)
        {
            if (columns.Count == 0) { return ""; }
            return $"INSERT INTO {tablename}({string.Join(",", columns.Select(p => p.COLUMN_NAME))})VALUES({string.Join(",", columns.Select(p => "@" + p.COLUMN_NAME))})";
        }
        /// <summary>
        /// 根据表名获取插入语句
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="columns">输出列集合</param>
        /// <returns></returns>
        public static string GetInsertSQL(string tablename ,out List<Columns> columns)
        {
            columns = GetColumns(tablename);
            return $"INSERT INTO {tablename}({string.Join(",", columns.Select(p => p.COLUMN_NAME))})VALUES({string.Join(",", columns.Select(p => "@" + p.COLUMN_NAME))})";
        }

        public static bool InsertData<T>(string tablename, T item)
        {
            List<SqlParameter> paramlist = Params(item);
            return MSSQLDbHelper.Instance.ExecuteNonQuery(GetInsertSQL(GetColumnsByEntity<T>(), tablename), paramlist.ToArray()) > 0;
        }
        /// <summary>
        /// 仅对有标记特性的列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tablename"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool InsertData_AttrCols<T>(string tablename, T item)
        {
            List<SqlParameter> paramlist = Params(item);
            return MSSQLDbHelper.Instance.ExecuteNonQuery(GetInsertSQL(GetColumnsByEntityAttr<T>(), tablename), paramlist.ToArray()) > 0;
        }
        public static bool InsertData<T>(string tablename, T item,SqlTransaction myTrans)
        {
            List<SqlParameter> paramlist = Params(item);
            return MSSQLDbHelper.Instance.ExecuteNonQuery(myTrans,GetInsertSQL(GetColumnsByEntity<T>(), tablename), paramlist.ToArray()) > 0;
        }
        /// <summary>
        /// 获取Merge SQL
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="pkcolumn">合并对比字段,多个以逗号(,)分隔</param>
        /// <returns>merge sql</returns>
        public static string GetMergeSQL(string tablename, string pkcolumn)
        {
            List<Columns> columns = GetColumns(tablename);
            if (columns.Count == 0) { return ""; }
            string c1 = string.Empty;
            pkcolumn.Split(',').Any(p => { c1 += $"@{p} AS {p},"; return false; });
            c1 = c1.Remove(c1.Length - 1);
            string c2 = string.Empty;
            pkcolumn.Split(',').Any(p => { c2 += $"T1.{p} = T2.{p} AND "; return false; });
            c2 = c2.Remove(c2.Length - 5);
            string sql = $"MERGE INTO {tablename} T1 USING(SELECT {c1} ) T2 ON ({c2}) WHEN MATCHED THEN UPDATE SET {string.Join(",", columns.Where(p => !pkcolumn.Split(',').Contains(p.COLUMN_NAME)).Select(p => p.COLUMN_NAME + "= @" + p.COLUMN_NAME))} WHEN NOT MATCHED THEN INSERT ({string.Join(",", columns.Select(p => p.COLUMN_NAME))})VALUES({string.Join(",", columns.Select(p => "@" + p.COLUMN_NAME))});";
            return sql;
        }
        /// <summary>
        /// 获取Merge SQL
        /// </summary>
        /// <param name="columns">自定义列</param>
        /// <param name="tablename">表名</param>
        /// <param name="pkcolumn">合并对比字段,多个以逗号(,)分隔</param>
        /// <returns>merge sql</returns>
        public static string GetMergeSQL(List<Columns> columns, string tablename, string pkcolumn)
        {
            if (columns.Count == 0) { return ""; }
            string c1 = string.Empty;
            pkcolumn.Split(',').Any(p => { c1 += $"@{p} AS {p},"; return false; });
            c1 = c1.Remove(c1.Length - 1);
            string c2 = string.Empty;
            pkcolumn.Split(',').Any(p => { c2 += $"T1.{p} = T2.{p} AND "; return false; });
            c2 = c2.Remove(c2.Length - 5);
            string sql = $"MERGE INTO {tablename} T1 USING(SELECT {c1}) T2 ON ({c2}) WHEN MATCHED THEN UPDATE SET {string.Join(",", columns.Where(p => !pkcolumn.Split(',').Contains(p.COLUMN_NAME)).Select(p => p.COLUMN_NAME + "= @" + p.COLUMN_NAME))} WHEN NOT MATCHED THEN INSERT ({string.Join(",", columns.Select(p => p.COLUMN_NAME))})VALUES({string.Join(",", columns.Select(p => "@" + p.COLUMN_NAME))});";
            return sql;
        }
        /// <summary>
        /// 合并数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tablename">表名</param>
        /// <param name="pkcolumn">合并对比字段,多个以逗号(,)分隔</param>
        /// <param name="item">传入数据</param>
        /// <returns></returns>
        public static bool MergeData<T>(string tablename,string pkcolumn,T item)
        {
            List<SqlParameter> paramlist = Params(item);
            return MSSQLDbHelper.Instance.ExecuteNonQuery(GetMergeSQL(tablename, pkcolumn), paramlist.ToArray()) >0;
        }
        /// <summary>
        /// 合并数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columns">自定义列</param>
        /// <param name="tablename">表名</param>
        /// <param name="pkcolumn">合并对比字段,多个以逗号(,)分隔</param>
        /// <param name="item">传入数据</param>
        /// <returns></returns>
        public static bool MergeData<T>(List<Columns> columns,string tablename, string pkcolumn, T item)
        {
            if (columns.Count == 0) { return false; }
            List<SqlParameter> paramlist = Params(item, columns);
            return MSSQLDbHelper.Instance.ExecuteNonQuery(GetMergeSQL(columns,tablename, pkcolumn), paramlist.ToArray()) > 0;
        }
        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="columns">列</param>
        /// <param name="tablename">表</param>
        /// <returns></returns>
        public static string GetCreateSQL(List<Columns> columns, string tablename)
        {
            if (columns.Count == 0) { return ""; }
            string sql = $"CREATE TABLE {tablename}({string.Join(",", columns.Select(p => $"{p.COLUMN_NAME} {p.DATA_TYPE} {((p.DATA_TYPE == "VARCHAR2" || p.DATA_TYPE == "NVARCHAR2") ? "(" + p.DATA_LENGTH + ")" : "")}"))})";
            return sql;
        }
        /// <summary>
        /// 获得参数隐射
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">实体数据</param>
        /// <param name="columns">自定义列，当null时自动根据实体属性映射</param>
        /// <returns>参数集合</returns>
        public static List<SqlParameter> Params<T>(T item,List<Columns> columns=null)
        {
            List<SqlParameter> paramlist=new List<SqlParameter>();
            PropertyInfo[] pis = item.GetType().GetProperties();
            foreach (var pi in pis)
            {
                Type pt = pi.PropertyType;
                if (pt.IsGenericType && pt.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    pt = pt.GetGenericArguments()[0];
                }
                if (DataType.Contains(pt.Name))
                {
                    if (columns != null && !columns.Any(p => p.COLUMN_NAME.ToLower() == pi.Name.ToLower()))
                        continue;
                    paramlist.Add(new SqlParameter("@"+pi.Name,pi.GetValue(item, null)));
                }
            }
            return paramlist;
        }
        /// <summary>
        /// 集合对象传入 批量操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static List<SqlParameter> Params<T>(List<T> items, List<Columns> columns = null)
        {
            List<SqlParameter> paramlist = new List<SqlParameter>();
            var propertylist = EntityListSplite(items,columns);
            foreach (var dic in propertylist)
            {
                object value = dic.Value.Value.ToArray();
                SqlParameter p = new SqlParameter("@"+dic.Key, dic.Value.Key);
                p.Value = value;
                paramlist.Add(p);
            }
            return paramlist;
        }
        public static Dictionary<string, KeyValuePair<SqlDbType, List<object>>> EntityListSplite<T>(List<T> datalist)
        {
            Dictionary<string, KeyValuePair<SqlDbType, List<object>>> res = new Dictionary<string, KeyValuePair<SqlDbType, List<object>>>();

            foreach (var item in datalist)
            {
                PropertyInfo[] pis = item.GetType().GetProperties();
                foreach (var pi in pis)
                {
                    Type pt = pi.PropertyType;
                    if (pt.IsGenericType && pt.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        pt = pt.GetGenericArguments()[0];
                    }
                    if (DataType.Contains(pt.Name))
                    {
                        SqlDbType odbType = GetODbType(pt.Name);
                        if (res.Keys.Contains(pi.Name))
                        {
                            res[pi.Name].Value.Add(pi.GetValue(item, null));
                        }
                        else
                        {
                            res.Add(pi.Name, new KeyValuePair<SqlDbType, List<object>>(odbType, new List<object> { pi.GetValue(item, null) }));
                        }
                    }
                }
            }
            return res;
        }
        public static Dictionary<string, KeyValuePair<SqlDbType, List<object>>> EntityListSplite<T>(List<T> datalist, List<Columns> columns)
        {
            Dictionary<string, KeyValuePair<SqlDbType, List<object>>> res = new Dictionary<string, KeyValuePair<SqlDbType, List<object>>>();

            foreach (var item in datalist)
            {
                PropertyInfo[] pis = item.GetType().GetProperties();
                
                foreach(var col in columns)
                {
                    var pi = pis.FirstOrDefault(p => p.Name.ToLower() == col.COLUMN_NAME.ToLower());
                    if (pi == null) continue;
                    Type pt = pi.PropertyType;
                    if (pt.IsGenericType && pt.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        pt = pt.GetGenericArguments()[0];
                    }
                    if (DataType.Contains(pt.Name))
                    {
                        SqlDbType odbType = GetODbType(pt.Name);
                        if (res.Keys.Contains(pi.Name))
                        {
                            res[pi.Name].Value.Add(pi.GetValue(item, null));
                        }
                        else
                        {
                            res.Add(pi.Name, new KeyValuePair<SqlDbType, List<object>>(odbType, new List<object> { pi.GetValue(item, null) }));
                        }
                    }
                }
            }
            return res;
        }
        private static SqlDbType GetODbType(string typeName)
        {
            SqlDbType odbType = SqlDbType.VarChar;
            switch (typeName)
            {
                case "Byte":
                case "Boolean":
                    odbType = SqlDbType.Bit;
                    break;
                case "Int16":
                    odbType = SqlDbType.SmallInt;
                    break;
                case "Int32":
                    odbType = SqlDbType.Int;
                    break;
                case "Int64":
                    odbType = SqlDbType.BigInt;
                    break;
                case "Double":
                case "Float":
                case "Decimal":
                    odbType = SqlDbType.Decimal;
                    break;
                case "DateTime":
                    odbType = SqlDbType.Date;
                    break;
                case "Guid":
                    odbType = SqlDbType.UniqueIdentifier;
                    break;
            }
            return odbType;
        }
        /// <summary>
        /// 根据实体获得列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static List<Columns> GetColumnsByEntity<T>()
        {
            List<Columns> columns = new List<Columns>();
            PropertyInfo[] pis = typeof(T).GetProperties();
            foreach (var pi in pis)
            {
                Type pt = pi.PropertyType;
                if (pt.IsGenericType && pt.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    pt = pt.GetGenericArguments()[0];
                }
                if (DataType.Contains(pt.Name))
                {
                    columns.Add(new Columns { COLUMN_NAME=pi.Name,DATA_TYPE = pt.Name });
                }
            }
            return columns;
        }
        public static List<Columns> GetColumnsByEntityAttr<T>()
        {
            List<Columns> columns = new List<Columns>();
            PropertyInfo[] pis = typeof(T).GetProperties();
            foreach (var pi in pis)
            {
                Type pt = pi.PropertyType;
                var attrs = pi.GetCustomAttributes(typeof(DataColumnAttribute), false);
                if (attrs == null)
                    continue;
                if (pt.IsGenericType && pt.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    pt = pt.GetGenericArguments()[0];
                }
                if (DataType.Contains(pt.Name))
                {
                    columns.Add(new Columns { COLUMN_NAME = pi.Name });
                }
            }
            return columns;
        }
    }
}
