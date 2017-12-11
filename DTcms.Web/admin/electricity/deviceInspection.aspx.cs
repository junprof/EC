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
    public partial class deviceInspection : ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int type;
        protected int pageSize;
        protected List<Model.dt_device_h_ex> datalist;
        protected string keywords = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.keywords = DTRequest.GetQueryString("keywords");
            this.type = DTRequest.GetQueryInt("type", 1);

            this.pageSize = GetPageSize(10); //每页数量
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("deviceInspection", DTEnums.ActionEnum.View.ToString()); //检查权限
                Model.manager model = GetAdminInfo(); //取得当前管理员信息
                string _where = "";
                if (model.role_type != 1)
                {
                    _where = $" and i.user_id={model.id} ";
                }
                if (!string.IsNullOrEmpty(keywords))
                {
                    _where +=($" and (i.name like  '%{keywords}%' or i.onenetnum='%{keywords}%' or i.remarks like '%{keywords}%')");
                }
                RptBind(" 1=1 " + _where , " addtime desc");
            }
        }

        #region 数据绑定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            this.page = DTRequest.GetQueryInt("page", 1);
            BLL.dt_item bll = new BLL.dt_item();
            //this.rptList.DataSource = bll.GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            //this.rptList.DataBind();
            int startIdx = (this.page - 1) * this.pageSize;
            int endIdx = startIdx + this.pageSize;
            datalist = new Model.dt_device_h().GetListByPage(new Model.dt_device_h_adq { sqlwhere=_strWhere}, _orderby, startIdx, endIdx, this.type);
            //ds = bll.GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("deviceInspection.aspx", "keywords={0}&page={1}&type={2}", this.keywords, "__id__",this.type.ToString());
            PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion

        #region 返回每页数量=============================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Utils.GetCookie("electricity_page_size", "DTcmsPage"), out _pagesize))
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
                    Utils.WriteCookie("electricity_page_size", "DTcmsPage", _pagesize.ToString(), 14400);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("deviceInspection.aspx", "type={0}&keywords={1}", this.type.ToString(),this.keywords));
        }

        //批量删除
        //protected void btnDelete_Click(object sender, EventArgs e)
        //{
        //    ChkAdminLevel("electricity_list", DTEnums.ActionEnum.Delete.ToString()); //检查权限
        //    int sucCount = 0;
        //    int errorCount = 0;
        //    BLL.manager bll = new BLL.manager();
        //    for (int i = 0; i < rptList.Items.Count; i++)
        //    {
        //        int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
        //        CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
        //        if (cb.Checked)
        //        {
        //            if (bll.Delete(id))
        //            {
        //                sucCount += 1;
        //            }
        //            else
        //            {
        //                errorCount += 1;
        //            }
        //        }
        //    }
        //    AddAdminLog(DTEnums.ActionEnum.Delete.ToString(), "删除管理员" + sucCount + "条，失败" + errorCount + "条"); //记录日志
        //    JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", Utils.CombUrlTxt("manager_list.aspx", "keywords={0}", this.keywords));
        //}

        public string getVal(string val, double trailerval, double warningval)
        {
            if (!string.IsNullOrEmpty(val))
            {
                val = val.Substring(2) + val.Substring(0, 2);
                double res = Int32.Parse(val, System.Globalization.NumberStyles.HexNumber) * 0.1;
                if (warningval != 0 && res >= warningval)
                {//超过告警值返回
                    return "<font color=\"red\">" + res + "</font>";
                }
                if (trailerval != 0 && res >= trailerval)
                {//超过告警值返回
                    return "<font color=\"#dca72c\">" + res + "</font>";
                }
                return res.ToString();
            }else
            {
                return "0";
            }
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("deviceInspection.aspx", "type={0}&keywords={1}",this.type.ToString(), txtKeywords.Text));
        }
    }
}