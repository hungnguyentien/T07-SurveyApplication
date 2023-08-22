using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Persistence
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SurveyApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SurveyManagerConnectionString")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<ILoaiHinhDonViRepository, LoaiHinhDonViRepository>();
            //services.AddScoped<IDonViRepository, DonViRepository>();

            return services;
        }
    }
}
