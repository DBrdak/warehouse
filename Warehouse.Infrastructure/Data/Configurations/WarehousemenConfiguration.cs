using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Sectors;
using Warehouse.Domain.Shared;
using Warehouse.Domain.Warehousemen;
using Warehouse.Infrastructure.Data.DataConverters;

namespace Warehouse.Infrastructure.Data.Configurations;

internal sealed class WarehousemenConfiguration : IEntityTypeConfiguration<Warehouseman>
{
    public void Configure(EntityTypeBuilder<Warehouseman> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK_Magazynierzy");

        builder.ToTable("Magazynierzy");

        builder.HasIndex(e => e.IdentificationNumber, "UQ_Magazynierzy_NumerIdentyfikacyjny").IsUnique();

        builder.Property(e => e.Id)
            .HasColumnName("id_magazyniera")
            .HasConversion(d => d.Id, s => new WarehousemanId(s));

        builder.Property(e => e.SectorId)
            .HasColumnName("id_sektora")
            .HasConversion(d => d.Id, s => new SectorId(s));

        builder.Property(e => e.IsFired)
            .HasColumnName("czy_zwolniony");

        builder.Property(e => e.FirstName)
            .HasMaxLength(55)
            .IsUnicode(false)
            .HasColumnName("imie")
            .HasConversion(d => d.Value, s => DataConverter.ConvertToDomainModel<FirstName>(s));

        builder.Property(e => e.LastName)
            .HasMaxLength(55)
            .IsUnicode(false)
            .HasColumnName("nazwisko")
            .HasConversion(d => d.Value, s => DataConverter.ConvertToDomainModel<LastName>(s));

        builder.Property(e => e.IdentificationNumber)
            .HasColumnName("numer_identyfikacyjny")
            .HasConversion(d => d.Value, s => DataConverter.ConvertToDomainModel<IdentificationNumber>(s));

        builder.Property(e => e.Position)
            .HasMaxLength(55)
            .IsUnicode(false)
            .HasColumnName("pozycja")
            .HasConversion(d => d.Value, s => DataConverter.ConvertToDomainModel<Position>(s));

        builder.HasOne(w => w.Sector)
            .WithMany(s => s.Warehousemen)
            .HasForeignKey(d => d.SectorId)
            .HasConstraintName("FK_Magazynierzy_Sektory");

        builder.Property(e => e.IsDeleted)
            .HasColumnName("czy_usunieto");

        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}