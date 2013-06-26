using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SSW.Framework.Web.Mvc.Security
{
    /// <summary>
    /// Authorization Provider Interface
    /// </summary>
    public interface IAuthorizationProvider
    {
        /// <summary>
        /// Get an IPrincipal object
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        IPrincipal GetPrincipal(IIdentity identity);
    }
}
