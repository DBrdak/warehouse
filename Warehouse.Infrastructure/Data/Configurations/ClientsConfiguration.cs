using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Clients;
using Warehouse.Infrastructure.Data.DataConverters;

namespace Warehouse.Infrastructure.Data.Configurations;

internal sealed class ClientsConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK__Klienci__EB1748C9C8BA3B5E");

        builder.ToTable("Klienci");

        builder.HasIndex(e => e.Nip, "UQ__Klienci__DF97D0E86DA94539").IsUnique();

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("id_klienta")
            .HasConversion(d => d.Id, s => new ClientId(s));

        builder.Property(e => e.Name)
            .HasMaxLength(55)
            .IsUnicode(false)
            .HasColumnName("nazwa")
            .HasConversion(d => d.Value, s => DataConverter.ConvertToDomainModel<ClientName>(s));

        builder.Property(e => e.Nip)
            .HasMaxLength(12)
            .IsUnicode(false)
            .HasColumnName("nip")
            .HasConversion(d => d.Value, s => DataConverter.ConvertToDomainModel<NIP>(s));
    }
}