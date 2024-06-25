using Warehouse.Domain.Shared;

namespace Warehouse.Domain.PalletSpaces;

public sealed record PalletSpaceId : EntityId
{
    public PalletSpaceId(Guid id) : base(id)
    {
    }

    public PalletSpaceId(string id) : base(id)
    {
    }

    public PalletSpaceId()
    {
    }
}