using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Sectors;
using Warehouse.Domain.Shared;
using Warehouse.Infrastructure.DataModels;

namespace Warehouse.Infrastructure.Configurations;

internal sealed class SectorsConfiguration : IEntityTypeConfiguration<SectorDataModel>
{
    public void Configure(EntityTypeBuilder<SectorDataModel> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK__Sektory__5FD5DBD2DD7B2EDD");
        builder.ToTable("Sektory");

        builder.HasIndex(e => e.Number, "UQ__Sektory__AF86E65285CA37F0").IsUnique();

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("id_sektora");

        builder.Property(e => e.Number)
            .HasColumnName("numer");
    }
}