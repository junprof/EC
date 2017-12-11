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


namespace OnenetWaringMonitor
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
       // public static Task thread;
        public delegate void showHandler(string m);
       // public static showHandler sdel;
        static void Main(string[] args)
        {

            timer = new System.Timers.Timer();
            // 每隔10秒执行
            timer.Interval = 10 * 1000;
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
            datacount = 0;
            timercount = 0;
            curxcendcount = 0;//当前线程结束数量
            xccount = 0;//线程总数
            try
            {

                string[] strData = new string[] { "", "", "", "", "" };

                //string strSql = "select top 121 * from Send where show=0 order by astate asc";
                DTcms.BLL.dt_msg obll = new DTcms.BLL.dt_msg();
                DataSet ds = obll.GetList(121, " state!=1", " addtime asc");
                datacount = ds.Tables[0].Rows.Count;
                int xhmaxcount = 120;
                if (datacount < 121)
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
                    if (datacount > 120)
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
                isAllComplete = true;
                timer.Enabled = true;
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
                
                string[] strData = new string[] { "", "", "", "", "" };
                callbackinfo smsc = (callbackinfo)o;
                DataSet ds = smsc.Ds;
                //string sqlstr = "";
                //string updatesql = "";
                //string ids = "";
                for (int i = smsc.Startindex; i <= smsc.Endindex; i++)
                {
                    //发短信操作
                    string phone = smsc.Ds.Tables[0].Rows[i]["phone"].ToString();
                    string title = smsc.Ds.Tables[0].Rows[i]["title"].ToString();
                    string res = "";
                    foreach (var item in phone.Split(','))
                    {
                        if (item != "")
                        {
                            res += SmsHelper.Send(item, "流量通科技", title, "liuliangt", "hvu7jcvzo81mvoo8endj4etoqmyhng8g", "d9cb15bb613274662803c4edfe121db12611","");
                        }
                    }
                    
                    if (res.Contains("000000"))
                    {
                        DTcms.BLL.dt_msg bll = new DTcms.BLL.dt_msg();
                        bll.UpdateState(true,System.Guid.Parse(smsc.Ds.Tables[0].Rows[0]["hid"].ToString()));
                        Console.WriteLine(smsc.Ds.Tables[0].Rows[0]["hid"].ToString() + "发送成功" + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"));
                        Log.Info(smsc.Ds.Tables[0].Rows[0]["hid"].ToString() + "_" + res + "\r\n");
                    }else
                    {
                        Console.WriteLine(smsc.Ds.Tables[0].Rows[0]["hid"].ToString() + "发送失败" + "-"+res+"-" + DateTime.Now.ToString("yyyyMMddHHmmss"));
                        Log.Error(smsc.Ds.Tables[0].Rows[0]["hid"].ToString() + "_" + res + "\r\n");
                    }
                    
                }
                
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                Console.WriteLine(pc + "操作失败");
            }
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