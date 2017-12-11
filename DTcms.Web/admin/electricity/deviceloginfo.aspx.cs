using System;
using System.Linq;
using DTcms.Common;

namespace DTcms.Web.admin.electricity
{
    public partial class deviceloginfo : ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Model.manager model = GetAdminInfo(); //取得当前管理员信息

            int type = DTRequest.GetQueryIntValue("type",1);
            string id = DTRequest.GetQueryString("id");
            ChkAdminLevel("deviceloginfo", DTEnums.ActionEnum.Edit.ToString()); //检查权限

            var h = new Model.dt_device_h { id=id}.Get(type);
            if (h==null)
            {
                JscriptMsg("记录不存在或已被删除！", "back");
                return;
            }
            ShowInfo(h);
        }

        #region 赋值操作=================================
        private void ShowInfo(Model.dt_device_h_ex h)
        {
            this.lblDeviceSN.Text = h.device_sn;
            this.lblDeviceName.Text = h.device_name;
            this.lblPosition.Text = h.position;
            this.lblAddr.Text = h.addr;
            this.lblRemark.Text = h.remark;
            if (!string.IsNullOrEmpty(h.photolist))
            {
                this.rpt.DataSource = h.photolist.Replace("[", "").Replace("]", "").Replace(" ", "").Split(',').Select(p=>new { picid=p});
                this.rpt.DataBind();
            }
        }
        #endregion
    }
}