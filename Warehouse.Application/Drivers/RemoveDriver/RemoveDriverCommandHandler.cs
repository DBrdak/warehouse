using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Domain.Drivers;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Drivers.RemoveDriver;

internal sealed class RemoveDriverCommandHandler : ICommandHandler<RemoveDriverCommand>
{
    private readonly IDriverRepository _driverRepository;

    public RemoveDriverCommandHandler(IDriverRepository driverRepository)
    {
        _driverRepository = driverRepository;
    }

    public async Task<Result> Handle(RemoveDriverCommand request, CancellationToken cancellationToken) =>
        await Task.Run(() => _driverRepository.Remove(new(request.Id)), cancellationToken);
}
