using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using LibraryAp.MultiTenancy;

namespace LibraryAp.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}