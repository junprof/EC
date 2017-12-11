using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace DTcms.Web.Controllers
{
    /// <summary>
    /// 身份验证
    /// </summary>
    public class VerifyAttribute:ActionFilterAttribute
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
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            // check null parameter
            if (actionContext.ActionArguments.Count > 0)
            {
                if(actionContext.ActionArguments.Any(p => { return p.Value == null; }))
                {
                    Model.BaseResponse res = new Model.BaseResponse
                    {
                        error = 5,
                        data = "参数缺失"
                    };
                    actionContext.Response = ApiBaseController.Json(res);
                    base.OnActionExecuting(actionContext);
                }
            }
            var attr = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().OfType<AllowAnonymousAttribute>();
            bool isAnonymous = attr.Any(a => a is AllowAnonymousAttribute);
            if (isAnonymous)
                base.OnActionExecuting(actionContext);
            else
            {
                // token in header
                var request = actionContext.Request;
                string token = string.Empty;
                if (request.Headers.Contains("token"))
                {
                    token = HttpUtility.UrlDecode(request.Headers.GetValues("token").FirstOrDefault());
                }
                if (!string.IsNullOrEmpty(token)) // app Access
                {
                    var currentUser = new Models.UserSession().GetUserSession(token);
                    if (currentUser == null)
                    {
                        Model.BaseResponse res = new Model.BaseResponse
                        {
                            error = 2,
                            data = "用户信息已失效，请重新登录"
                        };
                        actionContext.Response = ApiBaseController.Json(res);
                    }
                    else
                    {
                        new Models.UserSession().SetUserSession(currentUser);
                    }
                }
                else   // web access
                {
                    if (Models.UserSession.GetCurrentUser() == null)
                    {
                        Model.BaseResponse res = new Model.BaseResponse
                        {
                            error = 2,
                            data = "用户信息已失效，请重新登录"
                        };
                        actionContext.Response = ApiBaseController.Json(res);
                    }
                }
                base.OnActionExecuting(actionContext);
            }
        }
    }
}