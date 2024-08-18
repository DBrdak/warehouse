using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Application.Freights.Models;
using Warehouse.Domain.Freights;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Freights.GetAllImports;

internal sealed class GetAllImportedFreightsQueryHandler : IQueryHandler<GetAllImportedFreightsQuery, List<FreightModel>>
{
    private readonly IFreightRepository _freightRepository;

    public GetAllImportedFreightsQueryHandler(IFreightRepository freightRepository)
    {
        _freightRepository = freightRepository;
    }

    public async Task<Result<List<FreightModel>>> Handle(GetAllImportedFreightsQuery request, CancellationToken cancellationToken)
    {
        var importedFreightsGetResult = await _freightRepository.GetAllImportsDetailedAsync(cancellationToken);

        if (importedFreightsGetResult.IsFailure)
        {
            return importedFreightsGetResult.Error;
        }

        var importedFreights = importedFreightsGetResult.Value;

        return importedFreights.Select(FreightModel.FromDomainModel).ToList();
    }
}
