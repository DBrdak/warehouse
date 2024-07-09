using Warehouse.Application.Abstractions.Reports;
using Warehouse.Domain.Shared.Extensions;
using Warehouse.Domain.Shared.Results;
using Warehouse.Infrastructure.Reports.Pdf;

namespace Warehouse.Infrastructure.Reports;

internal sealed class ReportFactory<TReportModel> : IReportFactory<TReportModel> where TReportModel : IReportModel
{
    public Result GenerateReport(TReportModel driver)
    {
        try
        {
            driver
                .ToReport()
                .GenerateAndSave()
                .OpenFile();
        }
        catch
        {
            return Error.ExceptionWithMessage("Utworzenie raportu nie powiodło się");
        }

        return Result.Success();
    }
}