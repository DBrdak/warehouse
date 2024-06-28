using MediatR;
using Warehouse.Domain.Shared.Messaging;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Abstractions.Messaging;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand;

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>;