using DTcms.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace DTcms.Web.Models
{
    public class UserSession: IRequiresSessionState
    {
        #region Property
        public int USERID { get; set; }
        public string USERNAME { get; set; }
        public string REALNAME { get; set; }
        public int ROLEID { get; set; }
        public string ROLENAME { get; set; }
        public int ROLETYPE { get; set; }
        public string PHONE { get; set; }
        public string EMAIL { get; set; }
        public int ISLOCKED { get; set; }
        public DateTime? UPDATETIME { get; set; }
        public string UNITID { get; set; }
        public string UNITNAME { get; set; }
        public string SESSIONID { get; set; }
        public string avatar { get; set; }
        public string LOGFLAG { get; set; }
        /// <summary>
        /// 1 WEB 2 ANDROID 3 IOS
        /// </summary>
        public int? Plateform { get; set; }
        public string Version { get; set; }
        public string ChannelID { get; set; }
        public DateTime? LASTLOGINTIME { get; set; }
        #endregion
        void ManagerToSession(UserSession us, Model.manager m)
        {
            us.USERID = m.id;
            us.USERNAME = m.user_name;
            us.PHONE = m.telephone;
            us.REALNAME = m.real_name;
            us.EMAIL = m.email;
            us.ISLOCKED = m.is_lock;
            us.ROLEID = m.role_id;
            us.ROLETYPE = m.role_type;
            us.UNITID = m.UNITID;
            us.UNITNAME = m.UNITNAME;
            us.ROLENAME = m.ROLENAME;
            us.avatar = m.avatar;
        }
        public static string GetClientIP()
        {
            if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            {
                return HttpContext.Current.Request.
                    ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            else
            {
                return HttpContext.Current.Request.
                    ServerVariables["REMOTE_ADDR"];
            }
        }
        public UserSession Login(string username, string password, out string msg)
        {
            msg = string.Empty;
            try {
                var m = new BLL.manager().GetModel(username, password, true);
                if (m == null)
                {
                    throw new Model.UserException("用户名或密码错误");
                }
                if (m.is_lock == 1)
                {
                    throw new Model.UserException("帐号已锁定");
                }
                ManagerToSession(this, m);
                SESSIONID = Guid.NewGuid().ToString().Replace("-", "");

                Utils.WriteCookie("DTRememberName", m.user_name, 14400);
                Utils.WriteCookie("AdminName", "DTcms", m.user_name);
            }
            catch (Model.UserException ex)
            {
                msg = ex.Message;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return this;
        }
        public UserSession Login(string username, out string msg)
        {
            msg = string.Empty;
            try
            {
                var m = new BLL.manager().GetModelByUsername(username);
                if (m == null)
                {
                    throw new Model.UserException("帐号不存在");
                }
                if (m.is_lock == 1)
                {
                    throw new Model.UserException("帐号已锁定");
                }
                ManagerToSession(this, m);
                //SESSIONID = Guid.NewGuid().ToString().Replace("-", "");

                Utils.WriteCookie("DTRememberName", m.user_name, 14400);
                Utils.WriteCookie("AdminName", "DTcms", m.user_name);
            }
            catch (Model.UserException ex)
            {
                msg = ex.Message;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return this;
        }
        /// <summary>
        /// 将session保存到缓存中
        /// </summary>
        /// <param name="userSession"></param>
        public void SetUserSession(UserSession userSession)
        {
            if (userSession != null&&!string.IsNullOrEmpty(userSession.SESSIONID))
            {
                BLL.Cache.DataCache.Add(userSession.SESSIONID, userSession, new DateTimeOffset(DateTime.Now.AddMinutes(new BLL.siteconfig().loadConfig().websessioncache)));
                HttpContext.Current.Session[DTKeys.SESSION_USER_INFO] = userSession;
                //添加TB_USER_SESSION
                new Model.dt_session()
                {
                    LastLoginTime = userSession.LASTLOGINTIME,
                    SessionID = userSession.SESSIONID,
                    LOGFLAG = userSession.LOGFLAG,
                    UserName = userSession.USERNAME,
                    Version = userSession.Version,
                    Plateform=userSession.Plateform,
                    ChannelID = userSession.ChannelID,
                }.Merge();
            }
        }
        /// <summary>
        /// 根据session 获取缓存中的session
        ///   app
        /// </summary>
        /// <param name="sessionid"></param>
        /// <returns></returns>
        public UserSession GetUserSession(string sessionid)
        {
            if (string.IsNullOrEmpty(sessionid))
            {
                return null;
            }
            UserSession userSession = BLL.Cache.DataCache.Get(sessionid) as UserSession;
            if (userSession == null) // attempt access db session
            {
                var dbsession = new Model.dt_session().Get(sessionid);
                if (dbsession == null)
                {
                    return null;
                }
                else
                {
                    // session timeout
                    if (dbsession.LastLoginTime < DateTime.Now.AddMinutes(-1 * new BLL.siteconfig().loadConfig().websessioncache))
                        return null;
                    string msg = string.Empty;
                    userSession = Login(dbsession.UserName, out msg);
                    userSession.SESSIONID = sessionid;
                    userSession.LASTLOGINTIME = dbsession.LastLoginTime;
                    userSession.Plateform = dbsession.Plateform;
                    userSession.Version = dbsession.Version;
                }
            }
            //if (userSession != null)
            //{
            //    userSession.LASTLOGINTIME = DateTime.Now;
            //    new Model.dt_session()
            //    {
            //        LastLoginTime = userSession.LASTLOGINTIME,
            //        SessionID = userSession.SESSIONID,
            //        LOGFLAG = userSession.LOGFLAG,
            //        UserName = userSession.USERNAME
            //    }.Merge();
            //    BLL.Cache.MemoryCacheManager.Add(userSession.SESSIONID, userSession, new DateTimeOffset(DateTime.Now.AddMinutes(new BLL.siteconfig().loadConfig().websessioncache)));
            //    SetUserSession(userSession);
            //}
            return userSession;
        }
        /// <summary>
        /// 获取当前session里的用户信息
        /// </summary>
        /// <returns></returns>
        public static UserSession GetCurrentUser()
        {
            var ouser = HttpContext.Current.Session[DTKeys.SESSION_USER_INFO] as UserSession;
            if (ouser != null)
            {
                if(!new BLL.manager().Exists(ouser.USERNAME))
                {
                    return null;
                }
            }
            else // attempt from db
            {

            }
            return ouser;
        }
    }
}