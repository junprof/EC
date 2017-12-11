using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using DTcms.Model;
namespace DTcms.BLL
{
    /// <summary>
    /// dt_historydata
    /// </summary>
    public partial class dt_historydata
    {
        private readonly DTcms.DAL.dt_historydata dal = new DTcms.DAL.dt_historydata();
        public dt_historydata()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(Guid id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(DTcms.Model.dt_historydata model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DTcms.Model.dt_historydata model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(Guid id)
        {

            return dal.Delete(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DTcms.Model.dt_historydata GetModel(Guid id)
        {

            return dal.GetModel(id);
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
        public List<DTcms.Model.dt_historydata> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<DTcms.Model.dt_historydata> DataTableToList(DataTable dt)
        {
            List<DTcms.Model.dt_historydata> modelList = new List<DTcms.Model.dt_historydata>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                DTcms.Model.dt_historydata model;
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
        public List<Model.dt_historydata> GetRecentData()
        {
            return dal.GetDataList("addtime >= '2017-08-12 22:00:00' and addtime <= 2017-08-13 00:00:00");
        }
        public List<Model.dt_item> GetChatData(int itemid)
        {
            var datalist = Cache.DataCache.GetAndUpdate("chatdata", GetRecentData);
            List<Model.dt_item> items = new List<Model.dt_item>();
            datalist = datalist.Where(p => p.item_id == itemid).ToList();
            datalist.ForEach(p=> {
                Model.dt_item item = new Model.dt_item
                {
                    id=p.item_id.Value,
                    addtime=p.addtime,
                    value=p.value
                };
                items.Add(item);
            });
            
            return items;
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
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
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
        #endregion  ExtensionMethod
    }
}

