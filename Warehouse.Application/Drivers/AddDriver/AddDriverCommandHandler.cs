using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Application.Drivers.Models;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Drivers.AddDriver;

internal sealed class AddDriverCommandHandler : ICommandHandler<AddDriverCommand, DriverModel>
{
    public async Task<Result<DriverModel>> Handle(AddDriverCommand request, CancellationToken cancellationToken)
    {
        return null;
    }
}
