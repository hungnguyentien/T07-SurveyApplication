using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using SurveyApplication.Infrastructure;
using SurveyApplication.Persistence;
using SurveyApplication.Identity;
using SurveyApplication.Application.Features.Accounts.Handlers.Queries;
using SurveyApplication.Application.Features.Accounts.Requests.Queries;
using SurveyApplication.Domain.Common.Identity;

namespace SurveyApplication.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.ConfigureInfrastructureServices(configuration);
            services.ConfigurePersistenceServices(configuration);
            services.AddTransient<IRequestHandler<LoginRequest, AuthResponse>, LoginRequestHandler>();

            //services.ConfigureIdentityServices(configuration);
            return services;
        }
    }
}
