using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Application.Drivers.Models;
using Warehouse.Domain.Drivers;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Drivers.UpdateDriver;

internal sealed class UpdateDriverCommandHandler : ICommandHandler<UpdateDriverCommand, DriverModel>
{
    private readonly IDriverRepository _driverRepository;

    public UpdateDriverCommandHandler(IDriverRepository driverRepository)
    {
        _driverRepository = driverRepository;
    }

    public async Task<Result<DriverModel>> Handle(UpdateDriverCommand request, CancellationToken cancellationToken)
    {
        var driverGetResult =
            await _driverRepository.GetByIdAsync(new DriverId(request.Id), cancellationToken);

        if (driverGetResult.IsFailure)
        {
            return driverGetResult.Error;
        }

        var driver = driverGetResult.Value;

        Result[] editReults =
        [
            driver.EditFirstName(request.NewFirstName),
            driver.EditLastName(request.NewLastName),
            driver.EditVehiclePlate(request.NewVehiclePlate)
        ];

        if (Result.Aggregate(editReults) is var result && result.IsFailure)
        {
            return result.Error;
        }

        var updateResult = _driverRepository.Update(driver);

        if (updateResult.IsFailure)
        {
            return updateResult.Error;
        }

        return DriverModel.FromDomainModel(driver);
    }
}
