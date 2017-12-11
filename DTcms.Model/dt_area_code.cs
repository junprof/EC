using System;
namespace DTcms.Model
{
    /// <summary>
    /// dt_area_code:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class dt_area_code
    {
        public dt_area_code()
        { }
        #region Model
        private string _code;
        private string _name;
        private string _name_short;
        private string _parent_code;
        private int? _lev;
        /// <summary>
        /// 
        /// </summary>
        public string code
        {
            set { _code = value; }
            get { return _code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string name_short
        {
            set { _name_short = value; }
            get { return _name_short; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string parent_code
        {
            set { _parent_code = value; }
            get { return _parent_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? lev
        {
            set { _lev = value; }
            get { return _lev; }
        }
        #endregion Model

    }
}

