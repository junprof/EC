using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Data.SqlClient;
using DTcms.DBUtility;
using DTcms.Common;
using System.Collections.Generic;

namespace DTcms.DAL
{
    /// <summary>
    /// 数据访问类:dt_historydata
    /// </summary>
    public partial class dt_historydata
    {
        public dt_historydata()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(Guid id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from dt_historydata");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.UniqueIdentifier,16)          };
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(DTcms.Model.dt_historydata model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into dt_historydata(");
            strSql.Append("id,name,value,addtime,updatetime,type,checkcode,functioncode,datahead,item_id,online,trailerval,warningval)");
            strSql.Append(" values (");
            strSql.Append("@id,@name,@value,@addtime,@updatetime,@type,@checkcode,@functioncode,@datahead,@item_id,@online,@trailerval,@warningval)");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.UniqueIdentifier,16),
                    new SqlParameter("@name", SqlDbType.NVarChar,50),
                    new SqlParameter("@value", SqlDbType.VarChar,200),
                    new SqlParameter("@addtime", SqlDbType.DateTime),
                    new SqlParameter("@updatetime", SqlDbType.DateTime),
                    new SqlParameter("@type", SqlDbType.Int,4),
                    new SqlParameter("@checkcode", SqlDbType.VarChar,32),
                    new SqlParameter("@functioncode", SqlDbType.VarChar,32),
                    new SqlParameter("@datahead", SqlDbType.VarChar,64),
                    new SqlParameter("@item_id", SqlDbType.Int,4),
                    new SqlParameter("@online", SqlDbType.Bit,1),
                    new SqlParameter("@trailerval", SqlDbType.VarChar,500),
                    new SqlParameter("@warningval", SqlDbType.VarChar,500)};
            parameters[0].Value = Guid.NewGuid();
            parameters[1].Value = model.name;
            parameters[2].Value = model.value;
            parameters[3].Value = model.addtime;
            parameters[4].Value = model.updatetime;
            parameters[5].Value = model.type;
            parameters[6].Value = model.checkcode;
            parameters[7].Value = model.functioncode;
            parameters[8].Value = model.datahead;
            parameters[9].Value = model.item_id;
            parameters[10].Value = model.online;
            parameters[11].Value = model.trailerval;
            parameters[12].Value = model.warningval;

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
        public bool Update(DTcms.Model.dt_historydata model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dt_historydata set ");
            strSql.Append("name=@name,");
            strSql.Append("value=@value,");
            strSql.Append("updatetime=@updatetime,");
            strSql.Append("type=@type,");
            strSql.Append("checkcode=@checkcode,");
            strSql.Append("functioncode=@functioncode,");
            strSql.Append("datahead=@datahead,");
            strSql.Append("item_id=@item_id,");
            strSql.Append("online=@online,");
            strSql.Append("trailerval=@trailerval,");
            strSql.Append("warningval=@warningval");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@name", SqlDbType.NVarChar,50),
                    new SqlParameter("@value", SqlDbType.VarChar,200),
                    new SqlParameter("@updatetime", SqlDbType.DateTime),
                    new SqlParameter("@type", SqlDbType.Int,4),
                    new SqlParameter("@checkcode", SqlDbType.VarChar,32),
                    new SqlParameter("@functioncode", SqlDbType.VarChar,32),
                    new SqlParameter("@datahead", SqlDbType.VarChar,64),
                    new SqlParameter("@item_id", SqlDbType.Int,4),
                    new SqlParameter("@online", SqlDbType.Bit,1),
                    new SqlParameter("@trailerval", SqlDbType.VarChar,500),
                    new SqlParameter("@warningval", SqlDbType.VarChar,500),
                    new SqlParameter("@id", SqlDbType.UniqueIdentifier,16),
                    new SqlParameter("@addtime", SqlDbType.DateTime)};
            parameters[0].Value = model.name;
            parameters[1].Value = model.value;
            parameters[2].Value = model.updatetime;
            parameters[3].Value = model.type;
            parameters[4].Value = model.checkcode;
            parameters[5].Value = model.functioncode;
            parameters[6].Value = model.datahead;
            parameters[7].Value = model.item_id;
            parameters[8].Value = model.online;
            parameters[9].Value = model.trailerval;
            parameters[10].Value = model.warningval;
            parameters[11].Value = model.id;
            parameters[12].Value = model.addtime;

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
        public bool Delete(Guid id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from dt_historydata ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.UniqueIdentifier,16)          };
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
            strSql.Append("delete from dt_historydata ");
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
        public DTcms.Model.dt_historydata GetModel(Guid id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,name,value,addtime,updatetime,type,checkcode,functioncode,datahead,item_id,online,trailerval,warningval from dt_historydata ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.UniqueIdentifier,16)          };
            parameters[0].Value = id;

            DTcms.Model.dt_historydata model = new DTcms.Model.dt_historydata();
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
        public DTcms.Model.dt_historydata DataRowToModel(DataRow row)
        {
            DTcms.Model.dt_historydata model = new DTcms.Model.dt_historydata();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = new Guid(row["id"].ToString());
                }
                if (row["name"] != null)
                {
                    model.name = row["name"].ToString();
                }
                if (row["value"] != null)
                {
                    model.value = row["value"].ToString();
                }
                if (row["addtime"] != null && row["addtime"].ToString() != "")
                {
                    model.addtime = DateTime.Parse(row["addtime"].ToString());
                }
                if (row["updatetime"] != null && row["updatetime"].ToString() != "")
                {
                    model.updatetime = DateTime.Parse(row["updatetime"].ToString());
                }
                if (row["type"] != null && row["type"].ToString() != "")
                {
                    model.type = int.Parse(row["type"].ToString());
                }
                if (row["checkcode"] != null)
                {
                    model.checkcode = row["checkcode"].ToString();
                }
                if (row["functioncode"] != null)
                {
                    model.functioncode = row["functioncode"].ToString();
                }
                if (row["datahead"] != null)
                {
                    model.datahead = row["datahead"].ToString();
                }
                if (row["item_id"] != null && row["item_id"].ToString() != "")
                {
                    model.item_id = int.Parse(row["item_id"].ToString());
                }
                if (row["online"] != null && row["online"].ToString() != "")
                {
                    if ((row["online"].ToString() == "1") || (row["online"].ToString().ToLower() == "true"))
                    {
                        model.online = true;
                    }
                    else
                    {
                        model.online = false;
                    }
                }
                if (row["trailerval"] != null)
                {
                    model.trailerval = row["trailerval"].ToString();
                }
                if (row["warningval"] != null)
                {
                    model.warningval = row["warningval"].ToString();
                }
            }
            return model;
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
            strSql.Append(" id,name,value,addtime,updatetime,type,checkcode,functioncode,datahead,item_id,online,trailerval,warningval ");
            strSql.Append(" FROM dt_historydata ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }
        public List<Model.dt_historydata> GetDataList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" id,name,value,addtime,updatetime,type,checkcode,functioncode,datahead,item_id,online,trailerval,warningval ");
            strSql.Append(" FROM dt_historydata ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            
            var ds= DbHelperSQL.Query(strSql.ToString());
            var dt = ds.Tables[0];
            return (from DataRow dr in dt.Rows select new Common.DBRowConvertor(dr).ConvertToEntity<Model.dt_historydata>()).ToList();
        }
        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM dt_historydata ");
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
            strSql.Append(")AS Row, T.*  from dt_historydata T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select h.*,i.user_id ");
            strSql.Append(" FROM dt_historydata h left join dt_item i on h.item_id=i.id ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
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
			parameters[0].Value = "dt_historydata";
			parameters[1].Value = "id";
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
            strSql.Append("select h.*,i.user_id FROM dt_historydata h left join dt_item i on i.id=h.item_id");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
            /// <summary>
            /// 获得数据列表
            /// </summary>
        public DataSet GetList_json(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select h.*,i.user_id ");
            strSql.Append(" FROM dt_historydata h left join dt_item i on h.item_id=i.id ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion  ExtensionMethod
    }
}

