using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DTcms.Web.Controllers
{
    [RoutePrefix("api/PictureViewer")]
    public class PictureViewerController : ApiBaseController
    {
        [HttpPost]
        [Route("GetViews")]
        public HttpResponseMessage GetViews()
        {
            var viewer= new Model.dt_pictureview().GetByUserId(CurrentUser.USERID);
            if(viewer==null ||viewer.Viewer.Count==0)
            {
                resObj.error = 1;
                resObj.data = "未设置图片";
            }else
            {
                resObj.data = viewer.Viewer;
            }
            return Json(resObj);
        }
    }
}
