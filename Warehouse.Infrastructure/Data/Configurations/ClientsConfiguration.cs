﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Clients;
using Warehouse.Infrastructure.Data.DataConverters;

namespace Warehouse.Infrastructure.Data.Configurations;

internal sealed class ClientsConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK_Klienci");

        builder.ToTable("Klienci");

        builder.HasIndex(e => e.Nip, "UQ_Klienci_Nip").IsUnique();

        builder.Property(e => e.Id)
            .HasColumnName("id_klienta")
            .HasConversion(d => d.Id, s => new ClientId(s));

        builder.Property(e => e.Name)
            .HasMaxLength(55)
            .IsUnicode(false)
            .HasColumnName("nazwa")
            .HasConversion(d => d.Value, s => DataConverter.ConvertToDomainModel<ClientName>(s));

        builder.Property(e => e.Nip)
            .IsUnicode(false)
            .HasColumnName("nip")
            .HasConversion(d => d.Value, s => DataConverter.ConvertToDomainModel<NIP>(s));

        builder.Property(e => e.IsDeleted)
            .HasColumnName("czy_usunieto");

        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}