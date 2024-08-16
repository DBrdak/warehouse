namespace Warehouse.Infrastructure.Reports.Shared;

internal interface IReport
{
    string GenerateAndSave();
}