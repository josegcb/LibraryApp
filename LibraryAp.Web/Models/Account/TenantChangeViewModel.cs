using Abp.AutoMapper;
using LibraryAp.Sessions.Dto;

namespace LibraryAp.Web.Models.Account
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}