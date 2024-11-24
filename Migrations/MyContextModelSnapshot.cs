﻿// <auto-generated />
using System;
using Biblioteca.Contexto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Biblioteca.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("Biblioteca.Models.Dato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Tipo")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Datos");
                });

            modelBuilder.Entity("Biblioteca.Models.Libro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AutorId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CategoriaId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("EditorialId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Ejemplares")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Fechareg")
                        .HasColumnType("TEXT");

                    b.Property<string>("ISBN")
                        .HasColumnType("TEXT");

                    b.Property<string>("Rutaportada")
                        .HasColumnType("TEXT");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Ubicacion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AutorId");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("EditorialId");

                    b.ToTable("Libros");
                });

            modelBuilder.Entity("Biblioteca.Models.Persona", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Cargo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Intentos")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Materno")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Paterno")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Personas");
                });

            modelBuilder.Entity("Biblioteca.Models.Prestamo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Estado")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaConfirmacion")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Fechadevolucion")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Fechaprestamo")
                        .HasColumnType("TEXT");

                    b.Property<int?>("LibroId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PersonaId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("LibroId");

                    b.HasIndex("PersonaId");

                    b.ToTable("Prestamos");
                });

            modelBuilder.Entity("Biblioteca.Models.Sancion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Idprestamo")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Montototal")
                        .HasColumnType("TEXT");

                    b.Property<string>("Motivo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PersonaId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PrestamoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PersonaId");

                    b.HasIndex("PrestamoId");

                    b.ToTable("Sanciones");
                });

            modelBuilder.Entity("Biblioteca.Models.Libro", b =>
                {
                    b.HasOne("Biblioteca.Models.Dato", "Autor")
                        .WithMany()
                        .HasForeignKey("AutorId");

                    b.HasOne("Biblioteca.Models.Dato", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId");

                    b.HasOne("Biblioteca.Models.Dato", "Editorial")
                        .WithMany()
                        .HasForeignKey("EditorialId");

                    b.Navigation("Autor");

                    b.Navigation("Categoria");

                    b.Navigation("Editorial");
                });

            modelBuilder.Entity("Biblioteca.Models.Prestamo", b =>
                {
                    b.HasOne("Biblioteca.Models.Libro", "Libro")
                        .WithMany("Prestamos")
                        .HasForeignKey("LibroId");

                    b.HasOne("Biblioteca.Models.Persona", "Persona")
                        .WithMany("Prestamos")
                        .HasForeignKey("PersonaId");

                    b.Navigation("Libro");

                    b.Navigation("Persona");
                });

            modelBuilder.Entity("Biblioteca.Models.Sancion", b =>
                {
                    b.HasOne("Biblioteca.Models.Persona", "Persona")
                        .WithMany("Sanciones")
                        .HasForeignKey("PersonaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Biblioteca.Models.Prestamo", "Prestamo")
                        .WithMany()
                        .HasForeignKey("PrestamoId");

                    b.Navigation("Persona");

                    b.Navigation("Prestamo");
                });

            modelBuilder.Entity("Biblioteca.Models.Libro", b =>
                {
                    b.Navigation("Prestamos");
                });

            modelBuilder.Entity("Biblioteca.Models.Persona", b =>
                {
                    b.Navigation("Prestamos");

                    b.Navigation("Sanciones");
                });
#pragma warning restore 612, 618
        }
    }
}