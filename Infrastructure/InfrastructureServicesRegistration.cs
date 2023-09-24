using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SurveyApplication.Domain.Common.Configurations;
using SurveyApplication.Domain.Interfaces.Infrastructure;
using SurveyApplication.Infrastructure.Mail;

namespace SurveyApplication.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.AddTransient<IEmailSender, EmailSender>();

        return services;
    }
}