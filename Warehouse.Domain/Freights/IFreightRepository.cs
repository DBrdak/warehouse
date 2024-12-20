﻿using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Freights;

public interface IFreightRepository
{
    Task<Result<Freight>> AddAsync(Freight freight, CancellationToken cancellationToken);
    Result<Freight> Update(Freight freight);
    Result Remove(Freight freight);

    Task<Result<Freight>> GetByIdAsync(FreightId freightId, CancellationToken cancellationToken);

    Task<Result> AddRangeAsync(IEnumerable<Freight> freights, CancellationToken cancellationToken);

    Task<Result<List<Freight>>> GetManyByIdAsync(
        List<FreightId> freightsId,
        CancellationToken cancellationToken);

    Result UpdateRange(IEnumerable<Freight> freights);

    Task<Result<List<Freight>>> GetAllImportsDetailedAsync(CancellationToken cancellationToken);
}