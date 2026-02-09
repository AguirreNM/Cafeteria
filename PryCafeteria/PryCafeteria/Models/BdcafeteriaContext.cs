using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PryCafeteria.Models;

public partial class BdcafeteriaContext : DbContext
{
    public BdcafeteriaContext()
    {
    }

    public BdcafeteriaContext(DbContextOptions<BdcafeteriaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Cupone> Cupones { get; set; }

    public virtual DbSet<DetallePedido> DetallePedidos { get; set; }

    public virtual DbSet<DireccionesEntrega> DireccionesEntregas { get; set; }

    public virtual DbSet<MetodosPago> MetodosPagos { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProductosTamanio> ProductosTamanios { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Tamanio> Tamanios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Persist Security Info=False;Integrated Security=true;  Initial Catalog= BDCAFETERIA; Server= localhost\\SQLEXPRESS;Encrypt=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.CategoriaId).HasName("PK__Categori__F353C1E5EFC32C83");

            entity.HasIndex(e => e.NombreCategoria, "UQ__Categori__A21FBE9F50992B60").IsUnique();

            entity.Property(e => e.NombreCategoria)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Cupone>(entity =>
        {
            entity.HasKey(e => e.CuponId).HasName("PK__Cupones__C4356897BB81CEF4");

            entity.HasIndex(e => e.NombreCupon, "UQ__Cupones__DFFA581E70FE1535").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.FechaFin).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.NombreCupon)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TipoDescuento)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ValorDescuento).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<DetallePedido>(entity =>
        {
            entity.HasKey(e => e.DetalleId).HasName("PK__DetalleP__6E19D6DA408176C5");

            entity.ToTable("DetallePedido");

            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Pedido).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.PedidoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetallePedido_Pedidos");

            entity.HasOne(d => d.ProductoTamanio).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.ProductoTamanioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetallePedido_ProductosTamanios");
        });

        modelBuilder.Entity<DireccionesEntrega>(entity =>
        {
            entity.HasKey(e => e.DireccionId).HasName("PK__Direccio__68906D64312744DC");

            entity.ToTable("DireccionesEntrega");

            entity.Property(e => e.Calle)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CodigoPostal)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Distrito)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreDireccion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Numero)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Referencias)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("referencias");

            entity.HasOne(d => d.Usuario).WithMany(p => p.DireccionesEntregas)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DireccionesEntrega_Usuarios");
        });

        modelBuilder.Entity<MetodosPago>(entity =>
        {
            entity.HasKey(e => e.MetodoPagoId).HasName("PK__MetodosP__A8FEAF5444056FA2");

            entity.ToTable("MetodosPago");

            entity.HasIndex(e => e.NombreMetodoPago, "UQ__MetodosP__F81E38EDF06F6AED").IsUnique();

            entity.Property(e => e.NombreMetodoPago)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.PedidoId).HasName("PK__Pedidos__09BA1430FB8B5A3E");

            entity.Property(e => e.Descuento).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.FechaPedido)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TipoEntrega)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Direccion).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.DireccionId)
                .HasConstraintName("FK_Pedidos_DireccionesEntrega");

            entity.HasOne(d => d.MetodoPago).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.MetodoPagoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pedidos_MetodosPago");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pedidos_Usuarios");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.ProductoId).HasName("PK__Producto__A430AEA363C471C3");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Disponible).HasDefaultValue(true);
            entity.Property(e => e.Imagen)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Categoria).WithMany(p => p.Productos)
                .HasForeignKey(d => d.CategoriaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Categorias");
        });

        modelBuilder.Entity<ProductosTamanio>(entity =>
        {
            entity.HasKey(e => e.ProductoTamanioId).HasName("PK__Producto__B64AF7683C2EBD99");

            entity.HasIndex(e => new { e.ProductoId, e.TamanioId }, "UQ_ProductosTamanios").IsUnique();

            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Producto).WithMany(p => p.ProductosTamanios)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductosTamanios_Productos");

            entity.HasOne(d => d.Tamanio).WithMany(p => p.ProductosTamanios)
                .HasForeignKey(d => d.TamanioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductosTamanios_Tamanios");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PK__Roles__F92302F1516FC662");

            entity.HasIndex(e => e.RolNombre, "UQ__Roles__65F09DC1547771B2").IsUnique();

            entity.Property(e => e.RolNombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Tamanio>(entity =>
        {
            entity.HasKey(e => e.TamanioId).HasName("PK__Tamanios__3C536BF5A5D6817C");

            entity.HasIndex(e => e.NombreTamanio, "UQ__Tamanios__F822ECC2AE1E0233").IsUnique();

            entity.Property(e => e.NombreTamanio)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE7B81E20315D");

            entity.HasIndex(e => e.Email, "UQ__Usuarios__A9D10534BED76006").IsUnique();

            entity.Property(e => e.Contrasena)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
