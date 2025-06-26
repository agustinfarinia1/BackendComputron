using Microsoft.EntityFrameworkCore;

namespace BackendProyectoFinal.Models
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        { }
        public DbSet<CategoriaProducto> CategoriaProductos { get; set; }
        /*
        public DbSet<Producto> Productos { get; set; }
        public DbSet<TipoFactura> TipoFacturas { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        */
    }
}
