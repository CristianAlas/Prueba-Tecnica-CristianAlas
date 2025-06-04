using Microsoft.EntityFrameworkCore;

namespace Productos2019App.Models
{
    /*"Aquí se configura el Entity Framework con las entidades Categoria, Producto y Venta. 
     * También defino las relaciones y las claves primarias."*/
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Venta> Venta { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Asignación de nombres de tablas correctos
            modelBuilder.Entity<Categoria>().ToTable("Categoria");
            modelBuilder.Entity<Producto>().ToTable("Producto");
            modelBuilder.Entity<Venta>().ToTable("Venta");

            // Asignación de claves primarias
            modelBuilder.Entity<Categoria>().HasKey(c => c.CodigoCategoria);
            modelBuilder.Entity<Producto>().HasKey(p => p.CodigoProducto);
            modelBuilder.Entity<Venta>().HasKey(v => v.CodigoVenta);

            // Relaciones con claves foráneas reales
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Productos)
                .HasForeignKey(p => p.CodigoCategoria);

            modelBuilder.Entity<Venta>()
                .HasOne(v => v.Producto)
                .WithMany(p => p.Venta)
                .HasForeignKey(v => v.CodigoProducto);
        }


    }
}
