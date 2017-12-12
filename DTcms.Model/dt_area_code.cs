using DTcms.Common;
using DTcms.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DTcms.Model
{
    /// <summary>
    /// dt_area_code:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class dt_area_code
    {
        public dt_area_code()
        { }
        #region Model
        /// <summary>
        /// 
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name_short { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string parent_code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? lev { get; set; }
        
        #endregion Model
        #region Method
        public static Dictionary<string, dt_area_code_ex> BASEUNITS;
        static dt_area_code()
        {
            Refresh();
        }
        public static void Refresh()
        {
            InitUnits();
        }
        protected static void InitUnits()
        {
            string sql = "SELECT A.* FROM dt_area_code A";
            var dt = MSSQLDbHelper.Instance.ExecuteTable(sql);
            var datalist = (from DataRow dr in dt.Rows select new DBRowConvertor(dr).ConvertToEntity<dt_area_code_ex>()).ToList();
            BASEUNITS = new Dictionary<string, dt_area_code_ex>();
            datalist.ForEach(p => {
                BASEUNITS.Add(p.code, p);
            });
        }


        public static dt_area_code_ex GetUnit(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return null;
            }
            dt_area_code_ex res;
            if (BASEUNITS.TryGetValue(code, out res))
            {
                return res;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
    public class dt_area_code_ex: dt_area_code
    {
        public string parent_name { get; set; }
    }
}

