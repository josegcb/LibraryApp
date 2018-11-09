using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using LibraryAp.Configuration.Dto;

namespace LibraryAp.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : LibraryApAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
