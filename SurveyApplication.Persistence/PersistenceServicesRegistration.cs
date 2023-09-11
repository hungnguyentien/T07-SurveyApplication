using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SurveyApplication.Domain.Common;
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
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        services.AddDbContext<SurveyApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SurveyManagerConnectionString"),
                b => b.MigrationsAssembly(typeof(SurveyApplicationDbContext).Assembly.FullName)));

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<SurveyApplicationDbContext>().AddDefaultTokenProviders();

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

        services.AddScoped<ILoaiHinhDonViRepository, LoaiHinhDonViRepository>();
        services.AddScoped<IBangKhaoSatRepository, BangKhaoSatRepository>();
        services.AddScoped<IDotKhaoSatRepository, DotKhaoSatRepository>();
        services.AddScoped<IDonViRepository, DonViRepository>();
        services.AddScoped<INguoiDaiDienRepository, NguoiDaiDienRepository>();
        services.AddScoped<IGuiEmailRepository, GuiEmailRepository>();
        services.AddScoped<ICauHoiRepository, CauHoiRepository>();
        services.AddScoped<IKetQuaRepository, KetQuaRepository>();
        services.AddScoped<ICotRepository, CotRepository>();
        services.AddScoped<IHangRepository, HangRepository>();
        services.AddScoped<IBangKhaoSatCauHoiRepository, BangKhaoSatCauHoiRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();

        //TODO Lĩnh vục hoạt động
        services.AddScoped<ILinhVucHoatDongRepository, LinhVucHoatDongRepository>();

        return services;
    }
}