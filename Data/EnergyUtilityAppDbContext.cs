using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EnergyUtilityApp;

public partial class EnergyUtilityAppDbContext : IdentityDbContext<ApplicationUser>
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

    public virtual DbSet<ApiKeyLookup> UserApiKeys { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // set the default schema
        modelBuilder.HasDefaultSchema("app");
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApiKeyLookup>(entity =>
        {
            entity.HasKey(e => e.ApiKey).HasName("user_api_keys_pkey");

            entity.ToTable("user_api_keys", "auth");

            entity.Property(e => e.ApiKey).HasColumnName("api_key");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
        });

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
            entity.Property(e => e.Text)
                .HasMaxLength(100)
                .HasColumnName("text");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
