using Microsoft.Extensions.DependencyInjection;

namespace Warehouse.Persistance;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        return services;
    }
}