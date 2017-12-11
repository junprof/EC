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
    public partial class history_list : ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;
        protected DataSet ds;
        protected List<Model.dt_historydata_ex> datalist;
        protected string keywords = string.Empty, hid=string.Empty;
        protected int state, online,id;


        protected void Page_Load(object sender, EventArgs e)
        {
            this.keywords = DTRequest.GetQueryString("keywords");
            this.online = DTRequest.GetQueryInt("online", -1);
            this.id = DTRequest.GetQueryInt("id",-1);
            this.hid = string.IsNullOrEmpty(Request.QueryString["hid"])?"": Request.QueryString["hid"].Replace("'","");
            this.pageSize = GetPageSize(10); //每页数量
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("electricity_list", DTEnums.ActionEnum.View.ToString()); //检查权限
                if (id>0)
                {
                    Model.manager model = GetAdminInfo(); //取得当前管理员信息
                    string _where = "";
                    if (model.role_type != 1)
                    {
                        _where = $" and i.user_id={model.id} ";
                    }
                    RptBind(" 1=1 "+ _where + " and h.item_id=" + this.id + " " + CombSqlTxt(keywords, this.online,this.hid), " h.addtime desc");
                }
            }
        }

        #region 数据绑定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            ddlOnLine.SelectedValue = this.online.ToString();
            //txtKeywords.Text = this.keywords;
            this.page = DTRequest.GetQueryInt("page", 1);
            BLL.dt_historydata bll = new BLL.dt_historydata();
            //ds = bll.GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            datalist = bll.GetList2(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("history_list.aspx", "keywords={0}&page={1}&online={2}&id={3}", this.keywords, "__id__", this.online.ToString(), this.id.ToString());
            PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(string _keywords, int _online,string _hid)
        {
            StringBuilder strTemp = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (_online > -1)
            {
                strTemp.Append($" and h.online={_online}");
            }
            if (string.IsNullOrEmpty(_keywords))
            {
                _keywords = DateTime.Now.ToString("yyyy-MM-dd");
            }
            if (!string.IsNullOrEmpty(_hid))
            {
                strTemp.Append($" and h.id='{_hid}'");
            }
            DateTime dt;
            string sr = _keywords + " 00:00:00";
            if (!Utils.IsDate(sr, out dt))
            {
                _keywords = DateTime.Now.ToString("yyyy-MM-dd");
                dt = DateTime.Parse(_keywords+ " 00:00:00");
            }
            txtKeywords.Text = _keywords;
            strTemp.Append($" and (h.addtime between '" + dt.ToString("yyyy-MM-dd 00:00:00") + "' and '" + dt.ToString("yyyy-MM-dd 23:59:59") + "')");


            return strTemp.ToString();
        }
        #endregion

        #region 返回每页数量=============================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Utils.GetCookie("history_list_page_size", "DTcmsPage"), out _pagesize))
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
                    Utils.WriteCookie("history_list_page_size", "DTcmsPage", _pagesize.ToString(), 14400);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("history_list.aspx", "keywords={0}&online={1}&id={2}", this.keywords, this.online.ToString(), this.id.ToString()));
        }

        public double getVal(string val)
        {
            val = val.Substring(2) + val.Substring(0, 2);
            return Int32.Parse(val, System.Globalization.NumberStyles.HexNumber) * 0.1;
        }

        public string getVal(string val, double trailerval, double warningval)
        {
            val = val.Substring(2) + val.Substring(0, 2);
            double res = Int32.Parse(val, System.Globalization.NumberStyles.HexNumber) * 0.1;
            if (warningval != 0 && res >= warningval)
            {//超过告警值返回
                return "<font color=\"red\" title=\"预警值："+trailerval+"，告警值："+warningval+"\">" + res + "</font>";
            }
            if (trailerval != 0 && res >= trailerval)
            {//超过告警值返回
                return "<font color=\"#dca72c\" title=\"预警值：" + trailerval + "，告警值：" + warningval + "\">" + res + "</font>";
            }
            return "<font title=\"预警值：" + trailerval + "，告警值：" + warningval + "\">" + res + "</font>";
        }
        public string getVal(double val, double? trailerval, double? warningval)
        {
            if (trailerval.HasValue && trailerval != 0 && val >= trailerval)
            {//超过告警值返回
                return "<font color=\"#dca72c\" title=\"预警值：" + trailerval + "\">" + val + "</font>";
            }
            return "<font title=\"预警值：" + trailerval + "\">" + val + "</font>";
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("history_list.aspx", "keywords={0}&online={1}&id={2}",
                txtKeywords.Text, this.online.ToString(), this.id.ToString()));
        }

        protected void ddlOnLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("history_list.aspx", "keywords={0}&online={1}&id={2}",
                this.keywords, ddlOnLine.SelectedValue, this.id.ToString()));
        }

    }
}