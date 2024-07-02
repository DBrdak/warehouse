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
        builder.HasKey(e => e.Id).HasName("PK__Towary__DEA5BF82E3A5895E");
        builder.ToTable("Towary");

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("id_towaru")
            .HasConversion(d => d.Id, s => new FreightId(s));

        builder.Property(e => e.ImportId)
            .HasColumnName("id_dostawy")
            .HasConversion(d => d.Id, s => new TransportId(s));

        builder.Property(e => e.PalletSpaceId)
            .HasColumnName("id_miejsca_paletowego")
            .HasConversion(d => d.Id, s => new PalletSpaceId(s));

        builder.Property(e => e.ExportId)
            .HasColumnName("id_odbioru")
            .HasConversion<Guid?>(
                d => d == null ? null : d.Id,
                s => s == null ? null : new TransportId((Guid)s));

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

        builder.HasOne(f => f.Import)
            .WithMany(t => t.Freights)
            .HasForeignKey(d => d.ImportId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK__Towary__id_dosta__5812160E");

        builder.HasOne(f => f.PalletSpace)
            .WithMany(p => p.Freights)
            .HasForeignKey(d => d.PalletSpaceId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK__Towary__id_miejs__571DF1D5");

        builder.HasOne(f => f.Export)
            .WithMany(t => t.Freights)
            .HasForeignKey(d => d.ExportId)
            .HasConstraintName("FK__Towary__id_odbio__59063A47");
    }
}