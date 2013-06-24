using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SSW.Framework.Web.Mvc.Security
{
    public interface IAuthorizationProvider
    {
        IPrincipal GetPrincipal(IIdentity identity);
    }
}
