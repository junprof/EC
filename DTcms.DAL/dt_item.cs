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
    /// 数据访问类:dt_item
    /// </summary>
    public partial class dt_item
    {
        public dt_item()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from dt_item");
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
        public int Add(DTcms.Model.dt_item model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into dt_item(");
            strSql.Append("name,remarks,addtime,position,user_id,area_code,addr,state,value,updatetime,online,onenetnum,warningAI,warningBI,warningCI,warningLI,warningOneTemperature,warningTwoTemperature,warningThreeTemperature,warningFourTemperature,trailerAI,trailerBI,trailerCI,trailerLI,trailerOneTemperature,trailerTwoTemperature,trailerThreeTemperature,trailerFourTemperature,trailerAV,trailerBV,trailerCV,warningAV,warningBV,warningCV,trailerAV2,trailerBV2,trailerCV2,warningAV2,warningBV2,warningCV2,isdel)");
            strSql.Append(" values (");
            strSql.Append("@name,@remarks,@addtime,@position,@user_id,@area_code,@addr,@state,@value,@updatetime,@online,@onenetnum,@warningAI,@warningBI,@warningCI,@warningLI,@warningOneTemperature,@warningTwoTemperature,@warningThreeTemperature,@warningFourTemperature,@trailerAI,@trailerBI,@trailerCI,@trailerLI,@trailerOneTemperature,@trailerTwoTemperature,@trailerThreeTemperature,@trailerFourTemperature,@trailerAV,@trailerBV,@trailerCV,@warningAV,@warningBV,@warningCV,@trailerAV2,@trailerBV2,@trailerCV2,@warningAV2,@warningBV2,@warningCV2,@isdel)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@name", SqlDbType.NVarChar,50),
                    new SqlParameter("@remarks", SqlDbType.NVarChar,200),
                    new SqlParameter("@addtime", SqlDbType.DateTime),
                    new SqlParameter("@position", SqlDbType.VarChar,32),
                    new SqlParameter("@user_id", SqlDbType.Int,4),
                    new SqlParameter("@area_code", SqlDbType.VarChar,20),
                    new SqlParameter("@addr", SqlDbType.NVarChar,50),
                    new SqlParameter("@state", SqlDbType.Int,4),
                    new SqlParameter("@value", SqlDbType.VarChar,200),
                    new SqlParameter("@updatetime", SqlDbType.DateTime),
                    new SqlParameter("@online", SqlDbType.Bit,1),
                    new SqlParameter("@onenetnum", SqlDbType.VarChar,64),
                    new SqlParameter("@warningAI", SqlDbType.Decimal,9),
                    new SqlParameter("@warningBI", SqlDbType.Decimal,9),
                    new SqlParameter("@warningCI", SqlDbType.Decimal,9),
                    new SqlParameter("@warningLI", SqlDbType.Decimal,9),
                    new SqlParameter("@warningOneTemperature", SqlDbType.Decimal,9),
                    new SqlParameter("@warningTwoTemperature", SqlDbType.Decimal,9),
                    new SqlParameter("@warningThreeTemperature", SqlDbType.Decimal,9),
                    new SqlParameter("@warningFourTemperature", SqlDbType.Decimal,9),
                    new SqlParameter("@trailerAI", SqlDbType.Decimal,9),
                    new SqlParameter("@trailerBI", SqlDbType.Decimal,9),
                    new SqlParameter("@trailerCI", SqlDbType.Decimal,9),
                    new SqlParameter("@trailerLI", SqlDbType.Decimal,9),
                    new SqlParameter("@trailerOneTemperature", SqlDbType.Decimal,9),
                    new SqlParameter("@trailerTwoTemperature", SqlDbType.Decimal,9),
                    new SqlParameter("@trailerThreeTemperature", SqlDbType.Decimal,9),
                    new SqlParameter("@trailerFourTemperature", SqlDbType.Decimal,9),
                    new SqlParameter("@trailerAV", SqlDbType.Decimal,9),
                    new SqlParameter("@trailerBV", SqlDbType.Decimal,9),
                    new SqlParameter("@trailerCV", SqlDbType.Decimal,9),
                    new SqlParameter("@warningAV", SqlDbType.Decimal,9),
                    new SqlParameter("@warningBV", SqlDbType.Decimal,9),
                    new SqlParameter("@warningCV", SqlDbType.Decimal,9),
                    new SqlParameter("@trailerAV2", SqlDbType.Decimal,9),
                    new SqlParameter("@trailerBV2", SqlDbType.Decimal,9),
                    new SqlParameter("@trailerCV2", SqlDbType.Decimal,9),
                    new SqlParameter("@warningAV2", SqlDbType.Decimal,9),
                    new SqlParameter("@warningBV2", SqlDbType.Decimal,9),
                    new SqlParameter("@warningCV2", SqlDbType.Decimal,9),
                    new SqlParameter("@isdel", SqlDbType.Bit,1)};
            parameters[0].Value = model.name;
            parameters[1].Value = model.remarks;
            parameters[2].Value = model.addtime;
            parameters[3].Value = model.position;
            parameters[4].Value = model.user_id;
            parameters[5].Value = model.area_code;
            parameters[6].Value = model.addr;
            parameters[7].Value = model.state;
            parameters[8].Value = model.value;
            parameters[9].Value = model.updatetime;
            parameters[10].Value = model.online;
            parameters[11].Value = model.onenetnum;
            parameters[12].Value = model.warningAI;
            parameters[13].Value = model.warningBI;
            parameters[14].Value = model.warningCI;
            parameters[15].Value = model.warningLI;
            parameters[16].Value = model.warningOneTemperature;
            parameters[17].Value = model.warningTwoTemperature;
            parameters[18].Value = model.warningThreeTemperature;
            parameters[19].Value = model.warningFourTemperature;
            parameters[20].Value = model.trailerAI;
            parameters[21].Value = model.trailerBI;
            parameters[22].Value = model.trailerCI;
            parameters[23].Value = model.trailerLI;
            parameters[24].Value = model.trailerOneTemperature;
            parameters[25].Value = model.trailerTwoTemperature;
            parameters[26].Value = model.trailerThreeTemperature;
            parameters[27].Value = model.trailerFourTemperature;
            parameters[28].Value = model.trailerAV;
            parameters[29].Value = model.trailerBV;
            parameters[30].Value = model.trailerCV;
            parameters[31].Value = model.warningAV;
            parameters[32].Value = model.warningBV;
            parameters[33].Value = model.warningCV;
            parameters[34].Value = model.trailerAV2;
            parameters[35].Value = model.trailerBV2;
            parameters[36].Value = model.trailerCV2;
            parameters[37].Value = model.warningAV2;
            parameters[38].Value = model.warningBV2;
            parameters[39].Value = model.warningCV2;
            parameters[40].Value = model.isdel;

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
        public bool Update(DTcms.Model.dt_item model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dt_item set ");
            strSql.Append("name=@name,");
            strSql.Append("remarks=@remarks,");
            strSql.Append("addtime=@addtime,");
            strSql.Append("position=@position,");
            strSql.Append("user_id=@user_id,");
            strSql.Append("area_code=@area_code,");
            strSql.Append("addr=@addr,");
            strSql.Append("state=@state,");
            strSql.Append("value=@value,");
            strSql.Append("updatetime=@updatetime,");
            strSql.Append("online=@online,");
            strSql.Append("onenetnum=@onenetnum,");
            strSql.Append("warningAI=@warningAI,");
            strSql.Append("warningBI=@warningBI,");
            strSql.Append("warningCI=@warningCI,");
            strSql.Append("warningLI=@warningLI,");
            strSql.Append("warningOneTemperature=@warningOneTemperature,");
            strSql.Append("warningTwoTemperature=@warningTwoTemperature,");
            strSql.Append("warningThreeTemperature=@warningThreeTemperature,");
            strSql.Append("warningFourTemperature=@warningFourTemperature,");
            strSql.Append("trailerAI=@trailerAI,");
            strSql.Append("trailerBI=@trailerBI,");
            strSql.Append("trailerCI=@trailerCI,");
            strSql.Append("trailerLI=@trailerLI,");
            strSql.Append("trailerOneTemperature=@trailerOneTemperature,");
            strSql.Append("trailerTwoTemperature=@trailerTwoTemperature,");
            strSql.Append("trailerThreeTemperature=@trailerThreeTemperature,");
            strSql.Append("trailerFourTemperature=@trailerFourTemperature,");
            strSql.Append("trailerAV=@trailerAV,");
            strSql.Append("trailerBV=@trailerBV,");
            strSql.Append("trailerCV=@trailerCV,");
            strSql.Append("warningAV=@warningAV,");
            strSql.Append("warningBV=@warningBV,");
            strSql.Append("warningCV=@warningCV,");
            strSql.Append("trailerAV2=@trailerAV2,");
            strSql.Append("trailerBV2=@trailerBV2,");
            strSql.Append("trailerCV2=@trailerCV2,");
            strSql.Append("warningAV2=@warningAV2,");
            strSql.Append("warningBV2=@warningBV2,");
            strSql.Append("warningCV2=@warningCV2,");
            strSql.Append("isdel=@isdel");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@name", SqlDbType.NVarChar,50),
                    new SqlParameter("@remarks", SqlDbType.NVarChar,200),
                    new SqlParameter("@addtime", SqlDbType.DateTime),
                    new SqlParameter("@position", SqlDbType.VarChar,32),
                    new SqlParameter("@user_id", SqlDbType.Int,4),
                    new SqlParameter("@area_code", SqlDbType.VarChar,20),
                    new SqlParameter("@addr", SqlDbType.NVarChar,50),
                    new SqlParameter("@state", SqlDbType.Int,4),
                    new SqlParameter("@value", SqlDbType.VarChar,200),
                    new SqlParameter("@updatetime", SqlDbType.DateTime),
                    new SqlParameter("@online", SqlDbType.Bit,1),
                    new SqlParameter("@onenetnum", SqlDbType.VarChar,64),
                    new SqlParameter("@warningAI", SqlDbType.Decimal,9),
                    new SqlParameter("@warningBI", SqlDbType.Decimal,9),
                    new SqlParameter("@warningCI", SqlDbType.Decimal,9),
                    new SqlParameter("@warningLI", SqlDbType.Decimal,9),
                    new SqlParameter("@warningOneTemperature", SqlDbType.Decimal,9),
                    new SqlParameter("@warningTwoTemperature", SqlDbType.Decimal,9),
                    new SqlParameter("@warningThreeTemperature", SqlDbType.Decimal,9),
                    new SqlParameter("@warningFourTemperature", SqlDbType.Decimal,9),
                    new SqlParameter("@trailerAI", SqlDbType.Decimal,9),
                    new SqlParameter("@trailerBI", SqlDbType.Decimal,9),
                    new SqlParameter("@trailerCI", SqlDbType.Decimal,9),
                    new SqlParameter("@trailerLI", SqlDbType.Decimal,9),
                    new SqlParameter("@trailerOneTemperature", SqlDbType.Decimal,9),
                    new SqlParameter("@trailerTwoTemperature", SqlDbType.Decimal,9),
                    new SqlParameter("@trailerThreeTemperature", SqlDbType.Decimal,9),
                    new SqlParameter("@trailerFourTemperature", SqlDbType.Decimal,9),
                    new SqlParameter("@trailerAV", SqlDbType.Decimal,9),
                    new SqlParameter("@trailerBV", SqlDbType.Decimal,9),
                    new SqlParameter("@trailerCV", SqlDbType.Decimal,9),
                    new SqlParameter("@warningAV", SqlDbType.Decimal,9),
                    new SqlParameter("@warningBV", SqlDbType.Decimal,9),
                    new SqlParameter("@warningCV", SqlDbType.Decimal,9),
                    new SqlParameter("@trailerAV2", SqlDbType.Decimal,9),
                    new SqlParameter("@trailerBV2", SqlDbType.Decimal,9),
                    new SqlParameter("@trailerCV2", SqlDbType.Decimal,9),
                    new SqlParameter("@warningAV2", SqlDbType.Decimal,9),
                    new SqlParameter("@warningBV2", SqlDbType.Decimal,9),
                    new SqlParameter("@warningCV2", SqlDbType.Decimal,9),
                    new SqlParameter("@isdel", SqlDbType.Bit,1),
                    new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.name;
            parameters[1].Value = model.remarks;
            parameters[2].Value = model.addtime;
            parameters[3].Value = model.position;
            parameters[4].Value = model.user_id;
            parameters[5].Value = model.area_code;
            parameters[6].Value = model.addr;
            parameters[7].Value = model.state;
            parameters[8].Value = model.value;
            parameters[9].Value = model.updatetime;
            parameters[10].Value = model.online;
            parameters[11].Value = model.onenetnum;
            parameters[12].Value = model.warningAI;
            parameters[13].Value = model.warningBI;
            parameters[14].Value = model.warningCI;
            parameters[15].Value = model.warningLI;
            parameters[16].Value = model.warningOneTemperature;
            parameters[17].Value = model.warningTwoTemperature;
            parameters[18].Value = model.warningThreeTemperature;
            parameters[19].Value = model.warningFourTemperature;
            parameters[20].Value = model.trailerAI;
            parameters[21].Value = model.trailerBI;
            parameters[22].Value = model.trailerCI;
            parameters[23].Value = model.trailerLI;
            parameters[24].Value = model.trailerOneTemperature;
            parameters[25].Value = model.trailerTwoTemperature;
            parameters[26].Value = model.trailerThreeTemperature;
            parameters[27].Value = model.trailerFourTemperature;
            parameters[28].Value = model.trailerAV;
            parameters[29].Value = model.trailerBV;
            parameters[30].Value = model.trailerCV;
            parameters[31].Value = model.warningAV;
            parameters[32].Value = model.warningBV;
            parameters[33].Value = model.warningCV;
            parameters[34].Value = model.trailerAV2;
            parameters[35].Value = model.trailerBV2;
            parameters[36].Value = model.trailerCV2;
            parameters[37].Value = model.warningAV2;
            parameters[38].Value = model.warningBV2;
            parameters[39].Value = model.warningCV2;
            parameters[40].Value = model.isdel;
            parameters[41].Value = model.id;

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
            strSql.Append("delete from dt_item ");
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
            strSql.Append("delete from dt_item ");
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
        public DTcms.Model.dt_item GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,name,remarks,addtime,position,user_id,area_code,addr,state,value,updatetime,online,onenetnum,warningAI,warningBI,warningCI,warningLI,warningOneTemperature,warningTwoTemperature,warningThreeTemperature,warningFourTemperature,trailerAI,trailerBI,trailerCI,trailerLI,trailerOneTemperature,trailerTwoTemperature,trailerThreeTemperature,trailerFourTemperature,trailerAV,trailerBV,trailerCV,warningAV,warningBV,warningCV,trailerAV2,trailerBV2,trailerCV2,warningAV2,warningBV2,warningCV2,isdel from dt_item ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;

            DTcms.Model.dt_item model = new DTcms.Model.dt_item();
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
        public DTcms.Model.dt_item DataRowToModel(DataRow row)
        {
            DTcms.Model.dt_item model = new DTcms.Model.dt_item();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["name"] != null)
                {
                    model.name = row["name"].ToString();
                }
                if (row["remarks"] != null)
                {
                    model.remarks = row["remarks"].ToString();
                }
                if (row["addtime"] != null && row["addtime"].ToString() != "")
                {
                    model.addtime = DateTime.Parse(row["addtime"].ToString());
                }
                if (row["position"] != null)
                {
                    model.position = row["position"].ToString();
                }
                if (row["user_id"] != null && row["user_id"].ToString() != "")
                {
                    model.user_id = int.Parse(row["user_id"].ToString());
                }
                if (row["area_code"] != null)
                {
                    model.area_code = row["area_code"].ToString();
                }
                if (row["addr"] != null)
                {
                    model.addr = row["addr"].ToString();
                }
                if (row["state"] != null && row["state"].ToString() != "")
                {
                    model.state = int.Parse(row["state"].ToString());
                }
                if (row["value"] != null)
                {
                    model.value = row["value"].ToString();
                }
                if (row["updatetime"] != null && row["updatetime"].ToString() != "")
                {
                    model.updatetime = DateTime.Parse(row["updatetime"].ToString());
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
                if (row["onenetnum"] != null)
                {
                    model.onenetnum = row["onenetnum"].ToString();
                }
                if (row["warningAI"] != null && row["warningAI"].ToString() != "")
                {
                    model.warningAI = double.Parse(row["warningAI"].ToString());
                }
                if (row["warningBI"] != null && row["warningBI"].ToString() != "")
                {
                    model.warningBI = double.Parse(row["warningBI"].ToString());
                }
                if (row["warningCI"] != null && row["warningCI"].ToString() != "")
                {
                    model.warningCI = double.Parse(row["warningCI"].ToString());
                }
                if (row["warningLI"] != null && row["warningLI"].ToString() != "")
                {
                    model.warningLI = double.Parse(row["warningLI"].ToString());
                }
                if (row["warningOneTemperature"] != null && row["warningOneTemperature"].ToString() != "")
                {
                    model.warningOneTemperature = double.Parse(row["warningOneTemperature"].ToString());
                }
                if (row["warningTwoTemperature"] != null && row["warningTwoTemperature"].ToString() != "")
                {
                    model.warningTwoTemperature = double.Parse(row["warningTwoTemperature"].ToString());
                }
                if (row["warningThreeTemperature"] != null && row["warningThreeTemperature"].ToString() != "")
                {
                    model.warningThreeTemperature = double.Parse(row["warningThreeTemperature"].ToString());
                }
                if (row["warningFourTemperature"] != null && row["warningFourTemperature"].ToString() != "")
                {
                    model.warningFourTemperature = double.Parse(row["warningFourTemperature"].ToString());
                }
                if (row["trailerAI"] != null && row["trailerAI"].ToString() != "")
                {
                    model.trailerAI = double.Parse(row["trailerAI"].ToString());
                }
                if (row["trailerBI"] != null && row["trailerBI"].ToString() != "")
                {
                    model.trailerBI = double.Parse(row["trailerBI"].ToString());
                }
                if (row["trailerCI"] != null && row["trailerCI"].ToString() != "")
                {
                    model.trailerCI = double.Parse(row["trailerCI"].ToString());
                }
                if (row["trailerLI"] != null && row["trailerLI"].ToString() != "")
                {
                    model.trailerLI = double.Parse(row["trailerLI"].ToString());
                }
                if (row["trailerOneTemperature"] != null && row["trailerOneTemperature"].ToString() != "")
                {
                    model.trailerOneTemperature = double.Parse(row["trailerOneTemperature"].ToString());
                }
                if (row["trailerTwoTemperature"] != null && row["trailerTwoTemperature"].ToString() != "")
                {
                    model.trailerTwoTemperature = double.Parse(row["trailerTwoTemperature"].ToString());
                }
                if (row["trailerThreeTemperature"] != null && row["trailerThreeTemperature"].ToString() != "")
                {
                    model.trailerThreeTemperature = double.Parse(row["trailerThreeTemperature"].ToString());
                }
                if (row["trailerFourTemperature"] != null && row["trailerFourTemperature"].ToString() != "")
                {
                    model.trailerFourTemperature = double.Parse(row["trailerFourTemperature"].ToString());
                }
                if (row["trailerAV"] != null && row["trailerAV"].ToString() != "")
                {
                    model.trailerAV = double.Parse(row["trailerAV"].ToString());
                }
                if (row["trailerBV"] != null && row["trailerBV"].ToString() != "")
                {
                    model.trailerBV = double.Parse(row["trailerBV"].ToString());
                }
                if (row["trailerCV"] != null && row["trailerCV"].ToString() != "")
                {
                    model.trailerCV = double.Parse(row["trailerCV"].ToString());
                }
                if (row["warningAV"] != null && row["warningAV"].ToString() != "")
                {
                    model.warningAV = double.Parse(row["warningAV"].ToString());
                }
                if (row["warningBV"] != null && row["warningBV"].ToString() != "")
                {
                    model.warningBV = double.Parse(row["warningBV"].ToString());
                }
                if (row["warningCV"] != null && row["warningCV"].ToString() != "")
                {
                    model.warningCV = double.Parse(row["warningCV"].ToString());
                }
                if (row["trailerAV2"] != null && row["trailerAV2"].ToString() != "")
                {
                    model.trailerAV2 = double.Parse(row["trailerAV2"].ToString());
                }
                if (row["trailerBV2"] != null && row["trailerBV2"].ToString() != "")
                {
                    model.trailerBV2 = double.Parse(row["trailerBV2"].ToString());
                }
                if (row["trailerCV2"] != null && row["trailerCV2"].ToString() != "")
                {
                    model.trailerCV2 = double.Parse(row["trailerCV2"].ToString());
                }
                if (row["warningAV2"] != null && row["warningAV2"].ToString() != "")
                {
                    model.warningAV2 = double.Parse(row["warningAV2"].ToString());
                }
                if (row["warningBV2"] != null && row["warningBV2"].ToString() != "")
                {
                    model.warningBV2 = double.Parse(row["warningBV2"].ToString());
                }
                if (row["warningCV2"] != null && row["warningCV2"].ToString() != "")
                {
                    model.warningCV2 = double.Parse(row["warningCV2"].ToString());
                }
                if (row["isdel"] != null && row["isdel"].ToString() != "")
                {
                    if ((row["isdel"].ToString() == "1") || (row["isdel"].ToString().ToLower() == "true"))
                    {
                        model.isdel = true;
                    }
                    else
                    {
                        model.isdel = false;
                    }
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
            strSql.Append("select id,name,remarks,addtime,position,user_id,area_code,addr,state,value,updatetime,online,onenetnum,warningAI,warningBI,warningCI,warningLI,warningOneTemperature,warningTwoTemperature,warningThreeTemperature,warningFourTemperature,trailerAI,trailerBI,trailerCI,trailerLI,trailerOneTemperature,trailerTwoTemperature,trailerThreeTemperature,trailerFourTemperature,trailerAV,trailerBV,trailerCV,warningAV,warningBV,warningCV,trailerAV2,trailerBV2,trailerCV2,warningAV2,warningBV2,warningCV2,isdel ");
            strSql.Append(" FROM dt_item ");
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
            strSql.Append(" i.*,m.telephone ");
            strSql.Append(" FROM dt_item i left join dt_manager m on i.user_id=m.id ");
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
            strSql.Append("select count(1) FROM dt_item ");
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
            strSql.Append(")AS Row, T.*  from dt_item T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE 1=1 " + strWhere);
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
			parameters[0].Value = "dt_item";
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
            strSql.Append("select i.*,m.real_name,a.name as areaname FROM dt_item i left join dt_manager m on i.user_id=m.id left join dt_area_code a on i.area_code=a.code ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
        public List<Model.dt_item_ex> GetList2(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select i.*,m.real_name,a.name as areaname FROM dt_item i left join dt_manager m on i.user_id=m.id left join dt_area_code a on i.area_code=a.code ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            var ds = DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
            return (from DataRow dr in ds.Tables[0].Rows select new DBRowConvertor(dr).ConvertToEntity<Model.dt_item_ex>()).ToList();
        }
        #endregion  ExtensionMethod
    }
}

