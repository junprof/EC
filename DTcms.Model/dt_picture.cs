using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using DTcms.Common;
using DTcms.DBUtility;
using System.Data;

namespace DTcms.Model
{
    public class PictureEntity
    {
        public string PICID { get; set; }

    }
    public class dt_picture
    {
        public string picid { get; set; }
        public string url { get; set; }
        public DateTime createtime { get; set; }
        /// <summary>
        /// 1 地址（服务器物理路径） 2 地址（链接） 3 数据库
        /// </summary>
        public int type { get; set; } = 1;
        public byte[] data { get; set; }

        public int? isvalid { get; set; }

        public bool Insert()
        {
            return MSSQLAccess.InsertData("DT_PICTURE", this);
        }

        public static object GetPicture(string PICID)
        {
            byte[] img = null;
            if (string.IsNullOrEmpty(PICID))
            {
                return null;
            }
            DataTable dt = MSSQLDbHelper.Instance.ExecuteTable("select * from dt_PICTURE where PICID=@PICID", new System.Data.SqlClient.SqlParameter("@PICID", PICID));
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                var dr = dt.Rows[0];
                if (dr[0] is DBNull)
                {
                    return null;
                }
                dt_picture pic = new DBRowConvertor(dr).ConvertToEntity<dt_picture>();
                if(pic.type == 1)
                {
                    //if (!File.Exists(pic.url))
                    //{
                    //    return null;
                    //}
                    //Stream stream = new FileStream(pic.url,FileMode.Open);
                    //img = new byte[stream.Length];
                    //stream.Read(img, 0, img.Length);
                    //stream.Close();
                    return pic.url;
                }
                else if (pic.type == 2)
                {

                }else if (pic.type == 3)
                {
                    img = pic.data;
                }
            }

            return img;
        }
        //public static string GetBase64Picture(string PICID)
        //{
        //    byte[] fileBytes = GetPicture(PICID);
        //    if (fileBytes != null)
        //    {
        //        return Convert.ToBase64String(fileBytes);
        //    }
        //    return null;
        //}
        public bool Delete()
        {
            string sql = "UPDATE DT_PICTURE SET ISVALID = 0 WHERE PICID = @PICID";
            return MSSQLDbHelper.Instance.ExecuteNonQuery(sql, new System.Data.SqlClient.SqlParameter("@PICID", picid))>0;
        }
    }
}
