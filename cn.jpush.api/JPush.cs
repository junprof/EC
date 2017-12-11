using cn.jpush.api.push;
using cn.jpush.api.push.mode;
using System.Collections.Generic;

namespace cn.jpush.api
{
    public class JPush
    {
        private JPushClient client;
        public JPush(string app_key, string master_secret)
        {
            client = new JPushClient(app_key, master_secret);
        }

        public MessageResult SendPush(string Msg,string RegId,Dictionary<string,object> extras)
        {
            PushPayload pay = PushObject_all_registrationId_alert(Msg, RegId, extras);
            var result = client.SendPush(pay);
            return result;
        }
        public static PushPayload PushObject_all_registrationId_alert(string Msg, string RegId, Dictionary<string, object> extras)
        {

            PushPayload pushPayload = new PushPayload();
            pushPayload.platform = Platform.all();
            string[] arrregid = RegId.Split(',');
            pushPayload.audience = Audience.s_registrationId(arrregid); //Audience.s_alias("alias1");


            pushPayload.notification = new Notification();//.setAlert(Msg);


            pushPayload.notification.AndroidNotification = new push.notification.AndroidNotification();
            pushPayload.notification.IosNotification = new push.notification.IosNotification();
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
            pushPayload.notification.setExtras(null);
            return pushPayload;

        }
    }
}
