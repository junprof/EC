using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;

namespace DTcms.BLL.Cache
{
    public class DataCache
    {
        public static object Get(string key)
        {
            return MemoryCache.Default.Get(key);
        }
        /// <summary>
        /// 获取缓存，若没有则更新
        ///     更新方法无参
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="func">更新方法</param>
        /// <param name="absoluteExpiration">缓存过期时间(时间点)</param>
        /// <param name="force">强制刷新</param>
        /// <returns></returns>
        public static T GetAndUpdate<T>(string key,Func<T> func, DateTimeOffset? absoluteExpiration = null,bool force=false) where T:new()
        {
            T t = new T();
            t = (T)Convert.ChangeType(Get(key),typeof(T));
            if(t == null || force)
            {
                lock (ObjLock)
                {
                    if (t == null || force)
                    {
                        t = (T)Convert.ChangeType(func(), typeof(T));
                        if (absoluteExpiration.HasValue)
                            Add(key, t, absoluteExpiration.Value);
                        else
                            Add(key, t);
                    }
                }
            }
            return t;
        }
        readonly static object ObjLock = new object();
        /// <summary>
        /// 默认缓存12小时
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Add(string key, object value)
        {
            DateTimeOffset absoluteExpiration = new DateTimeOffset(DateTime.Now.AddHours(12));
            return Add(key, value, absoluteExpiration);
        }
        public static bool Add(string key, object value, DateTimeOffset absoluteExpiration)
        {
            return MemoryCache.Default.Add(key, value, absoluteExpiration);
        }
        public static void Delete(string key)
        {
            MemoryCache cache= MemoryCache.Default;
            if(cache.Contains(key))
            MemoryCache.Default.Remove(key);
        }
        public static void Clear()
        {
            MemoryCache.Default.Any(p => { MemoryCache.Default.Remove(p.Key); return false; });
        }
        public static Dictionary<string,object> ListCache()
        {
            Dictionary<string, object> cacheDic = new Dictionary<string, object>();
            MemoryCache.Default.Any(p=> { cacheDic.Add(p.Key, p.Value); return false; });
            return cacheDic;
        }
    }
}
