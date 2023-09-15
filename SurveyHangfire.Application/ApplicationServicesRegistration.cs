using Hangfire.Application.Interfaces;
using Hangfire.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SurveyApplication.Persistence;
using SurveyApplication.Utility.HttpClientAccessorsUtils.Implementations;
using SurveyApplication.Utility.HttpClientAccessorsUtils.Interfaces;

namespace Hangfire.Application
{
    public static class ApplicationServicesRegistration
    {
        public static void DependencyInjectionService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IScheduleServices, ScheduleService>();
            //Base HttpClient 
            services.AddHttpClient<IBaseHttpClient, BaseHttpClient>();   //Transient, don't Inject to Scope or Singleton
            services.AddSingleton<IBaseHttpClientFactory, BaseHttpClientFactory>();
            // Config HttpContextAccessor
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IClientServices, ClientService>();
            services.ConfigurePersistenceServices(configuration);
        }
    }
}
