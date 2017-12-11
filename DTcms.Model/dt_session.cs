using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.Model
{
    public class dt_session
    {
        [DataColumn]
        public string SessionID { get; set; }
        [DataColumn]
        public string UserName { get; set; }
        [DataColumn]
        public DateTime? LastLoginTime { get; set; }
        [DataColumn]
        public int? Plateform { get; set; }
        [DataColumn]
        public string Version { get; set; }
        /// <summary>
        /// ip or imei
        /// </summary>
        [DataColumn]
        public string LOGFLAG { get; set; }
        [DataColumn]
        public string ChannelID { get; set; }
        public bool Insert()
        {
            return MSSQLAccess.InsertData("DT_SESSION", this);
        }
        public bool Insert(Model.dt_session item)
        {
            return MSSQLAccess.InsertData("DT_SESSION", item);
        }
        public bool Merge()
        {
            return MSSQLAccess.MergeData(MSSQLAccess.GetColumnsByEntity<dt_session>(),"DT_SESSION", "SESSIONID", this);
        }
        public dt_session Get(string sessionid)
        {
            var datalist = MSSQLAccess.GetData<dt_session>(MSSQLAccess.GetColumnsByEntity<dt_session>(), "DT_SESSION", "sessionid=@sessionid", new System.Data.SqlClient.SqlParameter("@sessionid", sessionid));
            if (datalist.Count > 0)
                return datalist[0];
            else
                return null;
        }
        public dt_session GetByUserName(string username)
        {
            string sql = "SELECT TOP 1 * FROM DT_SESSION WHERE USERNAME = @USERNAME ORDER BY LASTLOGINTIME DESC ";
            var dt = MSSQLDbHelper.Instance.ExecuteTable(sql, new System.Data.SqlClient.SqlParameter("@username", username));
            if (dt.Rows.Count > 0)
                return new DBRowConvertor(dt.Rows[0]).ConvertToEntity<dt_session>();
            else
                return null;
        }
    }
}
