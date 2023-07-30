using TripDatePlanner.Entities;
using TripDatePlanner.Mappers;
using TripDatePlanner.Mappers.Interfaces;
using TripDatePlanner.Models.Dto;
using TripDatePlanner.Services;
using TripDatePlanner.Services.Interfaces;
using Range = TripDatePlanner.Entities.Range;

namespace TripDatePlanner.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITripService, TripService>();
        services.AddScoped<IParticipantService, ParticipantService>();
        services.AddScoped<IRangeService, RangeService>();
        services.AddScoped<IUidGenerator<Trip>, UidGenerator<Trip>>();

        return services;
    }

    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
        Type multiMapperType = typeof(MultiMapper);
        foreach (Type interfaceType in multiMapperType.GetInterfaces())
        {
            services.AddSingleton(interfaceType, multiMapperType);
        }

        return services;
    }
}