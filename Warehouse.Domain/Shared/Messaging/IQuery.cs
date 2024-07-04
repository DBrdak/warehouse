using MediatR;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Shared.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>, IBaseQuery;

public interface IBaseQuery;