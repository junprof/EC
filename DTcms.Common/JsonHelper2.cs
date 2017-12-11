namespace DTcms.Common
{
    public class JsonHelper2
    {
        public static string Serialize<T>(T entity)
        {
            Newtonsoft.Json.Converters.IsoDateTimeConverter iso
                = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return Newtonsoft.Json.JsonConvert.SerializeObject(entity, iso);
        }
        public static T Deserialize<T>(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }
        public static string GetCommonObj(int errorCode, object strData)
        {
            var res = new
            {
                error = errorCode,
                data = strData
            };
            return Serialize(res);
        }
    }
}
