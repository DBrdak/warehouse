using Warehouse.Application.Abstractions.Reports;
using Warehouse.Application.Reports.Drivers;
using Warehouse.Domain.Clients;
using Warehouse.Domain.Drivers;

namespace Warehouse.Application.Reports.Clients;

public sealed record ClientReportModel : IReportModel
{
    public string Name { get; init; }
    public string Nip { get; init; }
    public IReadOnlyCollection<ClientTransportReportModel> Transports { get; init; }

    private ClientReportModel(
        string name,
        string nip,
        IReadOnlyCollection<ClientTransportReportModel> transports)
    {
        Name = name;
        Nip = nip;
        Transports = transports;
    }

    public static ClientReportModel FromDomainModel(Client domainModel) =>
        new(
            domainModel.Name.Value,
            domainModel.Nip.Value,
            domainModel.Transports.Select(ClientTransportReportModel.FromDomainModel).ToList());
}