﻿// <auto-generated />
using System;
using LaTiendaIS.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LaTiendaIS.Shared.Migrations
{
    [DbContext(typeof(DBLaTiendaContext))]
    partial class DBLaTiendaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LaTiendaIS.Shared.Models.ArticuloDTO", b =>
                {
                    b.Property<int>("IdCodigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCodigo"));

                    b.Property<int>("CodigoTienda")
                        .HasColumnType("int");

                    b.Property<double>("Costo")
                        .HasColumnType("float");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdCategoria")
                        .HasColumnType("int");

                    b.Property<int>("IdMarca")
                        .HasColumnType("int");

                    b.Property<float>("MargenDeGanacia")
                        .HasColumnType("real");

                    b.Property<float>("PorcentajeIVA")
                        .HasColumnType("real");

                    b.HasKey("IdCodigo");

                    b.HasIndex("IdCategoria");

                    b.HasIndex("IdMarca");

                    b.ToTable("Articulo");
                });

            modelBuilder.Entity("LaTiendaIS.Shared.Models.CategoriaDTO", b =>
                {
                    b.Property<int>("IdCategoria")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCategoria"));

                    b.Property<string>("DescripcionCategoria")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCategoria");

                    b.ToTable("Categoria");
                });

            modelBuilder.Entity("LaTiendaIS.Shared.Models.ClienteDTO", b =>
                {
                    b.Property<int>("IdCliente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCliente"));

                    b.Property<string>("CUIT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Domicilio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdCondicionTributaria")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCliente");

                    b.HasIndex("IdCondicionTributaria");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("LaTiendaIS.Shared.Models.ColorArticuloDTO", b =>
                {
                    b.Property<int>("IdColor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdColor"));

                    b.Property<string>("DescripcionColor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdColor");

                    b.ToTable("ColorArticulo");
                });

            modelBuilder.Entity("LaTiendaIS.Shared.Models.ComprobanteDTO", b =>
                {
                    b.Property<int>("IdComprobante")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdComprobante"));

                    b.Property<int>("IdTipoDeComprobante")
                        .HasColumnType("int");

                    b.Property<int>("IdVenta")
                        .HasColumnType("int");

                    b.HasKey("IdComprobante");

                    b.HasIndex("IdTipoDeComprobante");

                    b.HasIndex("IdVenta");

                    b.ToTable("Comprobante");
                });

            modelBuilder.Entity("LaTiendaIS.Shared.Models.CondicionTributariaDTO", b =>
                {
                    b.Property<int>("IdCondicionTributaria")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCondicionTributaria"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCondicionTributaria");

                    b.ToTable("CondicionTributaria");
                });

            modelBuilder.Entity("LaTiendaIS.Shared.Models.LineaDeVentaDTO", b =>
                {
                    b.Property<int>("IdLineaDeVenta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdLineaDeVenta"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int>("IdArticulo")
                        .HasColumnType("int");

                    b.Property<int>("IdVenta")
                        .HasColumnType("int");

                    b.HasKey("IdLineaDeVenta");

                    b.HasIndex("IdArticulo");

                    b.HasIndex("IdVenta");

                    b.ToTable("LineaDeVenta");
                });

            modelBuilder.Entity("LaTiendaIS.Shared.Models.MarcaDTO", b =>
                {
                    b.Property<int>("IdMarca")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdMarca"));

                    b.Property<string>("DescripcionMarca")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdMarca");

                    b.ToTable("Marca");
                });

            modelBuilder.Entity("LaTiendaIS.Shared.Models.PagoDTO", b =>
                {
                    b.Property<int>("IdPago")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPago"));

                    b.Property<double>("Cantidad")
                        .HasColumnType("float");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdCliente")
                        .HasColumnType("int");

                    b.Property<int>("IdVenta")
                        .HasColumnType("int");

                    b.HasKey("IdPago");

                    b.HasIndex("IdCliente");

                    b.HasIndex("IdVenta");

                    b.ToTable("Pago");

                    b.HasDiscriminator<string>("Discriminator").HasValue("PagoDTO");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("LaTiendaIS.Shared.Models.PuntoDeVentaDTO", b =>
                {
                    b.Property<int>("IdPuntoDeVenta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPuntoDeVenta"));

                    b.Property<int>("IdSucursal")
                        .HasColumnType("int");

                    b.Property<int>("NumeroPtoVenta")
                        .HasColumnType("int");

                    b.HasKey("IdPuntoDeVenta");

                    b.HasIndex("IdSucursal");

                    b.ToTable("PuntoDeVenta");
                });

            modelBuilder.Entity("LaTiendaIS.Shared.Models.StockDTO", b =>
                {
                    b.Property<int>("IdStock")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdStock"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int>("IdArticulo")
                        .HasColumnType("int");

                    b.Property<int>("IdColor")
                        .HasColumnType("int");

                    b.Property<int>("IdSucursal")
                        .HasColumnType("int");

                    b.Property<int>("IdTalle")
                        .HasColumnType("int");

                    b.HasKey("IdStock");

                    b.HasIndex("IdArticulo");

                    b.HasIndex("IdColor");

                    b.HasIndex("IdSucursal");

                    b.HasIndex("IdTalle");

                    b.ToTable("Stock");
                });

            modelBuilder.Entity("LaTiendaIS.Shared.Models.SucursalDTO", b =>
                {
                    b.Property<int>("IdSucursal")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSucursal"));

                    b.Property<int>("IdTienda")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdSucursal");

                    b.HasIndex("IdTienda");

                    b.ToTable("Sucursal");
                });

            modelBuilder.Entity("LaTiendaIS.Shared.Models.TalleDTO", b =>
                {
                    b.Property<int>("IdTalle")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTalle"));

                    b.Property<string>("DescripcionTalle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdTipoTalle")
                        .HasColumnType("int");

                    b.HasKey("IdTalle");

                    b.HasIndex("IdTipoTalle");

                    b.ToTable("Talle");
                });

            modelBuilder.Entity("LaTiendaIS.Shared.Models.TiendaDTO", b =>
                {
                    b.Property<int>("IdTienda")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTienda"));

                    b.Property<string>("CUIT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Telefono")
                        .HasColumnType("bigint");

                    b.HasKey("IdTienda");

                    b.ToTable("TiendaDTO");
                });

            modelBuilder.Entity("LaTiendaIS.Shared.Models.TipoDeComprobanteDTO", b =>
                {
                    b.Property<int>("IdTipoDeComprobante")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTipoDeComprobante"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdTipoDeComprobante");

                    b.ToTable("TipoDeComprobante");
                });

            modelBuilder.Entity("LaTiendaIS.Shared.Models.TipoTalleDTO", b =>
                {
                    b.Property<int>("IdTipoTalle")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTipoTalle"));

                    b.Property<string>("DescripcionTipoTalle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdTipoTalle");

                    b.ToTable("TipoTalle");
                });

            modelBuilder.Entity("LaTiendaIS.Shared.Models.VentaDTO", b =>
                {
                    b.Property<int>("IdVenta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdVenta"));

                    b.Property<DateTime>("FechaVenta")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("IdVenta");

                    b.ToTable("Venta");
                });

            modelBuilder.Entity("LaTiendaIS.Shared.Models.PagoConTarjetaDTO", b =>
                {
                    b.HasBaseType("LaTiendaIS.Shared.Models.PagoDTO");

                    b.Property<int>("CVV")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaVencimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("NombreTitular")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("NumeroDeTarjeta")
                        .HasColumnType("bigint");

                    b.HasDiscriminator().HasValue("PagoConTarjetaDTO");
                });

            modelBuilder.Entity("LaTiendaIS.Shared.Models.PagoEfectivoDTO", b =>
                {
                    b.HasBaseType("LaTiendaIS.Shared.Models.PagoDTO");

                    b.Property<double>("Monto")
                        .HasColumnType("float");

                    b.HasDiscriminator().HasValue("PagoEfectivoDTO");
                });

            modelBuilder.Entity("LaTiendaIS.Shared.Models.ArticuloDTO", b =>
                {
                    b.HasOne("LaTiendaIS.Shared.Models.CategoriaDTO", "Categoria")
                        .WithMany()
                        .HasForeignKey("IdCategoria")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LaTiendaIS.Shared.Models.MarcaDTO", "Marca")
                        .WithMany()
                        .HasForeignKey("IdMarca")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");

                    b.Navigation("Marca");
                });

            modelBuilder.Entity("LaTiendaIS.Shared.Models.ClienteDTO", b =>
                {
                    b.HasOne("LaTiendaIS.Shared.Models.CondicionTributariaDTO", "CondicionTributaria")
                        .WithMany()
                        .HasForeignKey("IdCondicionTributaria")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CondicionTributaria");
                });

            modelBuilder.Entity("LaTiendaIS.Shared.Models.ComprobanteDTO", b =>
                {
                    b.HasOne("LaTiendaIS.Shared.Models.TipoDeComprobanteDTO", "TipoDeComprobante")
                        .WithMany()
                        .HasForeignKey("IdTipoDeComprobante")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LaTiendaIS.Shared.Models.VentaDTO", "Venta")
                        .WithMany()
                        .HasForeignKey("IdVenta")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipoDeComprobante");

                    b.Navigation("Venta");
                });

            modelBuilder.Entity("LaTiendaIS.Shared.Models.LineaDeVentaDTO", b =>
                {
                    b.HasOne("LaTiendaIS.Shared.Models.ArticuloDTO", "Articulo")
                        .WithMany()
                        .HasForeignKey("IdArticulo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LaTiendaIS.Shared.Models.VentaDTO", "Venta")
                        .WithMany()
                        .HasForeignKey("IdVenta")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Articulo");

                    b.Navigation("Venta");
                });

            modelBuilder.Entity("LaTiendaIS.Shared.Models.PagoDTO", b =>
                {
                    b.HasOne("LaTiendaIS.Shared.Models.ClienteDTO", "Cliente")
                        .WithMany()
                        .HasForeignKey("IdCliente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LaTiendaIS.Shared.Models.VentaDTO", "Venta")
                        .WithMany()
                        .HasForeignKey("IdVenta")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Venta");
                });

            modelBuilder.Entity("LaTiendaIS.Shared.Models.PuntoDeVentaDTO", b =>
                {
                    b.HasOne("LaTiendaIS.Shared.Models.SucursalDTO", "Sucursal")
                        .WithMany()
                        .HasForeignKey("IdSucursal")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sucursal");
                });

            modelBuilder.Entity("LaTiendaIS.Shared.Models.StockDTO", b =>
                {
                    b.HasOne("LaTiendaIS.Shared.Models.ArticuloDTO", "Articulo")
                        .WithMany()
                        .HasForeignKey("IdArticulo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LaTiendaIS.Shared.Models.ColorArticuloDTO", "Color")
                        .WithMany()
                        .HasForeignKey("IdColor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LaTiendaIS.Shared.Models.SucursalDTO", "Sucursal")
                        .WithMany()
                        .HasForeignKey("IdSucursal")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LaTiendaIS.Shared.Models.TalleDTO", "Talle")
                        .WithMany()
                        .HasForeignKey("IdTalle")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Articulo");

                    b.Navigation("Color");

                    b.Navigation("Sucursal");

                    b.Navigation("Talle");
                });

            modelBuilder.Entity("LaTiendaIS.Shared.Models.SucursalDTO", b =>
                {
                    b.HasOne("LaTiendaIS.Shared.Models.TiendaDTO", "Tienda")
                        .WithMany()
                        .HasForeignKey("IdTienda")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tienda");
                });

            modelBuilder.Entity("LaTiendaIS.Shared.Models.TalleDTO", b =>
                {
                    b.HasOne("LaTiendaIS.Shared.Models.TipoTalleDTO", "TipoTalle")
                        .WithMany()
                        .HasForeignKey("IdTipoTalle")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipoTalle");
                });
#pragma warning restore 612, 618
        }
    }
}
