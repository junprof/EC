using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using DTcms.Model;
namespace DTcms.BLL
{
    /// <summary>
    /// dt_dimensioninfo
    /// </summary>
    public partial class dt_dimensioninfo
    {
        private readonly DTcms.DAL.dt_dimensioninfo dal = new DTcms.DAL.dt_dimensioninfo();
        public dt_dimensioninfo()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(DTcms.Model.dt_dimensioninfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(DTcms.Model.dt_dimensioninfo model)
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
        public DTcms.Model.dt_dimensioninfo GetModel(int id)
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
        public List<DTcms.Model.dt_dimensioninfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetDataList(strWhere);
            return (from DataRow dr in ds.Tables[0].Rows select new Common.DBRowConvertor(dr).ConvertToEntity<Model.dt_dimensioninfo>()).ToList();
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<DTcms.Model.dt_dimensioninfo> DataTableToList(DataTable dt)
        {
            List<DTcms.Model.dt_dimensioninfo> modelList = new List<DTcms.Model.dt_dimensioninfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                DTcms.Model.dt_dimensioninfo model;
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
        /// 获取后显示在图表上用
        /// </summary>
        public DataSet GetListShowImg(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        #endregion  ExtensionMethod
    }
}

