using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Configuration;
using System.Xml;
using DTcms.Common;

namespace DTcms.Web.UI
{
    /// <summary>
    /// DTcms的HttpModule类
    /// </summary>
    public class HttpModule : System.Web.IHttpModule
    {
        /// <summary>
        /// 实现接口的Init方法
        /// </summary>
        /// <param name="context"></param>
        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(ReUrl_BeginRequest);
        }

        /// <summary>
        /// 实现接口的Dispose方法
        /// </summary>
        public void Dispose()
        { }

        #region 页面请求事件处理===================================
        /// <summary>
        /// 页面请求事件处理
        /// </summary>
        /// <param name="sender">事件的源</param>
        /// <param name="e">包含事件数据的 EventArgs</param>
        private void ReUrl_BeginRequest(object sender, EventArgs e)
        {
            
        }
        #endregion

        #region 辅助方法(私有)=====================================
        /// <summary>
        /// 获取URL的虚拟目录(除安装目录)
        /// </summary>
        /// <param name="webPath">网站安装目录</param>
        /// <param name="requestPath">当前页面，包含目录</param>
        /// <returns>String</returns>
        private string GetFirstPath(string webPath, string requestPath)
        {
            if (requestPath.StartsWith(webPath))
            {
                string tempStr = requestPath.Substring(webPath.Length);
                if (tempStr.IndexOf("/") > 0)
                {
                    return tempStr.Substring(0, tempStr.IndexOf("/")).ToLower();
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取当前域名包含的站点目录
        /// </summary>
        /// <param name="requestDomain">获取的域名(含端口号)</param>
        /// <returns>String</returns>
        private string GetCurrDomainPath(string requestDomain)
        {
            //当前域名是否存在于站点目录列表
            if (SiteDomains.GetSiteDomains().Paths.ContainsValue(requestDomain))
            {
                return SiteDomains.GetSiteDomains().Domains[requestDomain];
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取当前页面包含的站点目录
        /// </summary>
        /// <param name="webPath">网站安装目录</param>
        /// <param name="requestPath">获取的页面，包含目录</param>
        /// <returns>String</returns>
        private string GetCurrPagePath(string webPath, string requestPath)
        {
            //获取URL的虚拟目录(除安装目录)
            string requestFirstPath = GetFirstPath(webPath, requestPath);
            if (requestFirstPath != string.Empty && SiteDomains.GetSiteDomains().Paths.ContainsKey(requestFirstPath))
            {
                return requestFirstPath;
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取站点的目录
        /// </summary>
        /// <param name="webPath">网站安装目录</param>
        /// <param name="requestPath">获取的页面，包含目录</param>
        /// <param name="requestDomain">获取的域名(含端口号)</param>
        /// <returns>String</returns>
        private string GetSitePath(string webPath, string requestPath, string requestDomain)
        {
            //获取当前域名包含的站点目录
            string domainPath = GetCurrDomainPath(requestDomain);
            if (domainPath != string.Empty)
            {
                return domainPath;
            }
            // 获取当前页面包含的站点目录
            string pagePath = GetCurrPagePath(webPath, requestPath);
            if (pagePath != string.Empty)
            {
                return pagePath;
            }
            return SiteDomains.GetSiteDomains().DefaultPath;
        }

        /// <summary>
        /// 遍历指定路径目录，如果缓存存在则直接返回
        /// </summary>
        /// <param name="cacheKey">缓存KEY</param>
        /// <param name="dirPath">指定路径</param>
        /// <returns>ArrayList</returns>
        private ArrayList GetSiteDirs(string cacheKey, string dirPath)
        {
            ArrayList _cache = CacheHelper.Get<ArrayList>(cacheKey); //从续存中取
            if (_cache == null)
            {
                _cache = new ArrayList();
                DirectoryInfo dirInfo = new DirectoryInfo(Utils.GetMapPath(dirPath));
                foreach (DirectoryInfo dir in dirInfo.GetDirectories())
                {
                    _cache.Add(dir.Name.ToLower());
                }
                CacheHelper.Insert(cacheKey, _cache, 2); //存入续存，弹性2分钟
            }
            return _cache;
        }

        /// <summary>
        /// 遍历指定路径的子目录，检查是否匹配
        /// </summary>
        /// <param name="cacheKey">缓存KEY</param>
        /// <param name="webPath">网站安装目录，以“/”结尾</param>
        /// <param name="dirPath">指定的路径，以“/”结尾</param>
        /// <param name="requestPath">获取的URL全路径</param>
        /// <returns>布尔值</returns>
        private bool IsDirExist(string cacheKey, string webPath, string dirPath, string requestPath)
        {
            ArrayList list = GetSiteDirs(cacheKey, dirPath); //取得所有目录
            string requestFirstPath = string.Empty; //获得当前页面除站点安装目录的虚拟目录名称
            string tempStr = string.Empty; //临时变量
            if (requestPath.StartsWith(webPath))
            {
                tempStr = requestPath.Substring(webPath.Length);
                if (tempStr.IndexOf("/") > 0)
                {
                    requestFirstPath = tempStr.Substring(0, tempStr.IndexOf("/"));
                }
            }
            if (requestFirstPath.Length > 0 && list.Contains(requestFirstPath.ToLower()))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 截取安装目录和站点目录部分
        /// </summary>
        /// <param name="webPath">站点安装目录</param>
        /// <param name="sitePath">站点目录</param>
        /// <param name="requestPath">当前页面路径</param>
        /// <returns>String</returns>
        private string CutStringPath(string webPath, string sitePath, string requestPath)
        {
            if (requestPath.StartsWith(webPath))
            {
                requestPath = requestPath.Substring(webPath.Length);
            }
            sitePath += "/";
            if (requestPath.StartsWith(sitePath))
            {
                requestPath = requestPath.Substring(sitePath.Length);
            }
            return "/" + requestPath;
        }

        #endregion

    }

    #region 站点URL字典信息类===================================
    /// <summary>
    /// 站点伪Url信息类
    /// </summary>
    public class SiteUrls
    {
        //属性声明
        private static object lockHelper = new object();
        private static volatile SiteUrls instance = null;
        private ArrayList _urls;
        public ArrayList Urls
        {
            get { return _urls; }
            set { _urls = value; }
        }
        //构造函数
        private SiteUrls()
        {
            
        }
        //返回URL字典
        public static SiteUrls GetUrls()
        {
            SiteUrls _cache = CacheHelper.Get<SiteUrls>(DTKeys.CACHE_SITE_HTTP_MODULE);
            lock (lockHelper)
            {
                if (_cache == null)
                {
                    CacheHelper.Insert(DTKeys.CACHE_SITE_HTTP_MODULE, new SiteUrls(), Utils.GetXmlMapPath(DTKeys.FILE_URL_XML_CONFING));
                    instance = CacheHelper.Get<SiteUrls>(DTKeys.CACHE_SITE_HTTP_MODULE);
                }
            }
            return instance;
        }

    }
    #endregion

    #region 站点绑定域名信息类==================================
    /// <summary>
    /// 域名字典
    /// </summary>
    public class SiteDomains
    {
        private static object lockHelper = new object();
        private static volatile SiteDomains instance = null;
        //默认站点目录
        private string _default_path = string.Empty;
        public string DefaultPath
        {
            get { return _default_path; }
            set { _default_path = value; }
        }
        //站点目录列表
        private Dictionary<string, string> _paths;
        public Dictionary<string, string> Paths
        {
            get { return _paths; }
            set { _paths = value; }
        }
        //站点域名列表
        private Dictionary<string, string> _domains;
        public Dictionary<string, string> Domains
        {
            get { return _domains; }
            set { _domains = value; }
        }
        //构造函数
        public SiteDomains()
        {
            
        }
        /// <summary>
        /// 返回域名字典
        /// </summary>
        public static SiteDomains GetSiteDomains()
        {
            SiteDomains _cache = CacheHelper.Get<SiteDomains>(DTKeys.CACHE_SITE_HTTP_DOMAIN);
            lock (lockHelper)
            {
                if (_cache == null)
                {
                    CacheHelper.Insert(DTKeys.CACHE_SITE_HTTP_DOMAIN, new SiteDomains(), 10);
                    instance = CacheHelper.Get<SiteDomains>(DTKeys.CACHE_SITE_HTTP_DOMAIN);
                }
            }
            return instance;
        }
    }
    #endregion
}