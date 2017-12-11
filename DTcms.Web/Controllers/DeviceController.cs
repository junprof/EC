using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DTcms.Web.Controllers
{
    [RoutePrefix("api/Device")]
    public class DeviceController : ApiBaseController
    {
        /// <summary>
        /// 设备列表
        /// </summary>
        /// <param name="param"> json格式{"pagenum":1,"pagesize":10}</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDeviceList")]
        public HttpResponseMessage GetDeviceList([FromBody]Model.dt_item_adq param)
        {
            param.user_id = CurrentUser.USERID;
            int startIdx = (param.pagenum - 1) * param.pagesize;
            int endIdx = startIdx + param.pagesize;
            resObj.data = new BLL.dt_item().GetListByPage2(param, " addtime desc", startIdx, endIdx);
            return Json(resObj);
        }
        /// <summary>
        /// 获取设备信息 by id
        /// </summary>
        /// <param name="item">{"id":""}</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDevice")]
        public HttpResponseMessage GetDevice([FromBody]dynamic item)
        {
            resObj.data = new BLL.dt_item().GetModel(item.id);
            return Json(resObj);
        }
        /// <summary>
        /// 添加设备巡检记录
        /// </summary>
        /// <param name="item">{"addr":"地址","position":"经度,纬度","device_sn":"设备编码","photolist":"设备照片列表","remark":"备注"}</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddDeviceInspection")]
        public HttpResponseMessage AddDeviceInspection([FromBody]Model.dt_device_h item)
        {
            item.id = Guid.NewGuid().ToString();
            item.addtime = DateTime.Now;
            item.user_id = CurrentUser.USERID;
            if (string.IsNullOrEmpty(item.device_sn))
            {
                resObj = new Model.BaseResponse(1, "请输入设备编号");
            }
            else
            {
                item.photolist = item.photolist.Replace("[", "").Replace("]", "").Replace(" ", "");
                if (!item.Insert(1))
                {
                    resObj = new Model.BaseResponse(1, "操作失败");
                }
            }
            return Json(resObj);
        }
        /// <summary>
        /// 添加设备检修记录
        /// </summary>
        /// <param name="item">{"addr":"地址","position":"经度,纬度","device_sn":"设备编码","photolist":"设备照片列表","remark":"备注"}</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddDeviceRepair")]
        public HttpResponseMessage AddDeviceRepair([FromBody]Model.dt_device_h item)
        {
            item.id = Guid.NewGuid().ToString();
            item.addtime = DateTime.Now;
            item.user_id = CurrentUser.USERID;
            if (string.IsNullOrEmpty(item.device_sn))
            {
                resObj = new Model.BaseResponse(1, "请输入设备编号");
            }
            else {
                item.photolist=item.photolist.Replace("[", "").Replace("]", "").Replace(" ", "");
                if (!item.Insert(2))
                {
                    resObj = new Model.BaseResponse(1, "操作失败");
                }
            }
            return Json(resObj);
        }
        /// <summary>
        /// 获取检修记录
        /// </summary>
        /// <param name="param">{"pagenum":1,"pagesize":10,"device_name":"设备名称","start_time":"查询时间","end_time":"查询时间"}</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDeviceRepairList")]
        public HttpResponseMessage GetDeviceRepairList([FromBody]Model.dt_device_h_adq param)
        {
            int startIdx = (param.pagenum - 1) * param.pagesize;
            int endIdx = startIdx + param.pagesize;
            param.user_id = CurrentUser.USERID;
            resObj.data = new Model.dt_device_h().GetListByPage(param, "addtime desc", startIdx, endIdx, 2);
            return Json(resObj);
        }
        /// <summary>
        /// 获取巡检记录
        /// </summary>
        /// <param name="param">{"pagenum":1,"pagesize":10,"device_name":"设备名称","start_time":"查询时间","end_time":"查询时间"}</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDeviceInspectList")]
        public HttpResponseMessage GetDeviceInspectList([FromBody]Model.dt_device_h_adq param)
        {
            int startIdx = (param.pagenum - 1) * param.pagesize;
            int endIdx = startIdx + param.pagesize;
            param.user_id = CurrentUser.USERID;
            resObj.data = new Model.dt_device_h().GetListByPage(param, "addtime desc", startIdx, endIdx, 1);
            return Json(resObj);
        }
        [HttpGet]
        [Route("DeviceInspect")]
        public HttpResponseMessage GetDeviceInspect([FromUri]string id)
        {
            resObj.data = new Model.dt_device_h { id = id}.Get(1);
            return Json(resObj);
        }
        [HttpGet]
        [Route("DeviceRepair")]
        public HttpResponseMessage GetDeviceRepair([FromUri]string id)
        {
            resObj.data = new Model.dt_device_h { id = id }.Get(2);
            return Json(resObj);
        }

        [HttpPost]
        [Route("GetReport")]
        [AllowAnonymous]
        public HttpResponseMessage GetReport([FromBody]Model.ReportParam param)
        {
            if (!param.item_id.HasValue)
            {
                resObj = new Model.BaseResponse(1, "无效设备");
            }
            if (!param.dim_id.HasValue)
            {
                resObj = new Model.BaseResponse(1,"未知维度");
            }
            if (resObj.error == 0)
            {
                string where = " 1=1 ";
                if (!param.starttime.HasValue || !param.endtime.HasValue)
                {
                    where += " and a.updatetime >= '" + DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss")+"' ";
                }
                else {
                    where += " and a.updatetime >= '" + param.starttime.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' and a.updatetime <= '" + param.endtime.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                }
                if (!string.IsNullOrEmpty(param.hid))
                {
                    where += " and a.hid = '"+param.hid+"'";
                    new BLL.dt_msg().SetReaded(param.msgid);
                }
                BLL.dt_dimensioninfo bll = new BLL.dt_dimensioninfo();
                var data = bll.GetModelList($"{where} and a.dimension={param.dim_id} and b.item_id = '{param.item_id}'");
                if (data.Count == 0)
                {
                    resObj = new Model.BaseResponse(1, "无数据");
                }
                else {

                    // DD
                    string format = "yyyy-MM-dd";
                    switch (param.Acc)
                    {
                        case "DD":
                            format = "yyyy-MM-dd";
                            break;
                        case "HH":
                            format = "yyyy-MM-dd HH:00:00";
                            break;
                        case "mm":
                            format = "yyyy-MM-dd HH:mm:00";
                            break;
                        default:
                            break;
                    }
                    var ex = from p in data
                             group p by p.updatetime.ToString(format) into g
                             select new Model.ReportData
                             {
                                 time = Convert.ToDateTime(g.Key),
                                 value = g.Max(p=>p.value)
                             };
                    resObj.data = new { MessageCount = new MessageController().GetMsgCount(), ReportData = ex.OrderBy(p => p.time).ToList() };
                }
            }
            return Json(resObj);
        }
    }
}
