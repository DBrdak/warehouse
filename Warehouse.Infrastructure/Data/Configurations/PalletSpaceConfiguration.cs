using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.PalletSpaces;
using Warehouse.Domain.Sectors;
using Warehouse.Infrastructure.Data.DataConverters;

namespace Warehouse.Infrastructure.Data.Configurations;

internal sealed class PalletSpaceConfiguration : IEntityTypeConfiguration<PalletSpace>
{
    public void Configure(EntityTypeBuilder<PalletSpace> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK__Miejsca___BEC03B59881AD6DC");

        builder.ToTable("Miejsca_paletowe");

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("id_miejsca_paletowego")
            .HasConversion(d => d.Id, s => new PalletSpaceId(s));

        builder.Property(e => e.SectorId)
            .HasColumnName("id_sektora")
            .HasConversion(d => d.Id, s => new SectorId(s));

        builder.Property(e => e.Number)
            .HasColumnName("numer")
            .HasConversion(d => d.Value, s => DataConverter.ConvertToDomainModel<PalletSpaceNumber>(s));

        builder.Property(e => e.Shelf)
            .HasColumnName("polka")
            .HasConversion(d => d.Value, s => DataConverter.ConvertToDomainModel<Shelf>(s));

        builder.Property(e => e.Rack)
            .HasColumnName("regal")
            .HasConversion(d => d.Value, s => DataConverter.ConvertToDomainModel<Rack>(s));

        builder.HasOne(p => p.Sector)
            .WithMany(s => s.PalletSpaces)
            .HasForeignKey(d => d.SectorId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK__Miejsca_p__id_se__4E88ABD4");

        builder.Navigation(e => e.Sector).AutoInclude();

        builder.Navigation(e => e.Freights).AutoInclude();
    }
}