using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.electricity
{
    public partial class electricity_add : ManagePage
    {
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作类型
        private int id = 0;
        public string point1 = "120.684855", point2 = "28.013549";
        protected void Page_Load(object sender, EventArgs e)
        {
            Model.manager model = GetAdminInfo(); //取得当前管理员信息

            string _action = DTRequest.GetQueryString("action");
            if (!string.IsNullOrEmpty(_action) && _action == DTEnums.ActionEnum.Edit.ToString())
            {
                ChkAdminLevel("electricity_list", DTEnums.ActionEnum.Edit.ToString()); //检查权限
                tbequipmentId.Enabled = false;
                this.action = DTEnums.ActionEnum.Edit.ToString();//修改类型
                if (!int.TryParse(Request.QueryString["id"] as string, out this.id))
                {
                    JscriptMsg("传输参数不正确！", "back");
                    return;
                }
                if (!new BLL.dt_item().Exists(this.id))
                {
                    JscriptMsg("记录不存在或已被删除！", "back");
                    return;
                }
                else if (model.role_type != 1)
                {
                    Model.dt_item itemmodel = new BLL.dt_item().GetModel(this.id);
                    if (itemmodel.user_id != model.id)
                    {
                        JscriptMsg("记录不存在或已被删除！", "back");
                        return;
                    }
                }
            }else
            {
                ChkAdminLevel("electricity_list", DTEnums.ActionEnum.Add.ToString()); //检查权限
            }
            if (!Page.IsPostBack)
            {
                string zb = "";
                string elng = DTRequest.GetQueryStringValue("elng", "");
                string elat = DTRequest.GetQueryStringValue("elat", "");
                if (!string.IsNullOrEmpty(elat) && !string.IsNullOrEmpty(elng))
                {
                    zb = elng.Replace("D", ".") + "," + elat.Replace("D", ".");
                }

                //BLL.dt_area_code bll = new BLL.dt_area_code
                ddlArea.DataTextField = "name";
                ddlArea.DataValueField = "code";
                ddlArea.DataSource = ManagePage.arealist;
                ddlArea.DataBind();


                BLL.manager bll = new BLL.manager();
                ddlUser.DataTextField = "real_name";
                ddlUser.DataValueField = "id";
                ddlUser.DataSource = bll.GetList(9999,""," id asc");
                ddlUser.DataBind();
                ListItem li = new ListItem();
                li.Value = "";
                li.Text = "请选择区域";
                ddlArea.Items.Insert(0,li);
                li.Value = "";
                li.Text = "请选择用户";
                ddlUser.Items.Insert(0, li);

                if (this.action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id,zb);
                }else
                {
                    if (!string.IsNullOrEmpty(zb))
                    {
                        tbPosition.Text = zb;
                    }
                }
            }
        }

        #region 赋值操作=================================
        private void ShowInfo(int _id,string zb)
        {
            BLL.dt_item bll = new BLL.dt_item();
            Model.dt_item model = bll.GetModel(_id);
            if (model.state == 1)
            {
                cbIsLock.Checked = false;
            }
            else
            {
                cbIsLock.Checked = true;

            }
            
            tbName.Text = model.name;
            tbequipmentId.Text = model.onenetnum;
            ddlUser.SelectedValue = model.user_id.ToString();
            ddlArea.SelectedValue = model.area_code;
            tbtrailerAI.Text = model.trailerAI.ToString();
            //tbwarningAI.Text = model.warningAI.ToString();
            tbtrailerBI.Text = model.trailerBI.ToString();
            //tbwarningBI.Text = model.warningBI.ToString();
            tbtrailerCI.Text = model.trailerCI.ToString();
            //tbwarningCI.Text = model.warningCI.ToString();
            tbtrailerLI.Text = model.trailerLI.ToString();
            //tbwarningLI.Text = model.warningLI.ToString();
            tbtrailerOneTemperature.Text = model.trailerOneTemperature.ToString();
            //tbwarningOneTemperature.Text = model.warningOneTemperature.ToString();
            tbtrailerTwoTemperature.Text = model.trailerTwoTemperature.ToString();
            //tbwarningTwoTemperature.Text = model.warningTwoTemperature.ToString();
            tbtrailerThreeTemperature.Text = model.trailerThreeTemperature.ToString();
            //tbwarningThreeTemperature.Text = model.warningThreeTemperature.ToString();
            tbtrailerFourTemperature.Text = model.trailerFourTemperature.ToString();
            //tbwarningFourTemperature.Text = model.warningFourTemperature.ToString();
            tbTrailerU1.Text = model.trailerAV.ToString();
            tbTrailerU2.Text = model.trailerBV.ToString();
            tbTrailerU3.Text = model.trailerCV.ToString();
            tbPosition.Text = model.position;
            if (zb != "")
            {
                tbPosition.Text = zb;
            }
            

        }
        #endregion

        #region 增加操作=================================
        private int DoAdd()
        {
            Model.dt_item model = new Model.dt_item();
            BLL.dt_item bll = new BLL.dt_item();
            if (cbIsLock.Checked == true)
            {
                model.state = 2;
            }
            else
            {
                model.state = 1;

            }

            model.addr = "";
            model.addtime = DateTime.Now;
            model.area_code = ddlArea.SelectedValue;
            model.name = tbName.Text.Trim();
            model.onenetnum = tbequipmentId.Text.Trim();
            model.position = "";
            model.remarks = "";
            model.trailerAI = double.Parse(tbtrailerAI.Text.Trim());
            //model.warningAI = double.Parse(tbwarningAI.Text.Trim());
            model.trailerBI = double.Parse(tbtrailerBI.Text.Trim());
           // model.warningBI = double.Parse(tbwarningBI.Text.Trim());
            model.trailerCI = double.Parse(tbtrailerCI.Text.Trim());
           // model.warningCI = double.Parse(tbwarningCI.Text.Trim());
            model.trailerLI = double.Parse(tbtrailerLI.Text.Trim());
           // model.warningLI = double.Parse(tbwarningLI.Text.Trim());
            model.trailerOneTemperature = double.Parse(tbtrailerOneTemperature.Text.Trim());
           // model.warningOneTemperature = double.Parse(tbwarningOneTemperature.Text.Trim());
            model.trailerTwoTemperature = double.Parse(tbtrailerTwoTemperature.Text.Trim());
           // model.warningTwoTemperature = double.Parse(tbwarningTwoTemperature.Text.Trim());
            model.trailerThreeTemperature = double.Parse(tbtrailerThreeTemperature.Text.Trim());
           // model.warningThreeTemperature = double.Parse(tbwarningThreeTemperature.Text.Trim());
            model.trailerFourTemperature = double.Parse(tbtrailerFourTemperature.Text.Trim());
            // model.warningFourTemperature = double.Parse(tbwarningFourTemperature.Text.Trim());
            model.trailerAV = double.Parse(tbTrailerU1.Text.Trim());
            model.trailerBV = double.Parse(tbTrailerU2.Text.Trim());
            model.trailerCV = double.Parse(tbTrailerU3.Text.Trim());
            model.position = tbPosition.Text.Trim();

            model.user_id = int.Parse(ddlUser.SelectedValue);

            int newid = bll.Add(model);
            if (newid > 0)
            {
                AddAdminLog("item_add", "添加设备:" + model.id + "_" + model.name+"成功"); //记录日志
                return newid;
            }
            return 0;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            bool result = false;
            BLL.dt_item bll = new BLL.dt_item();
            Model.dt_item model = bll.GetModel(_id);

            if (cbIsLock.Checked == true)
            {
                model.state = 2;
            }
            else
            {
                model.state = 1;

            }

            model.addr = "";
            model.area_code = ddlArea.SelectedValue;
            model.name = tbName.Text.Trim();
            //model.onenetnum = tbequipmentId.Text.Trim();
            model.position = "";
            model.remarks = "";
            model.trailerAI = double.Parse(tbtrailerAI.Text.Trim());
           // model.warningAI = double.Parse(tbwarningAI.Text.Trim());
            model.trailerBI = double.Parse(tbtrailerBI.Text.Trim());
           // model.warningBI = double.Parse(tbwarningBI.Text.Trim());
            model.trailerCI = double.Parse(tbtrailerCI.Text.Trim());
            //model.warningCI = double.Parse(tbwarningCI.Text.Trim());
            model.trailerLI = double.Parse(tbtrailerLI.Text.Trim());
           // model.warningLI = double.Parse(tbwarningLI.Text.Trim());
            model.trailerOneTemperature = double.Parse(tbtrailerOneTemperature.Text.Trim());
           // model.warningOneTemperature = double.Parse(tbwarningOneTemperature.Text.Trim());
            model.trailerTwoTemperature = double.Parse(tbtrailerTwoTemperature.Text.Trim());
           // model.warningTwoTemperature = double.Parse(tbwarningTwoTemperature.Text.Trim());
            model.trailerThreeTemperature = double.Parse(tbtrailerThreeTemperature.Text.Trim());
            //model.warningThreeTemperature = double.Parse(tbwarningThreeTemperature.Text.Trim());
            model.trailerFourTemperature = double.Parse(tbtrailerFourTemperature.Text.Trim());
            //model.warningFourTemperature = double.Parse(tbwarningFourTemperature.Text.Trim());
            model.trailerAV = double.Parse(tbTrailerU1.Text.Trim());
            model.trailerBV = double.Parse(tbTrailerU2.Text.Trim());
            model.trailerCV = double.Parse(tbTrailerU3.Text.Trim());
            model.position = tbPosition.Text.Trim();
            model.user_id = int.Parse(ddlUser.SelectedValue);


            if (bll.Update(model))
            {
                AddAdminLog("package_edit", "修改设备:" + model.id + "_" + model.name+"成功"); //记录日志
                result = true;
            }

            return result;
        }
        #endregion

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
            {
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误！", "");
                    return;
                }
                JscriptMsg_Parent("修改设备信息成功", $"electricity_add.aspx?action={DTEnums.ActionEnum.Edit}&id={this.id}");
            }
            else //添加
            {
                BLL.dt_item bll = new BLL.dt_item();
                if (bll.GetRecordCount($"onenetnum='{tbequipmentId.Text.Replace("'","").Trim()}' ") >0)
                {
                    JscriptMsg("设备ID已存在！", string.Empty);
                    return;
                }
                int newid = 0;
                if ((newid=DoAdd())>0)
                {
                    JscriptMsg("保存过程中发生错误！", "");
                    return;
                }
                JscriptMsg_Parent("添加设备信息成功", $"electricity_add.aspx?action={DTEnums.ActionEnum.Edit}&id={newid}");
            }
        }
    }
}