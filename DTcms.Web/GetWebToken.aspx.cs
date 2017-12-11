using DTcms.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WxPayAPI;

namespace DTcms.Web
{
    public partial class GetWebToken : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var state = Request.QueryString["state"];
                string weburl = new BLL.siteconfig().loadConfig().weburl;
                string redirect_uri = System.Web.HttpUtility.UrlEncode(string.Format("{0}/GetWebCode.aspx", weburl));
                string url = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type={2}&scope={3}&state={4}#wechat_redirect",
                    WxPayConfig.APPID, redirect_uri, "code", "snsapi_base", state);
                Response.Redirect(url);
            }
            catch
            {
                Response.Write("出错了");
            }
        }
    }
}