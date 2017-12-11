using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTcms.Common;
using DTcms.Model;

namespace DTcms.BLL
{
    /// <summary>
    /// 验证码
    /// </summary>
    public class VCode
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig();
        public static VCode Instance = new VCode();
        public BaseResponse Send( string phone,int codetype)
        {
            BaseResponse res = new BaseResponse();
            try {
                if (string.IsNullOrEmpty(phone))
                {
                    res.error = 1;
                    res.data = "手机号为空";
                    return res;
                }
                //是否在有效期内发送过
                Model.dt_verifycode code = Cache.DataCache.Get(phone + "_" + codetype) as Model.dt_verifycode;
                bool isnew = false;
                if (code == null)
                {
                    code = new Model.dt_verifycode().GetCode(phone, codetype);
                }
                if (code == null || DateTime.Now.Subtract(code.CREATETIME).Minutes > siteConfig.smscodecache) // 重新生成
                {
                    code = new Model.dt_verifycode();
                    code.CODE = Common.Utils.Number(4);
                    code.ID = Guid.NewGuid().ToString();
                    code.ISVALID = 1;
                    code.CREATETIME = DateTime.Now;
                    code.PHONE = phone;
                    code.CODETYPE = codetype;
                    isnew = true;
                }
                string result = SmsHelper.Send(phone, siteConfig.smstitle, $"您的验证码：{ code.CODE}，短信验证码{siteConfig.smscodecache}分钟之类有效，如非本人操作，请忽略本短信,", "liuliangt", siteConfig.smsusername, siteConfig.smspassword,siteConfig.smsapiurl);
                if (result.Contains("000000"))
                {
                    if(isnew)
                        code.Insert();
                    Cache.DataCache.Add(phone + "_" + codetype, code, new DateTimeOffset(DateTime.Now.AddMinutes(siteConfig.smscodecache)));
                    res.error = 0;
                    res.data = "发送成功";
                }
                else
                {
                    res.error = 1;
                    res.data = "发送失败";
                }
            }catch(Exception ex)
            {
                res.error = 4;
                res.data = ex;
            }
            return res;
        }
        public BaseResponse CheckCode(string phone,int codetype,string code)
        {
            BaseResponse res = new BaseResponse();
            var vcode = Cache.DataCache.Get(phone + "_" + codetype) as Model.dt_verifycode;
            if(vcode == null)
            {
                res.error = 1;
                res.data = "验证码已失效";
            }
            else
            {
                if(vcode.CODE == code)
                {
                    Cache.DataCache.Delete(phone + "_" + codetype);
                    vcode.SetInvalid();
                }else
                {
                    res.error = 1;
                    res.data = "验证码错误";
                }
            }
            return res;
        }
    }
}
