using DTcms.Common;
using System;
using System.IO;
using System.Web.Mvc;

namespace DTcms.Web.Controllers
{

    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Avatar(string id)
        {
            int uid = 0;
            if (!string.IsNullOrWhiteSpace(id) && int.TryParse(id,out uid))
            {
                ViewBag.uid = uid;
                var m = new BLL.manager().GetModel(uid);
                if (m != null && !string.IsNullOrWhiteSpace(m.avatar))
                {
                    ViewBag.Path = "/api/Picture/Get?picid=" + m.avatar;
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult Avatar(FormCollection form)
        {
            int x = Convert.ToInt32(form["x"]);
            int y = Convert.ToInt32(form["y"]);
            int w = Convert.ToInt32(form["w"]);
            int h = Convert.ToInt32(form["h"]);
            string imgsrc = form["imgsrc"].Substring(0, form["imgsrc"].LastIndexOf("?"));
            string path = Models.ImgHandler.CutAvatar(imgsrc, x, y, w, h);

            //保存Path

            ViewBag.Path = path;
            string picid = Guid.NewGuid().ToString();
            if (new Model.dt_picture { picid = picid, createtime = DateTime.Now, url = path, type = 1, isvalid = 1 }.Insert())
            {
                // success
                ViewBag.Path = "/api/Picture/Get?picid=" + picid;
                string uid = form["uid"];
                if (!string.IsNullOrEmpty(uid)&& new BLL.manager().SetAvatar(uid, picid)) {
                    ViewBag.Msg = "保存成功";
                }
                else
                {
                    ViewBag.Msg = "未能获取用户信息，登陆或已超时";
                }
            }
            else
            {
                ViewBag.Msg = "保存头像失败";
            }
            return View();
        }
        public ActionResult SecurityCode()
        {

            string code = Common.Utils.GetCheckCode(5); //验证码的字符为4个
            TempData["SecurityCode"] = code; //验证码存放在TempData中
            return File(Common.Utils.CreateValidateGraphic(code), "image/Jpeg");
        }
        /// <summary>
        /// 上传头像原图
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [NonAction]
        public ActionResult ProcessUpload(string file)
        {
            try
            {
                string uploadFolder = "\\tempUpload\\" + DateTime.Now.ToString("yyyyMM") + "\\";
                string imgName = DateTime.Now.ToString("ddHHmmssff");
                string imgType = file.Substring(file.LastIndexOf("."));
                string uploadPath = "";
                uploadPath = Server.MapPath(uploadFolder);
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                using (var inputStream = Request.InputStream)
                {
                    using (var flieStream = new FileStream(uploadPath + imgName + imgType, FileMode.Create))
                    {
                        inputStream.CopyTo(flieStream);
                    }
                }

                return Json(new { success = true, message = uploadFolder + imgName + imgType });
            }
            catch (Exception e)
            {
                return Json(new { fail = true, message = e.Message });
            }
        }
        [HttpPost]
        public JsonResult Login(string username,string password,string vcode)
        {
            Model.BaseResponse res = new Model.BaseResponse();
            var code = TempData["SecurityCode"]?.ToString()??"TEST";
            vcode = vcode?.ToUpper();
            if (vcode != code)
            {
                res.error = 1;
                res.data = "验证码错误";
                return Json(res);
            }
            BLL.manager bll = new BLL.manager();
            Model.manager model = bll.GetModel(username,password,true);
            if (model == null)
            {
                res.error = 1;
                res.data = "帐号或密码不正确";
            }
            else {
                model.password = string.Empty;
                Session[DTKeys.SESSION_ADMIN_INFO] = model;
                Session.Timeout = 45;
                Utils.WriteCookie("DTRememberName", model.user_name, 14400);
                Utils.WriteCookie("AdminName", "DTcms", model.user_name);
                Utils.WriteCookie("AdminPwd", "DTcms", model.password);
                res.data = model;
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public JsonResult EasyLoginWx(string weichatid)
        {
            Model.BaseResponse res = new Model.BaseResponse();

            BLL.manager bll = new BLL.manager();
            Model.manager model = bll.GetModel(weichatid);
            if (model == null)
            {
                res.error = 1;
                res.data = "未绑定微信";
            }
            else
            {
                model.password = string.Empty;
                Session[DTKeys.SESSION_ADMIN_INFO] = model;
                Session.Timeout = 240;
                res.data = model;
            }
            return Json(res,JsonRequestBehavior.AllowGet);
        }

    }
}