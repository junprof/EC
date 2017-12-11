using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Configuration;
using DTcms.Common;

namespace DTcms.Web.UI
{
    public partial class BasePage : System.Web.UI.Page
    {
        protected internal Model.siteconfig config = new BLL.siteconfig().loadConfig();
        /// <summary>
        /// 父类的构造函数
        /// </summary>
        public BasePage()
        {
            //是否关闭网站
            if (config.webstatus == 0)
            {
                //HttpContext.Current.Response.Redirect(linkurl("error", "?msg=" + Utils.UrlEncode(config.webclosereason)));
                return;
            }
            //取得站点信息
            //site = GetSiteModel();
            //抛出一个虚方法给继承重写
            ShowPage();
        }

        /// <summary>
        /// 页面处理虚方法
        /// </summary>
        protected virtual void ShowPage()
        {
            //虚方法代码
        }

        #region 辅助方法(私有)========================================
        /// <summary>
        /// 获取当前页面包含的站点目录
        /// </summary>
        private string GetFirstPath(string requestPath)
        {
            int indexNum = config.webpath.Length; //安装目录长度
            //如果包含安装目录和aspx目录也要过滤掉
            if (requestPath.StartsWith(config.webpath + DTKeys.DIRECTORY_REWRITE_ASPX + "/"))
            {
                indexNum = (config.webpath + DTKeys.DIRECTORY_REWRITE_ASPX + "/").Length;
            }
            string requestFirstPath = requestPath.Substring(indexNum);
            if (requestFirstPath.IndexOf("/") > 0)
            {
                requestFirstPath = requestFirstPath.Substring(0, requestFirstPath.IndexOf("/"));
            }
            if (requestFirstPath != string.Empty && SiteDomains.GetSiteDomains().Paths.ContainsKey(requestFirstPath))
            {
                return requestFirstPath;
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取链接的前缀
        /// </summary>
        /// <param name="requestPath">当前的URL地址</param>
        /// <param name="requestDomain">获得来源域名含端口号</param>
        /// <returns>String</returns>
        private string GetLinkStartString(string requestPath, string requestDomain)
        {
            string requestFirstPath = GetFirstPath(requestPath);//获得二级目录(不含站点安装目录)

            //检查是否与绑定的域名或者与默认频道分类的目录匹配
            if (SiteDomains.GetSiteDomains().Paths.ContainsValue(requestDomain))
            {
                return "/";
            }

            else if (requestFirstPath == string.Empty || requestFirstPath == SiteDomains.GetSiteDomains().DefaultPath)
            {
                return config.webpath;
            }
            else
            {
                return config.webpath + requestFirstPath + "/";
            }
        }

        /// <summary>
        /// 获取站点的目录
        /// </summary>
        /// <param name="requestPath">获取的页面，包含目录</param>
        /// <param name="requestDomain">获取的域名(含端口号)</param>
        /// <returns>String</returns>
        private string GetSitePath(string requestPath, string requestDomain)
        {
            //当前域名是否存在于站点目录列表
            if (SiteDomains.GetSiteDomains().Paths.ContainsValue(requestDomain))
            {
                return SiteDomains.GetSiteDomains().Domains[requestDomain];
            }

            // 获取当前页面包含的站点目录
            string pagePath = GetFirstPath(requestPath);
            if (pagePath != string.Empty)
            {
                return pagePath;
            }
            return SiteDomains.GetSiteDomains().DefaultPath;
        }


        /// <summary>
        /// 替换扩展名
        /// </summary>
        private string GetUrlExtension(string urlPage, string staticExtension)
        {
            return Utils.GetUrlExtension(urlPage, staticExtension);
        }

        #endregion
    }
}
