using BackendProyectoFinal.DTOs.Role;
using BackendProyectoFinal.Models;

namespace BackendProyectoFinal.Utils.Mappers
{
    public static class RoleMapper
    {
        public static RoleDTO ConvertRoleToDTO(Role role)
        {
            var roleDTO = new RoleDTO()
            {
                Id = role.RoleID,
                Name = role.Name
            };
            return roleDTO;
        }
    }
}
