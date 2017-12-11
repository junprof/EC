using System;
using System.Data;
using System.Collections.Generic;
using DTcms.Model;
namespace DTcms.BLL
{
    /// <summary>
    /// dt_msg
    /// </summary>
    public partial class dt_msg
    {
        private readonly DTcms.DAL.dt_msg dal = new DTcms.DAL.dt_msg();
        public dt_msg()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(Guid hid)
        {
            return dal.Exists(hid);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(DTcms.Model.dt_msg model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DTcms.Model.dt_msg model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(Guid hid)
        {

            return dal.Delete(hid);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DTcms.Model.dt_msg GetModel(Guid hid)
        {

            return dal.GetModel(hid);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<DTcms.Model.dt_msg> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<DTcms.Model.dt_msg> DataTableToList(DataTable dt)
        {
            List<DTcms.Model.dt_msg> modelList = new List<DTcms.Model.dt_msg>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                DTcms.Model.dt_msg model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public List<Model.MsgExtend> GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod
        #region  ExtensionMethod

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        public bool UpdateState(bool state, Guid id)
        {
            return dal.UpdateState(state, id);
        }

        public Model.BaseResponse UpdateMemo(Model.dt_msg item)
        {
            Model.BaseResponse res = new BaseResponse();
            if(!dal.UpdateState2(item))
            {
                res.error = 1;
                res.data = "更新失败，请稍候重试";
            }
            return res;
        }
        const string MsgCacheKey = "MSGCACH_U_";
        public Model.BaseResponse GetUnprocessMsg(int user_id)
        {
            DAL.dt_msg msgdal = new DAL.dt_msg { userid = user_id };
            var datalist = Cache.DataCache.GetAndUpdate<List<MsgExtend>>(MsgCacheKey + user_id, msgdal.GetUnprocessMsg, new DateTimeOffset(DateTime.Now.AddSeconds(10)));
            return new BaseResponse { error = 0, data = datalist };
        }
        public Model.BaseResponse SetReaded(string msgid)
        {
            BaseResponse res = new BaseResponse();
            if (!string.IsNullOrEmpty(msgid))
            {
                if (!dal.SetReaded(msgid))
                {
                    res.error = 1;
                    res.data = "操作失败";
                }
            }
            return res;
        }
        public Model.BaseResponse SetReaded(int user_id)
        {
            BaseResponse res = new BaseResponse();
            if (!dal.SetReaded(user_id))
            {
                res.error = 1;
                res.data = "操作失败";
            }
            return res;
        }

        #endregion  ExtensionMethod
    }
}

