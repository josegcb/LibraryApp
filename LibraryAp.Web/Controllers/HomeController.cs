using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using System.Linq;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Abp.Runtime.Validation;
using System.Collections.Generic;

namespace LibraryAp.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : LibraryApControllerBase
    {
        public ActionResult Index()
        {  
            return View("~/App/Main/views/layout/layout.cshtml"); //Layout of the angular application.
        }

        [System.Web.Mvc.HttpPost]       
        [Abp.Web.Security.AntiForgery.DisableAbpAntiForgeryTokenValidation]        
        public void OnExit() {
            IList<string> vList = new List<string>();
            vList.Add(Lib.DataFilters.MustHaveAgencyParam);
            ClearUserClaims(vList);
        }
    }
}