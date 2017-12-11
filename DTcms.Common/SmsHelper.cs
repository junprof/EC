using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTcms.Common
{
    public class SmsHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mobile">号码，多个可以用逗号隔开</param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="Nonce_str"></param>
        /// <param name="App_key"></param>
        /// <param name="App_secret"></param>
        /// <returns></returns>
        public static string Send(string mobile, string title, string content, string Nonce_str, string App_key, string App_secret,string url)
        {
            //批次号  以后改成自动的
            string batch_num = DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(111111, 999999);
            string mission_num = DateTime.Now.ToString("yyyyMMdd") + new Random().Next(111111, 999999);
            //获得时间戳
            string time_stamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            //发送消息正文，包含签名
            string Content = "【" + title + "】" + content;
            string stringA = "app_key=" + App_key + "&batch_num=" + batch_num + "&content=" + Content + "&dest_id=" + mobile + "&mission_num=" + mission_num + "&nonce_str=" + Nonce_str + "&sms_type=verify_code&time_stamp=" + time_stamp;
            //拼接参数值用以计算Sign
            string SignTemp = stringA.Trim() + "&app_secret=" + App_secret;
            string sign = Utils.GetMd5Hash(SignTemp);
            string str = Utils.SmsXmlInfo(mobile, Content, sign, mission_num, batch_num, time_stamp, App_key, Nonce_str).Trim();
            //通过Post方法提交数据，并且获得响应数据
            string backCode = Utils.Post(url, str);

            return backCode;
        }
    }
}
