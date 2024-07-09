using Warehouse.Infrastructure.Reports.Pdf;
using Warehouse.Infrastructure.Reports.Pdf.Drivers;
using Warehouse.Infrastructure.Reports.Spreadsheet;
using Warehouse.Infrastructure.Reports.Spreadsheet.Clients;

namespace Warehouse.Infrastructure.Reports.Shared;

internal static class RouteBuilder
{
    public static string RouteFor<TReport>(
        TReport report, string? fileNameParam = null) where TReport : IReport =>
        string.Join(
            @"\",
            Environment.CurrentDirectory,
            "Raporty",
            report.GetDirectoryName(),
            DateTime.Now.ToString("dd-MM-yyyy"),
            report.GetFileName(fileNameParam)).EnsurePathIsCreated();

    private static string GetDirectoryName<TReport>(this TReport report) where TReport : IReport =>
        report switch
        {
            DriverReport => "Portiernia",
            ClientReport => "Obsługa klienta"
        };

    private static string GetFileName<TReport>(this TReport report, string? fileNameParam) where TReport : IReport =>
        (report, string.IsNullOrWhiteSpace(fileNameParam)) switch
        {
            (IPdfReport, true) => $"{DateTime.Now:hh_mm_ss}.pdf", 
            (IXlsxReport, true) => $"{DateTime.Now:hh_mm_ss}.xlsx",
            (IPdfReport, false) => $"{fileNameParam + "-"}{DateTime.Now:hh_mm_ss}.pdf", 
            (IXlsxReport, false) => $"{fileNameParam + "-"}{DateTime.Now:hh_mm_ss}.xlsx"
        };

    private static string EnsurePathIsCreated(this string path)
    {
        var groupedPath = path.Split(@"\");
        var fileNameIndex = groupedPath.Length - 1;
        var dir = string.Join(@"\", groupedPath[..fileNameIndex]);

        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        return path;
    }
}