using Abp.Authorization;
using LibraryAp.Authorization.Roles;
using LibraryAp.Authorization.Users;

namespace LibraryAp.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
