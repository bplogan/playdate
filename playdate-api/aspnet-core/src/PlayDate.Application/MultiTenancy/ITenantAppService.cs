using Abp.Application.Services;
using PlayDate.MultiTenancy.Dto;

namespace PlayDate.MultiTenancy;

public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
{
}

