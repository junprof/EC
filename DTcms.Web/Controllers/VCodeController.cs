using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DTcms.Web.Controllers
{
    [RoutePrefix("api/VCode")]
    public class VCodeController : ApiBaseController
    {
        /// <summary>
        /// 验证码校验
        /// </summary>
        /// <param name="code"> json格式{"PHONE":"手机号","CODE":"验证码"} </param>
        /// <returns></returns>
        [HttpPost]
        [Route("CheckCode")]
        [AllowAnonymous]
        public Model.BaseResponse CheckCode([FromBody]Model.dt_verifycode code)
        {
            var r = (BLL.VCode.Instance.CheckCode(code.PHONE, 1, code.CODE));
            return r;
        }
        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="phone">{"PHONE":"手机号"}</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SendCode")]
        [AllowAnonymous]
        public Model.BaseResponse SendCode([FromBody]Model.dt_verifycode phone)
        {
            return BLL.VCode.Instance.Send(phone.PHONE, 1);
        }
    }
}
