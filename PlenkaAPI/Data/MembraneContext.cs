using System;
using Microsoft.EntityFrameworkCore;
using PlenkaAPI.Models;

#nullable disable

namespace PlenkaAPI.Data;

public class MembraneContext : DbContext
{
    public MembraneContext()
    {
        Materials.Load();
        Properties.Load();
        Users.Load();
        UserTypes.Load();
        Values.Load();
        Units.Load();
    }

    public MembraneContext(DbContextOptions<MembraneContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Material> Materials { get; set; }
    public virtual DbSet<Property> Properties { get; set; }
    public virtual DbSet<Unit> Units { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserType> UserTypes { get; set; }
    public virtual DbSet<Value> Values { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https: //go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            optionsBuilder.UseSqlite("DataSource=Membrane.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Material>(entity =>
        {
            entity.ToTable("material");

            entity.HasIndex(e => e.MateriadName, "IX_material_materiad_name")
                .IsUnique();

            entity.HasIndex(e => e.MaterialId, "IX_material_material_id")
                .IsUnique();

            entity.Property(e => e.MaterialId).HasColumnName("material_id");

            entity.Property(e => e.MateriadName)
                .IsRequired()
                .HasColumnName("materiad_name");
        });

        modelBuilder.Entity<Property>(entity =>
        {
            entity.HasKey(e => e.ProperrtyId);

            entity.ToTable("property");

            entity.HasIndex(e => e.ProperrtyId, "IX_property_properrty_id")
                .IsUnique();

            entity.Property(e => e.ProperrtyId).HasColumnName("properrty_id");

            entity.Property(e => e.PropertyName)
                .IsRequired()
                .HasColumnName("property_name");

            entity.Property(e => e.UnitId).HasColumnName("unit_id");

            entity.HasOne(d => d.Unit)
                .WithMany(p => p.Properties)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.ToTable("unit");

            entity.HasIndex(e => e.UnitId, "IX_unit_unit_id")
                .IsUnique();

            entity.Property(e => e.UnitId).HasColumnName("unit_id");

            entity.Property(e => e.UnitName)
                .IsRequired()
                .HasColumnName("unit_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("user");

            entity.HasIndex(e => e.UserId, "IX_user_user_id")
                .IsUnique();

            entity.HasIndex(e => e.UserName, "IX_user_user_name")
                .IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.Property(e => e.UserName)
                .IsRequired()
                .HasColumnName("user_name");

            entity.Property(e => e.UserPassword)
                .IsRequired()
                .HasColumnName("user_password");

            entity.Property(e => e.UserTypeId).HasColumnName("user_type_id");

            entity.HasOne(d => d.UserType)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.UserTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<UserType>(entity =>
        {
            entity.ToTable("user_type");

            entity.HasIndex(e => e.UserTypeId, "IX_user_type_user_type_id")
                .IsUnique();

            entity.HasIndex(e => e.UserTypeName, "IX_user_type_user_type_name")
                .IsUnique();

            entity.Property(e => e.UserTypeId).HasColumnName("user_type_id");

            entity.Property(e => e.UserTypeName).HasColumnName("user_type_name");
        });

        modelBuilder.Entity<Value>(entity =>
        {
            entity.HasKey(e => new {e.MatId, e.PropId});

            entity.ToTable("value");

            entity.Property(e => e.MatId).HasColumnName("mat_id");

            entity.Property(e => e.PropId).HasColumnName("prop_id");

            entity.Property(e => e.Value1).HasColumnName("value");

            entity.HasOne(d => d.Mat)
                .WithMany(p => p.Values)
                .HasForeignKey(d => d.MatId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Prop)
                .WithMany(p => p.Values)
                .HasForeignKey(d => d.PropId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    private void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        throw new NotImplementedException();
    }
}