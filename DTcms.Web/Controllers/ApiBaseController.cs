using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace DTcms.Web.Controllers
{
    [Verify]
    public class ApiBaseController : ApiController
    {
        private Models.UserSession _CurrentUser;
        protected Models.UserSession CurrentUser
        {
            get
            {
                if (_CurrentUser == null)
                {
                    _CurrentUser = Models.UserSession.GetCurrentUser();
                }
                try
                {
                    _CurrentUser.LOGFLAG = Models.UserSession.GetClientIP();
                }
                catch
                {
                    // ignored
                }
                return _CurrentUser;
            }
        }
        public Model.BaseResponse resObj { get; set; } = new Model.BaseResponse();
            
        /// <summary>
        /// 通用类型返回json格式
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>API要求的返回类型</returns>
        [NonAction]
        public static HttpResponseMessage Json(Object obj)
        {
            String str;
            if (obj is String || obj is Char)
            {
                str = obj.ToString();
            }
            else
            {
                str = Common.JsonHelper2.Serialize(obj);
            }
            HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(str, Encoding.GetEncoding("UTF-8"), "application/json") };
            return result;
        }

        /// <summary>
        /// 文件下载
        /// </summary>
        /// <returns></returns>

        [NonAction]
        public HttpResponseMessage DownLoad(string filename)
        {
            string filepath = System.Web.HttpRuntime.AppDomainAppPath + "/DownLoad/" + filename;
            if (!File.Exists(filepath))
                throw new HttpResponseException(HttpStatusCode.NotFound);
            FileStream fs = new FileStream(filepath, FileMode.Open);
            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StreamContent(fs);
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = System.Web.HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
            response.Content.Headers.ContentLength = fs.Length;
            return response;
        }
    }
}
