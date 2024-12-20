﻿using Warehouse.Domain.Sectors;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.PalletSpaces;

public interface IPalletSpaceRepository
{
    Task<Result<PalletSpace>> AddAsync(PalletSpace palletSpace, CancellationToken cancellationToken);
    Result Remove(PalletSpace palletSpaceId);
    Task<Result<PalletSpace>> GetByIdAsync(PalletSpaceId palletSpaceId, CancellationToken cancellationToken);

    Task<Result> AddRangeAsync(IEnumerable<PalletSpace> palletSpaces, CancellationToken cancellationToken);

    Task<Result<PalletSpace>> GetByDataAsync(
        PalletSpaceNumber palletSpaceNumber,
        Shelf shelfNumber,
        Rack rackNumber,
        SectorNumber sectorNumber);
}