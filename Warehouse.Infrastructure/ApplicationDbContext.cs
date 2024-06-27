using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Warehouse.Infrastructure.DataModels;
using Warehouse.Infrastructure.Models;

namespace Warehouse.Infrastructure;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Kierowcy> Kierowcies { get; set; }

    public virtual DbSet<Klienci> Kliencis { get; set; }

    public virtual DbSet<Magazynierzy> Magazynierzies { get; set; }

    public virtual DbSet<MiejscaPaletowe> MiejscaPaletowes { get; set; }

    public virtual DbSet<Sektory> Sektories { get; set; }

    public virtual DbSet<Towary> Towaries { get; set; }

    public virtual DbSet<Transporty> Transporties { get; set; }

    //TODO fetch from config
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
