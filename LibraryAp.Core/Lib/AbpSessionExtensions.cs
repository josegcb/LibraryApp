using Abp.Runtime.Session;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;


namespace Lib {
    public static class AbpSessionExtensions {        
        public static int GetAgenciaId(this IAbpSession abpSession) {
            var vValue = Get(Lib.DataFilters.MustHaveAgencyParam);
            return Convert.ToInt32(vValue); ;
        }
        private static string  Get(string ClainType) {
            var claimsprincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
            if (claimsprincipal == null) {
                return null;
            }
            var claim = claimsprincipal.Claims.FirstOrDefault(c => c.Type == ClainType);
            if (claim == null || string.IsNullOrEmpty(claim.Value)) {
                return null;
            }
            return claim.Value;

        }
    }
    


}



