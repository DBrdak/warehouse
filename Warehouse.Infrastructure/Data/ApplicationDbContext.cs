using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Warehouse.Application.Abstractions.Data;
using Warehouse.Infrastructure.Data.Options;

namespace Warehouse.Infrastructure.Data;

internal sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    private readonly ApplicationDbContextOptions _options;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IOptions<ApplicationDbContextOptions> appDbContextOptions)
        : base(options)
    {
        _options = appDbContextOptions.Value;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(_options.ConnectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        return base.SaveChangesAsync(cancellationToken);
    }
}