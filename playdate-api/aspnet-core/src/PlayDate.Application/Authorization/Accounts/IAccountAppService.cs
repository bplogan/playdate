using System.Threading.Tasks;
using Abp.Application.Services;
using PlayDate.Authorization.Accounts.Dto;

namespace PlayDate.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
