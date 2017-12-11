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
    public partial class electricity_list : ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;
        protected DataSet ds;
        protected List<Model.dt_item_ex> datalist;
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
                ChkAdminLevel("electricity_list", DTEnums.ActionEnum.View.ToString()); //检查权限
                if(!ChkAdminLevel_s("electricity_list", DTEnums.ActionEnum.Edit.ToString()))
                {
                    btnLock.Visible = false;
                    btnOpen.Visible = false;
                }
                if (!ChkAdminLevel_s("electricity_list", DTEnums.ActionEnum.Add.ToString()))
                {
                    btn_add.Visible = false;
                }
                if (!ChkAdminLevel_s("electricity_list", DTEnums.ActionEnum.Delete.ToString()))
                {
                    btnDel.Visible = false;
                }
                Model.manager model = GetAdminInfo(); //取得当前管理员信息
                string _where = "";
                if (model.role_type != 1)
                {
                    _where = $" and i.user_id={model.id} ";
                }
                RptBind(" 1=1 " + _where + CombSqlTxt(keywords, this.state, this.online), " i.id desc");
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
            //ds = bll.GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            datalist = bll.GetList2(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("electricity_list.aspx", "keywords={0}&page={1}&state={2}&online={3}", this.keywords, "__id__", this.state.ToString(), this.online.ToString());
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
                strTemp.Append($" and (i.name like  '%{_keywords}%' or i.onenetnum='%{_keywords}%' or i.remarks like '%{_keywords}%')");
            }

            return strTemp.ToString();
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
            Response.Redirect(Utils.CombUrlTxt("electricity_list.aspx", "keywords={0}&state={1}&online={2}", this.keywords, this.state.ToString(), this.online.ToString()));
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
        public string getVal(double val, double? trailerval, double? warningval)
        {
            if (warningval.HasValue && warningval != 0 && val >= warningval)
            {//超过告警值返回
                return "<font color=\"red\">" + val + "</font>";
            }
            if (trailerval.HasValue && trailerval != 0 && val >= trailerval)
            {//超过告警值返回
                return "<font color=\"#dca72c\">" + val + "</font>";
            }
            return val.ToString();
        }

        protected void btnLock_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("electricity_list", DTEnums.ActionEnum.Edit.ToString()); //检查权限
            int sucCount = 0;
            int errorCount = 0;
            string ids = "";
            BLL.dt_item bll = new BLL.dt_item();
            string idstr = Request.Form["chkId"];
            string[] idlist = idstr.Split(',');
            if (idlist.Length > 0)
            {
                for (int i = 0; i < idlist.Length; i++)
                {

                    if (idlist[i].Trim() != null)
                    {
                        Model.dt_item model = bll.GetModel(int.Parse(idlist[i]));
                        if (model != null && model.state != 1)
                        {
                            Model.manager usermodel = GetAdminInfo();
                            if (usermodel.id != model.user_id && usermodel.role_type != 1)
                            {
                                errorCount += 1;
                            }
                            else
                            {
                                ids += "," + idlist[i];
                                model.state = 1;
                                if (bll.Update(model))
                                {
                                    sucCount += 1;
                                }
                                else
                                {
                                    errorCount += 1;
                                }
                            }
                        }
                        else
                        {
                            errorCount += 1;
                        }
                    }
                }
            }
            AddAdminLog("item_lock", "批量锁定成功" + sucCount + "条，失败" + errorCount + "条，" + ids); //记录日志


            JscriptMsg_Parent("批量锁定成功" + sucCount + "条，失败" + errorCount + "条！", Utils.CombUrlTxt("electricity_list.aspx", "state={0}&keywords={1}&page={2}&online={3}",
                ddlState.SelectedValue, this.keywords, this.page.ToString(), this.online.ToString()));
        }

        protected void btnOpen_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("electricity_list", DTEnums.ActionEnum.Edit.ToString()); //检查权限
            int sucCount = 0;
            int errorCount = 0;
            string ids = "";
            BLL.dt_item bll = new BLL.dt_item();
            string idstr = Request.Form["chkId"];
            string[] idlist = idstr.Split(',');
            if (idlist.Length > 0)
            {
                for (int i = 0; i < idlist.Length; i++)
                {

                    if (idlist[i].Trim() != null)
                    {
                        Model.dt_item model = bll.GetModel(int.Parse(idlist[i]));
                        if (model != null && model.state != 2)
                        {
                            Model.manager usermodel = GetAdminInfo();
                            if (usermodel.id != model.user_id && usermodel.role_type != 1)
                            {
                                errorCount += 1;
                            }
                            else
                            {
                                ids += "," + idlist[i];
                                model.state = 2;
                                if (bll.Update(model))
                                {
                                    sucCount += 1;
                                }
                                else
                                {
                                    errorCount += 1;
                                }
                            }
                        }
                        else
                        {
                            errorCount += 1;
                        }
                    }
                }
            }
            AddAdminLog("item_open", "批量启用成功" + sucCount + "条，失败" + errorCount + "条，" + ids); //记录日志


            JscriptMsg_Parent("批量启用成功" + sucCount + "条，失败" + errorCount + "条！", Utils.CombUrlTxt("electricity_list.aspx", "state={0}&keywords={1}&page={2}&online={3}",
                ddlState.SelectedValue, this.keywords, this.page.ToString(), this.online.ToString()));
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("electricity_list.aspx", "state={0}&keywords={1}&page={2}&online={3}",
                ddlState.SelectedValue, this.keywords, this.page.ToString(), this.online.ToString()));
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("electricity_list.aspx", "state={0}&keywords={1}&online={2}",
                this.state.ToString(), txtKeywords.Text, this.online.ToString()));
        }

        protected void ddlOnLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("electricity_list.aspx", "state={0}&keywords={1}&online={2}",
                this.state.ToString(), this.keywords, ddlOnLine.SelectedValue));
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("electricity_list", DTEnums.ActionEnum.Delete.ToString()); //检查权限
            int sucCount = 0;
            int errorCount = 0;
            string ids = "";
            BLL.dt_item bll = new BLL.dt_item();
            string idstr = Request.Form["chkId"];
            string[] idlist = idstr.Split(',');
            if (idlist.Length > 0)
            {
                for (int i = 0; i < idlist.Length; i++)
                {

                    if (idlist[i].Trim() != null)
                    {
                        Model.dt_item model = bll.GetModel(int.Parse(idlist[i]));
                        Model.manager usermodel = GetAdminInfo();
                        if (usermodel.id != model.user_id && usermodel.role_type != 1)
                        {
                            errorCount += 1;
                        }
                        else
                        {
                            ids += "," + idlist[i];
                            if (bll.Delete(int.Parse(idlist[i])))
                            {
                                sucCount += 1;
                            }
                            else
                            {
                                errorCount += 1;
                            }
                        }
                    }
                }
            }
            AddAdminLog("item_del", "批量删除成功" + sucCount + "条，失败" + errorCount + "条，" + ids); //记录日志


            JscriptMsg_Parent("批量删除成功" + sucCount + "条，失败" + errorCount + "条！", Utils.CombUrlTxt("electricity_list.aspx", "state={0}&keywords={1}&page={2}&online={3}",
                ddlState.SelectedValue, this.keywords, this.page.ToString(), this.online.ToString()));
        }
    }
}