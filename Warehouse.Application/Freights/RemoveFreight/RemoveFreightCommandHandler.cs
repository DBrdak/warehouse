using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Domain.Freights;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Freights.RemoveFreight;

internal sealed class RemoveFreightCommandHandler : ICommandHandler<RemoveFreightCommand>
{
    private readonly IFreightRepository _freightRepository;

    public RemoveFreightCommandHandler(IFreightRepository freightRepository)
    {
        _freightRepository = freightRepository;
    }

    public async Task<Result> Handle(RemoveFreightCommand request, CancellationToken cancellationToken) =>
        await Task.Run(() => _freightRepository.Remove(new(request.Id)), cancellationToken);
}
