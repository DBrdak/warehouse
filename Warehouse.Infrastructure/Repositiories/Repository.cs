using Microsoft.EntityFrameworkCore;
using Warehouse.Domain.Shared;
using Warehouse.Domain.Shared.Results;
using Warehouse.Infrastructure.Data;
using Warehouse.Infrastructure.Utils;

namespace Warehouse.Infrastructure.Repositiories;

internal abstract class Repository<TEntity, TEntityId>
    where TEntity : Entity<TEntityId>
    where TEntityId : EntityId, new()
{
    private readonly ApplicationDbContext _dbContext;
    protected readonly DbSet<TEntity> Table;

    protected Repository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        Table = dbContext.Set<TEntity>();
    }

    public async Task<Result<TEntity>> GetByIdAsync(TEntityId entityId, CancellationToken cancellationToken) =>
        Result.Create(
            await Table.FirstOrDefaultAsync(e => e.Id == entityId, cancellationToken),
            DataAccessErrors.NotFound<TEntity>());

    public async Task<Result<TEntity>> AddAsync(
        TEntity entity,
        CancellationToken cancellationToken)
    {
        var addResult = await Table.AddAsync(
            entity,
            cancellationToken);

        return addResult.Entity;
    }

    public async Task<Result> AddRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken)
    {
        await Table.AddRangeAsync(
            entities,
            cancellationToken);

        return Result.Success();
    }

    public Result<TEntity> Update(TEntity entity)
    {
        var updateResult = Table.Update(entity);

        return updateResult.Entity;
    }

    public Result UpdateRange(IEnumerable<TEntity> entities)
    {
        Table.UpdateRange(entities);

        return Result.Success();
    }

    public Result Remove(
        TEntityId entityId)
    {
        _dbContext.Remove(entityId);

        return Result.Success();
    }

    public Result RemoveRange(IEnumerable<TEntityId> entityIds)
    {
        _dbContext.RemoveRange(entityIds.Select(id => id.Id));

        return Result.Success();
    }
}