namespace Warehouse.Infrastructure.Data.Options;

public sealed record ApplicationDbContextOptions
{
    public string ConnectionString { get; set; } = string.Empty;
}