using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DTcms.DBUtility;

namespace DTcms.DAL
{
    /// <summary>
    /// 数据访问类:dt_dimensioninfo
    /// </summary>
    public partial class dt_dimensioninfo
    {
        public dt_dimensioninfo()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from dt_dimensioninfo");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(DTcms.Model.dt_dimensioninfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into dt_dimensioninfo(");
            strSql.Append("dimension,value,trailerval,warningval,updatetime,hid)");
            strSql.Append(" values (");
            strSql.Append("@dimension,@value,@trailerval,@warningval,@updatetime,@hid)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@dimension", SqlDbType.Int,4),
                    new SqlParameter("@value", SqlDbType.Decimal,9),
                    new SqlParameter("@trailerval", SqlDbType.Decimal,9),
                    new SqlParameter("@warningval", SqlDbType.Decimal,9),
                    new SqlParameter("@updatetime", SqlDbType.DateTime),
                    new SqlParameter("@hid", SqlDbType.UniqueIdentifier,16)};
            parameters[0].Value = model.dimension;
            parameters[1].Value = model.value;
            parameters[2].Value = model.trailerval;
            parameters[3].Value = model.warningval;
            parameters[4].Value = model.updatetime;
            parameters[5].Value = Guid.NewGuid();

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        /// 更新一条数据
        /// </summary>
        public bool Update(DTcms.Model.dt_dimensioninfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dt_dimensioninfo set ");
            strSql.Append("value=@value,");
            strSql.Append("trailerval=@trailerval,");
            strSql.Append("warningval=@warningval,");
            strSql.Append("hid=@hid");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@value", SqlDbType.Decimal,9),
                    new SqlParameter("@trailerval", SqlDbType.Decimal,9),
                    new SqlParameter("@warningval", SqlDbType.Decimal,9),
                    new SqlParameter("@hid", SqlDbType.UniqueIdentifier,16),
                    new SqlParameter("@dimension", SqlDbType.Int,4),
                    new SqlParameter("@updatetime", SqlDbType.DateTime),
                    new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.value;
            parameters[1].Value = model.trailerval;
            parameters[2].Value = model.warningval;
            parameters[3].Value = model.hid;
            parameters[4].Value = model.dimension;
            parameters[5].Value = model.updatetime;
            parameters[6].Value = model.id;

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
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from dt_dimensioninfo ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;

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
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from dt_dimensioninfo ");
            strSql.Append(" where id in (" + idlist + ")  ");
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
        public DTcms.Model.dt_dimensioninfo GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 dimension,value,trailerval,warningval,updatetime,hid,id from dt_dimensioninfo ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;

            DTcms.Model.dt_dimensioninfo model = new DTcms.Model.dt_dimensioninfo();
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
        public DTcms.Model.dt_dimensioninfo DataRowToModel(DataRow row)
        {
            DTcms.Model.dt_dimensioninfo model = new DTcms.Model.dt_dimensioninfo();
            if (row != null)
            {
                if (row["dimension"] != null && row["dimension"].ToString() != "")
                {
                    model.dimension = int.Parse(row["dimension"].ToString());
                }
                if (row["value"] != null && row["value"].ToString() != "")
                {
                    model.value = decimal.Parse(row["value"].ToString());
                }
                if (row["trailerval"] != null && row["trailerval"].ToString() != "")
                {
                    model.trailerval = decimal.Parse(row["trailerval"].ToString());
                }
                if (row["warningval"] != null && row["warningval"].ToString() != "")
                {
                    model.warningval = decimal.Parse(row["warningval"].ToString());
                }
                if (row["updatetime"] != null && row["updatetime"].ToString() != "")
                {
                    model.updatetime = DateTime.Parse(row["updatetime"].ToString());
                }
                if (row["hid"] != null && row["hid"].ToString() != "")
                {
                    model.hid = new Guid(row["hid"].ToString());
                }
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
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
            strSql.Append("select dimension,value,trailerval,warningval,updatetime,hid,id ");
            strSql.Append(" FROM dt_dimensioninfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetDataList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.dimension,a.value,a.trailerval,a.warningval,a.updatetime,a.hid,a.id ");
            strSql.Append(" FROM dt_dimensioninfo a inner join dt_historydata b on a.hid=b.id ");
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
            strSql.Append(" dimension,value,trailerval,warningval,updatetime,hid,id ");
            strSql.Append(" FROM dt_dimensioninfo ");
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
            strSql.Append("select count(1) FROM dt_dimensioninfo ");
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
                strSql.Append("order by T.id desc");
            }
            strSql.Append(")AS Row, T.*  from dt_dimensioninfo T ");
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
			parameters[0].Value = "dt_dimensioninfo";
			parameters[1].Value = "hid";
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
        /// 获取后显示在图表上用
        /// </summary>
        public DataSet GetListShowImg(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select value,trailerval,warningval,updatetime ");
            strSql.Append(" FROM dt_dimensioninfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion  ExtensionMethod
    }
}

