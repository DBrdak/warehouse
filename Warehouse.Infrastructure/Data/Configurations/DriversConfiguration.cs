using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Drivers;
using Warehouse.Domain.Shared;
using Warehouse.Infrastructure.Data.DataConverters;

namespace Warehouse.Infrastructure.Data.Configurations;

internal sealed class DriversConfiguration : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK__Kierowcy__EE994F765B38A581");
        
        builder.ToTable("Kierowcy");

        builder.HasIndex(e => e.VehiclePlate, "UQ__Kierowcy__5EB0FACFAF7A65A9").IsUnique();

        builder.Property(e => e.Id)
            .HasColumnName("id_kierowcy")
            .HasConversion(d => d.Id, s => new DriverId(s));

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

        builder.Property(e => e.VehiclePlate)
            .HasMaxLength(8)
            .IsUnicode(false)
            .HasColumnName("numer_rejestracyjny_pojazdu")
            .HasConversion(d => d.Value, s => DataConverter.ConvertToDomainModel<VehiclePlate>(s));

        builder.Property(e => e.IsDeleted)
            .HasColumnName("czy_usunieto");

        builder.HasQueryFilter(e => e.IsDeleted);

    }
}