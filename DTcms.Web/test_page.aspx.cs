using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web
{
    public partial class test_page : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string openid = Request.QueryString["openid"] != null ? Request.QueryString["openid"]: "";
            Response.Write(openid);
        }
    }
}