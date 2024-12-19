using Abp.Application.Services;
using PlayDate.Authorization.Accounts.Dto;
using System.Threading.Tasks;

namespace PlayDate.Authorization.Accounts;

public interface IAccountAppService : IApplicationService
{
    Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

    Task<RegisterOutput> Register(RegisterInput input);
}
