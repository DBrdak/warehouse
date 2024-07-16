using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Application.Abstractions.Reports;
using Warehouse.Domain.Clients;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Reports.Clients;

internal sealed class GenerateClientReportCommandHandler : ICommandHandler<GenerateClientReportCommand>
{
    private readonly IReportFactory<ClientReportModel> _reportFactory;
    private readonly IClientRepository _clientRepository;

    public GenerateClientReportCommandHandler(IReportFactory<ClientReportModel> reportFactory, IClientRepository clientRepository)
    {
        _reportFactory = reportFactory;
        _clientRepository = clientRepository;
    }

    public async Task<Result> Handle(GenerateClientReportCommand request, CancellationToken cancellationToken)
    {
        var clientGetResult = await _clientRepository.GetByIdWithTransportsAsync(new ClientId(request.ClientId), cancellationToken);

        if (clientGetResult.IsFailure)
        {
            return clientGetResult.Error;
        }

        var client = ClientReportModel.FromDomainModel(clientGetResult.Value);

        return _reportFactory.GenerateReport(client);
    }
}
