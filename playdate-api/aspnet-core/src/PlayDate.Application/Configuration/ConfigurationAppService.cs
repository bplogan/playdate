﻿using Abp.Authorization;
using Abp.Runtime.Session;
using PlayDate.Configuration.Dto;
using System.Threading.Tasks;

namespace PlayDate.Configuration;

[AbpAuthorize]
public class ConfigurationAppService : PlayDateAppServiceBase, IConfigurationAppService
{
    public async Task ChangeUiTheme(ChangeUiThemeInput input)
    {
        await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
    }
}
