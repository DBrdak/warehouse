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

    public async Task<Result> Handle(RemoveFreightCommand request, CancellationToken cancellationToken)
    {
        var freightGetResult = await _freightRepository.GetByIdAsync(new FreightId(request.Id), cancellationToken);

        if (freightGetResult.IsFailure)
        {
            return freightGetResult.Error;
        }

        var freight = freightGetResult.Value;

        return _freightRepository.Remove(freight);
    }
}
