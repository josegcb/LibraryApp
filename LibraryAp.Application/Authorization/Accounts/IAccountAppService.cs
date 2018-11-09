using System.Threading.Tasks;
using Abp.Application.Services;
using LibraryAp.Authorization.Accounts.Dto;

namespace LibraryAp.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
