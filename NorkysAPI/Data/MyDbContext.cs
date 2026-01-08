using Microsoft.EntityFrameworkCore;
using NorkysAPI.Models;

namespace NorkysAPI.Data;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carrito> Carritos { get; set; }
    public virtual DbSet<CarritoDetalle> CarritoDetalles { get; set; }
    public virtual DbSet<Categoria> Categorias { get; set; }
    public virtual DbSet<Item> Items { get; set; }
    public virtual DbSet<Usuario> Usuarios { get; set; }
    public virtual DbSet<WishListItem> WishListItems { get; set; }
    public virtual DbSet<Direcciones> Direcciones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ===============================
        // ITEM
        // ===============================
        modelBuilder.Entity<Item>(entity =>
        {
            entity.ToTable("item");

            entity.HasKey(e => e.IdItem);

            entity.Property(e => e.IdItem)
                  .HasColumnName("iditem");

            entity.Property(e => e.Nombre)
                  .HasColumnName("nombre")
                  .HasMaxLength(100);

            entity.Property(e => e.Descripcion)
                  .HasColumnName("descripcion");

            entity.Property(e => e.Precio)
                  .HasColumnName("precio")
                  .HasColumnType("numeric(10,2)");

            entity.Property(e => e.ImagenUrl)
                  .HasColumnName("imagenurl");

            entity.Property(e => e.IdCategoria)
                  .HasColumnName("idcategoria");

            entity.HasOne(e => e.IdCategoriaNavigation)
                  .WithMany(c => c.Items)
                  .HasForeignKey(e => e.IdCategoria);
        });

        // ===============================
        // CATEGORIA
        // ===============================
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.ToTable("categoria");

            entity.HasKey(e => e.IdCategoria);

            entity.Property(e => e.IdCategoria)
                  .HasColumnName("idcategoria");

            entity.Property(e => e.Nombre)
                  .HasColumnName("nombre");
        });

        // ===============================
        // USUARIO
        // ===============================
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("usuario");

            entity.HasKey(e => e.IdUsuario);

            entity.Property(e => e.IdUsuario)
                  .HasColumnName("idusuario");

            entity.Property(e => e.Nombre)
                  .HasColumnName("nombre");

            entity.Property(e => e.Apellido)
                  .HasColumnName("apellido");

            entity.Property(e => e.Email)
                  .HasColumnName("email");
            entity.Property(e => e.PasswordHash)
                  .HasColumnName("passwordhash");

            entity.Property(e => e.DNI)
                  .HasColumnName("dni");
            entity.Property(e => e.EsAdmin)
                  .HasColumnName("esadmin");
        });

        // ===============================
        // CARRITO
        // ===============================
        modelBuilder.Entity<Carrito>(entity =>
        {
            entity.ToTable("carrito");

            entity.HasKey(e => e.IdCarrito);

            entity.Property(e => e.IdCarrito)
                  .HasColumnName("idcarrito");

            entity.Property(e => e.IdUsuario)
                  .HasColumnName("idusuario");

            entity.Property(e => e.FechaCreacion)
                  .HasColumnName("fechacreacion");

            entity.Property(e => e.Total)
                  .HasColumnName("total")
                  .HasColumnType("numeric(10,2)");

            entity.Property(e => e.Estado)
                  .HasColumnName("estado");

            entity.Property(e => e.MetodoPago)
                  .HasColumnName("metodopago");

            entity.Property(e => e.IdDireccion)
                  .HasColumnName("iddireccion");

            entity.Property(e => e.TituloDireccion)
                  .HasColumnName("titulodireccion");

            entity.Property(e => e.Direccion)
                  .HasColumnName("direccionentrega");

            entity.Property(e => e.Referencia)
                  .HasColumnName("referencia");

            entity.Property(e => e.Telefono1)
                  .HasColumnName("telefono1");

            entity.Property(e => e.Telefono2)
                  .HasColumnName("telefono2");

            entity.Property(e => e.LatY)
                  .HasColumnName("latyenvio");

            entity.Property(e => e.LongX)
                  .HasColumnName("longxenvio");

            entity.Property(e => e.NombreCliente)
                  .HasColumnName("nombrecliente");

            entity.Property(e => e.ApellidoCliente)
                  .HasColumnName("apellidocliente");

            entity.Property(e => e.EmailCliente)
                  .HasColumnName("emailcliente");

            entity.Property(e => e.DNI)
                  .HasColumnName("dni");

            entity.HasOne(e => e.IdUsuarioNavigation)
      .WithMany(u => u.Carritos)
      .HasForeignKey(e => e.IdUsuario);

        });

        // ===============================
        // CARRITO DETALLE
        // ===============================
        modelBuilder.Entity<CarritoDetalle>(entity =>
        {
            entity.ToTable("carritodetalle");

            entity.HasKey(e => e.IdCarritoDetalle);

            entity.Property(e => e.IdCarritoDetalle)
                  .HasColumnName("idcarritodetalle");

            entity.Property(e => e.IdCarrito)
                  .HasColumnName("idcarrito");

            entity.Property(e => e.IdItem)
                  .HasColumnName("iditem");

            entity.Property(e => e.Cantidad)
                  .HasColumnName("cantidad");

            entity.Property(e => e.PrecioUnitario)
                  .HasColumnName("preciounitario")
                  .HasColumnType("numeric(10,2)");

            entity.Property(e => e.Subtotal)
                  .HasColumnName("subtotal")
                  .HasColumnType("numeric(10,2)")
                  .ValueGeneratedOnAddOrUpdate();

            entity.HasOne(e => e.IdCarritoNavigation)
                  .WithMany(c => c.CarritoDetalles)
                  .HasForeignKey(e => e.IdCarrito);

            entity.HasOne(e => e.IdItemNavigation)
                  .WithMany()
                  .HasForeignKey(e => e.IdItem);
        });

        // ===============================
        // DIRECCIONES
        // ===============================
        modelBuilder.Entity<Direcciones>(entity =>
        {
            entity.ToTable("direcciones");

            entity.HasKey(e => e.IdDireccion);

            entity.Property(e => e.IdDireccion).HasColumnName("iddireccion");
            entity.Property(e => e.IdUsuario).HasColumnName("idusuario");

            entity.Property(e => e.Direccion)
                  .HasColumnName("direccion");

            entity.Property(e => e.TituloDireccion)
                  .HasColumnName("titulodireccion");

            entity.Property(e => e.Referencia).HasColumnName("referencia");
            entity.Property(e => e.Telefono1).HasColumnName("telefono1");
            entity.Property(e => e.Telefono2).HasColumnName("telefono2");
            entity.Property(e => e.LatY).HasColumnName("laty");
            entity.Property(e => e.LongX).HasColumnName("longx");
            entity.HasOne(d => d.Usuario)
          .WithMany(u => u.Direcciones)
          .HasForeignKey(d => d.IdUsuario)
          .HasConstraintName("direcciones_idusuario_fkey");
        });

        // ===============================
        // WISHLIST
        // ===============================
        modelBuilder.Entity<WishListItem>(entity =>
        {
            entity.ToTable("wishlistitem");
            entity.HasKey(e => e.IdWishListItem);

            entity.Property(e => e.IdWishListItem).HasColumnName("idwishlistitem");
            entity.Property(e => e.IdUsuario).HasColumnName("idusuario");
            entity.Property(e => e.IdItem).HasColumnName("iditem");
            entity.Property(e => e.FechaAgregado).HasColumnName("fechaagregado");

            entity.HasOne(e => e.Item)
                  .WithMany()
                  .HasForeignKey(e => e.IdItem);

            entity.HasOne(e => e.Usuario)
                  .WithMany()
                  .HasForeignKey(e => e.IdUsuario);
        });
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
