using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.PalletSpaces;

public interface IPalletSpaceRepository
{
    Task<Result<PalletSpace>> AddAsync(PalletSpace palletSpace, CancellationToken cancellationToken);
    Task<Result<PalletSpace>> UpdateAsync(PalletSpace palletSpace, CancellationToken cancellationToken);
    Task<Result> RemoveAsync(PalletSpaceId palletSpaceId, CancellationToken cancellationToken);
}