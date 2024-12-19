using System.Threading.Tasks;
using PlayDate.Configuration.Dto;

namespace PlayDate.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
