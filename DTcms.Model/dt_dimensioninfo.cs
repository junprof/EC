using System;
namespace DTcms.Model
{
    /// <summary>
    /// dt_dimensioninfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class dt_dimensioninfo
    {
        public dt_dimensioninfo()
        { }
        #region Model
        private int _dimension;
        private decimal? _value;
        private decimal? _trailerval;
        private decimal? _warningval;
        private DateTime _updatetime;
        private Guid _hid;
        private int _id;
        /// <summary>
        /// 1：A相电流，2：B相电流，3：C相电流，4：A相电压，5：B相电压，6：C相电压，7：A相电压1，8：B相电压1，9：C相电压1，10：漏电流，11：1路温度，12：2路温度，13：3路温度，14：4路温度，15：补位
        /// </summary>
        public int dimension
        {
            set { _dimension = value; }
            get { return _dimension; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? value
        {
            set { _value = value; }
            get { return _value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? trailerval
        {
            set { _trailerval = value; }
            get { return _trailerval; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? warningval
        {
            set { _warningval = value; }
            get { return _warningval; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime updatetime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid hid
        {
            set { _hid = value; }
            get { return _hid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        #endregion Model
        public int Second { get { return this.updatetime.Second; } }
        public int Minute { get { return this.updatetime.Minute; } }
        public int Hour { get { return this.updatetime.Hour; } }
        public int Day { get { return this.updatetime.Day; } }
    }
}

