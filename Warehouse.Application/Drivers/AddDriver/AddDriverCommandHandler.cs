using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Application.Drivers.Models;
using Warehouse.Domain.Drivers;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Drivers.AddDriver;

internal sealed class AddDriverCommandHandler : ICommandHandler<AddDriverCommand, DriverModel>
{
    private readonly IDriverRepository _driverRepository;

    public AddDriverCommandHandler(IDriverRepository driverRepository)
    {
        _driverRepository = driverRepository;
    }

    public async Task<Result<DriverModel>> Handle(AddDriverCommand request, CancellationToken cancellationToken)
    {
        var driverCreateResult = Driver.Create(request.FirstName, request.LastName, request.VehiclePlate);

        if (driverCreateResult.IsFailure)
        {
            return driverCreateResult.Error;
        }

        var driver = driverCreateResult.Value;

        var addResult = await _driverRepository.AddAsync(driver, cancellationToken);

        if (addResult.IsFailure)
        {
            return addResult.Error;
        }

        driver = addResult.Value;

        return DriverModel.FromDomainModel(driver);
    }
}
