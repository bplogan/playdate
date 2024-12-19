using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using PlayDate.Configuration;
using PlayDate.EntityFrameworkCore;
using PlayDate.Migrator.DependencyInjection;
using Castle.MicroKernel.Registration;
using Microsoft.Extensions.Configuration;

namespace PlayDate.Migrator;

[DependsOn(typeof(PlayDateEntityFrameworkModule))]
public class PlayDateMigratorModule : AbpModule
{
    private readonly IConfigurationRoot _appConfiguration;

    public PlayDateMigratorModule(PlayDateEntityFrameworkModule abpProjectNameEntityFrameworkModule)
    {
        abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

        _appConfiguration = AppConfigurations.Get(
            typeof(PlayDateMigratorModule).GetAssembly().GetDirectoryPathOrNull()
        );
    }

    public override void PreInitialize()
    {
        Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
            PlayDateConsts.ConnectionStringName
        );

        Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        Configuration.ReplaceService(
            typeof(IEventBus),
            () => IocManager.IocContainer.Register(
                Component.For<IEventBus>().Instance(NullEventBus.Instance)
            )
        );
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(PlayDateMigratorModule).GetAssembly());
        ServiceCollectionRegistrar.Register(IocManager);
    }
}
