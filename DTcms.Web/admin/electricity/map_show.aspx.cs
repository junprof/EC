using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;
using System.Text;

namespace DTcms.Web.admin.electricity
{
    public partial class map_show : ManagePage
    {
        public List<Model.dt_item> itemlist = new List<Model.dt_item>();
        public string areacode = "";
        public string keywords = "";
        protected int totalCount;
        protected int page;
        protected int pageSize = 9999;
        public string idstr = "";
        public string point1 = "120.684855", point2 = "28.013549";
        public Dictionary<string, string> dclist = new Dictionary<string, string>();
        public string openids = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            this.areacode = DTRequest.GetQueryString("areacode");
            this.keywords = DTRequest.GetQueryString("keywords");
            if (!Page.IsPostBack)
            {
                //Areadatabind("330300", 1, areacodelist, 1);
                databind();
            }


        }

        protected void databind()
        {

            var model = GetAdminInfo();
            this.page = DTRequest.GetQueryInt("page", 1);
            tbKeys.Text = this.keywords;
            string strwhere = " 1=1 ";
            StringBuilder strTemp = new StringBuilder();
            this.keywords = this.keywords.Replace("'", "");
            if (model.role_type != 1)
            {
                strwhere += $" and i.user_id={model.id} ";
            }
            if (!string.IsNullOrEmpty(this.areacode))
            {
                strwhere += $" and i.area_code='{this.areacode.Replace("'","")}' ";
            }
            if (!string.IsNullOrEmpty(this.keywords))
            {
                strwhere +=$" and (i.name like  '%{this.keywords}%' or i.onenetnum='{this.keywords}')";
            }
            BLL.dt_item bll = new BLL.dt_item();
            string _orderby = " addtime desc ";
            var q = bll.GetList(this.pageSize, this.page, strwhere, _orderby, out this.totalCount);
            rptList1.DataSource = q;
            rptList1.DataBind();
            if (this.totalCount > 0)
            {
                string position = q.Tables[0].Rows[0]["position"].ToString();
                point1 = position.Substring(0, position.IndexOf(","));
                point2 = position.Substring(position.IndexOf(",")+1);
            }
            string pageUrl = Utils.CombUrlTxt("map_show.aspx", "areacode={0}&keywords={1}&page={2}",
                this.areacode, this.keywords, "__id__");
            PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 3);
        }

        //public string getState(string mpdz)
        //{
        //    if (("," + openids + ",").Contains("," + mpdz + ","))
        //    {
        //        return "<img src=\"/admin/skin/default/open.png\" width=\"15\">";
        //    }
        //    return "<img src=\"/admin/skin/default/stop.png\" width=\"15\">";
        //}

       

        //关健字查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string keywords = tbKeys.Text;
            Response.Redirect(Utils.CombUrlTxt("map_show.aspx", "areacode={0}&keywords={1}",
                this.areacode, keywords));
        }
    }
}