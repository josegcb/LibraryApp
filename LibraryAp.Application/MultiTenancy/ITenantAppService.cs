using Abp.Application.Services;
using Abp.Application.Services.Dto;
using LibraryAp.MultiTenancy.Dto;

namespace LibraryAp.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
