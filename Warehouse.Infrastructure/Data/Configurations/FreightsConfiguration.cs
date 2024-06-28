using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Infrastructure.Data.DataModels;

namespace Warehouse.Infrastructure.Data.Configurations;

internal sealed class FreightsConfiguration : IEntityTypeConfiguration<FreightDataModel>
{
    public void Configure(EntityTypeBuilder<FreightDataModel> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK__Towary__DEA5BF82E3A5895E");
        builder.ToTable("Towary");

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("id_towaru");

        builder.Property(e => e.ImportId).HasColumnName("id_dostawy");

        builder.Property(e => e.PalletSpaceId).HasColumnName("id_miejsca_paletowego");

        builder.Property(e => e.ExportId).HasColumnName("id_odbioru");

        builder.Property(e => e.Quantity)
            .HasColumnType("decimal(10, 2)")
            .HasColumnName("ilosc");

        builder.Property(e => e.Unit)
            .HasMaxLength(55)
            .IsUnicode(false)
            .HasColumnName("jednostka");

        builder.Property(e => e.Name)
            .HasMaxLength(55)
            .IsUnicode(false)
            .HasColumnName("nazwa");

        builder.Property(e => e.Type)
            .HasMaxLength(55)
            .IsUnicode(false)
        .HasColumnName("rodzaj");

        builder
            .HasOne(d => d.Import)
            .WithMany(p => p.Freights)
            .HasForeignKey(d => d.ImportId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK__Towary__id_dosta__5812160E");

        builder
            .HasOne(d => d.PalletSpace)
            .WithMany(p => p.Freights)
            .HasForeignKey(d => d.PalletSpaceId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK__Towary__id_miejs__571DF1D5");

        builder
            .HasOne(d => d.Export)
            .WithMany(p => p.Freights)
            .HasForeignKey(d => d.ExportId)
            .HasConstraintName("FK__Towary__id_odbio__59063A47");
    }
}