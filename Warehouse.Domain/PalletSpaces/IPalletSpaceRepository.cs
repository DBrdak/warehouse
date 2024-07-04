using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.PalletSpaces;

public interface IPalletSpaceRepository
{
    Task<Result<PalletSpace>> AddAsync(PalletSpace palletSpace, CancellationToken cancellationToken);
    Result<PalletSpace> Update(PalletSpace palletSpace);
    Result Remove(PalletSpaceId palletSpaceId);

    Task<Result<PalletSpace>> GetByIdAsync(PalletSpaceId palletSpaceId, CancellationToken cancellationToken);
}