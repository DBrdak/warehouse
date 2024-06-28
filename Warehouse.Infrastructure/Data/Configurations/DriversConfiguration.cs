using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Infrastructure.Data.DataModels;

namespace Warehouse.Infrastructure.Data.Configurations;

internal sealed class DriversConfiguration : IEntityTypeConfiguration<DriverDataModel>
{
    public void Configure(EntityTypeBuilder<DriverDataModel> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK__Kierowcy__EE994F765B38A581");
        
        builder.ToTable("Kierowcy");

        builder.HasIndex(e => e.VehiclePlate, "UQ__Kierowcy__5EB0FACFAF7A65A9").IsUnique();

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("id_kierowcy");

        builder.Property(e => e.FirstName)
            .HasMaxLength(55)
            .IsUnicode(false)
            .HasColumnName("imie");

        builder.Property(e => e.LastName)
            .HasMaxLength(55)
            .IsUnicode(false)
            .HasColumnName("nazwisko");

        builder.Property(e => e.VehiclePlate)
            .HasMaxLength(8)
            .IsUnicode(false)
            .HasColumnName("numer_rejestracyjny_pojazdu");
    }
}