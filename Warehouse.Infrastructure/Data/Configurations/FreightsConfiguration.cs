using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Freights;
using Warehouse.Domain.PalletSpaces;
using Warehouse.Domain.Transports;
using Warehouse.Infrastructure.Data.DataConverters;

namespace Warehouse.Infrastructure.Data.Configurations;

internal sealed class FreightsConfiguration : IEntityTypeConfiguration<Freight>
{
    public void Configure(EntityTypeBuilder<Freight> builder)
    {
        // Specify the table name
        builder.ToTable("Towary");

        // Specify the primary key and its name
        builder.HasKey(e => e.Id).HasName("PK_Towary");

        // Configure the primary key column
        builder.Property(e => e.Id)
            .HasColumnName("id_towaru")
            .HasConversion(d => d.Id, s => new FreightId(s));

        // Configure the foreign key for ImportId
        builder.Property(e => e.ImportId)
            .HasColumnName("id_dostawy")
            .HasConversion(d => d.Id, s => new TransportId(s));

        // Configure the foreign key for PalletSpaceId
        builder.Property(e => e.PalletSpaceId)
            .HasColumnName("id_miejsca_paletowego")
            .HasConversion(d => d.Id, s => new PalletSpaceId(s));

        // Configure the foreign key for ExportId
        builder.Property(e => e.ExportId)
            .HasColumnName("id_odbioru")
            .HasConversion<Guid?>(
                d => d == null ? null : d.Id,
                s => s == null ? null : new TransportId((Guid)s));

        // Configure remaining properties
        builder.Property(e => e.Quantity)
            .HasColumnType("decimal(10, 2)")
            .HasColumnName("ilosc")
            .HasConversion(d => d.Value, s => DataConverter.ConvertToDomainModel<Quantity>(s));

        builder.Property(e => e.Unit)
            .HasMaxLength(55)
            .IsUnicode(false)
            .HasColumnName("jednostka")
            .HasConversion(d => d.Value, s => DataConverter.ConvertToDomainModel<Unit>(s));

        builder.Property(e => e.Name)
            .HasMaxLength(55)
            .IsUnicode(false)
            .HasColumnName("nazwa")
            .HasConversion(d => d.Value, s => DataConverter.ConvertToDomainModel<FreightName>(s));

        builder.Property(e => e.Type)
            .HasMaxLength(55)
            .IsUnicode(false)
            .HasColumnName("rodzaj")
            .HasConversion(d => d.Value, s => DataConverter.ConvertToDomainModel<FreightType>(s));

        // Configure relationships with foreign keys
        builder.HasOne(f => f.Import)
            .WithMany(t => t.DeliveredFreights)
            .HasForeignKey(f => f.ImportId)
            .HasConstraintName("FK_Towary_Dostawy");

        builder.HasOne(f => f.Export)
            .WithMany(t => t.ReceivedFreights)
            .HasForeignKey(f => f.ExportId)
            .HasConstraintName("FK_Towary_Odbiory");

        builder.HasOne(f => f.PalletSpace)
            .WithMany(p => p.Freights)
            .HasForeignKey(f => f.PalletSpaceId)
            .HasConstraintName("FK_Towary_MiejscaPaletowe");

        // Configure the soft delete flag
        builder.Property(e => e.IsDeleted)
            .HasColumnName("czy_usunieto");

        // Apply a global query filter for soft delete
        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}