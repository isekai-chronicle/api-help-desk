using api_help_desk.Controllers.Task;

namespace api_help_desk.Services
{
    public static class TaskService
    {
        public static IServiceCollection AddSupportServices(this IServiceCollection services)
        {
            services.AddScoped<TaskInterface, TaskRepository>();

            return services;
        }

    }
}
