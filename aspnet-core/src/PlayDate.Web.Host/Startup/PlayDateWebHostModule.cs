using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using PlayDate.Configuration;

namespace PlayDate.Web.Host.Startup
{
    [DependsOn(
       typeof(PlayDateWebCoreModule))]
    public class PlayDateWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public PlayDateWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PlayDateWebHostModule).GetAssembly());
        }
    }
}
