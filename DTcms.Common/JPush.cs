using cn.jpush.api;
using cn.jpush.api.push;
using cn.jpush.api.push.mode;
using System.Collections.Generic;

namespace DTcms.Common
{
    public class JPush
    {
        private JPushClient client;
        public JPush(string app_key, string master_secret)
        {
            client = new JPushClient(app_key, master_secret);
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="Msg">消息</param>
        /// <param name="RegId">关联设备标识</param>
        /// <param name="extras">附带信息</param>
        /// <param name="type">1 别名(默认)  2 registrationid</param>
        /// <returns></returns>
        public bool SendPush(string Msg,string RegId,Dictionary<string,object> extras,int type=1)
        {
            bool res = false;
            PushPayload pushPayload = new PushPayload();
            pushPayload.platform = Platform.all();
            string[] arrregid = RegId.Split(',');
             //Audience.s_alias("alias1");
            if (type == 1)
            {
                pushPayload.audience = Audience.s_alias(arrregid);
            }else if (type == 2)
            {
                pushPayload.audience = Audience.s_registrationId(arrregid);
            }

            pushPayload.notification = new Notification();//.setAlert(Msg);


            pushPayload.notification.AndroidNotification = new cn.jpush.api.push.notification.AndroidNotification();
            pushPayload.notification.IosNotification = new cn.jpush.api.push.notification.IosNotification();
            pushPayload.notification.AndroidNotification.setAlert(Msg);
            pushPayload.notification.AndroidNotification.setBuilderID(1);
            pushPayload.notification.IosNotification.setAlert(Msg);
            if (extras != null)
            {
                foreach (var item in extras)
                {
                    pushPayload.notification.AndroidNotification.extras.Add(item.Key, item.Value);
                    pushPayload.notification.IosNotification.extras.Add(item.Key, item.Value);
                }
            }
            pushPayload.notification.IosNotification.setSound("dianAnBao.wav");
            pushPayload.options.apns_production = true;
            pushPayload.platform = Platform.all();
            pushPayload.message = Message.content(Msg);
            pushPayload.message.content_type = "text";
            var result = client.SendPush(pushPayload);
            if (result.ResponseResult.responseCode == System.Net.HttpStatusCode.OK)
            {
                res = true;
            }
            else
            {
                res = false;
                Log.Warn(JsonHelper2.Serialize(result));
            }
            return res;
        }
        public static PushPayload PushObject_all_registrationId_alert(string Msg, string RegId, Dictionary<string, object> extras)
        {

            PushPayload pushPayload = new PushPayload();
            pushPayload.platform = Platform.all();
            string[] arrregid = RegId.Split(',');
            pushPayload.audience = Audience.s_registrationId(arrregid); //Audience.s_alias("alias1");


            pushPayload.notification = new Notification();//.setAlert(Msg);


            pushPayload.notification.AndroidNotification = new cn.jpush.api.push.notification.AndroidNotification();
            pushPayload.notification.IosNotification = new cn.jpush.api.push.notification.IosNotification();
            pushPayload.notification.AndroidNotification.setAlert(Msg);
            pushPayload.notification.AndroidNotification.setBuilderID(1);
            pushPayload.notification.IosNotification.setAlert(Msg);
            if (extras != null)
            {
                foreach (var item in extras)
                {
                    pushPayload.notification.AndroidNotification.extras.Add(item.Key, item.Value);
                    pushPayload.notification.IosNotification.extras.Add(item.Key, item.Value);
                }
            }
            pushPayload.notification.IosNotification.setSound("default");
            pushPayload.options.apns_production = true;
            pushPayload.platform = Platform.all();
            pushPayload.message = Message.content(Msg);
            pushPayload.message.content_type = "text";
            //pushPayload.notification.setExtras(null);
            return pushPayload;

        }
    }
}
