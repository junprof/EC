using DTcms.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DTcms.Web.weixin
{
    public partial class login : System.Web.UI.Page
    {
        string weichatid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if(Session[DTKeys.SESSION_ADMIN_INFO]==null && !string.IsNullOrEmpty(weichatid))
            //    LoginFromWeichatid();
        }
        private void Login()
        {
            string msg = string.Empty;
            string username = string.Empty;
            string userpwd = string.Empty;
            BLL.manager bll = new BLL.manager();
            Model.manager model = null;
            model = bll.GetModel(username, userpwd, true);
            if (model == null)
            {
                msg = "用户名或密码有误，请重试！";
                return;
            }
            Session[DTKeys.SESSION_ADMIN_INFO] = model;
            Session.Timeout = 45;
            Utils.WriteCookie("DTRememberName", model.user_name, 14400);
            Utils.WriteCookie("AdminName", "DTcms", model.user_name);
            Utils.WriteCookie("AdminPwd", "DTcms", model.password);
            Response.Redirect("index.aspx");
            return;
        }
        private void LoginFromWeichatid()
        {
            string msg = string.Empty;
            BLL.manager bll = new BLL.manager();
            Model.manager model = bll.GetModel(weichatid);
            if (model == null)
            {
                Response.Redirect("login.aspx");
                return;
            }
            Session[DTKeys.SESSION_ADMIN_INFO] = model;
            Session.Timeout = 45;
            Utils.WriteCookie("DTRememberName", model.user_name, 14400);
            Utils.WriteCookie("AdminName", "DTcms", model.user_name);
            Utils.WriteCookie("AdminPwd", "DTcms", model.password);
            Response.Redirect("index.aspx");
            return;
        }
    }
}