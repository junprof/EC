using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DTcms.Web.Controllers
{
    [RoutePrefix("api/Message")]
    public class MessageController : ApiBaseController
    {
        /// <summary>
        /// 告警信息获取
        /// </summary>
        /// <param name="param"> json格式{"pagenum":1,"pagesize":10}</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetMsg")]
        public HttpResponseMessage GetMsg([FromBody]Model.PagerBase param)
        {
            string where = " t.user_id =" + CurrentUser.USERID ;
            int startIdx = (param.pagenum - 1) * param.pagesize;
            int endIdx = startIdx + param.pagesize;
            resObj.data = new BLL.dt_msg().GetListByPage(where, " addtime desc", startIdx, endIdx);
            return Json(resObj);
        }
        /// <summary>
        /// 未读消息数
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetMsgCount")]
        public int GetMsgCount()
        {
            string where = $" t.user_id = {CurrentUser.USERID} and state != 1";
            return new BLL.dt_msg().GetRecordCount(where);
        }

        /// <summary>
        /// 修改告警状态
        /// </summary>
        /// <param name="item">{"hid":"","ISPROCESSED":"0 未处理 1 已处理","REMARK":"备注说明"}</param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateState")]
        public HttpResponseMessage UpdateState([FromBody]Model.msg_adq item)
        {
            return Json(new BLL.dt_msg().UpdateMemo(new Model.dt_msg { id=item.id,REMARK=item.REMARK,ISPROCESSED=item.ISPROCESSED}));
        }
        /// <summary>
        /// 获取预警记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetWarning")]
        public HttpResponseMessage GetWarning()
        {
            return Json(new BLL.dt_msg().GetUnprocessMsg(CurrentUser.USERID));
        }
        /// <summary>
        /// 设置已读
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Read")]
        public HttpResponseMessage Read([FromUri]string msgid)
        {
            return Json(new BLL.dt_msg().SetReaded(msgid));
        }
        [HttpPost]
        [Route("ReadAll")]
        public HttpResponseMessage ReadAll()
        {
            return Json(new BLL.dt_msg().SetReaded(CurrentUser.USERID));
        }

        [HttpPost]
        [Route("Send")]
        [AllowAnonymous]
        public HttpResponseMessage Send([FromBody]Model.PushMessage msg)
        {
            Model.siteconfig sc = new BLL.siteconfig().loadConfig();
            Common.JPush jpclient = new Common.JPush(sc.pushappkey, sc.pushappsecret);
            Dictionary<string, object> ed = new Dictionary<string, object>();
            if (msg.extras != null)
            {
                msg.extras.ForEach(p => { if (!ed.Keys.Contains(p.Key)) ed.Add(p.Key, p.Value); });
            }
            if (jpclient.SendPush(msg.message, msg.regids, ed))
            {
                resObj = new Model.BaseResponse(0, "success");
            }
            else
            {
                resObj = new Model.BaseResponse(1, "fail");
            }
            return Json(resObj);
        }
    }
}
