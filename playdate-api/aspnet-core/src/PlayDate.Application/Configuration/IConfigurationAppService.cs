using PlayDate.Configuration.Dto;
using System.Threading.Tasks;

namespace PlayDate.Configuration;

public interface IConfigurationAppService
{
    Task ChangeUiTheme(ChangeUiThemeInput input);
}
