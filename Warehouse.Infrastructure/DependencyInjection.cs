using Microsoft.Extensions.DependencyInjection;
using Warehouse.Application.Abstractions.Data;
using Warehouse.Domain.Clients;
using Warehouse.Domain.Drivers;
using Warehouse.Domain.Freights;
using Warehouse.Domain.PalletSpaces;
using Warehouse.Domain.Sectors;
using Warehouse.Domain.Transports;
using Warehouse.Domain.Warehousemen;
using Warehouse.Infrastructure.Data;
using Warehouse.Infrastructure.Data.Options;
using Warehouse.Infrastructure.Repositiories;

namespace Warehouse.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services) =>
        services
            .AddDataServices()
            .AddPersistence();

    private static IServiceCollection AddPersistence(this IServiceCollection services) =>
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>())
            .AddScoped<IClientRepository, ClientRepository>()
            .AddScoped<IDriverRepository, DriverRepository>()
            .AddScoped<IFreightRepository, FreightRepository>()
            .AddScoped<IPalletSpaceRepository, PalletSpaceRepository>()
            .AddScoped<ISectorRepository, SectorRepository>()
            .AddScoped<ITransportRepository, TransportRepository>()
            .AddScoped<IWarehousemanRepository, WarehousemanRepository>();

    private static IServiceCollection AddDataServices(this IServiceCollection services) =>
        services.ConfigureOptions<ApplicationDbContextOptionsSetup>()
            .AddDbContext<ApplicationDbContext>();
}