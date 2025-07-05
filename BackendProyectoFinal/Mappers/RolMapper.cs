using BackendProyectoFinal.DTOs.RolDTO;
using BackendProyectoFinal.Models;

namespace BackendProyectoFinal.Utils.Mappers
{
    public static class RolMapper
    {
        public static RolDTO ConvertRolToDTO(Rol rol)
        {
            var RolDTO = new RolDTO()
            {
                Id = rol.RolID,
                Nombre = rol.Nombre
            };
            return RolDTO;
        }
    }
}
