using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DTcms.Web.weixin
{
    public partial class index : System.Web.UI.Page
    {
        public string openid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            openid = Request["openid"];
        }
    }
}