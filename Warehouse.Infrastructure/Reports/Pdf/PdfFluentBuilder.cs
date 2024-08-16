using Warehouse.Application.Abstractions.Reports;
using Warehouse.Application.Reports.Clients;
using Warehouse.Application.Reports.Drivers;
using Warehouse.Infrastructure.Reports.Pdf.Clients;
using Warehouse.Infrastructure.Reports.Pdf.Drivers;
using Warehouse.Infrastructure.Reports.Shared;

namespace Warehouse.Infrastructure.Reports.Pdf;

internal static class PdfFluentBuilder
{
    public static IReport ToReport<TReportModel>(this TReportModel reportModel) where TReportModel : IReportModel? =>
        reportModel switch
        {
            DriverReportModel model => new DriverReport(model),
            ClientReportModel model => new ClientReport(model),
            _ => throw new ArgumentException("Nieprawidłowy typ danych do raportu")
        };
}