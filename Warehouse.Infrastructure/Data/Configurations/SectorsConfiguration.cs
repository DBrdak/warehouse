using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Sectors;
using Warehouse.Infrastructure.Data.DataConverters;

namespace Warehouse.Infrastructure.Data.Configurations;

internal sealed class SectorsConfiguration : IEntityTypeConfiguration<Sector>
{
    public void Configure(EntityTypeBuilder<Sector> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK_Sektory");

        builder.ToTable("Sektory");

        builder.HasIndex(e => e.Number, "UQ_Sektory_Numer").IsUnique();

        builder.Property(e => e.Id)
            
            .HasColumnName("id_sektora")
            .HasConversion(d => d.Id, s => new SectorId(s));

        builder.Property(e => e.Number)
            .HasColumnName("numer")
            .HasConversion(d => d.Value, s => DataConverter.ConvertToDomainModel<SectorNumber>(s));

        builder.Property(e => e.IsDeleted)
            .HasColumnName("czy_usunieto");

        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}