using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DTcms.Model;
using System.Net.Http.Headers;
using System.Web;
using System.IO;
using System.Threading.Tasks;
using DTcms.Common;

namespace DTcms.Web.Controllers
{
    [RoutePrefix("api/Picture")]
    public class PictureController : ApiBaseController
    {

        [AllowAnonymous]
        public HttpResponseMessage Get(string picid)
        {
            object o = dt_picture.GetPicture(picid);
            byte[] img = null;

            if (o != null)
            {
                if (o is string)
                {
                    Model.siteconfig siteconfig = new BLL.siteconfig().loadConfig();
                    string url = Path.Combine(siteconfig.filepath + o.ToString());

                    if (!File.Exists(url))
                    {
                        resObj = new BaseResponse(1, "图片不存在或已删除");
                        return Json(resObj);
                    }
                    Stream stream = new FileStream(url, FileMode.Open);
                    img = new byte[stream.Length];
                    stream.Read(img, 0, img.Length);
                    stream.Close();
                }
                else if (o is byte[])
                {
                    img = (byte[])o;
                }
                var resp = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(img)
                    //或者
                    //Content = new StreamContent(ms)
                };
                resp.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
                return resp;
            }
            else
            {
                resObj = new BaseResponse(1, "图片不存在或已删除");
                return Json(resObj);
            }

        }
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns>返回上传图片Id</returns>
        [Route("Upload")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> PostUpload()
        {
            siteconfig siteConfig = new DTcms.BLL.siteconfig().loadConfig();

            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            string date = DateTime.Now.ToString("yyyyMM");
            string day = DateTime.Now.Day.ToString();
            string path = Path.Combine(siteConfig.filepath, date, day);
            string relativepath = $"\\{date}\\{day}";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string temppath = Path.Combine(HttpRuntime.AppDomainAppPath, "tempupload");
            if (!Directory.Exists(temppath))
                Directory.CreateDirectory(temppath);
            var provider = new MultipartFormDataStreamProvider(temppath);
            await Request.Content.ReadAsMultipartAsync(provider);
            string[] supportformat = { ".jpg", ".bmp", ".png", ".gif", ".jpeg" };
            List<string> picids = new List<string>();
            foreach (MultipartFileData filedata in provider.FileData)
            {
                string filename = filedata.Headers.ContentDisposition.FileName;
                string ext = filename.Substring(filename.LastIndexOf('.')).Replace("\"", "").ToLower();
                if (supportformat.Contains(ext))
                {
                    string newfilename = string.Empty;
                    string newfile = string.Empty;
                    do
                    {
                        newfilename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Common.Utils.Number(5, true) + ext;
                        newfile = Path.Combine(path, newfilename);
                    }
                    while (File.Exists(newfilename));
                    string newrelativepath = relativepath + "\\" + newfilename;
                    try
                    {
                        if ((siteConfig.imgmaxheight > 0 || siteConfig.imgmaxwidth > 0))
                        {
                            Thumbnail.MakeThumbnailImage(filedata.LocalFileName, newfile,
                                siteConfig.imgmaxwidth, siteConfig.imgmaxheight);
                        }
                        else
                        {
                            FileInfo fi = new FileInfo(filedata.LocalFileName);
                            fi.MoveTo(newfile);
                        }
                        string picid = Guid.NewGuid().ToString();
                        if (new dt_picture { picid = picid, createtime = DateTime.Now, url = newrelativepath, type = 1, isvalid = 1 }.Insert())
                        {
                            // success
                            picids.Add(picid);
                        }
                        else
                        {
                            throw new Exception("数据库插入失败");
                        }
                    }
                    catch (Exception ex)
                    {
                        resObj = new BaseResponse(1, "图片上传失败");
                        Common.Log.Error(ex.Message);
                        if (File.Exists(newfile))
                        {
                            File.Delete(newfile);
                        }
                    }
                }
            }
            resObj.data =string.Join(",", picids.ToArray());
            resObj.error = 0;
            return Json(resObj);

        }
    }
}
