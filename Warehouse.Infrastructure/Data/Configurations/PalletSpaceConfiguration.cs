using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Infrastructure.Data.DataModels;

namespace Warehouse.Infrastructure.Data.Configurations;

internal sealed class PalletSpaceConfiguration : IEntityTypeConfiguration<PalletSpaceDataModel>
{
    public void Configure(EntityTypeBuilder<PalletSpaceDataModel> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK__Miejsca___BEC03B59881AD6DC");

        builder.ToTable("Miejsca_paletowe");

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("id_miejsca_paletowego");

        builder.Property(e => e.SectorId).HasColumnName("id_sektora");

        builder.Property(e => e.Number).HasColumnName("numer");

        builder.Property(e => e.Shelf).HasColumnName("polka");

        builder.Property(e => e.Rack).HasColumnName("regal");

        builder
            .HasOne(d => d.Sector)
            .WithMany(p => p.PalletSpaces)
            .HasForeignKey(d => d.SectorId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK__Miejsca_p__id_se__4E88ABD4");
    }
}