using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTcms.Web.Models
{
    public class LoginInfo
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        /// <summary>
        ///  1 WEB 2 ANDROID 3 IOS
        /// </summary>
        public int? Plateform { get; set; } = 1;
        /// <summary>
        /// APP版本号
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 手机IMEI
        /// </summary>
        public string IMEI { get; set; }
        /// <summary>
        /// push id
        /// </summary>
        public string ChannelID { get; set; }
    }
    /// <summary>
    /// 更改密码传递实体
    /// </summary>
    public class ChangeUserInfo
    {
        public string CODE { get; set; }
        public string PHONE { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
    }
}