using Abp.Application.Services;
using PlayDate.Sessions.Dto;
using System.Threading.Tasks;

namespace PlayDate.Sessions;

public interface ISessionAppService : IApplicationService
{
    Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
}
