using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Warehouse.Application.Reports.Drivers;
using Warehouse.Infrastructure.Reports.Shared;

namespace Warehouse.Infrastructure.Reports.Pdf.Drivers;

internal sealed class DriverReport : IPdfReport
{
    private readonly DriverReportModel _driver;

    public DriverReport(DriverReportModel driver)
    {
        _driver = driver;
    }

    public string GenerateAndSave()
    {
        var filePath = RouteBuilder.RouteFor(this, _driver.VehiclePlate);

        this.GeneratePdf(filePath);

        return filePath;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(2, Unit.Centimetre);
            page.Header().Element(ComposeHeader);
            page.Content().Element(ComposeContent);
            page.Footer().AlignCenter().Text(text =>
            {
                text.CurrentPageNumber();
                text.Span(" / ");
                text.TotalPages();
            });
        });
    }

    private void ComposeHeader(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem().Text($"Kierowca\n{_driver.FirstName} {_driver.LastName}", TextStyle.Default.FontSize(20).Bold().LineHeight(1.5f));
            row.ConstantItem(100).Text($"Pojazd\n{_driver.VehiclePlate}", TextStyle.Default.FontSize(20).Bold().LineHeight(1.5f));
        });
    }

    private void ComposeContent(IContainer container)
    {
        container.PaddingVertical(1, Unit.Centimetre).Column(column =>
        {
            column.Spacing(20);

            column.Item().Text("Transporty", TextStyle.Default.FontSize(18).Bold());

            if (_driver.Transports != null && _driver.Transports.Any())
            {
                foreach (var transport in _driver.Transports)
                {
                    column.Item().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Row(row =>
                    {
                        row.RelativeColumn().Text($"Numer transportu: {transport.Number}");
                        row.RelativeColumn().Text($"Typ: {transport.Type}");
                        row.RelativeColumn().Text($"Przyjęto: {transport.HandledAt}");
                        row.RelativeColumn().Text($"ID Magazyniera: {transport.WarehousemanIdentificationNumber}");
                        row.RelativeColumn().Text($"NIP Kontrahenta: {transport.ClientNip}");
                    });
                }
            }
            else
            {
                column.Item().Text("Brak transportów", TextStyle.Default.Italic());
            }
        });
    }
}