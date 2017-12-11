using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WxPayAPI;

namespace DTcms.Web
{
    public partial class GetWebCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var webCode = Request.Params["code"];
            var state = Request.Params["state"];
            var webTokenResult = Senparc.Weixin.MP.AdvancedAPIs.OAuth.GetAccessToken(WxPayConfig.APPID, WxPayConfig.APPSECRET, webCode);


            string weburl = new BLL.siteconfig().loadConfig().weburl;
            if (state == "mp")
            {
                Response.Redirect(string.Format("{0}/weixin/index.html?openid={1}",weburl, webTokenResult.openid));
            }
            else
            {
                //Response.Redirect(string.Format("", webTokenResult.openid, state));

            }

        }
    }
}