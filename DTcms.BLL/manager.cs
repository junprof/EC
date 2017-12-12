using System;
using System.Data;
using System.Collections.Generic;
using DTcms.Common;

namespace DTcms.BLL
{
    /// <summary>
    /// ����Ա��Ϣ��
    /// </summary>
    public partial class manager
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //���վ��������Ϣ
        private readonly DAL.manager dal;
        public manager()
        {
            dal = new DAL.manager(siteConfig.sysdatabaseprefix);
        }

        #region ��������=============================
        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// ��ѯ�û����Ƿ����
        /// </summary>
        public bool Exists(string user_name)
        {
            return dal.Exists(user_name);
        }

        /// <summary>
        /// �����û���ȡ��Salt
        /// </summary>
        public string GetSalt(string user_name)
        {
            return dal.GetSalt(user_name);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(Model.manager model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public bool Update(Model.manager model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public bool Delete(int id)
        {
            return dal.Delete(id);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public Model.manager GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// �����û������뷵��һ��ʵ��
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
        /// �����û������뷵��һ��ʵ��
        /// </summary>
        public Model.manager GetModel(string user_name, string password, bool is_encrypt)
        {
            //���һ���Ƿ���Ҫ����
            if (is_encrypt)
            {
                //��ȡ�ø��û��������Կ
                string salt = dal.GetSalt(user_name);
                if (string.IsNullOrEmpty(salt))
                {
                    return null;
                }
                //�����Ľ��м������¸�ֵ
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
                        res.data = "�޸�ʧ��";
                    }
                    else
                    {
                        res.error = 0;
                        res.data = "�޸ĳɹ�";
                    }
                }
                else
                {
                    res.error = 1;
                    res.data = "�ֻ��ŷ��ʺŰ󶨵ĺ���";
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
        /// ���ǰ��������
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// ��ò�ѯ��ҳ����
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