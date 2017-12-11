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
    /// 数据访问类:dt_msg
    /// </summary>
    public partial class dt_msg
    {
        public dt_msg()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(Guid hid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from dt_msg");
            strSql.Append(" where hid=@hid ");
            SqlParameter[] parameters = {
                    new SqlParameter("@hid", SqlDbType.UniqueIdentifier,16)         };
            parameters[0].Value = hid;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(DTcms.Model.dt_msg model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into dt_msg(");
            strSql.Append("hid,title,item_id,addtime,phone,user_id,state)");
            strSql.Append(" values (");
            strSql.Append("@hid,@title,@item_id,@addtime,@phone,@user_id,@state)");
            SqlParameter[] parameters = {
                    new SqlParameter("@hid", SqlDbType.UniqueIdentifier,16),
                    new SqlParameter("@title", SqlDbType.NVarChar,200),
                    new SqlParameter("@item_id", SqlDbType.Int,4),
                    new SqlParameter("@addtime", SqlDbType.DateTime),
                    new SqlParameter("@phone", SqlDbType.VarChar,200),
                    new SqlParameter("@user_id", SqlDbType.Int,4),
                    new SqlParameter("@state", SqlDbType.Bit,1)};
            parameters[0].Value = Guid.NewGuid();
            parameters[1].Value = model.title;
            parameters[2].Value = model.item_id;
            parameters[3].Value = model.addtime;
            parameters[4].Value = model.phone;
            parameters[5].Value = model.user_id;
            parameters[6].Value = model.state;

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
        public bool Update(DTcms.Model.dt_msg model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dt_msg set ");
            strSql.Append("title=@title,");
            strSql.Append("item_id=@item_id,");
            strSql.Append("addtime=@addtime,");
            strSql.Append("phone=@phone,");
            strSql.Append("user_id=@user_id,");
            strSql.Append("state=@state");
            strSql.Append(" where hid=@hid ");
            SqlParameter[] parameters = {
                    new SqlParameter("@title", SqlDbType.NVarChar,200),
                    new SqlParameter("@item_id", SqlDbType.Int,4),
                    new SqlParameter("@addtime", SqlDbType.DateTime),
                    new SqlParameter("@phone", SqlDbType.VarChar,200),
                    new SqlParameter("@user_id", SqlDbType.Int,4),
                    new SqlParameter("@state", SqlDbType.Bit,1),
                    new SqlParameter("@hid", SqlDbType.UniqueIdentifier,16)};
            parameters[0].Value = model.title;
            parameters[1].Value = model.item_id;
            parameters[2].Value = model.addtime;
            parameters[3].Value = model.phone;
            parameters[4].Value = model.user_id;
            parameters[5].Value = model.state;
            parameters[6].Value = model.hid;

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
        public bool Delete(Guid hid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from dt_msg ");
            strSql.Append(" where hid=@hid ");
            SqlParameter[] parameters = {
                    new SqlParameter("@hid", SqlDbType.UniqueIdentifier,16)         };
            parameters[0].Value = hid;

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
        public bool DeleteList(string hidlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from dt_msg ");
            strSql.Append(" where hid in (" + hidlist + ")  ");
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
        public DTcms.Model.dt_msg GetModel(Guid hid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 hid,title,item_id,addtime,phone,user_id,state,ISPROCESSED,REMARK from dt_msg ");
            strSql.Append(" where hid=@hid ");
            SqlParameter[] parameters = {
                    new SqlParameter("@hid", SqlDbType.UniqueIdentifier,16)         };
            parameters[0].Value = hid;

            DTcms.Model.dt_msg model = new DTcms.Model.dt_msg();
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
        public DTcms.Model.dt_msg DataRowToModel(DataRow row)
        {
            DTcms.Model.dt_msg model = new DTcms.Model.dt_msg();
            if (row != null)
            {
                if (row["hid"] != null && row["hid"].ToString() != "")
                {
                    model.hid = new Guid(row["hid"].ToString());
                }
                if (row["title"] != null)
                {
                    model.title = row["title"].ToString();
                }
                if (row["item_id"] != null && row["item_id"].ToString() != "")
                {
                    model.item_id = int.Parse(row["item_id"].ToString());
                }
                if (row["addtime"] != null && row["addtime"].ToString() != "")
                {
                    model.addtime = DateTime.Parse(row["addtime"].ToString());
                }
                if (row["phone"] != null)
                {
                    model.phone = row["phone"].ToString();
                }
                if (row["user_id"] != null && row["user_id"].ToString() != "")
                {
                    model.user_id = int.Parse(row["user_id"].ToString());
                }
                if (row["state"] != null && row["state"].ToString() != "")
                {
                    if ((row["state"].ToString() == "1") || (row["state"].ToString().ToLower() == "true"))
                    {
                        model.state = true;
                    }
                    else
                    {
                        model.state = false;
                    }
                }
                if (row["ISPROCESSED"] != null && row["ISPROCESSED"].ToString() != "")
                {
                    model.ISPROCESSED = int.Parse(row["ISPROCESSED"].ToString());
                }
                if (row["REMARK"] != null)
                {
                    model.REMARK = row["REMARK"].ToString();
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
            strSql.Append("select hid,title,item_id,addtime,phone,user_id,state,ISPROCESSED,REMARK ");
            strSql.Append(" FROM dt_msg ");
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
            strSql.Append(" hid,title,item_id,addtime,phone,user_id,state,ISPROCESSED,REMARK ");
            strSql.Append(" FROM dt_msg ");
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
            strSql.Append("select count(1) FROM dt_msg t");
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
        public List<Model.MsgExtend> GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
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
                strSql.Append("order by T.hid desc");
            }
            strSql.Append(")AS Row, T.*,i.name itemname,i.position  from dt_msg T left join DT_ITEM I on t.ITEM_ID = I.ID");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            var dt = MSSQLDbHelper.Instance.ExecuteTable(strSql.ToString());
            return (from DataRow dr in dt.Rows select new DBRowConvertor(dr).ConvertToEntity<Model.MsgExtend>()).ToList();
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
			parameters[0].Value = "dt_msg";
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
            strSql.Append("select m.*,i.name as equipmentname,i.onenetnum FROM dt_msg m left join dt_item i on m.item_id=i.id ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateState(bool state, Guid id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dt_msg set ");
            strSql.Append("state=@state");
            strSql.Append(" where hid=@hid");
            SqlParameter[] parameters = {
                    new SqlParameter("@state", SqlDbType.Bit,1),
                    new SqlParameter("@hid",SqlDbType.UniqueIdentifier,16)};
            parameters[0].Value = state;
            parameters[1].Value = id;

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
        public bool UpdateState2(Model.dt_msg md)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dt_msg set ");
            strSql.Append("ISPROCESSED=@ISPROCESSED,");
            strSql.Append("REMARK=@REMARK");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@ISPROCESSED", SqlDbType.Bit,1),
                    new SqlParameter("@REMARK", SqlDbType.NVarChar,1000),
                    new SqlParameter("@id",SqlDbType.NVarChar,36)};
            parameters[0].Value = md.ISPROCESSED;
            parameters[1].Value = md.REMARK;
            parameters[2].Value = md.id;

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
        public int userid { get; set; }
        public List<Model.MsgExtend> GetUnprocessMsg()
        {
            string sql = "SELECT M.*,I.NAME ITEMNAME,i.position FROM DT_MSG M ,DT_ITEM I WHERE M.ITEM_ID = I.ID AND ISNULL(M.ISPROCESSED,0) <> 1 and ISNULL(M.state,0) <> 1 AND M.USER_ID = @USERID ORDER BY ADDTIME DESC";
            var dt = MSSQLDbHelper.Instance.ExecuteTable(sql, new SqlParameter("@USERID",userid));
            return (from DataRow dr in dt.Rows select new DBRowConvertor(dr).ConvertToEntity<Model.MsgExtend>()).ToList();
        }
        public bool SetReaded(string msgid)
        {
            string sql = "UPDATE DT_MSG SET STATE =1 WHERE ID=@MSGID";
            return MSSQLDbHelper.Instance.ExecuteNonQuery(sql, new SqlParameter("@MSGID",msgid)) > 0;
        }
        public bool SetReaded(int user_id)
        {
            string sql = "UPDATE DT_MSG SET STATE =1 WHERE user_id = @user_id and state != 1";
            return MSSQLDbHelper.Instance.ExecuteNonQuery(sql, new SqlParameter("@user_id", user_id)) > 0;
        }
        #endregion  ExtensionMethod
    }
}

