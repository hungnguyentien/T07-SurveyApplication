using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Hangfire.Application;
using Hangfire.Application.Enums;
using Hangfire.Application.Interfaces;
using HangfireBasicAuthenticationFilter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NLog;
using SurveyApplication.Domain;
using SurveyApplication.Persistence;
using SurveyApplication.Utility.LogUtils;

namespace Hangfire;

public class Startup
{
    [Obsolete("Obsolete")]
    public Startup(IConfiguration configuration)
    {
        //Config NLog
        var appBasePath = Directory.GetCurrentDirectory();
        GlobalDiagnosticsContext.Set("appbasepath", appBasePath);
        LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"))
            .GetCurrentClassLogger();
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddCors(o =>
        {
            o.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
        AddSwaggerDoc(services);

        #region Configure Hangfire

        services.AddHangfire(c =>
            c.UseSqlServerStorage(Configuration.GetConnectionString("SurveyManagerConnectionString")));
        GlobalConfiguration.Configuration
            .UseSqlServerStorage(Configuration.GetConnectionString("SurveyManagerConnectionString"))
            .WithJobExpirationTimeout(TimeSpan.FromDays(7));

        #endregion

        #region Services Injection

        //Inject logic services
        services.DependencyInjectionService(Configuration);

        #endregion

        services.AddSingleton<ILoggerManager, LoggerManager>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

        app.UseAuthentication();

        #region Configure Hangfire

        app.UseHangfireServer();
        app.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            DashboardTitle = "Hangfire Dashboard",
            Authorization = new[]
            {
                new HangfireCustomBasicAuthenticationFilter
                {
                    User = Configuration.GetSection("HangfireCredentials:UserName").Value,
                    Pass = Configuration.GetSection("HangfireCredentials:Password").Value
                }
            }
        });

        #endregion

        #region Configure Swagger

        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SurveyHangfire.Api v1"));

        #endregion

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseCors("CorsPolicy");

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapControllerRoute(
                "default",
                "{controller=Home}/{action=Index}/{id?}");
        });

        #region Job Scheduling Tasks

        using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
        var hangFireService = serviceScope.ServiceProvider.GetRequiredService<IClientServices>();
        var lstJob = GetListJobSetup(app);
        foreach (var item in lstJob)
            switch (item.JobTypeId)
            {
                case (int)EnumJobType.Type.Recurring:
                    RecurringJob.AddOrUpdate(item.JobName,
                        () => hangFireService.RecurringJobAsync(item.Service, item.ApiUrl), item.CronString);
                    break;
            }

        #endregion
    }

    private static void AddSwaggerDoc(IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });

            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Survey Hangfire Api",
                Description = "A SURVEY HANGFIRE API",
                Contact = new OpenApiContact
                {
                    Name = "toannck",
                    Email = "toannck32@wrun.vn"
                }
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }

    private static List<JobSchedule> GetListJobSetup(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<SurveyApplicationDbContext>();
        var lstSetup = context.JobSchedule.AsNoTracking().Where(x => x.Status != (int)EnumCommon.Status.InActive)
            .ToList();
        return lstSetup;
    }
}