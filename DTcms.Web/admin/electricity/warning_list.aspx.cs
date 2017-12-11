using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.electricity
{
    public partial class warning_list : ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;
        protected DataSet ds;
        protected string keywords = string.Empty;
        protected int state;//状态
        protected int online;//在线状态

        protected void Page_Load(object sender, EventArgs e)
        {
            this.keywords = DTRequest.GetQueryString("keywords");
            this.state = DTRequest.GetQueryInt("state", -1);
            this.online = DTRequest.GetQueryInt("online", -1);
            
            this.pageSize = GetPageSize(10); //每页数量
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("warning_list", DTEnums.ActionEnum.View.ToString()); //检查权限

                Model.manager model = GetAdminInfo(); //取得当前管理员信息
                string _where = "";
                if (model.role_type != 1)
                {
                    _where = $" and i.user_id={model.id} ";
                }
                RptBind(" 1=1 "+ _where + CombSqlTxt(keywords, this.state, this.online), " i.id desc");
            }
        }

        #region 数据绑定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            ddlState.SelectedValue = this.state.ToString();
            ddlOnLine.SelectedValue = this.online.ToString();
            this.page = DTRequest.GetQueryInt("page", 1);
            BLL.dt_item bll = new BLL.dt_item();
            //this.rptList.DataSource = bll.GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            //this.rptList.DataBind();
            ds = bll.GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("warning_list.aspx", "keywords={0}&page={1}&state={2}&online={3}", this.keywords, "__id__", this.state.ToString(), this.online.ToString());
            PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(string _keywords, int _state, int _online)
        {
            StringBuilder strTemp = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (_state > 0)
            {
                strTemp.Append($" and i.state={_state}");
            }
            if (_online > -1)
            {
                strTemp.Append($" and i.online={_online}");
            }
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and (i.name like  '%" + _keywords + "%' or i.remarks like '%" + _keywords + "%')");
            }

            return strTemp.ToString();
        }
        #endregion

        #region 返回每页数量=============================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Utils.GetCookie("warning_list_page_size", "DTcmsPage"), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        #endregion



        //设置分页数量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Utils.WriteCookie("warning_list_page_size", "DTcmsPage", _pagesize.ToString(), 14400);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("warning_list.aspx", "keywords={0}&state={1}&online={2}", this.keywords, this.state.ToString(), this.online.ToString()));
        }


        public double getVal(string val)
        {
            val = val.Substring(2) + val.Substring(0, 2);
            return Int32.Parse(val, System.Globalization.NumberStyles.HexNumber) * 0.1;
        }

        

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("warning_list.aspx", "state={0}&keywords={1}&page={2}&online={3}",
                ddlState.SelectedValue, this.keywords, this.page.ToString(), this.online.ToString()));
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("warning_list.aspx", "state={0}&keywords={1}&online={2}",
                this.state.ToString(), txtKeywords.Text, this.online.ToString()));
        }

        protected void ddlOnLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("warning_list.aspx", "state={0}&keywords={1}&online={2}",
                this.state.ToString(), this.keywords, ddlOnLine.SelectedValue));
        }
    }
}