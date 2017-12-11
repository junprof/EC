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
        private double? _warningai;
        private double? _warningbi;
        private double? _warningci;
        private double? _warningli;
        private double? _warningonetemperature;
        private double? _warningtwotemperature;
        private double? _warningthreetemperature;
        private double? _warningfourtemperature;
        private double? _trailerai;
        private double? _trailerbi;
        private double? _trailerci;
        private double? _trailerli;
        private double? _traileronetemperature;
        private double? _trailertwotemperature;
        private double? _trailerthreetemperature;
        private double? _trailerfourtemperature;
        private double? _trailerav;
        private double? _trailerbv;
        private double? _trailercv;
        private double? _warningav;
        private double? _warningbv;
        private double? _warningcv;
        private double? _trailerav2;
        private double? _trailerbv2;
        private double? _trailercv2;
        private double? _warningav2;
        private double? _warningbv2;
        private double? _warningcv2;
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
        public double? warningAI
        {
            set { _warningai = value; }
            get { return _warningai; }
        }
        /// <summary>
        /// 警告B相电流
        /// </summary>
        public double? warningBI
        {
            set { _warningbi = value; }
            get { return _warningbi; }
        }
        /// <summary>
        /// 警告C相电流
        /// </summary>
        public double? warningCI
        {
            set { _warningci = value; }
            get { return _warningci; }
        }
        /// <summary>
        /// 警告漏电流
        /// </summary>
        public double? warningLI
        {
            set { _warningli = value; }
            get { return _warningli; }
        }
        /// <summary>
        /// 警告1路温度
        /// </summary>
        public double? warningOneTemperature
        {
            set { _warningonetemperature = value; }
            get { return _warningonetemperature; }
        }
        /// <summary>
        /// 警告2路温度
        /// </summary>
        public double? warningTwoTemperature
        {
            set { _warningtwotemperature = value; }
            get { return _warningtwotemperature; }
        }
        /// <summary>
        /// 警告3路温度
        /// </summary>
        public double? warningThreeTemperature
        {
            set { _warningthreetemperature = value; }
            get { return _warningthreetemperature; }
        }
        /// <summary>
        /// 警告4路温度
        /// </summary>
        public double? warningFourTemperature
        {
            set { _warningfourtemperature = value; }
            get { return _warningfourtemperature; }
        }
        /// <summary>
        /// 预警A电流
        /// </summary>
        public double? trailerAI
        {
            set { _trailerai = value; }
            get { return _trailerai; }
        }
        /// <summary>
        /// 预警B电流
        /// </summary>
        public double? trailerBI
        {
            set { _trailerbi = value; }
            get { return _trailerbi; }
        }
        /// <summary>
        /// 预警C电流
        /// </summary>
        public double? trailerCI
        {
            set { _trailerci = value; }
            get { return _trailerci; }
        }
        /// <summary>
        /// 预警漏电流
        /// </summary>
        public double? trailerLI
        {
            set { _trailerli = value; }
            get { return _trailerli; }
        }
        /// <summary>
        /// 预警1路温度
        /// </summary>
        public double? trailerOneTemperature
        {
            set { _traileronetemperature = value; }
            get { return _traileronetemperature; }
        }
        /// <summary>
        /// 预警2路温度
        /// </summary>
        public double? trailerTwoTemperature
        {
            set { _trailertwotemperature = value; }
            get { return _trailertwotemperature; }
        }
        /// <summary>
        /// 预警3路温度
        /// </summary>
        public double? trailerThreeTemperature
        {
            set { _trailerthreetemperature = value; }
            get { return _trailerthreetemperature; }
        }
        /// <summary>
        /// 预警4路温度
        /// </summary>
        public double? trailerFourTemperature
        {
            set { _trailerfourtemperature = value; }
            get { return _trailerfourtemperature; }
        }
        /// <summary>
        /// 预警A相电压22
        /// </summary>
        public double? trailerAV
        {
            set { _trailerav = value; }
            get { return _trailerav; }
        }
        /// <summary>
        /// 预警B向电压22
        /// </summary>
        public double? trailerBV
        {
            set { _trailerbv = value; }
            get { return _trailerbv; }
        }
        /// <summary>
        /// 预警C向电压22
        /// </summary>
        public double? trailerCV
        {
            set { _trailercv = value; }
            get { return _trailercv; }
        }
        /// <summary>
        /// 警告A相电压22
        /// </summary>
        public double? warningAV
        {
            set { _warningav = value; }
            get { return _warningav; }
        }
        /// <summary>
        /// 警告B相电压22
        /// </summary>
        public double? warningBV
        {
            set { _warningbv = value; }
            get { return _warningbv; }
        }
        /// <summary>
        /// 警告C相电压22
        /// </summary>
        public double? warningCV
        {
            set { _warningcv = value; }
            get { return _warningcv; }
        }
        /// <summary>
        /// 预警A相电压23
        /// </summary>
        public double? trailerAV2
        {
            set { _trailerav2 = value; }
            get { return _trailerav2; }
        }
        /// <summary>
        /// 预警B相电压23
        /// </summary>
        public double? trailerBV2
        {
            set { _trailerbv2 = value; }
            get { return _trailerbv2; }
        }
        /// <summary>
        /// 预警C相电压23
        /// </summary>
        public double? trailerCV2
        {
            set { _trailercv2 = value; }
            get { return _trailercv2; }
        }
        /// <summary>
        /// 警告A相电压23
        /// </summary>
        public double? warningAV2
        {
            set { _warningav2 = value; }
            get { return _warningav2; }
        }
        /// <summary>
        /// 警告B相电压23
        /// </summary>
        public double? warningBV2
        {
            set { _warningbv2 = value; }
            get { return _warningbv2; }
        }
        /// <summary>
        /// 警告C相电压23
        /// </summary>
        public double? warningCV2
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
        public double AU { get { return Math.Round(string.IsNullOrEmpty(_value) ? 0 : Int32.Parse(_value.Substring(42, 4).Substring(2) + _value.Substring(42, 4).Substring(0, 2), System.Globalization.NumberStyles.HexNumber) * 0.1, 2); } }
        public double BU { get { return Math.Round(string.IsNullOrEmpty(_value) ? 0 : Int32.Parse(_value.Substring(46, 4).Substring(2) + _value.Substring(46, 4).Substring(0, 2), System.Globalization.NumberStyles.HexNumber) * 0.1, 2); } }
        public double CU { get { return Math.Round(string.IsNullOrEmpty(_value) ? 0 : Int32.Parse(_value.Substring(50, 4).Substring(2) + _value.Substring(50, 4).Substring(0, 2), System.Globalization.NumberStyles.HexNumber) * 0.1, 2); } }
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
    public class dt_item_ex : dt_item
    {
        public string real_name { get; set; }
        public string areaname { get; set; }
    }
    public class dt_item_adq : PagerBase
    {
        public string devicename { get; set; }
        public int? user_id { get; set; }
    }
}

