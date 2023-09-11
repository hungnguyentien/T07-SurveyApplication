using Hangfire.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Hangfire.Application.Services
{
    public static class DIServiceWrapper
    {
        public static void DependencyInjectionService(this IServiceCollection services)
        {
            services.AddScoped<IScheduleServices, ScheduleService>();
        }
    }
}
