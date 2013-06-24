using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace SSW.Framework.Web.Mvc
{

    /// <summary>
    /// </summary>
    /// <example>
    ///    var cacheKey = CacheHelper.CreateKey("Cache Name", "Add Optional Id's", "Add Optional Filters");
    ///    return CacheHelper.GetCacheItem(cacheKey, new TimeSpan(0, 30, 0), () =>
    ///        {
    ///            return "Database call or something Big";
    ///        });
    /// </example>
    public static class CacheHelper
    {
        public static T GetCacheItem<T>(this Cache cache, string key, TimeSpan expirationPeriod, Func<T> func)
        {
            var obj = cache[key];
            if (obj == null)
            {
                obj = func.Invoke();
                if (obj != null && expirationPeriod > TimeSpan.Zero)
                {
                    cache.Insert(key, obj, null, DateTime.Now.Add(expirationPeriod), Cache.NoSlidingExpiration);
                }
            }

            return (T)obj;
        }
        public static T GetCacheItem<T>(string key, TimeSpan expirationPeriod, Func<T> func)
        {
            return HttpRuntime.Cache.GetCacheItem(key, expirationPeriod, func);
        }

        public static void RemoveCache(this Cache cache, string key)
        {
            cache.Remove(key);
        }
        public static void RemoveCache(string key)
        {
            HttpRuntime.Cache.RemoveCache(key);
        }

        public static string CreateKey(params object[] values)
        {
            return string.Join(";", values);
        }

        private static bool IsNotEmptyEnumerable(object obj)
        {
            var enu = obj as IEnumerable;
            if (enu != null)
            {
                var rator = enu.GetEnumerator();
                if (rator.MoveNext())
                {
                    rator.Reset();
                    return true;
                }
                return false;
            }

            return true;
        }

        [Obsolete]
        public static T GetOrStore<T>(this Cache cache, string key, Func<T> generator)
        {
            var result = cache[key];
            if (result == null)
            {
                result = generator();
                cache[key] = result;
            }
            return (T)result;
        }
    }
}
