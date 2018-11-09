using System.Reflection;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Modules;
using LibraryAp.Authorization.Roles;
using LibraryAp.Authorization.Users;
using LibraryAp.Models;
using LibraryAp.Roles.Dto;
using LibraryAp.Users.Dto;
using LibraryAp.Dtos;

namespace LibraryAp
{
    [DependsOn(typeof(LibraryApCoreModule), typeof(AbpAutoMapperModule))]
    public class LibraryApApplicationModule : AbpModule
    {
        public override void PreInitialize() {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(mapper => {

                mapper.CreateMap<AgenciaCreateInput, Agencia>().ReverseMap();
                mapper.CreateMap<Agencia, AgenciaOutput>().ReverseMap();
                mapper.CreateMap <AgenciaUpdateInput, Agencia>().ReverseMap();
                mapper.CreateMap<AgenciaDeleteInput, Agencia>().ReverseMap();

              

                mapper.CreateMap<CreateCajaInput, Caja>().ReverseMap();
                mapper.CreateMap<Caja, CajaOutput>().ReverseMap();
                mapper.CreateMap<UpdateCajaInput, Caja>().ReverseMap();
                mapper.CreateMap<DeleteCajaInput, Caja>().ReverseMap();

               


            });

        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            // TODO: Is there somewhere else to store these, with the dto classes
            Configuration.Modules.AbpAutoMapper().Configurators.Add(cfg =>
            {
                // Role and permission
                cfg.CreateMap<Permission, string>().ConvertUsing(r => r.Name);
                cfg.CreateMap<RolePermissionSetting, string>().ConvertUsing(r => r.Name);

                cfg.CreateMap<CreateRoleDto, Role>().ForMember(x => x.Permissions, opt => opt.Ignore());
                cfg.CreateMap<RoleDto, Role>().ForMember(x => x.Permissions, opt => opt.Ignore());
                
                cfg.CreateMap<UserDto, User>();
                cfg.CreateMap<UserDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());

                cfg.CreateMap<CreateUserDto, User>();
                cfg.CreateMap<CreateUserDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());
                
                
                




            });
        }
    }
}
