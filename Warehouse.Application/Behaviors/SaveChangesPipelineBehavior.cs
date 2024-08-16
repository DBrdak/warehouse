using MediatR;
using Warehouse.Application.Abstractions.Data;
using Warehouse.Domain.Shared.Messaging;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Behaviors;

internal sealed class SaveChangesPipelineBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseCommand
    where TResponse : Result
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly Error saveChangesFailure = new("Błąd zapisu danych");

    public SaveChangesPipelineBehavior(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var result = await next();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return result;
    }
}