using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL
{
    /// <summary>
    /// 数据访问类:dt_area_code
    /// </summary>
    public partial class dt_area_code
    {
        public dt_area_code()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from dt_area_code");
            strSql.Append(" where code=@code ");
            SqlParameter[] parameters = {
                    new SqlParameter("@code", SqlDbType.NVarChar,20)            };
            parameters[0].Value = code;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(DTcms.Model.dt_area_code model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into dt_area_code(");
            strSql.Append("code,name,name_short,parent_code,lev)");
            strSql.Append(" values (");
            strSql.Append("@code,@name,@name_short,@parent_code,@lev)");
            SqlParameter[] parameters = {
                    new SqlParameter("@code", SqlDbType.NVarChar,20),
                    new SqlParameter("@name", SqlDbType.NVarChar,20),
                    new SqlParameter("@name_short", SqlDbType.NVarChar,20),
                    new SqlParameter("@parent_code", SqlDbType.NVarChar,20),
                    new SqlParameter("@lev", SqlDbType.Int,4)};
            parameters[0].Value = model.code;
            parameters[1].Value = model.name;
            parameters[2].Value = model.name_short;
            parameters[3].Value = model.parent_code;
            parameters[4].Value = model.lev;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DTcms.Model.dt_area_code model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dt_area_code set ");
            strSql.Append("name=@name,");
            strSql.Append("name_short=@name_short,");
            strSql.Append("parent_code=@parent_code,");
            strSql.Append("lev=@lev");
            strSql.Append(" where code=@code ");
            SqlParameter[] parameters = {
                    new SqlParameter("@name", SqlDbType.NVarChar,20),
                    new SqlParameter("@name_short", SqlDbType.NVarChar,20),
                    new SqlParameter("@parent_code", SqlDbType.NVarChar,20),
                    new SqlParameter("@lev", SqlDbType.Int,4),
                    new SqlParameter("@code", SqlDbType.NVarChar,20)};
            parameters[0].Value = model.name;
            parameters[1].Value = model.name_short;
            parameters[2].Value = model.parent_code;
            parameters[3].Value = model.lev;
            parameters[4].Value = model.code;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string code)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from dt_area_code ");
            strSql.Append(" where code=@code ");
            SqlParameter[] parameters = {
                    new SqlParameter("@code", SqlDbType.NVarChar,20)            };
            parameters[0].Value = code;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string codelist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from dt_area_code ");
            strSql.Append(" where code in (" + codelist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DTcms.Model.dt_area_code GetModel(string code)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 code,name,name_short,parent_code,lev from dt_area_code ");
            strSql.Append(" where code=@code ");
            SqlParameter[] parameters = {
                    new SqlParameter("@code", SqlDbType.NVarChar,20)            };
            parameters[0].Value = code;

            DTcms.Model.dt_area_code model = new DTcms.Model.dt_area_code();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DTcms.Model.dt_area_code DataRowToModel(DataRow row)
        {
            DTcms.Model.dt_area_code model = new DTcms.Model.dt_area_code();
            if (row != null)
            {
                if (row["code"] != null)
                {
                    model.code = row["code"].ToString();
                }
                if (row["name"] != null)
                {
                    model.name = row["name"].ToString();
                }
                if (row["name_short"] != null)
                {
                    model.name_short = row["name_short"].ToString();
                }
                if (row["parent_code"] != null)
                {
                    model.parent_code = row["parent_code"].ToString();
                }
                if (row["lev"] != null && row["lev"].ToString() != "")
                {
                    model.lev = int.Parse(row["lev"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select code,name,name_short,parent_code,lev ");
            strSql.Append(" FROM dt_area_code ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" code,name,name_short,parent_code,lev ");
            strSql.Append(" FROM dt_area_code ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM dt_area_code ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.code desc");
            }
            strSql.Append(")AS Row, T.*  from dt_area_code T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "dt_area_code";
			parameters[1].Value = "code";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

        #endregion  BasicMethod
        #region  ExtensionMethod
        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM dt_area_code");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
        #endregion  ExtensionMethod
    }
}

