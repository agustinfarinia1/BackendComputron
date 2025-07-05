using BackendProyectoFinal.DTOs.CategoriaProductoDTO;
using BackendProyectoFinal.Models;

namespace BackendProyectoFinal.Utils.Mappers
{
    public static class CategoriaProductoMapper
    {
        public static CategoriaProductoDTO ConvertCategoriaProductoToDTO(CategoriaProducto categoria)
        {
            var categoriaDTO = new CategoriaProductoDTO()
            {
                Id = categoria.CategoriaProductoID,
                Nombre = categoria.Nombre
            };
            return categoriaDTO;
        }
    }
}
