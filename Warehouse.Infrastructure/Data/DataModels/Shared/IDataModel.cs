using Warehouse.Domain.Shared;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Infrastructure.Data.DataModels.Shared;

internal interface IDataModel<out TEntity> where TEntity : IEntity
{
    TEntity ToDomainModel();
}