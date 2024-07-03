using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Sectors;
using Warehouse.Infrastructure.Data.DataConverters;

namespace Warehouse.Infrastructure.Data.Configurations;

internal sealed class SectorsConfiguration : IEntityTypeConfiguration<Sector>
{
    public void Configure(EntityTypeBuilder<Sector> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK__Sektory__5FD5DBD2DD7B2EDD");

        builder.ToTable("Sektory");

        builder.HasIndex(e => e.Number, "UQ__Sektory__AF86E65285CA37F0").IsUnique();

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("id_sektora")
            .HasConversion(d => d.Id, s => new SectorId(s));

        builder.Property(e => e.Number)
            .HasColumnName("numer")
            .HasConversion(d => d.Value, s => DataConverter.ConvertToDomainModel<SectorNumber>(s));

        builder.Navigation(e => e.PalletSpaces).AutoInclude();
        builder.Navigation(e => e.Warehousemen).AutoInclude();
    }
}