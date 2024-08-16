using QuestPDF.Infrastructure;
using Warehouse.Infrastructure.Reports.Shared;

namespace Warehouse.Infrastructure.Reports.Pdf;

internal interface IPdfReport : IDocument, IReport;