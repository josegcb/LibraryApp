using System.Linq;
using System.Reflection;
using System.Web.Http;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;
using Swashbuckle.Application;

namespace LibraryAp.Api
{
    [DependsOn(typeof(AbpWebApiModule), typeof(LibraryApApplicationModule))]
    public class LibraryApWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(LibraryApApplicationModule).Assembly, "app")
                .Build();

            Configuration.Modules.AbpWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));
            ConfigureSwaggerUi();
        }

        private void ConfigureSwaggerUi() {
            Configuration.Modules.AbpWebApi().HttpConfiguration
                .EnableSwagger(c => {
                    c.SingleApiVersion("v1", "LibraryAp.WebApi");
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                })
                .EnableSwaggerUi(c => {
                    c.InjectJavaScript(Assembly.GetAssembly(typeof(LibraryApWebApiModule)), "LibraryAp.WebApi.Api.Scripts.Swagger-Custom.js");
                });
        }

    }
}
