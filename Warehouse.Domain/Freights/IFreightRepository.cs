using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Freights;

public interface IFreightRepository
{
    Task<Result<Freight>> AddAsync(Freight freight, CancellationToken cancellationToken);
    Result<Freight> Update(Freight freight);
    Result Remove(FreightId freightId);

    Task<Result<Freight>> GetByIdAsync(FreightId freightId, CancellationToken cancellationToken);
}