using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DTcms.Web.Controllers
{
    [RoutePrefix("api/UserManager")]
    public class UserManagerController : ApiBaseController
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="code"> json格式{"PHONE":"手机号","CODE":"验证码"} </param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public HttpResponseMessage Login([FromBody]Models.LoginInfo LoginInfo)
        {
            Models.UserSession UserManager = new Models.UserSession();
            string msg = string.Empty;
            var userSession = new Models.UserSession().Login(LoginInfo.UserName, LoginInfo.Password, out msg);
            if (string.IsNullOrWhiteSpace(msg))
            {
                if (!string.IsNullOrWhiteSpace(LoginInfo.IMEI))
                    userSession.LOGFLAG = LoginInfo.IMEI;
                userSession.LASTLOGINTIME = DateTime.Now;
                userSession.Version = LoginInfo.Version;
                userSession.Plateform = LoginInfo.Plateform;
                userSession.ChannelID = LoginInfo.ChannelID;
                UserManager.SetUserSession(userSession);
                resObj.data = userSession;
            }
            else
            {
                resObj.error = 1;
                resObj.data = msg;
            }
            return Json(resObj);
        }
        /// <summary>
        /// 更改密码
        /// </summary>
        /// <param name="md">{"PHONE":"手机号","CODE":"短信验证码","USERNAME":"用户名","PASSWORD":"新密码"}</param>
        /// <returns></returns>
        [HttpPost]
        [Route("ChangePwd")]
        [AllowAnonymous]
        public HttpResponseMessage ChangePwd([FromBody]Models.ChangeUserInfo md)
        {
            resObj = (BLL.VCode.Instance.CheckCode(md.PHONE, 1, md.CODE));
            if (resObj.error == 0)
            {
                resObj = new BLL.manager().ChangePwd(md.USERNAME, md.PASSWORD,md.PHONE);
            }
            return Json(resObj);
        }
    }
}
