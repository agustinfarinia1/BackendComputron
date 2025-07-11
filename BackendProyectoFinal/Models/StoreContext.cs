using Microsoft.EntityFrameworkCore;

namespace BackendProyectoFinal.Models
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ItemCart> ItemsCarts { get; set; }
        public DbSet<ItemOrder> ItemsOrders { get; set; }
        /*
        public DbSet<TipoFactura> TipoFacturas { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        */
    }
}
