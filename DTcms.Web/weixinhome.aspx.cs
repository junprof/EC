using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DTcms.Web
{
    public partial class weixinhome : System.Web.UI.Page
    {
        private static string Token = "DRXrSqYfNXp8hYuIA0ydG5zsVQ52RW0V";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.HttpMethod.ToLower() == "post")
            {

            }
            else
            {
                string echoStr = Request.QueryString["echoStr"];
                if (!CheckSignature())
                {
                    Response.Write("参数错误！");
                    Response.End();
                }
                else
                {
                    if (!string.IsNullOrEmpty(echoStr))
                    {
                        Response.Write(echoStr);
                        Response.End();
                    }
                }
            }
            //Response.Write("zfgjwebweixin");
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// * 将token、timestamp、nonce三个参数进行字典序排序
        /// * 将三个参数字符串拼接成一个字符串进行sha1加密
        /// * 开发者获得加密后的字符串可与signature对比，标识该请求来源于易信。
        /// <returns></returns>     
        private bool CheckSignature()
        {
            string signature = Request.QueryString["signature"];
            string timestamp = Request.QueryString["timestamp"];
            string nonce = Request.QueryString["nonce"];
            return Check(signature, timestamp, nonce, Token);
        }

        /// <summary>
        /// 检查签名是否正确
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool Check(string signature, string timestamp, string nonce, string token = null)
        {
            return signature == GetSignature(timestamp, nonce, token);
        }

        /// <summary>
        /// 返回正确的签名
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string GetSignature(string timestamp, string nonce, string token = null)
        {
            token = token ?? Token;
            var arr = new[] { token, timestamp, nonce }.OrderBy(z => z).ToArray();
            var arrString = string.Join("", arr);
            //var enText = FormsAuthentication.HashPasswordForStoringInConfigFile(arrString, "SHA1");//使用System.Web.Security程序集
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var sha1Arr = sha1.ComputeHash(Encoding.UTF8.GetBytes(arrString));
            StringBuilder enText = new StringBuilder();
            foreach (var b in sha1Arr)
            {
                enText.AppendFormat("{0:x2}", b);
            }
            return enText.ToString().ToLower();
        }
    }
}