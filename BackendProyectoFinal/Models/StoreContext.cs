﻿using Microsoft.EntityFrameworkCore;

namespace BackendProyectoFinal.Models
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        { }
        public DbSet<CategoriaProducto> CategoriaProductos { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Domicilio> Domicilios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Carrito> Carritos { get; set; }
        public DbSet<EstadoPedido> EstadosDePedido { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        /*
        public DbSet<TipoFactura> TipoFacturas { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        */
    }
}
