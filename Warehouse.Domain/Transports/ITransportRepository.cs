using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Transports;

public interface ITransportRepository
{
    Task<Result<Transport>> AddAsync(Transport transport, CancellationToken cancellationToken);
    Result Remove(Transport transport);
    Task<Result<Transport>> GetByIdAsync(TransportId transportId, CancellationToken cancellationToken);

    Task<Result<List<Transport>>> GetAllExportsAsync(CancellationToken cancellationToken);

    Task<Result<List<Transport>>> GetAllImportsAsync(CancellationToken cancellationToken);

    Task<Result<int>> GetLastTransportNumberAsync(CancellationToken cancellationToken);

    Task<Result<Transport>> GetDetailedByIdAsync(TransportId transportId, CancellationToken cancellationToken);
}