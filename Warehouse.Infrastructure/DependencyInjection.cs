using Microsoft.Extensions.DependencyInjection;

namespace Warehouse.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        return services;
    }
}