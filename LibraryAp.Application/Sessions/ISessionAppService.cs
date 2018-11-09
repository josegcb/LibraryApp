using System.Threading.Tasks;
using Abp.Application.Services;
using LibraryAp.Sessions.Dto;

namespace LibraryAp.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
