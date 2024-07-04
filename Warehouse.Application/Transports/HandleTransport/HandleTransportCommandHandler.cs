using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Application.Transports.Models;
using Warehouse.Domain.Clients;
using Warehouse.Domain.Drivers;
using Warehouse.Domain.Shared.Results;
using Warehouse.Domain.Transports;
using Warehouse.Domain.Warehousemen;

namespace Warehouse.Application.Transports.HandleTransport;

internal sealed class HandleTransportCommandHandler : ICommandHandler<HandleTransportCommand, TransportModel>
{
    private readonly ITransportRepository _transportRepository;
    private readonly IWarehousemanRepository _warehousemanRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IDriverRepository _driverRepository;

    public HandleTransportCommandHandler(ITransportRepository transportRepository, IWarehousemanRepository warehousemanRepository, IClientRepository clientRepository, IDriverRepository driverRepository)
    {
        _transportRepository = transportRepository;
        _warehousemanRepository = warehousemanRepository;
        _clientRepository = clientRepository;
        _driverRepository = driverRepository;
    }

    public async Task<Result<TransportModel>> Handle(HandleTransportCommand request, CancellationToken cancellationToken)
    {
        var (warehousemanGetResult, driverGetResult, clientGetResult) = (
            await _warehousemanRepository.GetByIdAsync(
                new(request.WarehousemanId),
                cancellationToken), await _driverRepository.GetByIdAsync(
                new(request.DriverId),
                cancellationToken), await _clientRepository.GetByIdAsync(
                new(request.ClientId),
                cancellationToken));

        if (Result.Aggregate(warehousemanGetResult, driverGetResult, clientGetResult) is var result &&
            result.IsFailure)
        {
            return result.Error;
        }

        var (warehouseman, driver, client) = 
            (warehousemanGetResult.Value, driverGetResult.Value, clientGetResult.Value);

        var receiveResult = TransportService.HandleTransport(
            request.Number,
            request.Type,
            warehouseman,
            driver,
            client,
            request.DateTime?.ToDateTime());

        if (receiveResult.IsFailure)
        {
            return receiveResult.Error;
        }

        var transport = receiveResult.Value;

        var addResult = await _transportRepository.AddAsync(transport, cancellationToken);

        if (addResult.IsFailure)
        {
            return addResult.Error;
        }

        transport = addResult.Value;

        return TransportModel.FromDomainModel(transport);
    }
}
