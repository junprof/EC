using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTcms.Model
{
    public class UserException : Exception
    {
        public UserException(string msg):base(msg)
        {

        }
    }
    public class BaseResponse
    {
        /// <summary>
        /// 0 成功 1 常规错误 2 超时 3 没有权限 4 系统异常 5 参数缺失
        /// </summary>
        public int error { get; set; } = 0;
        public object data { get; set; }
        public BaseResponse()
        {

        }
        /// <summary>
        /// 快速信息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        public BaseResponse(int code,string msg)
        {
            error = code;
            data = msg;
        }
    }
    public class PagerBase
    {
        public int pagenum { get; set; }
        public int pagesize { get; set; }
    }
    public class PushMessage
    {
        public string message { get; set; }
        public string regids { get; set; }
        public List<KeyValuePair<string,object>> extras { get; set; }
    }
}