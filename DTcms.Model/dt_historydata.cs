using System;
namespace DTcms.Model
{
    /// <summary>
    /// dt_historydata:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class dt_historydata
    {
        public dt_historydata()
        { }
        #region Model
        private Guid _id;
        private string _name;
        private string _value;
        private DateTime _addtime;
        private DateTime? _updatetime;
        private int? _type;
        private string _checkcode;
        private string _functioncode;
        private string _datahead;
        private int? _item_id;
        private bool _online;
        private string _trailerval;
        private string _warningval;
        /// <summary>
        /// 
        /// </summary>
        public Guid id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string value
        {
            set { _value = value; }
            get { return _value; }
        }
        /// <summary>
        /// getdate()
        /// </summary>
        public DateTime addtime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? updatetime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 1：A相电流，2：B相电流，3：C相电流，
        /// </summary>
        public int? type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 校验码
        /// </summary>
        public string checkcode
        {
            set { _checkcode = value; }
            get { return _checkcode; }
        }
        /// <summary>
        /// 功能码
        /// </summary>
        public string functioncode
        {
            set { _functioncode = value; }
            get { return _functioncode; }
        }
        /// <summary>
        /// 数据头
        /// </summary>
        public string datahead
        {
            set { _datahead = value; }
            get { return _datahead; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? item_id
        {
            set { _item_id = value; }
            get { return _item_id; }
        }
        /// <summary>
        /// 是否在线
        /// </summary>
        public bool online
        {
            set { _online = value; }
            get { return _online; }
        }
        /// <summary>
        /// 预警值
        /// </summary>
        public string trailerval
        {
            set { _trailerval = value; }
            get { return _trailerval; }
        }
        /// <summary>
        /// 告警值
        /// </summary>
        public string warningval
        {
            set { _warningval = value; }
            get { return _warningval; }
        }
        #endregion Model

    }
}

