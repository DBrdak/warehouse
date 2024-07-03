using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Application.Clients.UpdateClient;
using Warehouse.Application.Drivers.Models;
using Warehouse.Domain.Clients;
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


        var editResult = request switch
        {
            _ when request.NewFirstName is not null => driver.EditFirstName(request.NewFirstName),
            _ when request.NewLastName is not null => driver.EditLastName(request.NewLastName),
            _ when request.NewVehiclePlate is not null => driver.EditVehiclePlate(request.NewVehiclePlate),
            _ => UpdateDriverErrors.InvalidRequest
        };

        if (editResult.IsFailure)
        {
            return editResult.Error;
        }

        var updateResult = _driverRepository.Update(driver);

        if (updateResult.IsFailure)
        {
            return updateResult.Error;
        }

        return DriverModel.FromDomainModel(driver);
    }
}
