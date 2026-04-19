using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SewControl.Persistence.Data;

public class TallerContext : DbContext
{
    public TallerContext(DbContextOptions<TallerContext> options) : base(options) { }

    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Costurera> Costureras => Set<Costurera>();
    public DbSet<Encargo> Encargos => Set<Encargo>();
    public DbSet<Prenda> Prendas => Set<Prenda>();
    public DbSet<Arreglo> Arreglos => Set<Arreglo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cliente>(e =>
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.Nombre).IsRequired().HasMaxLength(100);
            e.Property(c => c.Apellido).IsRequired().HasMaxLength(100);
            e.Property(c => c.Telefono).IsRequired().HasMaxLength(20);
            e.HasQueryFilter(c => !c.IsDeleted);
        });

        modelBuilder.Entity<Costurera>(e =>
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.Nombre).IsRequired().HasMaxLength(100);
            e.Property(c => c.Apellido).IsRequired().HasMaxLength(100);
            e.HasQueryFilter(c => !c.IsDeleted);
        });

        modelBuilder.Entity<Encargo>(e =>
        {
            e.HasKey(enc => enc.Id);
            e.Property(enc => enc.PrecioTotal).HasColumnType("decimal(10,2)");
            e.Property(enc => enc.Anticipo).HasColumnType("decimal(10,2)");
            e.Property(enc => enc.Observaciones).HasMaxLength(500);

            e.HasOne(enc => enc.Cliente)
             .WithMany(c => c.Encargos)
             .HasForeignKey(enc => enc.ClienteId)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(enc => enc.Costurera)
             .WithMany(c => c.Encargos)
             .HasForeignKey(enc => enc.CostureraId)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasQueryFilter(enc => !enc.IsDeleted);
        });

        modelBuilder.Entity<Prenda>(e =>
        {
            e.HasKey(p => p.Id);
            e.Property(p => p.Descripcion).IsRequired().HasMaxLength(200);
            e.HasOne(p => p.Encargo)
             .WithMany(enc => enc.Prendas)
             .HasForeignKey(p => p.EncargoId);
            e.HasQueryFilter(p => !p.IsDeleted);
        });

        modelBuilder.Entity<Arreglo>(e =>
        {
            e.HasKey(a => a.Id);
            e.Property(a => a.Descripcion).IsRequired().HasMaxLength(200);
            e.Property(a => a.Costo).HasColumnType("decimal(10,2)");
            e.HasOne(a => a.Encargo)
             .WithMany(enc => enc.Arreglos)
             .HasForeignKey(a => a.EncargoId);
            e.HasQueryFilter(a => !a.IsDeleted);
        });
    }
}
