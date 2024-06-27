using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using Warehouse.Domain.Transports;
using Warehouse.Infrastructure.DataModels;

namespace Warehouse.Infrastructure.Configurations;

internal sealed class TransportsConfiguration : IEntityTypeConfiguration<TransportDataModel>
{
    public void Configure(EntityTypeBuilder<TransportDataModel> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK__Transpor__7AC9B35ED3D035F8");

        builder.ToTable("Transporty");

        builder.HasIndex(e => e.Number, "UQ__Transpor__AF86E6525A448A02").IsUnique();

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("id_transportu");

        builder.Property(e => e.HandledAt)
            .HasColumnType("datetime")
            .HasColumnName("data_czas");

        builder.Property(e => e.DriverId).HasColumnName("id_kierowcy");

        builder.Property(e => e.ClientId).HasColumnName("id_klienta");

        builder.Property(e => e.WarehousemanId).HasColumnName("id_magazyniera");

        builder.Property(e => e.Number).HasColumnName("numer");

        builder.Property(e => e.Type)
            .HasMaxLength(6)
            .IsUnicode(false)
            .HasColumnName("rodzaj");

        builder
            .HasOne(d => d.Driver)
            .WithMany(p => p.Transports)
            .HasForeignKey(d => d.DriverId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK__Transport__id_ki__534D60F1");

        builder
            .HasOne(d => d.Client)
            .WithMany(p => p.Transports)
            .HasForeignKey(d => d.ClientId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK__Transport__id_kl__5441852A");

        builder
            .HasOne(d => d.Warehouseman)
            .WithMany(p => p.Transports)
            .HasForeignKey(d => d.WarehousemanId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK__Transport__id_ma__52593CB8");
    }
}