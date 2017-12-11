using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTcms.DBUtility;
using DTcms.Common;
using System.Data.SqlClient;

namespace DTcms.Model
{
    public class dt_verifycode
    {
        public string ID { get; set; }
        public string PHONE { get; set; }
        public string CODE { get; set; }
        public DateTime CREATETIME { get; set; }
        public int CODETYPE { get; set; }
        public int ISVALID { get; set; }
        public bool Insert()
        {
           return MSSQLAccess.InsertData("DT_VERIFYCODE", this);
        }
        public bool SetInvalid()
        {
            string sql = "UPDATE DT_VERIFYCODE SET ISVALID = 0,CHECKTIME=GETDATE() WHERE ID = @ID ";
            return MSSQLDbHelper.Instance.ExecuteNonQuery(sql, new SqlParameter("@ID", ID))>0;
        }
        public dt_verifycode GetCode(string phone,int codetype)
        {
            string sql = "SELECT * FROM DT_VERIFYCODE  WHERE PHONE = @PHONE AND CODETYPE = @CODETYPE AND ISVALID = 1 ORDER BY CREATETIME DESC";
            var dt = MSSQLDbHelper.Instance.ExecuteTable(sql, new SqlParameter("@PHONE", phone), new SqlParameter("@CODETYPE", codetype));
            if (dt.Rows.Count > 0)
            {
                return (new DBRowConvertor(dt.Rows[0]).ConvertToEntity<Model.dt_verifycode>());
            }
            else
            {
                return null;
            }
        }
    }
}
