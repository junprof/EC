using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTcms.Common;
using DTcms.DBUtility;

namespace DTcms.Model
{
    public class dt_pictureview
    {
        public string id { get; set; }
        public string unitid { get; set; }
        public int userid { get; set; }
        public string pictures { get; set; }
        public List<ViewModel> Viewer { get; set; } = new List<ViewModel>();
        public DateTime? updatetime { get; set; }
        public int? isvalid { get; set; }

        public dt_pictureview GetByUserId(int userid)
        {
            string sql = "SELECT A.* FROM DT_PICTUREVIEW A WHERE A.ISVALID=1 AND A.USERID=@USERID ORDER BY UPDATETIME DESC";
            var dt = MSSQLDbHelper.Instance.ExecuteTable(sql, new System.Data.SqlClient.SqlParameter("@USERID", userid));
            if (dt.Rows.Count > 0)
            {
                var item = new DBRowConvertor(dt.Rows[0]).ConvertToEntity<dt_pictureview>();
                if (!string.IsNullOrEmpty(item.pictures))
                {
                    item.Viewer = JsonHelper2.Deserialize<List<ViewModel>>(item.pictures).OrderBy(p => p.sort).ToList();
                    item.pictures = string.Empty;
                }
                return item;
            }
            else
            {
                return null;
            }
        }
    }
    public class ViewModel
    {
        public string title { get; set; }
        public string picid { get; set; }
        public string url { get; set; }
        public int sort { get; set; }
    }
}
