namespace Warehouse.Domain.Shared;

public abstract record EntityId
{
    public Guid Id { get; init; }

    protected EntityId(Guid id)
    {
        Id = id;
    }

    protected EntityId(string id) : this(Guid.Parse(id))
    {

    }

    protected EntityId()
    {
        Id = Guid.NewGuid();
    }
}