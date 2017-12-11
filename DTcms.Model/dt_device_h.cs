using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DTcms.Common;
using DTcms.DBUtility;
using System.Text;
using System.Data.SqlClient;

namespace DTcms.Model
{
    public class dt_device_h
    {
        #region PROPERTY
        public string id { get; set; }
        public int? user_id { get; set; }
        public DateTime? addtime { get; set; }
        public string addr { get; set; }
        /// <summary>
        /// 经纬度
        /// </summary>
        public string position { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string device_sn { get; set; }
        /// <summary>
        /// 图片列表
        /// </summary>
        public string photolist { get; set; }
        public string remark { get; set; }
        #endregion
        public bool Insert(int type)
        {
            string table = "dt_device_inspection";
            if(type==1)
                table = "dt_device_inspection";
            else if(type==2)
                table = "dt_device_repair";
            return MSSQLAccess.InsertData(table,this);
        }
        public dt_device_h_ex Get(int type)
        {
            string table = "dt_device_inspection";
            if (type == 1)
                table = "dt_device_inspection";
            else if (type == 2)
                table = "dt_device_repair";
            string sql = "select t.*,i.name device_name,m.user_name from "+table +" t left join dt_item i on t.device_sn=i.onenetnum left join dt_manager m on t.user_id=m.id where t.id=@id";
            var dt = MSSQLDbHelper.Instance.ExecuteTable(sql, new System.Data.SqlClient.SqlParameter("@id", id));
            if (dt.Rows.Count > 0)
                return new DBRowConvertor(dt.Rows[0]).ConvertToEntity<dt_device_h_ex>();
            else
                return null;
        }
        public List<dt_device_h_ex> GetListByPage(dt_device_h_adq adq, string orderby, int startIndex, int endIndex,int type)
        {
            string table = "dt_device_inspection";
            if (type == 1)
                table = "dt_device_inspection";
            else if (type == 2)
                table = "dt_device_repair";
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
            strSql.Append(")AS Row, T.*,i.name as device_name,m.user_name  from "+ table + " T left join dt_item i on t.device_sn=i.onenetnum left join dt_manager m on t.user_id=m.id");
            strSql.Append(" WHERE 1=1 ");
            List<SqlParameter> paramlist = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(adq.device_sn))
            {
                strSql.Append(" and device_sn = @device_sn");
                paramlist.Add(new SqlParameter("@device_sn", adq.device_sn));
            }
            if (!string.IsNullOrEmpty(adq.device_name))
            {
                strSql.Append(" and i.name = @device_name");
                paramlist.Add(new SqlParameter("@device_name", adq.device_name));
            }
            if (adq.user_id.HasValue)
            {
                strSql.Append(" and t.user_id = @user_id");
                paramlist.Add(new SqlParameter("@user_id", adq.user_id));
            }
            if (adq.start_time.HasValue)
            {
                strSql.Append(" and t.addtime >= @start_time");
                paramlist.Add(new SqlParameter("@start_time", adq.start_time));
            }
            if (adq.end_time.HasValue)
            {
                strSql.Append(" and t.addtime <= @end_time");
                paramlist.Add(new SqlParameter("@end_time", adq.end_time));
            }
            if (!string.IsNullOrWhiteSpace(adq.sqlwhere))
            {
                strSql.Append(" and " + adq.sqlwhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            var dt = MSSQLDbHelper.Instance.ExecuteTable(strSql.ToString(),paramlist.ToArray());
            return (from DataRow dr in dt.Rows select new DBRowConvertor(dr).ConvertToEntity<Model.dt_device_h_ex>()).ToList();
        }
    }
    public class dt_device_h_ex : dt_device_h
    {
        public string device_name { get; set; }
        public string user_name { get; set; }
    }
    public class dt_device_h_adq : PagerBase
    {
        public DateTime? start_time { get; set; }
        public DateTime? end_time { get; set; }
        public string device_name { get; set; }
        public string device_sn { get; set; }
        public int? user_id { get; set; }
        public string sqlwhere { get; set; }
        public string id { get; set; }
    }
    public class ReportParam
    {
        public int? item_id { get; set; }
        public string onenetnum { get; set; }
        public int? dim_id { get; set; }
        public string msgid { get; set; }
        public string hid { get; set; }
        public DateTime? starttime { get; set; }
        public DateTime? endtime { get; set; }
        /// <summary>
        /// 精度
        /// </summary>
        public string Acc { get; set; } = "DD";
    }
    public class ReportData
    {
        public DateTime time { get; set; }
        public decimal? value { get; set; }
        public int Month { get { return time.Month; } }
        public int Day { get { return time.Day; } }
        public int Hour { get { return time.Hour; } }
        public int Minute { get { return time.Minute; } }
        public int Second { get { return time.Second; } }
    }
}
