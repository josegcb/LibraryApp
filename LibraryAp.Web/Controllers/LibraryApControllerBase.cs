using Abp.IdentityFramework;
using Abp.UI;
using Abp.Web.Mvc.Controllers;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http.ModelBinding;
using System.Web.Http.Results;
using System.Web.Mvc;
using Lib;
using System.Web;
using System.Collections.Generic;

namespace LibraryAp.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class LibraryApControllerBase : AbpController
    {
        protected LibraryApControllerBase()
        {
            LocalizationSourceName = LibraryApConsts.LocalizationSourceName;
        }

        protected virtual void CheckModelState()
        {
            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException(L("FormIsNotValidMessage"));
            }
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public JsonResult JsonInputErrorsMessage(object valModel, System.Web.Mvc.ModelStateDictionary valModelStateErrors) {
            return Json(new { success = false, data = valModel, errors = valModelStateErrors.Values.Where(i => i.Errors.Count > 0) });
        }

        public JsonResult JsonMessage(bool valSuccess, string valMessage) {
            if (!string.IsNullOrEmpty(valMessage.Trim())) {
                return Json(new { success = valSuccess, message = valMessage });
            }
            return Json(new { success = valSuccess });
        }

        protected override void OnException(ExceptionContext valContext) {
            var currentException = valContext.Exception as ConcurrencyException;
            if (currentException != null) {
                Server.ClearError();
                ThrowConcurrencyException(currentException);
            } else {
                base.OnException(valContext);
            }
        }

        private void ThrowConcurrencyException(ConcurrencyException valCurrentException) {
            string vExStr = valCurrentException.Message;
            Response.StatusCode = 500;
            Response.SuppressContent = true;
            Response.StatusDescription = vExStr;
            HttpContext.ApplicationInstance.CompleteRequest();
        }

        public void ClearUserClaims(IList<string> valList ) {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            var identity = new System.Security.Claims.ClaimsIdentity(User.Identity);
            foreach (var item in valList) {
                var Claim = identity.FindFirst(item);
                if (Claim != null) {
                    identity.RemoveClaim(identity.FindFirst(item));
                }
            }
        }
    }
}