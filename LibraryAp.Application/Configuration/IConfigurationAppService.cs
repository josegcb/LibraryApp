using System.Threading.Tasks;
using Abp.Application.Services;
using LibraryAp.Configuration.Dto;

namespace LibraryAp.Configuration
{
    public interface IConfigurationAppService: IApplicationService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}