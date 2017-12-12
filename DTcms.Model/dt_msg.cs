using System;
namespace DTcms.Model
{
    /// <summary>
    /// dt_msg:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class dt_msg
    {

        public string id { get; set; }
        /// <summary>
        /// 对应的历史记录
        /// </summary>
        public Guid hid { get; set; }
        /// <summary>
        ///
        /// </summary>
        public string title { get; set; }
        /// <summary>
        ///
        /// </summary>
        public int? item_id { get; set; }
        /// <summary>
        ///
        /// </summary>
        public DateTime? addtime { get; set; }
        /// <summary>
        /// 通知到的号码，如其他的，请填下对应编号
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 通知到的用户
        /// </summary>
        public int? user_id { get; set; }
        /// <summary>
        ///
        /// </summary>
        public bool state { get; set; }
        public int? ISPROCESSED { get; set; } = 0;
        public string REMARK { get; set; } = string.Empty;

        public string content { get; set; } = string.Empty;
        public int dim_id { get; set; }
    }
    public class msg_adq
    {
        public string id { get; set; }
        public int? ISPROCESSED { get; set; }
        public string REMARK { get; set; }
    }
    public class MsgExtend:dt_msg
    {
        public string itemname { get; set; } = string.Empty;
		public string position {get;set; } = string.Empty;
        public string processed_name { get { return ISPROCESSED == 1 ? "已处理" : "未处理"; } }

    }
}

