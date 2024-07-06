using Microsoft.Extensions.DependencyInjection;
using Warehouse.Application.Behaviors;

namespace Warehouse.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services) =>
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

            configuration.AddOpenBehavior(typeof(SaveChangesPipelineBehavior<,>));
        });
}