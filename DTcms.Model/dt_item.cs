using System;
namespace DTcms.Model
{
    /// <summary>
    /// dt_item:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class dt_item
    {
        public dt_item()
        { }
        #region Model
        private int _id;
        private string _name;
        private string _remarks;
        private DateTime? _addtime = DateTime.Now;
        private string _position;
        private int? _user_id;
        private string _area_code;
        private string _addr;
        private int? _state;
        private string _value;
        private DateTime? _updatetime;
        private bool _online;
        private string _onenetnum;
        private decimal? _warningai;
        private decimal? _warningbi;
        private decimal? _warningci;
        private decimal? _warningli;
        private decimal? _warningonetemperature;
        private decimal? _warningtwotemperature;
        private decimal? _warningthreetemperature;
        private decimal? _warningfourtemperature;
        private decimal? _trailerai;
        private decimal? _trailerbi;
        private decimal? _trailerci;
        private decimal? _trailerli;
        private decimal? _traileronetemperature;
        private decimal? _trailertwotemperature;
        private decimal? _trailerthreetemperature;
        private decimal? _trailerfourtemperature;
        private decimal? _trailerav;
        private decimal? _trailerbv;
        private decimal? _trailercv;
        private decimal? _warningav;
        private decimal? _warningbv;
        private decimal? _warningcv;
        private decimal? _trailerav2;
        private decimal? _trailerbv2;
        private decimal? _trailercv2;
        private decimal? _warningav2;
        private decimal? _warningbv2;
        private decimal? _warningcv2;
        private bool _isdel;
        /// <summary>
        ///
        /// </summary>
        public int id
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
        /// 备注
        /// </summary>
        public string remarks
        {
            set { _remarks = value; }
            get { return _remarks; }
        }
        /// <summary>
        ///
        /// </summary>
        public DateTime? addtime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 位置
        /// </summary>
        public string position
        {
            set { _position = value; }
            get { return _position; }
        }
        /// <summary>
        /// 所属用户
        /// </summary>
        public int? user_id
        {
            set { _user_id = value; }
            get { return _user_id; }
        }
        /// <summary>
        /// 所在地区
        /// </summary>
        public string area_code
        {
            set { _area_code = value; }
            get { return _area_code; }
        }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string addr
        {
            set { _addr = value; }
            get { return _addr; }
        }
        /// <summary>
        /// 状态：1：锁定，2：启用
        /// </summary>
        public int? state
        {
            set { _state = value; }
            get { return _state; }
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
        ///
        /// </summary>
        public DateTime? updatetime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
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
        ///
        /// </summary>
        public string onenetnum
        {
            set { _onenetnum = value; }
            get { return _onenetnum; }
        }
        /// <summary>
        /// 警告A相电流
        /// </summary>
        public decimal? warningAI
        {
            set { _warningai = value; }
            get { return _warningai; }
        }
        /// <summary>
        /// 警告B相电流
        /// </summary>
        public decimal? warningBI
        {
            set { _warningbi = value; }
            get { return _warningbi; }
        }
        /// <summary>
        /// 警告C相电流
        /// </summary>
        public decimal? warningCI
        {
            set { _warningci = value; }
            get { return _warningci; }
        }
        /// <summary>
        /// 警告漏电流
        /// </summary>
        public decimal? warningLI
        {
            set { _warningli = value; }
            get { return _warningli; }
        }
        /// <summary>
        /// 警告1路温度
        /// </summary>
        public decimal? warningOneTemperature
        {
            set { _warningonetemperature = value; }
            get { return _warningonetemperature; }
        }
        /// <summary>
        /// 警告2路温度
        /// </summary>
        public decimal? warningTwoTemperature
        {
            set { _warningtwotemperature = value; }
            get { return _warningtwotemperature; }
        }
        /// <summary>
        /// 警告3路温度
        /// </summary>
        public decimal? warningThreeTemperature
        {
            set { _warningthreetemperature = value; }
            get { return _warningthreetemperature; }
        }
        /// <summary>
        /// 警告4路温度
        /// </summary>
        public decimal? warningFourTemperature
        {
            set { _warningfourtemperature = value; }
            get { return _warningfourtemperature; }
        }
        /// <summary>
        /// 预警A电流
        /// </summary>
        public decimal? trailerAI
        {
            set { _trailerai = value; }
            get { return _trailerai; }
        }
        /// <summary>
        /// 预警B电流
        /// </summary>
        public decimal? trailerBI
        {
            set { _trailerbi = value; }
            get { return _trailerbi; }
        }
        /// <summary>
        /// 预警C电流
        /// </summary>
        public decimal? trailerCI
        {
            set { _trailerci = value; }
            get { return _trailerci; }
        }
        /// <summary>
        /// 预警漏电流
        /// </summary>
        public decimal? trailerLI
        {
            set { _trailerli = value; }
            get { return _trailerli; }
        }
        /// <summary>
        /// 预警1路温度
        /// </summary>
        public decimal? trailerOneTemperature
        {
            set { _traileronetemperature = value; }
            get { return _traileronetemperature; }
        }
        /// <summary>
        /// 预警2路温度
        /// </summary>
        public decimal? trailerTwoTemperature
        {
            set { _trailertwotemperature = value; }
            get { return _trailertwotemperature; }
        }
        /// <summary>
        /// 预警3路温度
        /// </summary>
        public decimal? trailerThreeTemperature
        {
            set { _trailerthreetemperature = value; }
            get { return _trailerthreetemperature; }
        }
        /// <summary>
        /// 预警4路温度
        /// </summary>
        public decimal? trailerFourTemperature
        {
            set { _trailerfourtemperature = value; }
            get { return _trailerfourtemperature; }
        }
        /// <summary>
        /// 预警A相电压22
        /// </summary>
        public decimal? trailerAV
        {
            set { _trailerav = value; }
            get { return _trailerav; }
        }
        /// <summary>
        /// 预警B向电压22
        /// </summary>
        public decimal? trailerBV
        {
            set { _trailerbv = value; }
            get { return _trailerbv; }
        }
        /// <summary>
        /// 预警C向电压22
        /// </summary>
        public decimal? trailerCV
        {
            set { _trailercv = value; }
            get { return _trailercv; }
        }
        /// <summary>
        /// 警告A相电压22
        /// </summary>
        public decimal? warningAV
        {
            set { _warningav = value; }
            get { return _warningav; }
        }
        /// <summary>
        /// 警告B相电压22
        /// </summary>
        public decimal? warningBV
        {
            set { _warningbv = value; }
            get { return _warningbv; }
        }
        /// <summary>
        /// 警告C相电压22
        /// </summary>
        public decimal? warningCV
        {
            set { _warningcv = value; }
            get { return _warningcv; }
        }
        /// <summary>
        /// 预警A相电压23
        /// </summary>
        public decimal? trailerAV2
        {
            set { _trailerav2 = value; }
            get { return _trailerav2; }
        }
        /// <summary>
        /// 预警B相电压23
        /// </summary>
        public decimal? trailerBV2
        {
            set { _trailerbv2 = value; }
            get { return _trailerbv2; }
        }
        /// <summary>
        /// 预警C相电压23
        /// </summary>
        public decimal? trailerCV2
        {
            set { _trailercv2 = value; }
            get { return _trailercv2; }
        }
        /// <summary>
        /// 警告A相电压23
        /// </summary>
        public decimal? warningAV2
        {
            set { _warningav2 = value; }
            get { return _warningav2; }
        }
        /// <summary>
        /// 警告B相电压23
        /// </summary>
        public decimal? warningBV2
        {
            set { _warningbv2 = value; }
            get { return _warningbv2; }
        }
        /// <summary>
        /// 警告C相电压23
        /// </summary>
        public decimal? warningCV2
        {
            set { _warningcv2 = value; }
            get { return _warningcv2; }
        }
        /// <summary>
        ///
        /// </summary>
        public bool isdel
        {
            set { _isdel = value; }
            get { return _isdel; }
        }
        #endregion Model
        #region extend
        public double AU { get; }
        public double BU { get; }
        public double CU { get; }
        public double DU { get; }
        public double EU { get; }
        public double FU { get; }
        public double GU { get; }
        public double HU { get; }
        //A相电流
        public double AI { get { return Math.Round(string.IsNullOrEmpty(_value) ? 0 : Int32.Parse(_value.Substring(14, 4).Substring(2)+ _value.Substring(14, 4).Substring(0,2), System.Globalization.NumberStyles.HexNumber) * 0.1,2); } }
        public double BI { get { return Math.Round(string.IsNullOrEmpty(_value) ? 0 : Int32.Parse(_value.Substring(18, 4).Substring(2) + _value.Substring(18, 4).Substring(0, 2), System.Globalization.NumberStyles.HexNumber) * 0.1,2); } }
        public double CI { get { return Math.Round(string.IsNullOrEmpty(_value) ? 0 : Int32.Parse(_value.Substring(22, 4).Substring(2) + _value.Substring(22, 4).Substring(0, 2), System.Globalization.NumberStyles.HexNumber) * 0.1, 2); } }
        /// <summary>
        /// 漏电
        /// </summary>
        public double LI { get { return Math.Round(string.IsNullOrEmpty(_value) ? 0 : Int32.Parse(_value.Substring(98, 4).Substring(2) + _value.Substring(98, 4).Substring(0, 2), System.Globalization.NumberStyles.HexNumber) * 0.1,2); } }
        //1-4路温度
        public double Temp1 { get { return Math.Round(string.IsNullOrEmpty(_value) ? 0 : Int32.Parse(_value.Substring(102, 4).Substring(2) + _value.Substring(102, 4).Substring(0, 2), System.Globalization.NumberStyles.HexNumber) * 0.1, 2); } }
        public double Temp2 { get { return Math.Round(string.IsNullOrEmpty(_value) ? 0 : Int32.Parse(_value.Substring(106, 4).Substring(2) + _value.Substring(106, 4).Substring(0, 2), System.Globalization.NumberStyles.HexNumber) * 0.1, 2); } }
        public double Temp3 { get { return Math.Round(string.IsNullOrEmpty(_value) ? 0 : Int32.Parse(_value.Substring(126, 4).Substring(2) + _value.Substring(126, 4).Substring(0, 2), System.Globalization.NumberStyles.HexNumber) * 0.1, 2); } }
        public double Temp4 { get { return Math.Round(string.IsNullOrEmpty(_value) ? 0 : Int32.Parse(_value.Substring(130, 4).Substring(2) + _value.Substring(130, 4).Substring(0, 2), System.Globalization.NumberStyles.HexNumber) * 0.1, 2); } }
        #endregion
    }
    public class dt_item_adq : PagerBase
    {
        public string devicename { get; set; }
        public int? user_id { get; set; }
    }
}

