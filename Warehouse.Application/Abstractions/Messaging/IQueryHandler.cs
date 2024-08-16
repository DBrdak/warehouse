using MediatR;
using Warehouse.Domain.Shared.Messaging;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;