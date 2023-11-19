using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common;
using SurveyApplication.Domain.Common.Configurations;
using SurveyApplication.Domain.Interfaces;
using SurveyApplication.Domain.Interfaces.Persistence;
using SurveyApplication.Domain.Models;
using SurveyApplication.Persistence.Repositories;

namespace SurveyApplication.Persistence;

public static class PersistenceServicesRegistration
{
    public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<StorageConfigs>(configuration.GetSection("StorageConfigs"));

        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 6;
        });

        services.AddScoped<IPasswordHasher<ApplicationUser>, PasswordHasher<ApplicationUser>>();
        // connect to msssql with connection string from app settings
        services.AddDbContext<SurveyApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SurveyManagerConnectionString"),
                b => b.MigrationsAssembly(typeof(SurveyApplicationDbContext).Assembly.FullName)));

        //// connect to postgres with connection string from app settings
        //services.AddDbContext<SurveyApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("SurveyManagerConnectionString"), b => b.MigrationsAssembly(typeof(SurveyApplicationDbContext).Assembly.FullName)));

        services.AddIdentity<ApplicationUser, Role>()
            .AddEntityFrameworkStores<SurveyApplicationDbContext>().AddDefaultTokenProviders()
            .AddPasswordValidator<PasswordValidator<ApplicationUser>>();

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
                };
            });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<ISurveyRepositoryWrapper, SurveyRepositoryWrapper>();
        return services;
    }
}