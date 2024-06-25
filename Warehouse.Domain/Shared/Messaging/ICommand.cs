using MediatR;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Domain.Shared.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand;

public interface ICommand<TReponse> : IRequest<Result<TReponse>>, IBaseCommand;

public interface IBaseCommand;