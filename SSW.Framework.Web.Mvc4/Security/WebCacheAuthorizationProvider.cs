using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SSW.Framework.Web.Mvc.Security
{
    public class WebCacheAuthorizationProvider : IAuthorizationProvider
    {
        
        private const string CacheKeyPrefix = "WEBUSER_";

        private IAuthorizationProvider _baseProvider;
        private HttpContextBase _context;

        public WebCacheAuthorizationProvider(HttpContextBase context, IAuthorizationProvider baseProvider)
        {
            _context = context;
            _baseProvider = baseProvider;
        }

        private static string GetCacheKey(string name)
        {
            return CacheKeyPrefix + name;
        }

        public IPrincipal GetPrincipal(IIdentity identity)
        {
            var key = GetCacheKey(identity.Name);
            var user = _context.Cache.GetOrStore(key, () => _baseProvider.GetPrincipal(identity));
            return user;
        }

        public static void RemoveCache(string userName)
        {
            var key = GetCacheKey(userName);
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Cache.Remove(key);
            }
        }
    }
}
