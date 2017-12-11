using DTcms.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace DTcms.Web.weixin
{
    /// <summary>
    /// wxhandler 的摘要说明
    /// </summary>
    public class wxhandler : IHttpHandler,IRequiresSessionState
    {
        HttpRequest request = null;
        HttpContext context = null;
        string res = string.Empty;
        private Model.manager _userInfo;
        public Model.manager UserInfo
        {
            get
            {
                _userInfo = context.Session[DTKeys.SESSION_ADMIN_INFO] as Model.manager;
                return _userInfo;
            }
        }
        public void ProcessRequest(HttpContext context)
        {
            request = context.Request;
            this.context = context;
            try {
                
                if (UserInfo == null)
                {
                    res = JsonHelper.GetCommonObj(2, "登录超时");
                }
                else {
                    string action = request["action"];
                    switch (action)
                    {
                        case "feedbacklist":
                            GetFeedbackList();
                            break;
                        case "feedback":
                            Feedback();
                            break;
                        case "device":
                            device();
                            break;
                        case "chat":
                            GetChatData();
                            break;
                        case "bind":
                            Bind();
                            break;
                        case "unbind":
                            UnBind();
                            break;
                        case "easy":
                            LoginFromWeichatid();
                            break;
                        case "login":
                            Login();
                            break;
                        default:
                            break;
                    }
                }
            }catch(Exception ex)
            {
                Log.Error(ex.Message);
                res= JsonHelper2.GetCommonObj(1, "系统异常"+ex.Message);
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(res);
        }
        void Feedback()
        {
            BLL.dt_feedback bll = new BLL.dt_feedback();
            string title = request["title"];
            string content = request["content"];
            try {
                Model.dt_feedback item = new Model.dt_feedback
                {
                    Title = title,
                    Content = content,
                    AddTime = DateTime.Now,
                    IsDel = 0,
                    ManagerId = 2
                };
                int re = bll.Add(item);
                if (re > 0)
                    this.res = JsonHelper2.GetCommonObj(0, "反馈成功");
                else
                    this.res = JsonHelper2.GetCommonObj(1, "反馈失败");
            }catch(Exception ex)
            {
                Log.Error(ex.Message);
                this.res = JsonHelper2.GetCommonObj(1, "系统异常");
            }
        }
        void GetFeedbackList()
        {
            BLL.dt_feedback bll = new BLL.dt_feedback();
            int pageSize = int.Parse(request["pagesize"]);
            int pageIdx = int.Parse(request["pageidx"]);
            int totalCount = 0;
            var ds = bll.GetList(pageSize, pageIdx, " ", " i.id desc", out totalCount);
            var data = (from DataRow dr in ds.Tables[0].Rows select new DBRowConvertor(dr).ConvertToEntity<Model.dt_feedback>()).ToList();
            this.res = DTcms.Common.JsonHelper2.GetCommonObj(0, data);
        }

        void GetChatData()
        {
            string type = request["type"];
            string itemid = request["itemid"];
            BLL.dt_dimensioninfo bll = new BLL.dt_dimensioninfo();
            //TODO
            var data= bll.GetModelList($"a.updatetime>='2017-08-12 22:00:00' and a.updatetime <'2017-08-13 00:00:00' and a.dimension={type} and b.item_id = '{itemid}'");
            data = data.Where(p => p.Second > 40).ToList();
            this.res = DTcms.Common.JsonHelper2.GetCommonObj(0, data);
        }
        void device()
        {
            BLL.dt_item bll = new BLL.dt_item();
            int pageSize = int.Parse(request["pagesize"]);
            int pageIdx = int.Parse(request["pageidx"]);
            int totalCount = 0;
            var ds = bll.GetList(pageSize, pageIdx, " ", " i.id desc",out totalCount);
            var data = (from DataRow dr in ds.Tables[0].Rows select new DBRowConvertor(dr).ConvertToEntity<Model.dt_item>()).ToList();
            this.res= DTcms.Common.JsonHelper2.GetCommonObj(0, data);
        }

        void Bind()
        {
            string token = request["token"];
            BLL.manager bll = new BLL.manager();
            if(bll.BindWeichat(UserInfo.id, token))
            {
                res = JsonHelper2.GetCommonObj(0, "绑定成功");
            }
            else
            {
                res = JsonHelper2.GetCommonObj(1, "绑定失败");
            }
        }
        void UnBind()
        {
            string token = request["token"];
            BLL.manager bll = new BLL.manager();
            if (bll.UnBindWeichat(UserInfo.id, token))
            {
                res = JsonHelper2.GetCommonObj(0, "解绑成功");
            }
            else
            {
                res = JsonHelper2.GetCommonObj(1, "解绑失败");
            }
        }

        void LoginFromWeichatid()
        {
            string msg = string.Empty;
            BLL.manager bll = new BLL.manager();
            string token = request["token"];
            Model.manager model = bll.GetModel(token);
            if (model == null)
            {
                res = JsonHelper2.GetCommonObj(1, "未绑定");
            }
            else {
                context.Session[DTKeys.SESSION_ADMIN_INFO] = model;
                context.Session.Timeout = 45;
                Utils.WriteCookie("DTRememberName", model.user_name, 14400);
                Utils.WriteCookie("AdminName", "DTcms", model.user_name);
                Utils.WriteCookie("AdminPwd", "DTcms", model.password);
                res = JsonHelper2.GetCommonObj(0, model);
            }
        }
        void Login()
        {
            string msg = string.Empty;
            BLL.manager bll = new BLL.manager();
            string username = request["username"];
            string password = request["pwd"];
            Model.manager model = bll.GetModel(username,password);
            if (model == null)
            {
                res = JsonHelper2.GetCommonObj(1, "未绑定");
            }
            else {
                context.Session[DTKeys.SESSION_ADMIN_INFO] = model;
                context.Session.Timeout = 45;
                Utils.WriteCookie("DTRememberName", model.user_name, 14400);
                Utils.WriteCookie("AdminName", "DTcms", model.user_name);
                Utils.WriteCookie("AdminPwd", "DTcms", model.password);
                res = JsonHelper2.GetCommonObj(0, model);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}