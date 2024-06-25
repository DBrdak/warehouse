using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Transports;

public interface ITransportRepository
{
    Task<Result<Transport>> AddAsync(Transport transport, CancellationToken cancellationToken);
    Task<Result<Transport>> UpdateAsync(Transport transport, CancellationToken cancellationToken);
    Task<Result> RemoveAsync(TransportId transportId, CancellationToken cancellationToken);
}