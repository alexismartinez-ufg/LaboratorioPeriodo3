using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LaboratorioPeriodo3.DB.Models;

public partial class LaboratorioPeriodo3Context : DbContext
{
    public LaboratorioPeriodo3Context()
    {
    }

    public LaboratorioPeriodo3Context(DbContextOptions<LaboratorioPeriodo3Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Autore> Autores { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<LibrosAutore> LibrosAutores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autore>(entity =>
        {
            entity.HasKey(e => e.AutorId).HasName("PK__Autores__F58AE909E94B3F36");

            entity.Property(e => e.AutorId).HasColumnName("AutorID");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK__Libros__06370DAD6C9D3516");

            entity
                .HasMany(e => e.LibrosAutores) // Un libro tiene muchos LibrosAutore
                .WithOne(la => la.CodigoLibroNavigation) // Cada LibrosAutore tiene un libro asociado
                .HasForeignKey(la => la.CodigoLibro);

            entity.Property(e => e.Codigo).ValueGeneratedNever();
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.Editorial)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Isbn)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("ISBN");
            entity.Property(e => e.NacionalidadAutor)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LibrosAutore>(entity =>
        {
            // Establecer clave compuesta
            entity.HasKey(e => new { e.CodigoLibro, e.AutorId });

            entity.Property(e => e.AutorId).HasColumnName("AutorID");

            entity.HasOne(d => d.Autor)
                  .WithMany()
                  .HasForeignKey(d => d.AutorId);

            entity.HasOne(d => d.CodigoLibroNavigation)
                  .WithMany(e => e.LibrosAutores) // Referencia a la colección en Libro
                  .HasForeignKey(d => d.CodigoLibro);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
