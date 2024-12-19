using System.Threading.Tasks;
using Abp.Application.Services;
using PlayDate.Sessions.Dto;

namespace PlayDate.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
