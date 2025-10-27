using Business.Custom;
using Business.Interfaces.BusinessBasic;
using Business.Interfaces.IBusinessImplements;
using Business.Interfaces.IBusinessImplements.Auth;
using Business.Mensajeria;
using Business.Mensajeria.Implements;
using Business.Mensajeria.Interfaces;
using Business.Messaging.Implements;
using Business.Repository;
using Business.Services;
using Business.Services.Auth;
using Data.Interfaces.DataBasic;
using Data.Interfaces.IDataImplement;
using Data.Interfaces.IDataImplement.Auth;
using Data.Repositoy;
using Data.Services;
using Data.Services.Auth;
using Entity.Domain.Interfaces;
using Entity.Domain.Models.Implements;
using Entity.DTOs.Default;
using Entity.DTOs.Select;
using Entity.Infrastructure.LogService;
using Helpers.AutoMapper;
using ModelSecurity.Infrastructure.Cookies.Implements;
using Utilities.Custom;

namespace Web.Service
{
    public static class ApplicationService
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddScoped<IUserService, UserService>();
            
            services.AddScoped<IRolService, RolService>();
            services.AddScoped<IFormService, FormService>();
            services.AddScoped<IRolUserService, RolUserService>();
            services.AddScoped<IModuleService, ModuleService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IFormModuleService, FormModuleService>();
            services.AddScoped<IRolFormPermissionService, RolFormPermissionService>();

            //Jwt
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IToken, TokenBusiness>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IAuthCookieFactory, AuthCookieFactory>();

            // Servicio de Mensajeria
            services.AddTransient<IServiceEmail, ServiceEmail>();
            services.AddScoped<INotifyManager, NotifyManager>();
            services.AddScoped<IServiceTelegram, ServiceTelegram>();

            // Auditoría
            services.AddScoped<IAuditService, AuditService>();
            services.AddHttpContextAccessor(); // <-- Para IHttpContextAccessor

            // Data genérica y repositorios
            services.AddScoped(typeof(IData<>), typeof(DataGeneric<>));
            services.AddScoped<IRolUserRepository, RolUserRepository>();
            services.AddScoped<IFormModuleRepository, FormModuleRepository>();
            services.AddScoped<IRolFormPermissionRepository, RolFormPermissionRepository>();


            services.AddScoped<IUserRepository, UserRepository>();

            //services.AddHttpClient<IApiColombiaGatewayService, ApiColombiaGatewayService>();
            //services.AddScoped<ITouristicAttractionService, TouristicAttractionService>();


            return services;
        }
    }
}
