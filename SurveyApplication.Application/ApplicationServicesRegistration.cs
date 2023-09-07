using System.Reflection;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SurveyApplication.Identity;
using SurveyApplication.Infrastructure;
using SurveyApplication.Persistence;

namespace SurveyApplication.Application;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.ConfigureIdentityServices(configuration);
        services.ConfigureInfrastructureServices(configuration);
        services.ConfigurePersistenceServices(configuration);
        return services;
    }
}