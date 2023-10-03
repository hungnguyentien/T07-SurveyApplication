using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.OpenApi.Models;
using SurveyApplication.API.Middleware;
using SurveyApplication.Application;
using SurveyApplication.Application.Services.Interfaces;
using SurveyApplication.Domain;
using SurveyApplication.Domain.Common.Configurations;
using SurveyApplication.Persistence;
using SurveyApplication.Utility.LogUtils;
using System.Reflection;

namespace SurveyApplication.API;

public class Startup
{
    private IConfiguration Configuration { get; }

    [Obsolete("Obsolete")]
    public Startup(IConfiguration configuration)
    {
        //Config NLog
        var appBasePath = Directory.GetCurrentDirectory();
        NLog.GlobalDiagnosticsContext.Set("appbasepath", appBasePath);
        NLog.LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config")).GetCurrentClassLogger();
        Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        AddSwaggerDoc(services);
        services.ConfigureApplicationServices(Configuration);
        services.AddControllers();
        services.AddCors(o =>
        {
            o.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
        services.AddSingleton<ILoggerManager, LoggerManager>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
    {
        if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

        app.UseMiddleware<ExceptionMiddleware>();

        app.UseAuthentication();

        app.UseSwagger(c =>
        {
            c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
            {
                swaggerDoc.ExternalDocs = new OpenApiExternalDocs
                {
                    Description = "Survey Management Api",
                    Url = new Uri($"{httpReq.Scheme}://{httpReq.Host.Value}")
                };
                //swaggerDoc.Servers = new List<OpenApiServer>() { new() { Url = "/api2" } };
            });
        });

        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SurveyManagement.Api v1"));

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseCors("CorsPolicy");

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        // Auto Migration
        AutoMigration(Configuration, app);
        //TODO BUG
        UpdatePermissionTable(serviceProvider);
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
                Title = "Survey Management Api",
                Description = "A SURVEY MANAGEMENT API",
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

    private static void UpdatePermissionTable(IServiceProvider serviceProvider)
    {
        var dataDefaultService = (IDataDefaultService)serviceProvider.GetService(typeof(IDataDefaultService));
        if (dataDefaultService != null)
            dataDefaultService.DataAdmin().Wait();
    }

    private static void AutoMigration(IConfiguration configuration, IApplicationBuilder app)
    {
        try
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
            var settings = configuration.GetSection(nameof(SurveyConfiguration)).Get<SurveyConfiguration>();
            if (serviceScope == null) return;
            var context = serviceScope.ServiceProvider.GetRequiredService<SurveyApplicationDbContext>();
            var migrationId = string.Empty;
            List<string> migrationIds = new();
            var buildNumber = settings.BuildNumber;
            var enviroment = settings.Env;
            var customerCode = settings.CustomerCode;
            var dbAssebmly = Assembly.GetAssembly(context.GetType());
            if (dbAssebmly != null)
            {
                var types = dbAssebmly.GetTypes();
                if (types.Any())
                {
                    migrationIds = types.Where(x => x.BaseType == typeof(Migration))
                        .Select(item => item.GetCustomAttributes<MigrationAttribute>().First().Id)
                        .OrderBy(o => o).ToList();
                    migrationId = migrationIds.LastOrDefault();
                    if (settings.UseDebugMode)
                    {
                        var logManager = new LoggerManager();
                        logManager.LogInfo("Survey: MigrationId  List:");
                        for (var i = migrationIds.Count - 1; i >= 0; i--)
                        {
                            logManager.LogInfo($"Survey: MigrationId => {migrationIds[i]}");
                        }
                    }
                }
            }

            // 1. Migration
            if (settings.AutoMigration)   // automatic migrations: add migration + update database
            {
                context.Database.Migrate();
            }
            else // insert all of migrations
            {
                if (migrationIds.Any())
                {
                    var version = typeof(Migration).Assembly.GetName().Version ?? new Version();
                    var efVersion = $"{version.Major}.{version.Minor}.{version.Build}";
                    foreach (var mid in migrationIds)
                    {
                        string sql =
                            $@" IF NOT EXISTS ( SELECT 1 FROM __EFMigrationsHistory WHERE MigrationId = '{mid}' )
                                BEGIN
                                    INSERT INTO __EFMigrationsHistory(MigrationId,ProductVersion) VALUES ('{mid}','{efVersion}')
                                END";

                        context.Database.ExecuteSqlRaw(sql);
                    }
                }
            }

            // 2. Release History
            if (string.IsNullOrEmpty(buildNumber) || buildNumber == "#{Octopus.Release.Number}" ||
                string.IsNullOrEmpty(migrationId) || enviroment == "#{Octopus.Environment.Name}") return;
            {
                var existed = context.ReleaseHistory.FirstOrDefault(x => x.BuildNumber == buildNumber && x.MigrationId == migrationId);
                if (existed != null) return;
                context.ReleaseHistory.Add(new ReleaseHistory
                {
                    BuildNumber = buildNumber,
                    MigrationId = migrationId,
                    ReleaseDate = DateTime.Now,
                    CustomerCode = customerCode,
                    Env = enviroment
                });
                context.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            var logManager = new LoggerManager();
            logManager.LogError(ex, $"AutoMigration StackTrace: {ex.StackTrace}");
            logManager.LogError(ex, $"AutoMigration Message: {ex.Message}");
        }
    }
}