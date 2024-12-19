using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using PlayDate.EntityFrameworkCore;
using PlayDate.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace PlayDate.Web.Tests
{
    [DependsOn(
        typeof(PlayDateWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class PlayDateWebTestModule : AbpModule
    {
        public PlayDateWebTestModule(PlayDateEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PlayDateWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(PlayDateWebMvcModule).Assembly);
        }
    }
}