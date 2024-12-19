using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using PlayDate.Authorization;

namespace PlayDate
{
    [DependsOn(
        typeof(PlayDateCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class PlayDateApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<PlayDateAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(PlayDateApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
