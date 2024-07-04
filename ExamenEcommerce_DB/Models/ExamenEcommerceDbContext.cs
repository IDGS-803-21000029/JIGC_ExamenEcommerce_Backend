using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace ExamenEcommerce_DB.Models;

public partial class ExamenEcommerceDbContext : DbContext
{
    public ExamenEcommerceDbContext()
    {
    }

    public ExamenEcommerceDbContext(DbContextOptions<ExamenEcommerceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Producto> Productos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Producto>(entity =>
        {
            entity.ToTable("Productos");
            entity.HasKey(e => e.IdProducto);

            entity.Property(e => e.IdProducto)
                .HasColumnName("IdProducto")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Descripcion)
                .HasMaxLength(500);

            entity.Property(e => e.Precio)
                .HasColumnType("decimal(18, 2)");

            entity.Property(e => e.Imagen)
                .HasColumnType("nvarchar(max)");

            entity.Property(e => e.Categoria)
                .HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
