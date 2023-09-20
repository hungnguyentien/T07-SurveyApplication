using System.Reflection;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SurveyApplication.Application.Features.Accounts.Handlers.Queries;
using SurveyApplication.Application.Features.Accounts.Requests.Queries;
using SurveyApplication.Application.Services;
using SurveyApplication.Application.Services.Interfaces;
using SurveyApplication.Domain.Common.Identity;
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
        services.ConfigureInfrastructureServices(configuration);
        services.ConfigurePersistenceServices(configuration);
        services.AddTransient<IRequestHandler<LoginRequest, AuthResponse>, LoginRequestHandler>();
        services.AddScoped<IDataDefaultService, DataDefaultService>();
        return services;
    }
}