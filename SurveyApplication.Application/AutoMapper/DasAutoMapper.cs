using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace DASHangfire.Application.AutoMapper;

public static class DasAutoMapper
{
    public static void Configure(IServiceCollection services)
    {
        // .... Ignore code before this

        // Auto Mapper Configurations
        var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });

        var mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);
    }
}