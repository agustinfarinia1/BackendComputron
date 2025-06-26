using BackendProyectoFinal.DTOs;
using BackendProyectoFinal.Models;

namespace BackendProyectoFinal.Mappers
{
    public static class CategoriaProductoMapper
    {
        public static CategoriaProductoDTO ConvertCategoriaToDTO(CategoriaProducto categoria)
        {
            var beerDTO = new CategoriaProductoDTO()
            {
                Id = categoria.CategoriaProductoID,
                Nombre = categoria.Nombre
            };
            return beerDTO;
        }
    }
}
