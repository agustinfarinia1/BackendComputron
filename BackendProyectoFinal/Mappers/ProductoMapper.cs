using BackendProyectoFinal.DTOs.ProductoDTO;
using BackendProyectoFinal.Models;

namespace BackendProyectoFinal.Mappers
{
    public static class ProductoMapper
    {
        public static Producto ConvertDTOToProducto(ProductoInsertDTO productoDTO)
        {
            // Al crearse un Producto, no puede estar eliminado
            var producto = new Producto()
            {
                Titulo = productoDTO.Titulo,
                CodigoML = productoDTO.CodigoML,
                MarcaID = productoDTO.MarcaId,
                Precio = productoDTO.Precio,
                Cantidad = productoDTO.Cantidad,
                Imagen = productoDTO.Imagen,
                FechaCreacion = productoDTO.FechaCreacion,
                CategoriaProductoID = productoDTO.CategoriaProductoID,
                Eliminado = false
            };
            return producto;
        }

        public static ProductoDTO ConvertProductoToDTO(Producto producto)
        {
            var productoDTO = new ProductoDTO()
            {
                Id = producto.ProductoID,
                Titulo = producto.Titulo,
                CodigoML = producto.CodigoML,
                MarcaId = producto.MarcaID,
                Precio = producto.Precio,
                Cantidad = producto.Cantidad,
                Imagen = producto.Imagen,
                FechaCreacion = producto.FechaCreacion,
                CategoriaProductoID = producto.CategoriaProductoID,
                Eliminado = producto.Eliminado
            };
            return productoDTO;
        }

        public static void ActualizarProducto(Producto producto, ProductoUpdateDTO productoDTO)
        {
            if (!string.IsNullOrWhiteSpace(productoDTO.Titulo))
                producto.Titulo = productoDTO.Titulo;

            if (!string.IsNullOrWhiteSpace(productoDTO.CodigoML))
                producto.CodigoML = productoDTO.CodigoML;
            
            if (productoDTO.MarcaId > 0)
                producto.MarcaID = productoDTO.MarcaId;

            if (productoDTO.Precio > 0)
                producto.Precio = productoDTO.Precio;

            if (productoDTO.Cantidad > 0)
                producto.Cantidad = productoDTO.Cantidad;

            if (!string.IsNullOrWhiteSpace(productoDTO.Imagen))
                producto.Imagen = productoDTO.Imagen;

            if (productoDTO.FechaCreacion > DateOnly.MinValue)
                producto.FechaCreacion = productoDTO.FechaCreacion;
            
            if (productoDTO.CategoriaProductoID > 0)
                producto.CategoriaProductoID = productoDTO.CategoriaProductoID;

            if (productoDTO.Eliminado.HasValue) { 
                producto.Eliminado = (bool) productoDTO.Eliminado;
            }
        }
    }
}
