using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using DTcms.Common;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using DTcms.DBUtility;

namespace OnenetDataGet
{
    class Program
    {
        public static System.Timers.Timer timer;
        public static List<Task> TaskList = new List<Task>();
        public static bool isAllComplete = true;
        public static int datacount = 0;
        public static int timercount = 0;
        public static int curxcendcount = 0;//当前线程结束数量
        public static int xccount = 0;//线程总数
        //public static Task thread;
        public delegate void showHandler(string m);
       // public static showHandler sdel;
        static void Main(string[] args)
        {
            //Dictionary<string, string> dclist = new Dictionary<string, string>();
            //dclist.Add("api-key", "9ei=2lJcB3sRIHVzXIL1M5wsRh8=");
            //string idstr = "10037577";
            ////查询设备状态
            ////string res = Utils.SendRequest("http://api.heclouds.com/devices/status", "GET", "?devIds=" + idstr, dclist, 0);//批量查询设备状态

            //string res = Utils.SendRequest("http://api.heclouds.com/devices/datapoints", "GET", "?devIds=" + idstr, dclist, 0);//批量查询设备最新数据  限制一次最多1000个设备


            //Console.WriteLine(res);
            //Console.ReadLine();

            timer = new System.Timers.Timer();
            // 每隔10秒执行
            timer.Interval = 20 * 1000;
            //设置是执行一次（false）还是一直执行(true)； 
            timer.AutoReset = true;
            //timer.AutoReset = false;
            //是否执行System.Timers.Timer.Elapsed事件；
            timer.Enabled = true;
            // 开始
            timer.Start();
            // 设置timer可以激发Elapsed事件
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            Console.ReadLine();
        }
        /// <summary>
        /// 时间间隔到达后执行的代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (isAllComplete)
            {
                smstimer();
            }
        }

        public static void smstimer()
        {
            isAllComplete = false;
            //if (smscount >= 500)
            //{
            //    smscount = 0;
            //}
            datacount = 0;
            timercount = 0;
            curxcendcount = 0;//当前线程结束数量
            xccount = 0;//线程总数
            try
            {

                string[] strData = new string[] { "", "", "", "", "" };

                //string strSql = "select top 121 * from Send where show=0 order by astate asc";
                DTcms.BLL.dt_item obll = new DTcms.BLL.dt_item();
                DataSet ds = obll.GetList(501, "i.state=2 and i.isdel!=1 ", " i.updatetime asc,i.id asc");
                datacount = ds.Tables[0].Rows.Count;
                int xhmaxcount = 500;
                if (datacount < 501)
                {
                    xhmaxcount = datacount;
                }
                else
                {
                    timer.Enabled = false;
                }
                xccount = xhmaxcount % 8 == 0 ? xhmaxcount / 8 : Convert.ToInt32(xhmaxcount / 8) + 1;
                for (int i = 0; i < xccount; i++)
                {
                    //thread = new Task(exesms, smsc);
                    //thread.Start();

                    callbackinfo _callbackinfo = new callbackinfo();
                    _callbackinfo.Ds = ds;
                    _callbackinfo.Startindex = (i * 8);
                    _callbackinfo.Endindex = (i + 1) * 8 - 1;
                    if (_callbackinfo.Endindex > xhmaxcount - 1)
                    {
                        _callbackinfo.Endindex = xhmaxcount - 1;
                    }
                    var task = Task.Factory.StartNew(callback, _callbackinfo);
                    TaskList.Add(task);

                }
                //异步等待所有任务执行完毕  
                Task.Factory.StartNew(x =>
                {
                    Task.WaitAll(TaskList.ToArray());
                    //标记所有任务运行完成  
                    isAllComplete = true;
                    if (datacount > 500)
                    {
                        smstimer();
                    }
                    else
                    {
                        if (!timer.Enabled)
                        {
                            timer.Enabled = true;
                        }
                    }
                }, null);

            }
            catch
            {

            }
            //Thread.Sleep(100);

            //if (datacount > 80)
            //{
            //    smstimer();
            //}
            //else
            //{
            //    if (!timer.Enabled)
            //    {
            //        timer.Enabled = true;
            //    }
            //}
        }

        #region 清除HTML标记
        public static string DropHTML(string Htmlstring)
        {
            if (string.IsNullOrEmpty(Htmlstring)) return "";
            //删除脚本  
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML  
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring.Replace("&emsp;", "");
            return Htmlstring;
        }
        #endregion

        public static void callback(Object o)
        {
            string pc = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            try
            {
                DTcms.BLL.dt_msg bll = new DTcms.BLL.dt_msg();

                string[] strData = new string[] { "", "", "", "", "" };
                callbackinfo smsc = (callbackinfo)o;
                DataSet ds = smsc.Ds;
                List<string> sqlstr = new List<string>();
                List<string> updatesql = new List<string>();
                List<string> msgsql = new List<string>();
                List<string> dimsqllist = new List<string>();
                string ids = "";
                for (int i = smsc.Startindex; i <= smsc.Endindex; i++)
                {
                    ids += "," + ds.Tables[0].Rows[i]["onenetnum"];
                }
                ids = ids.Substring(1);

                Dictionary<string, string> dclist = new Dictionary<string, string>();
                dclist.Add("api-key", "9ei=2lJcB3sRIHVzXIL1M5wsRh8=");
                //查询设备状态
                string stateres = Utils.SendRequest("http://api.heclouds.com/devices/status", "GET", "?devIds=" + ids, dclist, 0);//批量查询设备状态

                string res = Utils.SendRequest("http://api.heclouds.com/devices/datapoints", "GET", "?devIds=" + ids, dclist, 0);//批量查询设备最新数据  限制一次最多1000个设备
                if (stateres != "发生异常" && res != "发生异常")
                {
                    DatapointsModel datapointsModel = JsonHelper.JSONToObject<DatapointsModel>(res);//解析Json数据
                    DataStatusModel dataStatusModel = JsonHelper.JSONToObject<DataStatusModel>(stateres);//解析Json数据

                    Dictionary<int, string> msgdic = new Dictionary<int, string>();
                    foreach (var item in datapointsModel.data.devices)
                    {
                        msgdic.Clear();
                        double AIVAL = 0;
                        double BIVAL = 0;
                        double CIVAL = 0;
                        double LIVAL = 0;
                        double OneTemperatureVAL = 0;
                        double TwoTemperatureVAL = 0;
                        double ThreeTemperatureVAL = 0;
                        double FourTemperatureVAL = 0;
                        string val = "";
                        string updatetime = "";
                        int online = 0;
                        var m = dataStatusModel.data.devices.SingleOrDefault(x => x.id == item.id);
                        if (m != null)
                        {
                            if (m.online)
                                online = 1;
                            else
                                online = 0;
                        }
                        foreach (var item_s in item.datastreams)
                        {
                            if (item_s.id == "K1")
                            {
                                val = item_s.value;
                                updatetime = item_s.at;
                            }
                        }
                        var q = ds.Tables[0].Select($" onenetnum='{item.id}' ");
                        if (q.Length > 0)
                        {
                            string AI = val.Substring(14, 4);//A相电流
                            string BI = val.Substring(18, 4);//B相电流
                            string CI = val.Substring(22, 4);//C相电流
                            string LI = val.Substring(98, 4);//漏电流
                            string OneTemperature = val.Substring(102, 4);//1路温度
                            string TwoTemperature = val.Substring(106, 4);//2路温度
                            string ThreeTemperature = val.Substring(126, 4);//3路温度
                            string FourTemperature = val.Substring(130, 4);//4路温度

                            
                            int statevalAI = getVal(AI, Convert.ToDouble(q[0]["trailerAI"]), Convert.ToDouble(q[0]["warningAI"]),ref AIVAL);
                            int statevalBI = getVal(BI, Convert.ToDouble(q[0]["trailerBI"]), Convert.ToDouble(q[0]["warningBI"]),ref BIVAL);
                            int statevalCI = getVal(CI, Convert.ToDouble(q[0]["trailerCI"]), Convert.ToDouble(q[0]["warningCI"]),ref CIVAL);
                            int statevalLI = getVal(LI, Convert.ToDouble(q[0]["trailerLI"]), Convert.ToDouble(q[0]["warningLI"]),ref LIVAL);

                            int stateOneTemperature = getVal(OneTemperature, Convert.ToDouble(q[0]["trailerOneTemperature"]), Convert.ToDouble(q[0]["warningOneTemperature"]),ref OneTemperatureVAL);
                            int stateTwoTemperature = getVal(TwoTemperature, Convert.ToDouble(q[0]["trailerTwoTemperature"]), Convert.ToDouble(q[0]["warningTwoTemperature"]),ref TwoTemperatureVAL);
                            int stateThreeTemperature = getVal(ThreeTemperature, Convert.ToDouble(q[0]["trailerThreeTemperature"]), Convert.ToDouble(q[0]["warningThreeTemperature"]),ref ThreeTemperatureVAL);
                            int stateFourTemperature = getVal(FourTemperature, Convert.ToDouble(q[0]["trailerFourTemperature"]), Convert.ToDouble(q[0]["warningFourTemperature"]),ref FourTemperatureVAL);
                            if (statevalAI != 0 || statevalBI != 0 || statevalCI != 0 || statevalLI != 0 || stateOneTemperature != 0 || stateTwoTemperature != 0 || stateThreeTemperature != 0 || stateFourTemperature != 0)
                            {
                                //if (statevalAI == 1 || statevalBI == 1 || statevalCI == 1 || statevalLI == 1 || stateOneTemperature == 1 || stateTwoTemperature == 1 || stateThreeTemperature == 1 || stateFourTemperature == 1)
                                //{
                                //    havetrailer = true;
                                //}
                                if (statevalAI == 1)
                                {
                                    msgdic.Add(1, $"电流超出预警（{AI}A）");
                                }
                                if (statevalBI == 1)
                                {
                                    msgdic.Add(2, $"电流超出预警（{BI}A）");
                                }
                                if (statevalCI == 1)
                                {
                                    msgdic.Add(3, $"电流超出预警（{CI}A）");
                                }
                                if (statevalLI == 1)
                                {
                                    msgdic.Add(10, $"电流超出预警（{LI}A）");
                                }
                                if (stateOneTemperature == 1)
                                {
                                    msgdic.Add(11, $"超温预警（{OneTemperature}℃）");
                                }
                                if (stateTwoTemperature == 1)
                                {
                                    msgdic.Add(12, $"超温预警（{TwoTemperature}℃）");
                                }
                                if (stateThreeTemperature == 1)
                                {
                                    msgdic.Add(13, $"超温预警（{ThreeTemperature}℃）");
                                }
                                if (stateFourTemperature == 1)
                                {
                                    msgdic.Add(14, $"超温预警（{FourTemperature}℃）");
                                }
                                #region 注释
                                //string content = "";
                                //if (statevalAI == 1)
                                //{//表示达到预警值
                                //    content = "，A相电流预警";
                                //}
                                //else if (statevalAI == 2)
                                //{//表示达到告警值
                                //    content = "，A相电流告警";
                                //}

                                //if (statevalBI == 1)
                                //{//表示达到预警值
                                //    content = "，B相电流预警";
                                //}
                                //else if (statevalBI == 2)
                                //{//表示达到告警值
                                //    content = "，B相电流告警";
                                //}

                                //if (statevalCI == 1)
                                //{//表示达到预警值
                                //    content = "，C相电流预警";
                                //}
                                //else if (statevalCI == 2)
                                //{//表示达到告警值
                                //    content = "，C相电流告警";
                                //}

                                //if (statevalLI == 1)
                                //{//表示达到预警值
                                //    content = "，漏电流预警";
                                //}
                                //else if (statevalLI == 2)
                                //{//表示达到告警值
                                //    content = "，漏电流告警";
                                //}

                                //if (stateOneTemperature == 1)
                                //{//表示达到预警值
                                //    content = "，1路温度预警";
                                //}
                                //else if (stateOneTemperature == 2)
                                //{//表示达到告警值
                                //    content = "，1路温度告警";
                                //}

                                //if (stateTwoTemperature == 1)
                                //{//表示达到预警值
                                //    content = "，2路温度预警";
                                //}
                                //else if (stateTwoTemperature == 2)
                                //{//表示达到告警值
                                //    content = "，2路温度告警";
                                //}

                                //if (stateThreeTemperature == 1)
                                //{//表示达到预警值
                                //    content = "，3路温度预警";
                                //}
                                //else if (stateThreeTemperature == 2)
                                //{//表示达到告警值
                                //    content = "，3路温度告警";
                                //}

                                //if (stateFourTemperature == 1)
                                //{//表示达到预警值
                                //    content = "，4路温度预警";
                                //}
                                //else if (stateFourTemperature == 2)
                                //{//表示达到告警值
                                //    content = "，4路温度告警";
                                //}
                                #endregion
                                //msg += content.Substring(1);

                            }
                        }
                        

                        string trailerval = "{\"AI\":"+ q[0]["trailerAI"] + ",\"BI\":"+ q[0]["trailerBI"] + ",\"CI\":"+ q[0]["trailerCI"] + ",\"LI\":"+ q[0]["trailerLI"] + ",\"OneTemperature\":"+ q[0]["trailerOneTemperature"]
                            + ",\"TwoTemperature\":"+ q[0]["trailerTwoTemperature"] + ",\"ThreeTemperature\":"+ q[0]["trailerThreeTemperature"] + ",\"FourTemperature\":"+ q[0]["trailerFourTemperature"] + " }";

                        string warningval = "{\"AI\":" + q[0]["warningAI"] + ",\"BI\":" + q[0]["warningBI"] + ",\"CI\":" + q[0]["warningCI"] + ",\"LI\":" + q[0]["warningLI"] + ",\"OneTemperature\":" + q[0]["warningOneTemperature"]
                            + ",\"TwoTemperature\":" + q[0]["warningTwoTemperature"] + ",\"ThreeTemperature\":" + q[0]["warningThreeTemperature"] + ",\"FourTemperature\":" + q[0]["warningFourTemperature"] + " }";
                        string id = System.Guid.NewGuid().ToString();
                        sqlstr.Add($" insert into dt_historydata(id,name,value,addtime,updatetime,type,item_id,online,trailerval,warningval)  values('{id}','K1','{val}',getdate(),'{updatetime}',1,{q[0]["id"]},{online},'{trailerval}','{warningval}') ");
                        dimsqllist.Add($"insert into dt_dimensioninfo(dimension,value,trailerval,warningval,updatetime,hid) values(1,{AIVAL},{Convert.ToDouble(q[0]["trailerAI"])},{Convert.ToDouble(q[0]["warningAI"])},getdate(),'{id}')");
                        dimsqllist.Add($"insert into dt_dimensioninfo(dimension,value,trailerval,warningval,updatetime,hid) values(2,{BIVAL},{Convert.ToDouble(q[0]["trailerBI"])},{Convert.ToDouble(q[0]["warningBI"])},getdate(),'{id}')");
                        dimsqllist.Add($"insert into dt_dimensioninfo(dimension,value,trailerval,warningval,updatetime,hid) values(3,{CIVAL},{Convert.ToDouble(q[0]["trailerCI"])},{Convert.ToDouble(q[0]["warningCI"])},getdate(),'{id}')");
                        dimsqllist.Add($"insert into dt_dimensioninfo(dimension,value,trailerval,warningval,updatetime,hid)  values(10,{LIVAL},{Convert.ToDouble(q[0]["trailerLI"])},{Convert.ToDouble(q[0]["warningLI"])},getdate(),'{id}')");
                        dimsqllist.Add($"insert into dt_dimensioninfo(dimension,value,trailerval,warningval,updatetime,hid) values(11,{OneTemperatureVAL},{Convert.ToDouble(q[0]["trailerOneTemperature"])},{Convert.ToDouble(q[0]["warningOneTemperature"])},getdate(),'{id}')");
                        dimsqllist.Add($"insert into dt_dimensioninfo(dimension,value,trailerval,warningval,updatetime,hid)  values(12,{TwoTemperatureVAL},{Convert.ToDouble(q[0]["trailerTwoTemperature"])},{Convert.ToDouble(q[0]["warningTwoTemperature"])},getdate(),'{id}')");
                        dimsqllist.Add($"insert into dt_dimensioninfo(dimension,value,trailerval,warningval,updatetime,hid) values(13,{ThreeTemperatureVAL},{Convert.ToDouble(q[0]["trailerThreeTemperature"])},{Convert.ToDouble(q[0]["warningThreeTemperature"])},getdate(),'{id}')");
                        dimsqllist.Add($"insert into dt_dimensioninfo(dimension,value,trailerval,warningval,updatetime,hid) values(14,{FourTemperatureVAL},{Convert.ToDouble(q[0]["trailerFourTemperature"])},{Convert.ToDouble(q[0]["warningFourTemperature"])},getdate(),'{id}')");

                        updatesql.Add($" update dt_item set value='{val}',updatetime='{updatetime}',online={online} where onenetnum='{item.id}' ");

                        foreach (var dic in msgdic)
                        {
                            if (bll.GetRecordCount($" addtime >= convert(datetime,convert(varchar(10),GETDATE(),120)) and isprocessed <> 1 and item_id = {q[0]["id"]} and dim_id = {dic.Key} ") == 0)
                            {
                                msgsql.Add($" insert into dt_msg(id,hid,title,item_id,addtime,phone,user_id,state,content,dim_id) values('{Guid.NewGuid().ToString()}','{id}','{$"您的设备({q[0]["name"].ToString().Replace("'", "")})有预警消息"}',{q[0]["id"]},getdate(),'{q[0]["telephone"]}',{q[0]["user_id"]},0,'{dic.Value}',{dic.Key}) ");
                            }
                        }
                    }

                    if (DbHelperSQL.ExecuteSqlTran(sqlstr) > 0)
                    {
                        DbHelperSQL.ExecuteSqlTran(dimsqllist);
                        DbHelperSQL.ExecuteSqlTran(updatesql);

                        if (msgsql.Count>0)
                        {
                            DbHelperSQL.ExecuteSqlTran(msgsql);
                        }

                        Log.Info(pc + "\r\n" + ids + "\r\n");
                        Console.WriteLine(pc + "操作成功");
                    }else
                    {
                        Log.Fatal(pc + "\r\n" + ids + "\r\n");
                        Console.WriteLine(pc + "操作失败");
                    }


                }else
                {
                    Log.Fatal(pc + "\r\n" + ids + "\r\n");
                    Console.WriteLine(pc + "操作失败1");
                }

                
                //for (int i = smsc.Startindex; i <= smsc.Endindex; i++)
                //{
                //    sqlstr += "INSERT INTO dt_historydata([name],[value],[addtime],[updatetime],[type],[checkcode],[functioncode],[datahead])";
                //    sqlstr += " VALUES('K1','')";
                //}

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                Console.WriteLine(pc + "操作失败");
            }
        }

        /// <summary>
        /// 获取预警或告警的状态 2：表示达到告警值，1：表示达到预警值，0：表示正常
        /// </summary>
        /// <param name="val">4位的值</param>
        /// <param name="trailerval">预警值</param>
        /// <param name="warningval">告警值</param>
        /// <returns>2：表示达到告警值，1：表示达到预警值，0：表示正常</returns>
        public static int getVal(string val, double trailerval, double warningval,ref double res)
        {
            val = val.Substring(2) + val.Substring(0, 2);
            res = Int32.Parse(val, System.Globalization.NumberStyles.HexNumber) * 0.1;
            if (warningval != 0 && res >= warningval)
            {//超过告警值返回
                return 2;
            }
            if (trailerval != 0 && res >= trailerval)
            {//超过预警值返回
                return 1;
            }
            return 0;
        }


    }



    public class DatapointsModel
    {
        public int errno { get; set; }
        public string error { get; set; }
        public datapointsData data { get; set; }
    }

    public class datapointsData
    {
        public datapointsDevice[] devices { get; set; }
    }

    public class datapointsDevice
    {
        public int id { get; set; }
        public string title { get; set; }
        public datapointsDatastreams[] datastreams { get; set; }
    }

    public class datapointsDatastreams
    {
        public string id { get; set; }
        public string at { get; set; }
        public string value { get; set; }
    }

    public class DataStatusModel
    {
        public int errno { get; set; }
        public string error { get; set; }
        public DataStatusModelData data { get; set; }
    }

    public class DataStatusModelData
    {
        public int total_count { get; set; }
        public DataStatusModelDevice[] devices { get; set; }
    }

    public class DataStatusModelDevice
    {
        public int id { get; set; }
        public string title { get; set; }
        public bool online { get; set; }
    }


    public class callbackinfo
    {
        private DataSet ds;

        public DataSet Ds
        {
            get { return ds; }
            set { ds = value; }
        }

        private int startindex = 0;

        public int Startindex
        {
            get { return startindex; }
            set { startindex = value; }
        }

        private int endindex = 0;

        public int Endindex
        {
            get { return endindex; }
            set { endindex = value; }
        }


    }
}