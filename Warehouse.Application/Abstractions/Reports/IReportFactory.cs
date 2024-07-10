using Warehouse.Domain.Shared.Results;

namespace Warehouse.Application.Abstractions.Reports;

public interface IReportFactory<in TReportModel> where TReportModel : IReportModel
{
    Result GenerateReport(TReportModel reportModel);
}