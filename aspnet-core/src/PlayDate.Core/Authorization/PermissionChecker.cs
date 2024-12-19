using Abp.Authorization;
using PlayDate.Authorization.Roles;
using PlayDate.Authorization.Users;

namespace PlayDate.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
