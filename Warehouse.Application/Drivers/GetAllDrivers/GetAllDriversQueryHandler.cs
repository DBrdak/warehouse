using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Application.Drivers.Models;
using Warehouse.Domain.Drivers;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Drivers.GetAllDrivers;

internal sealed class GetAllDriversQueryHandler : IQueryHandler<GetAllDriversQuery, List<DriverModel>>
{
    private readonly IDriverRepository _driverRepository;

    public GetAllDriversQueryHandler(IDriverRepository driverRepository)
    {
        _driverRepository = driverRepository;
    }

    public async Task<Result<List<DriverModel>>> Handle(GetAllDriversQuery request, CancellationToken cancellationToken)
    {
        var getDriversResult = await _driverRepository.GetAllAsync(cancellationToken);

        if (getDriversResult.IsFailure)
        {
            return getDriversResult.Error;
        }

        var drivers = getDriversResult.Value;

        return drivers.Select(DriverModel.FromDomainModel).ToList();
    }
}
