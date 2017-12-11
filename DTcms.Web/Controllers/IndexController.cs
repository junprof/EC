using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DTcms.Web.Controllers
{
    public class IndexController : ApiBaseController
    {
        public HttpResponseMessage Union()
        {
            var pv = new PictureViewerController();
            pv.GetViews();
            resObj.data = new {
                Messages = new BLL.dt_msg().GetListByPage(" t.user_id =" + CurrentUser.USERID, " addtime desc", 0, 5),
                MessageCount = new MessageController().GetMsgCount(),
                PictureViewer = pv.resObj.data
            };
            return Json(resObj);
        }
    }
}
