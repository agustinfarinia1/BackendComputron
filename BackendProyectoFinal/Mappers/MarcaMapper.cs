using BackendProyectoFinal.DTOs.MarcaDTO;
using BackendProyectoFinal.Models;

namespace BackendProyectoFinal.Mappers
{
    public static class MarcaMapper
    {
        public static MarcaDTO ConvertMarcaToDTO(Marca marca)
        {
            var marcaDTO = new MarcaDTO()
            {
                Id = marca.MarcaID,
                Nombre = marca.Nombre
            };
            return marcaDTO;
        }
    }
}
