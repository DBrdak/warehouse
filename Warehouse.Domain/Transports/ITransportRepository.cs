using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Transports;

public interface ITransportRepository
{
    Task<Result<Transport>> AddAsync(Transport transport, CancellationToken cancellationToken);
    Result<Transport> Update(Transport transport);
    Result Remove(TransportId transportId);

    Task<Result<Transport>> GetByIdAsync(TransportId transportId, CancellationToken cancellationToken);
}