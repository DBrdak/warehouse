﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Clients;
using Warehouse.Domain.Drivers;
using Warehouse.Domain.Transports;
using Warehouse.Domain.Warehousemen;
using Warehouse.Infrastructure.Data.DataConverters;

namespace Warehouse.Infrastructure.Data.Configurations;

internal sealed class TransportsConfiguration : IEntityTypeConfiguration<Transport>
{
    public void Configure(EntityTypeBuilder<Transport> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK_Transporty");

        builder.ToTable("Transporty");

        builder.HasIndex(e => e.Number, "UQ_Transporty_Numer").IsUnique();

        builder.Property(e => e.Id)
            .HasColumnName("id_transportu")
            .HasConversion(d => d.Id, s => new TransportId(s));

        builder.Property(e => e.HandledAt)
            .HasColumnType("datetime")
            .HasColumnName("data_czas")
            .HasConversion(d => d.ToUniversalTime(), s => s.ToLocalTime());

        builder.Property(e => e.DriverId)
            .HasColumnName("id_kierowcy")
            .HasConversion(d => d.Id, s => new DriverId(s));

        builder.Property(e => e.ClientId)
            .HasColumnName("id_klienta")
            .HasConversion(d => d.Id, s => new ClientId(s));

        builder.Property(e => e.WarehousemanId)
            .HasColumnName("id_magazyniera")
            .HasConversion(d => d.Id, s => new WarehousemanId(s));

        builder.Property(e => e.Number)
            .HasColumnName("numer")
            .HasConversion(d => d.Value, s => DataConverter.ConvertToDomainModel<TransportNumber>(s));

        builder.Property(e => e.Type)
            .HasMaxLength(6)
            .IsUnicode(false)
            .HasColumnName("rodzaj")
            .HasConversion(d => d.Value, s => DataConverter.ConvertToDomainModel<TransportType>(s));

        builder.HasOne(t => t.Driver)
            .WithMany(d => d.Transports)
            .HasForeignKey(d => d.DriverId)
            .HasConstraintName("FK_Transporty_Kierowcy");

        builder.HasOne(t => t.Client)
            .WithMany(c => c.Transports)
            .HasForeignKey(d => d.ClientId)
            .HasConstraintName("FK_Transporty_Klienci");

        builder.HasOne(t => t.Warehouseman)
            .WithMany(w => w.Transports)
            .HasForeignKey(d => d.WarehousemanId)
            .HasConstraintName("FK_Transporty_Magazynierzy");

        builder.Property(e => e.IsDeleted)
            .HasColumnName("czy_usunieto");

        builder.HasQueryFilter(e => !e.IsDeleted);

        builder.Ignore(e => e.Freights);
    }
}