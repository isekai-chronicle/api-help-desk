using api_help_desk.Controllers.Dictionary;
using api_help_desk.Controllers.Language;
using api_help_desk.Controllers.Security.Authentication;
using api_help_desk.Controllers.Task;
using api_help_desk.Interfaces;

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

            return services;
        }

    }
}
