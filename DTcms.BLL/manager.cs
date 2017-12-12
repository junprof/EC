using System;
using System.Data;
using System.Collections.Generic;
using DTcms.Common;

namespace DTcms.BLL
{
    /// <summary>
    /// 管理员信息表
    /// </summary>
    public partial class manager
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息
        private readonly DAL.manager dal;
        public manager()
        {
            dal = new DAL.manager(siteConfig.sysdatabaseprefix);
        }

        #region 基本方法=============================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// 查询用户名是否存在
        /// </summary>
        public bool Exists(string user_name)
        {
            return dal.Exists(user_name);
        }

        /// <summary>
        /// 根据用户名取得Salt
        /// </summary>
        public string GetSalt(string user_name)
        {
            return dal.GetSalt(user_name);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.manager model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.manager model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            return dal.Delete(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.manager GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 根据用户名密码返回一个实体
        /// </summary>
        public Model.manager GetModel(string user_name, string password)
        {
            return dal.GetModel(user_name, password);
        }
        public Model.manager GetModelByUsername(string user_name)
        {
            return dal.GetModel(user_name);
        }

        /// <summary>
        /// 根据用户名密码返回一个实体
        /// </summary>
        public Model.manager GetModel(string user_name, string password, bool is_encrypt)
        {
            //检查一下是否需要加密
            if (is_encrypt)
            {
                //先取得该用户的随机密钥
                string salt = dal.GetSalt(user_name);
                if (string.IsNullOrEmpty(salt))
                {
                    return null;
                }
                //把明文进行加密重新赋值
                password = DESEncrypt.Encrypt(password, salt);
            }
            return dal.GetModel(user_name, password);
        }
        public Model.BaseResponse ChangePwd(string username,string password,string phone)
        {
            Model.BaseResponse res = new Model.BaseResponse();
            try {
                var m = dal.GetModel(username);
                if (m.telephone == phone)
                {
                    string salt = m.salt;
                    string newpassword = DESEncrypt.Encrypt(password, salt);
                    var r = dal.ChangePwd(username, newpassword);
                    if (!r)
                    {
                        res.error = 1;
                        res.data = "修改失败";
                    }
                    else
                    {
                        res.error = 0;
                        res.data = "修改成功";
                    }
                }
                else
                {
                    res.error = 1;
                    res.data = "手机号非帐号绑定的号码";
                }
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
                res.error = 4;
                res.data = ex.Message;
            }
            return res;
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        public Model.manager GetModel(string weichatid)
        {
            return dal.GetModelByWeichatId(weichatid);
        }
        public bool BindWeichat(int id, string weichatid)
        {
            return dal.SetWeichatid(id, weichatid);
        }
        public bool UnBindWeichat(int id, string weichatid)
        {
            return dal.DeleteWeichatBind(id, weichatid);
        }
        public bool SetAvatar(string id, string avatar)
        {
            return dal.SetAvatar(id, avatar);
        }
        #endregion
    }
}