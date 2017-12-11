using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.Model
{
    public class dt_units
    {
        public string UNITID { get; set; }
        public string UNITNAME { get; set; }
        public string MANAGEUNITID { get; set; }
        public string MANAGEUNITNAME { get; set; }
        public int? SORT { get; set; }
        public int? UNITTYPE { get; set; }
        public DateTime? UPDATETIME { get; set; }

        public static Dictionary<string,dt_units> BASEUNITS;
        static dt_units()
        {
            Refresh();
        }
        public static void Refresh()
        {
            InitUnits();
        }
        protected static void InitUnits()
        {
            string sql = "SELECT A.*,B.UNITNAME AS MANAGEUNITNAME FROM DT_UNITS A LEFT JOIN DT_UNITS B ON A.MANAGEUNITID=B.UNITID";
            var dt = MSSQLDbHelper.Instance.ExecuteTable(sql);
            var datalist = (from DataRow dr in dt.Rows select new DBRowConvertor(dr).ConvertToEntity<dt_units>()).ToList();
            BASEUNITS = new Dictionary<string, dt_units>();
            datalist.ForEach(p=> {
                BASEUNITS.Add(p.UNITID, p);
            });
        }

        public static dt_units GetUnit(string unitID)
        {
            if (string.IsNullOrEmpty(unitID))
            {
                return null;
            }
            dt_units res;
            if (BASEUNITS.TryGetValue(unitID, out res))
            {
                return res;
            }
            else
            {
                return null;
            }
        }
    }
}
