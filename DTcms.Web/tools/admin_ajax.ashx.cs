using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.SessionState;
using DTcms.Web.UI;
using DTcms.Common;
using System.Linq;

namespace DTcms.Web.tools
{
    /// <summary>
    /// admin_ajax 的摘要说明
    /// </summary>
    public class admin_ajax : IHttpHandler, IRequiresSessionState
    {
        Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //系统配置信息

        public void ProcessRequest(HttpContext context)
        {
            //检查管理员是否登录
            if (!new ManagePage().IsAdminLogin())
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"尚未登录或已超时，请登录后操作！\"}");
                return;
            }
            //取得处事类型
            string action = DTRequest.GetQueryString("action");
            switch (action)
            {
                case "get_map": //验证用户名
                    string keywords = DTRequest.GetFormStringValue("keywords", "");
                    string areacode = DTRequest.GetFormStringValue("areacode", "");
                    int page = DTRequest.GetFormIntValue("page", 1);
                    int pageSize = DTRequest.GetFormIntValue("pageSize", 1);
                    string returnstr = getmap(areacode, keywords, page, pageSize);
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(returnstr);
                    break;
                case "navigation_validate": //验证导航菜单别名是否重复
                    navigation_validate(context);
                    break;
                case "manager_validate": //验证管理员用户名是否重复
                    manager_validate(context);
                    break;
                case "get_navigation_list": //获取后台导航字符串
                    get_navigation_list(context);
                    break;
                case "getJsonImgData"://获取图表数据
                    int id = DTRequest.GetFormIntValue("id", 0);
                    string Date = DTRequest.GetFormStringValue("Date");
                    getJsonImgData(context, id, Date);
                    break;

            }
        }

        #region 验证导航菜单别名是否重复========================
        private void navigation_validate(HttpContext context)
        {
            string navname = DTRequest.GetString("param");
            string old_name = DTRequest.GetString("old_name");
            if (string.IsNullOrEmpty(navname))
            {
                context.Response.Write("{ \"info\":\"该导航别名不可为空！\", \"status\":\"n\" }");
                return;
            }
            if (navname.ToLower() == old_name.ToLower())
            {
                context.Response.Write("{ \"info\":\"该导航别名可使用\", \"status\":\"y\" }");
                return;
            }
            //检查保留的名称开头
            if (navname.ToLower().StartsWith("channel_"))
            {
                context.Response.Write("{ \"info\":\"该导航别名系统保留，请更换！\", \"status\":\"n\" }");
                return;
            }
            BLL.navigation bll = new BLL.navigation();
            if (bll.Exists(navname))
            {
                context.Response.Write("{ \"info\":\"该导航别名已被占用，请更换！\", \"status\":\"n\" }");
                return;
            }
            context.Response.Write("{ \"info\":\"该导航别名可使用\", \"status\":\"y\" }");
            return;
        }
        #endregion

        #region 验证管理员用户名是否重复========================
        private void manager_validate(HttpContext context)
        {
            string user_name = DTRequest.GetString("param");
            if (string.IsNullOrEmpty(user_name))
            {
                context.Response.Write("{ \"info\":\"请输入用户名\", \"status\":\"n\" }");
                return;
            }
            BLL.manager bll = new BLL.manager();
            if (bll.Exists(user_name))
            {
                context.Response.Write("{ \"info\":\"用户名已被占用，请更换！\", \"status\":\"n\" }");
                return;
            }
            context.Response.Write("{ \"info\":\"用户名可使用\", \"status\":\"y\" }");
            return;
        }
        #endregion

        #region 获取后台导航字符串==============================
        private void get_navigation_list(HttpContext context)
        {
            Model.manager adminModel = new ManagePage().GetAdminInfo(); //获得当前登录管理员信息
            if (adminModel == null)
            {
                return;
            }
            Model.manager_role roleModel = new BLL.manager_role().GetModel(adminModel.role_id); //获得管理角色信息
            if (roleModel == null)
            {
                return;
            }
            DataTable dt = new BLL.navigation().GetList(0, DTEnums.NavigationEnum.System.ToString());
            this.get_navigation_childs(context, dt, 0, roleModel.role_type, roleModel.manager_role_values);

        }
        private void get_navigation_childs(HttpContext context, DataTable oldData, int parent_id, int role_type, List<Model.manager_role_value> ls)
        {
            DataRow[] dr = oldData.Select("parent_id=" + parent_id);
            bool isWrite = false; //是否输出开始标签
            for (int i = 0; i < dr.Length; i++)
            {
                //检查是否显示在界面上====================
                bool isActionPass = true;
                if (int.Parse(dr[i]["is_lock"].ToString()) == 1)
                {
                    isActionPass = false;
                }
                //检查管理员权限==========================
                if (isActionPass && role_type > 1)
                {
                    string[] actionTypeArr = dr[i]["action_type"].ToString().Split(',');
                    foreach (string action_type_str in actionTypeArr)
                    {
                        //如果存在显示权限资源，则检查是否拥有该权限
                        if (action_type_str == "Show")
                        {
                            Model.manager_role_value modelt = ls.Find(p => p.nav_name == dr[i]["name"].ToString() && p.action_type == "Show");
                            if (modelt == null)
                            {
                                isActionPass = false;
                            }
                        }
                    }
                }
                //如果没有该权限则不显示
                if (!isActionPass)
                {
                    if (isWrite && i == (dr.Length - 1) && parent_id > 0)
                    {
                        context.Response.Write("</ul>\n");
                    }
                    continue;
                }
                //如果是顶级导航
                if (parent_id == 0)
                {
                    context.Response.Write("<div class=\"list-group\">\n");
                    context.Response.Write("<h1 title=\"" + dr[i]["sub_title"].ToString() + "\">");
                    if (!string.IsNullOrEmpty(dr[i]["icon_url"].ToString().Trim()))
                    {
                        context.Response.Write("<img src=\"" + dr[i]["icon_url"].ToString() + "\" />");
                    }
                    context.Response.Write("</h1>\n");
                    context.Response.Write("<div class=\"list-wrap\">\n");
                    context.Response.Write("<h2>" + dr[i]["title"].ToString() + "<i></i></h2>\n");
                    //调用自身迭代
                    this.get_navigation_childs(context, oldData, int.Parse(dr[i]["id"].ToString()), role_type, ls);
                    context.Response.Write("</div>\n");
                    context.Response.Write("</div>\n");
                }
                else //下级导航
                {
                    if (!isWrite)
                    {
                        isWrite = true;
                        context.Response.Write("<ul>\n");
                    }
                    context.Response.Write("<li>\n");
                    context.Response.Write("<a navid=\"" + dr[i]["name"].ToString() + "\"");
                    if (!string.IsNullOrEmpty(dr[i]["link_url"].ToString()))
                    {
                        if (int.Parse(dr[i]["channel_id"].ToString()) > 0)
                        {
                            context.Response.Write(" href=\"" + dr[i]["link_url"].ToString() + "?channel_id=" + dr[i]["channel_id"].ToString() + "\" target=\"mainframe\"");
                        }
                        else
                        {
                            context.Response.Write(" href=\"" + dr[i]["link_url"].ToString() + "\" target=\"mainframe\"");
                        }
                    }
                    if (!string.IsNullOrEmpty(dr[i]["icon_url"].ToString()))
                    {
                        context.Response.Write(" icon=\"" + dr[i]["icon_url"].ToString() + "\"");
                    }
                    context.Response.Write(" target=\"mainframe\">\n");
                    context.Response.Write("<span>" + dr[i]["title"].ToString() + "</span>\n");
                    context.Response.Write("</a>\n");
                    //调用自身迭代
                    this.get_navigation_childs(context, oldData, int.Parse(dr[i]["id"].ToString()), role_type, ls);
                    context.Response.Write("</li>\n");

                    if (i == (dr.Length - 1))
                    {
                        context.Response.Write("</ul>\n");
                    }
                }
            }
        }
        #endregion


        public void getJsonImgData(HttpContext context, int id, string Date)
        {
            BLL.dt_dimensioninfo bll = new BLL.dt_dimensioninfo();

            if (string.IsNullOrEmpty(Date))
            {
                Date = DateTime.Now.ToString("yyyy-MM-dd");
            }

            DateTime dt;
            string sr = Date + " 00:00:00";
            if (!Utils.IsDate(sr, out dt))
            {
                Date = DateTime.Now.ToString("yyyy-MM-dd");
                dt = DateTime.Parse(Date + " 00:00:00");
            }
            var q = bll.GetList($"hid={id} and updatetime between '" + dt.ToString("yyyy-MM-dd 00:00:00") + "' and '" + dt.ToString("yyyy-MM-dd 23:59:59") + "') ");
            context.Response.Write(JsonHelper.ObjectToJSON(q));

        }

        public string getmap(string areacode, string keywords, int page, int pageSize)
        {
            return mapToJson("", areacode, keywords, page, pageSize);
        }

        public string mapToJson(string jsonName, string areacode, string keywords, int page, int pageSize)
        {
            ManagePage pg = new ManagePage();
            var model = pg.GetAdminInfo();
            string strwhere = " 1=1 ";
            StringBuilder strTemp = new StringBuilder();
            keywords = keywords.Replace("'", "");
            if (model.role_type != 1)
            {
                strwhere += $" and user_id={model.id} ";
            }
            if (!string.IsNullOrEmpty(areacode))
            {
                strwhere += $" and area_code='{areacode.Replace("'", "")}' ";
            }
            if (!string.IsNullOrEmpty(keywords))
            {
                strwhere +=$" and (name like  '%{keywords}%' or onenetnum='{keywords}')";
            }
            BLL.dt_item bll = new BLL.dt_item();
            var itemlist = bll.GetModelList(strwhere+" order by addtime desc ");
            string jsonstr = string.Empty;
            if (itemlist != null && itemlist.Count > 0)
            {
                itemlist = itemlist.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                jsonstr = JsonHelper2.Serialize( itemlist.Select(p=>  new { id=p.id,sbid=p.onenetnum,title=p.name,gps=p.position,online=p.online?1:0}));
                //for (int i = 0; i < itemlist.Count; i++)
                //{
                //    Json.Append("{");
                //    Json.Append("id:\"" + itemlist[i].id + "\"");
                //    Json.Append(",");
                //    Json.Append("sbid:\"" + itemlist[i].onenetnum + "\"");
                //    Json.Append(",");
                //    Json.Append("title:\"" + itemlist[i].name + "\"");
                //    Json.Append(",");
                //    Json.Append("gps:\"" + itemlist[i].position + "\"");
                //    Json.Append(",");
                //    Json.Append("online:\"" + itemlist[i].online + "\"");
                //    Json.Append("}");
                //    if (i != itemlist.Count - 1)
                //    {
                //        Json.Append(",");
                //    }
                //}
            }


            return jsonstr;

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}