using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EnergyUtilityApp;

public partial class EnergyUtilityAppDbContext : DbContext
{
    public EnergyUtilityAppDbContext()
    {
    }

    public EnergyUtilityAppDbContext(DbContextOptions<EnergyUtilityAppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<OptionValue> OptionValues { get; set; }

    public virtual DbSet<ParameterTable> ParameterTables { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OptionValue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("option_values_pkey");

            entity.ToTable("option_values");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Option).HasColumnName("option");
            entity.Property(e => e.ParameterId).HasColumnName("parameter_id");
            entity.Property(e => e.Value)
                .HasMaxLength(60)
                .HasColumnName("value");
        });

        modelBuilder.Entity<ParameterTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("parameter_table_pkey");

            entity.ToTable("parameter_table");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DefaultValue)
                .HasMaxLength(8)
                .HasColumnName("default_value");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
            entity.Property(e => e.Options)
                .HasMaxLength(25)
                .HasColumnName("options");
            entity.Property(e => e.Required)
                .HasMaxLength(3)
                .HasColumnName("required");
            entity.Property(e => e.Type)
                .HasMaxLength(8)
                .HasColumnName("type");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
