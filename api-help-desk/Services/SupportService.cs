

using api_help_desk.Controllers.Dictionary;
using api_help_desk.Controllers.Language;
using api_help_desk.Controllers.List;
using api_help_desk.Controllers.Project;
using api_help_desk.Controllers.Security.Authentication;
using api_help_desk.Controllers.Task;
using api_help_desk.Controllers.Area;
using api_help_desk.Controllers.Role;
using api_help_desk.Controllers.Domain;
using api_help_desk.Controllers.User;
using api_help_desk.Controllers.Paths;
using api_help_desk.Controllers.Component;


namespace api_help_desk.Services
{
    public static class SupportService
    {
        public static IServiceCollection AddSupportServices(this IServiceCollection services)
        {
            services.AddScoped<TaskInterface, TaskService>();
            services.AddScoped<ProjectInterface, ProjectService>();
            services.AddScoped<ListInterface, ListService>();
            services.AddScoped<LanguageInterface, LanguageService>();
            services.AddScoped<DictionaryInterface, DictionaryService>();
            services.AddScoped<AuthenticationInterface, AuthenticationService>();
            services.AddScoped<AreaInterface, AreaService>();
            services.AddScoped<RoleInterface, RoleService>();
            services.AddScoped<DomainInterface, DomainService>();
            services.AddScoped<UserInterface, UserService>();
            services.AddScoped<PathsInterface, PathsService>();
            services.AddScoped<ComponentInterface, ComponentService>();
            return services;
        }

    }
}
