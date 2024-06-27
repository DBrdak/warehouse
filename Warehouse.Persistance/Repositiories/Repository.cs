using Microsoft.EntityFrameworkCore;
using Warehouse.Domain.Shared;
using Warehouse.Domain.Shared.Results;
using Warehouse.Infrastructure;

namespace Warehouse.Persistance.Repositiories;

internal abstract class Repository<TEntity, TEntityId>
    where TEntity : Entity<TEntityId>
    where TEntityId : EntityId, new()
{
    private readonly ApplicationDbContext _dbContext;
    protected DbSet<TEntity> Table => _dbContext.Set<TEntity>();

    protected Repository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<IEnumerable<TEntity>>> GetAllAsync(
        CancellationToken cancellationToken) =>
        Table;

    public async Task<Result<TEntity>> GetById(
        TEntityId id,
        CancellationToken cancellationToken) =>
        await Table.FirstOrDefaultAsync(e => e.Id == id);

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
        await _dbContext.AddRangeAsync(
            entities,
            cancellationToken);

        return Result.Success();
    }

    public Result<TEntity> Update(TEntity entity)
    {
        var updateResult = Table.Update(entity);

        return updateResult.Entity;
    }

    public Result UpdateRange(TEntity entity)
    {
        Table.UpdateRange(entity);

        return Result.Success();
    }

    public async Task<Result> RemoveAsync(
        TEntityId entityId,
        CancellationToken cancellationToken)
    {
        var query = Table.Where(e => e.Id == entityId);

        await query.ExecuteDeleteAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> RemoveRange(
        IEnumerable<TEntityId> entityIds,
        CancellationToken cancellationToken)
    {
        var query = Table.Where(e => entityIds.Any(id => id == e.Id));

        await query.ExecuteDeleteAsync(cancellationToken);

        return Result.Success();
    }
}