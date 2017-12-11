using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL
{
    /// <summary>
    /// 数据访问类:管理员
    /// </summary>
    public partial class manager
    {
        private string databaseprefix; //数据库表名前缀
        public manager(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        #region 基本方法=============================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "manager");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 查询用户名是否存在
        /// </summary>
        public bool Exists(string user_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "manager");
            strSql.Append(" where user_name=@user_name ");
            SqlParameter[] parameters = {
					new SqlParameter("@user_name", SqlDbType.NVarChar,100)};
            parameters[0].Value = user_name;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 根据用户名取得Salt
        /// </summary>
        public string GetSalt(string user_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 salt from " + databaseprefix + "manager");
            strSql.Append(" where user_name=@user_name");
            SqlParameter[] parameters = {
                    new SqlParameter("@user_name", SqlDbType.NVarChar,100)};
            parameters[0].Value = user_name;
            string salt = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString(), parameters));
            if (string.IsNullOrEmpty(salt))
            {
                return "";
            }
            return salt;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.manager model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "manager(");
            strSql.Append("role_id,role_type,user_name,password,salt,real_name,telephone,email,is_lock,add_time,unitid,avatar)");
            strSql.Append(" values (");
            strSql.Append("@role_id,@role_type,@user_name,@password,@salt,@real_name,@telephone,@email,@is_lock,@add_time,@unitid,@avatar)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@role_id", SqlDbType.Int,4),
					new SqlParameter("@role_type", SqlDbType.Int,4),
					new SqlParameter("@user_name", SqlDbType.NVarChar,100),
					new SqlParameter("@password", SqlDbType.NVarChar,100),
					new SqlParameter("@salt", SqlDbType.NVarChar,20),
					new SqlParameter("@real_name", SqlDbType.NVarChar,50),
					new SqlParameter("@telephone", SqlDbType.NVarChar,35),
					new SqlParameter("@email", SqlDbType.NVarChar,30),
					new SqlParameter("@is_lock", SqlDbType.Int,4),
					new SqlParameter("@add_time", SqlDbType.DateTime),
                    new SqlParameter("@unitid", SqlDbType.NVarChar,50),
                    new SqlParameter("@avatar", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.role_id;
            parameters[1].Value = model.role_type;
            parameters[2].Value = model.user_name;
            parameters[3].Value = model.password;
            parameters[4].Value = model.salt;
            parameters[5].Value = model.real_name;
            parameters[6].Value = model.telephone;
            parameters[7].Value = model.email;
            parameters[8].Value = model.is_lock;
            parameters[9].Value = model.add_time;
            parameters[10].Value = model.UNITID;
            parameters[11].Value = model.avatar;

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
        public bool Update(Model.manager model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "manager set ");
            strSql.Append("role_id=@role_id,");
            strSql.Append("role_type=@role_type,");
            strSql.Append("user_name=@user_name,");
            strSql.Append("password=@password,");
            strSql.Append("real_name=@real_name,");
            strSql.Append("telephone=@telephone,");
            strSql.Append("email=@email,");
            strSql.Append("is_lock=@is_lock,");
            strSql.Append("add_time=@add_time,");
            strSql.Append("unitid=@unitid,");
            strSql.Append("avatar=@avatar");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@role_id", SqlDbType.Int,4),
					new SqlParameter("@role_type", SqlDbType.Int,4),
					new SqlParameter("@user_name", SqlDbType.NVarChar,100),
					new SqlParameter("@password", SqlDbType.NVarChar,100),
					new SqlParameter("@real_name", SqlDbType.NVarChar,50),
					new SqlParameter("@telephone", SqlDbType.NVarChar,35),
					new SqlParameter("@email", SqlDbType.NVarChar,30),
					new SqlParameter("@is_lock", SqlDbType.Int,4),
					new SqlParameter("@add_time", SqlDbType.DateTime),
                    new SqlParameter("@unitid", SqlDbType.NVarChar,50),
                    new SqlParameter("@avatar", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.role_id;
            parameters[2].Value = model.role_type;
            parameters[3].Value = model.user_name;
            parameters[4].Value = model.password;
            parameters[5].Value = model.real_name;
            parameters[6].Value = model.telephone;
            parameters[7].Value = model.email;
            parameters[8].Value = model.is_lock;
            parameters[9].Value = model.add_time;
            parameters[10].Value = model.UNITID;
            parameters[11].Value = model.avatar;

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
            strSql.Append("delete from " + databaseprefix + "manager ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
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
        /// 得到一个对象实体
        /// </summary>
        public Model.manager GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 a.id,role_id,role_name as ROLENAME,a.role_type,user_name,password,salt,real_name,telephone,email,is_lock,add_time,UNITID,avatar from " + databaseprefix + "manager a LEFT JOIN " + databaseprefix + "manager_role b on a.role_id=b.id");
            strSql.Append(" where a.id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.manager model = new Model.manager();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                //{
                //    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                //}
                //if (ds.Tables[0].Rows[0]["role_id"].ToString() != "")
                //{
                //    model.role_id = int.Parse(ds.Tables[0].Rows[0]["role_id"].ToString());
                //}
                //if (ds.Tables[0].Rows[0]["role_type"].ToString() != "")
                //{
                //    model.role_type = int.Parse(ds.Tables[0].Rows[0]["role_type"].ToString());
                //}
                //model.user_name = ds.Tables[0].Rows[0]["user_name"].ToString();
                //model.password = ds.Tables[0].Rows[0]["password"].ToString();
                //model.salt = ds.Tables[0].Rows[0]["salt"].ToString();
                //model.real_name = ds.Tables[0].Rows[0]["real_name"].ToString();
                //model.telephone = ds.Tables[0].Rows[0]["telephone"].ToString();
                //model.email = ds.Tables[0].Rows[0]["email"].ToString();
                //if (ds.Tables[0].Rows[0]["is_lock"].ToString() != "")
                //{
                //    model.is_lock = int.Parse(ds.Tables[0].Rows[0]["is_lock"].ToString());
                //}
                //if (ds.Tables[0].Rows[0]["add_time"].ToString() != "")
                //{
                //    model.add_time = DateTime.Parse(ds.Tables[0].Rows[0]["add_time"].ToString());
                //}
                //model.UNITID = ds.Tables[0].Rows[0]["UNITID"].ToString();
                model = new DBRowConvertor(ds.Tables[0].Rows[0]).ConvertToEntity<Model.manager>();
                if (!string.IsNullOrEmpty(model.UNITID))
                {
                    model.UNITNAME = Model.dt_units.GetUnit(model.UNITID)?.UNITNAME;
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        public Model.manager GetModel(string user_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id from " + databaseprefix + "manager");
            strSql.Append(" where user_name=@user_name and is_lock=0");
            SqlParameter[] parameters = {
                    new SqlParameter("@user_name", SqlDbType.NVarChar,100)};
            parameters[0].Value = user_name;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj != null)
            {
                return GetModel(Convert.ToInt32(obj));
            }
            return null;
        }
        /// <summary>
        /// 根据用户名密码返回一个实体
        /// </summary>
        public Model.manager GetModel(string user_name, string password)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id from " + databaseprefix + "manager");
            strSql.Append(" where user_name=@user_name and password=@password and is_lock=0");
            SqlParameter[] parameters = {
					new SqlParameter("@user_name", SqlDbType.NVarChar,100),
                    new SqlParameter("@password", SqlDbType.NVarChar,100)};
            parameters[0].Value = user_name;
            parameters[1].Value = password;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj != null)
            {
                return GetModel(Convert.ToInt32(obj));
            }
            return null;
        }
        public bool ChangePwd(string username,string password)
        {
            string sql = "UPDATE DT_MANAGER SET PASSWORD=@PASSWORD WHERE USER_NAME=@USERNAME";
            return MSSQLDbHelper.Instance.ExecuteNonQuery(sql, new SqlParameter("@PASSWORD", password), new SqlParameter("@USERNAME", username))>0;
        }
        public bool SetAvatar(string uid,string avatar)
        {
            string sql = "UPDATE DT_MANAGER SET AVATAR=@AVATAR WHERE ID=@ID";
            return MSSQLDbHelper.Instance.ExecuteNonQuery(sql, new SqlParameter("@AVATAR", avatar), new SqlParameter("@ID", uid)) > 0;
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
            strSql.Append(" id,role_id,role_type,user_name,password,salt,real_name,telephone,email,is_lock,add_time,unitid,avatar ");
            strSql.Append(" FROM " + databaseprefix + "manager ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + databaseprefix + "manager");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
        /// <summary>
        /// 根据微信号获取用户
        /// </summary>
        /// <param name="weichatid"></param>
        /// <returns></returns>
        public Model.manager GetModelByWeichatId(string weichatid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.*,1 isbind from " + databaseprefix + "manager a inner join " + databaseprefix + "manager_bind b on a.id = b.userid");
            strSql.Append(" where b.weichatid=@weichatid ");
            SqlParameter[] parameters = {
                    new SqlParameter("@weichatid", SqlDbType.NVarChar,100)};
            parameters[0].Value = weichatid;

            var ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
                return (new Common.DBRowConvertor(ds.Tables[0].Rows[0]).ConvertToEntity<Model.manager>());
            else
                return null;
        }
        public bool DeleteWeichatBind(int uid,string weichatid)
        {
            string sql = "DELETE " + databaseprefix + "manager_bind WHERE WEICHATID = @weichatid AND USERid = @id";
            SqlParameter[] parameters = {
                    new SqlParameter("@weichatid", SqlDbType.NVarChar,100),
                    new SqlParameter("@id", SqlDbType.Int)};
            parameters[0].Value = weichatid;
            parameters[1].Value = uid;
            int rows = DbHelperSQL.ExecuteSql(sql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool SetWeichatid(int uid ,string weichatid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("merge into " + databaseprefix + "manager_bind AS T USING (select @userid USERID,@weichatid WEICHATID ) AS S ON T.USERID=S.USERID AND T.WEICHATID=S.WEICHATID WHEN NOT MATCHED THEN INSERT VALUES(S.USERID,S.WEICHATID);");
            SqlParameter[] parameters = {
                    new SqlParameter("@userid", SqlDbType.Int),
                    new SqlParameter("@weichatid", SqlDbType.NVarChar,100)};
            parameters[0].Value = uid;
            parameters[1].Value = weichatid;
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

        #endregion
    }
}