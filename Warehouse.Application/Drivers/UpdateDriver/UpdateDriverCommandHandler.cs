using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Application.Drivers.Models;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Drivers.UpdateDriver;

internal sealed class UpdateDriverCommandHandler : ICommandHandler<UpdateDriverCommand, DriverModel>
{
    public async Task<Result<DriverModel>> Handle(UpdateDriverCommand request, CancellationToken cancellationToken)
    {
        return null;
    }
}
