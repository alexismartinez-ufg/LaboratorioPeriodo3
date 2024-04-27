﻿// <auto-generated />
using System;
using LaboratorioPeriodo3.DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LaboratorioPeriodo3.DB.Migrations
{
    [DbContext(typeof(LaboratorioPeriodo3Context))]
    [Migration("20240427153040_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LaboratorioPeriodo3.DB.Models.Autore", b =>
                {
                    b.Property<int>("AutorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("AutorID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AutorId"));

                    b.Property<string>("Apellido")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Nombre")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("AutorId")
                        .HasName("PK__Autores__F58AE909E94B3F36");

                    b.ToTable("Autores");
                });

            modelBuilder.Entity("LaboratorioPeriodo3.DB.Models.Libro", b =>
                {
                    b.Property<int>("Codigo")
                        .HasColumnType("int");

                    b.Property<int?>("AnioEdicion")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("text");

                    b.Property<string>("Editorial")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Isbn")
                        .HasMaxLength(13)
                        .IsUnicode(false)
                        .HasColumnType("varchar(13)")
                        .HasColumnName("ISBN");

                    b.Property<string>("NacionalidadAutor")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Codigo")
                        .HasName("PK__Libros__06370DAD6C9D3516");

                    b.ToTable("Libros");
                });

            modelBuilder.Entity("LaboratorioPeriodo3.DB.Models.LibrosAutore", b =>
                {
                    b.Property<int?>("CodigoLibro")
                        .HasColumnType("int");

                    b.Property<int?>("AutorId")
                        .HasColumnType("int")
                        .HasColumnName("AutorID");

                    b.HasKey("CodigoLibro", "AutorId");

                    b.HasIndex("AutorId");

                    b.ToTable("LibrosAutores");
                });

            modelBuilder.Entity("LaboratorioPeriodo3.DB.Models.LibrosAutore", b =>
                {
                    b.HasOne("LaboratorioPeriodo3.DB.Models.Autore", "Autor")
                        .WithMany()
                        .HasForeignKey("AutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LaboratorioPeriodo3.DB.Models.Libro", "CodigoLibroNavigation")
                        .WithMany("LibrosAutores")
                        .HasForeignKey("CodigoLibro")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Autor");

                    b.Navigation("CodigoLibroNavigation");
                });

            modelBuilder.Entity("LaboratorioPeriodo3.DB.Models.Libro", b =>
                {
                    b.Navigation("LibrosAutores");
                });
#pragma warning restore 612, 618
        }
    }
}
