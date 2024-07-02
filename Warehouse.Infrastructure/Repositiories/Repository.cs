using Warehouse.Domain.Shared;
using Warehouse.Domain.Shared.Results;
using Warehouse.Infrastructure.Data;

namespace Warehouse.Infrastructure.Repositiories;

internal abstract class Repository<TEntity, TEntityId>
    where TEntity : Entity<TEntityId>
    where TEntityId : EntityId, new()
{
    private readonly ApplicationDbContext _dbContext;
    protected readonly DataModelService<TEntity> DataModelService;

    protected Repository(ApplicationDbContext dbContext, DataModelService<TEntity> dataModelService)
    {
        _dbContext = dbContext;
        DataModelService = dataModelService;
    }

    public async Task<Result<TEntity>> AddAsync(
        TEntity entity,
        CancellationToken cancellationToken)
    {
        var dataModelConverResult = DataModelService.ConvertToDataModel(entity);

        if (dataModelConverResult.IsFailure)
        {
            return dataModelConverResult.Error;
        }

        var dataModel = dataModelConverResult.Value;

        var addResult = await _dbContext.AddAsync(
            dataModel,
            cancellationToken);

        return addResult.Entity.ToDomainModel();
    }

    public async Task<Result> AddRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken)
    {
        entities = entities.ToList();
        var dataModelConverResults = entities.Select(DataModelService.ConvertToDataModel).ToList();

        if (Result.Aggregate(dataModelConverResults) is var result && result.IsFailure)
        {
            return result.Error;
        }

        var dataModels = dataModelConverResults.Select(x => x.Value);

        await _dbContext.AddRangeAsync(
            dataModels,
            cancellationToken);

        return Result.Success();
    }

    public Result<TEntity> Update(TEntity entity)
    {
        var dataModelConverResult = DataModelService.ConvertToDataModel(entity);

        if (dataModelConverResult.IsFailure)
        {
            return dataModelConverResult.Error;
        }

        var dataModel = dataModelConverResult.Value;

        var updateResult = _dbContext.Update(dataModel);

        return updateResult.Entity.ToDomainModel();
    }

    public Result UpdateRange(IEnumerable<TEntity> entities)
    {
        entities = entities.ToList();
        var dataModelConverResults = entities.Select(DataModelService.ConvertToDataModel).ToList();

        if (Result.Aggregate(dataModelConverResults) is var result && result.IsFailure)
        {
            return result.Error;
        }

        var dataModels = dataModelConverResults.Select(x => x.Value);

        _dbContext.UpdateRange(dataModels);

        return Result.Success();
    }

    public Result RemoveAsync(
        TEntityId entityId)
    {
        _dbContext.Remove(entityId.Id);

        return Result.Success();
    }

    public Result RemoveRange(IEnumerable<TEntityId> entityIds)
    {
        _dbContext.RemoveRange(entityIds.Select(id => id.Id));

        return Result.Success();
    }
}