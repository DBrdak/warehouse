using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Shared;
using Warehouse.Domain.Warehousemen;
using Warehouse.Infrastructure.DataModels;

namespace Warehouse.Infrastructure.Configurations;

internal sealed class WarehousemenConfiguration : IEntityTypeConfiguration<WarehousemanDataModel>
{
    public void Configure(EntityTypeBuilder<WarehousemanDataModel> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK__Magazyni__9BCB02B0B7D1AF2A");

        builder.ToTable("Magazynierzy");

        builder.HasIndex(e => e.IdNumber, "UQ__Magazyni__2EA36CA064CD9141").IsUnique();

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("id_magazyniera");

        builder.Property(e => e.SectorId).HasColumnName("id_sektora");

        builder.Property(e => e.FirstName)
            .HasMaxLength(55)
            .IsUnicode(false)
            .HasColumnName("imie");

        builder.Property(e => e.LastName)
            .HasMaxLength(55)
            .IsUnicode(false)
            .HasColumnName("nazwisko");

        builder.Property(e => e.IdNumber).HasColumnName("numer_identyfikacyjny");

        builder.Property(e => e.Position)
            .HasMaxLength(55)
            .IsUnicode(false)
            .HasColumnName("pozycja");

        builder
            .HasOne(d => d.Sector)
            .WithMany(p => p.Warehousemen)
            .HasForeignKey(d => d.SectorId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK__Magazynie__id_se__4BAC3F29");
    }
}