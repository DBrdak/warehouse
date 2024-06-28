using Warehouse.Domain.Clients;
using Warehouse.Domain.Shared;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Infrastructure.Data.DataModels.Shared;

internal class DataModelService<TEntity> where TEntity : IEntity
{
    private readonly InvalidCastException _convertDomainModelToDataModelException =
        new($"Cannot cast {typeof(TEntity).Name} to data model");

    public IDataModel<TEntity> ConvertToDataModel(TEntity entity) =>
        entity switch
        {
            Client client => ClientDataModel.FromDomainModel(client) as IDataModel<TEntity>,
            _ => throw _convertDomainModelToDataModelException
        } ??
        throw _convertDomainModelToDataModelException;
}