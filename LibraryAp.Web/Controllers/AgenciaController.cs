using Lib;
using LibraryAp.Dtos;
using LibraryAp.Managers.Interfaces;
using LibraryAp.Models;
using LibraryAp.Services;
using LibraryAp.Services.Interfaces;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace LibraryAp.Web.Controllers
{
    public class AgenciaController : LibraryApControllerBase {
        // GET: Gencia
        IAgenciaAppService _AgenciaAppService;

        public AgenciaController(IAgenciaAppService initAgenciaAppService) {
            _AgenciaAppService = initAgenciaAppService;
        }

        public ActionResult Index() {
            return View();
        }
        [HttpPost]
        public JsonResult Escoger(int AgenciaId) {
            try {
                var authenticationManager = HttpContext.GetOwinContext().Authentication;
                var identity = new System.Security.Claims.ClaimsIdentity(User.Identity);
                var Claim = identity.FindFirst(Lib.DataFilters.MustHaveAgencyParam);
                if (Claim != null) {
                    identity.RemoveClaim(identity.FindFirst(Lib.DataFilters.MustHaveAgencyParam));
                }
                identity.AddClaim(new System.Security.Claims.Claim(Lib.DataFilters.MustHaveAgencyParam, AgenciaId.ToString()));
                authenticationManager.AuthenticationResponseGrant =
                    new AuthenticationResponseGrant(
                        new ClaimsPrincipal(identity),
                        new AuthenticationProperties { IsPersistent = true }
                    );
                return Json(new { success = true });
            } catch (Exception ex) {
                return Json(new { success = false, errors = ex.Message });

            }
        }

        [HttpPost]
        public JsonResult ListAll() {
            IEnumerable<AgenciaOutput> vList = _AgenciaAppService.ListAll();
            return Json(vList);
        }

        [HttpPost]
        public JsonResult DescripcionesStatus() {
            IEnumerable<EnumJson> vList = Lib.EnumHelper<eStatus>.GetDescriptions();
            return Json(vList);
        }

        [HttpPost]
        public JsonResult DescripcionesComisionPor() {
            IEnumerable<EnumJson> vList = Lib.EnumHelper<eComisionPor>.GetDescriptions();
            return Json(vList);
        }
        protected override void OnException(ExceptionContext context) {
            base.OnException(context);
        }

        [HttpPost]
        public JsonResult ListProperties() {
            IList<object> vList = FilterHelper.ListProperties< AgenciaOutput>();
            return Json(vList);
        }

        [HttpPost]
        public JsonResult ListAllByFilters(string vData) {
            JavaScriptSerializer vSerializer = new JavaScriptSerializer();
            List<EntityFilter> vTmp = vSerializer.Deserialize<List<EntityFilter>>(vData);
            IEnumerable<AgenciaOutput> vResult = _AgenciaAppService.ListAllByFilter(vTmp);
            return Json(vResult);
        }

       
    }    
}