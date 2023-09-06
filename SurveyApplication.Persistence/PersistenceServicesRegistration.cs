using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SurveyApplication.Persistence.Repositories;
using System.Text;
using SurveyApplication.Domain.Common;
using SurveyApplication.Domain.Interfaces;
using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Persistence
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SurveyApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SurveyManagerConnectionString")));

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddDbContext<SurveyApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SurveyManagerConnectionString"),
                b => b.MigrationsAssembly(typeof(SurveyApplicationDbContext).Assembly.FullName)));

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
            //TODO Lĩnh vục hoạt động
            services.AddScoped<ILinhVucHoatDongRepository, LinhVucHoatDongRepository>();

            return services;
        }
    }
}
