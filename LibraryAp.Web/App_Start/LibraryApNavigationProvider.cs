using Abp.Application.Navigation;
using Abp.Localization;
using LibraryAp.Authorization;
using System.Linq;

namespace LibraryAp.Web
{
    /// <summary>
    /// This class defines menus for the application.
    /// It uses ABP's menu system.
    /// When you add menu items here, they are automatically appear in angular application.
    /// See .cshtml and .js files under App/Main/views/layout/header to know how to render menu.
    /// </summary>
    public class LibraryApNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        "Home",
                        new LocalizableString("HomePage", LibraryApConsts.LocalizationSourceName),
                        url: "#/",
                        icon: "fa fa-home",
                        requiresAuthentication: true
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Tenants",
                        L("Tenants"),
                        url: "#tenants",
                        icon: "fa fa-globe",
                        requiredPermissionName: PermissionNames.Pages_Tenants
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Users",
                        L("Users"),
                        url: "#users",
                        icon: "fa fa-users",
                        requiredPermissionName: PermissionNames.Pages_Users
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Roles",
                        L("Roles"),
                        url: "#users",
                        icon: "fa fa-tag",
                        requiredPermissionName: PermissionNames.Pages_Roles
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        "About",
                        new LocalizableString("About", LibraryApConsts.LocalizationSourceName),
                        url: "#/about",
                        icon: "fa fa-info"
                        )
                )
                .AddItem(
                    new MenuItemDefinition(
                        "Agencias",
                        new LocalizableString("Agencias", LibraryApConsts.LocalizationSourceName),
                        url: "#/agencias",
                        icon: "fa fa-info"
                        )
                )
                .AddItem(
                    new MenuItemDefinition(
                        "Cajas",
                        new LocalizableString("Cajas", LibraryApConsts.LocalizationSourceName),
                        url: "#/cajas",
                        icon: "fa fa-info"
                        )
                )
                
                ;
            //josegcb var claimsprincipal = System.Threading.Thread.CurrentPrincipal as System.Security.Claims.ClaimsPrincipal;
            //if (!claimsprincipal.Identity.IsAuthenticated) return;

            //var identity = claimsprincipal.Identity as System.Security.Claims.ClaimsIdentity;


            //var currentClaim = identity.Claims.FirstOrDefault(c => c.Type == "AgenciaId");

            //if (currentClaim != null)
            //    identity.RemoveClaim(currentClaim);

            //identity.AddClaim(new System.Security.Claims.Claim("AgenciaId", "1"));
            //claimsprincipal.AddIdentity(identity);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, LibraryApConsts.LocalizationSourceName);
        }
    }
}
