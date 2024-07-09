using Warehouse.Application.Abstractions.Messaging;
using Warehouse.Application.Abstractions.Reports;
using Warehouse.Domain.Drivers;
using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Reports.Drivers;

internal sealed class GenerateDriverReportCommandHandler : ICommandHandler<GenerateDriverReportCommand>
{
    private readonly IReportFactory<DriverReportModel> _reportFactory;
    private readonly IDriverRepository _driverRepository;

    public GenerateDriverReportCommandHandler(IReportFactory<DriverReportModel> reportFactory, IDriverRepository driverRepository)
    {
        _reportFactory = reportFactory;
        _driverRepository = driverRepository;
    }

    public async Task<Result> Handle(GenerateDriverReportCommand request, CancellationToken cancellationToken)
    {
        var driverGetResult = await _driverRepository.GetByIdWithTransportsAsync(new(request.DriverId), cancellationToken);

        if (driverGetResult.IsFailure)
        {
            return driverGetResult.Error;
        }

        var driver = DriverReportModel.FromDomainModel(driverGetResult.Value);

        return _reportFactory.GenerateReport(driver);
    }
}