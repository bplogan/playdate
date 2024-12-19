using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace PlayDate.Controllers
{
    public abstract class PlayDateControllerBase : AbpController
    {
        protected PlayDateControllerBase()
        {
            LocalizationSourceName = PlayDateConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
