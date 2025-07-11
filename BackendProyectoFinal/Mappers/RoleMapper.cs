using BackendProyectoFinal.DTOs.RoleDTO;
using BackendProyectoFinal.Models;

namespace BackendProyectoFinal.Utils.Mappers
{
    public static class RoleMapper
    {
        public static RoleDTO ConvertRoleToDTO(Role role)
        {
            var roleDTO = new RoleDTO()
            {
                Id = role.RolID,
                Name = role.Name
            };
            return roleDTO;
        }
    }
}
