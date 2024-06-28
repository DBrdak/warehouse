using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Infrastructure.Data.DataModels;

namespace Warehouse.Infrastructure.Data.Configurations;

internal sealed class ClientsConfiguration : IEntityTypeConfiguration<ClientDataModel>
{
    public void Configure(EntityTypeBuilder<ClientDataModel> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK__Klienci__EB1748C9C8BA3B5E");
        builder.ToTable("Klienci");

        builder.HasIndex(e => e.NIP, "UQ__Klienci__DF97D0E86DA94539").IsUnique();

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("id_klienta");

        builder.Property(e => e.Name)
            .HasMaxLength(55)
            .IsUnicode(false)
            .HasColumnName("nazwa");

        builder.Property(e => e.NIP)
            .HasMaxLength(12)
            .IsUnicode(false)
            .HasColumnName("nip");
    }
}