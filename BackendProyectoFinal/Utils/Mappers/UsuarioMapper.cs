using BackendProyectoFinal.DTOs;
using BackendProyectoFinal.Models;

namespace BackendProyectoFinal.Utils.Mappers
{
    public class UsuarioMapper
    {
        public static Usuario ConvertDTOToUsuario(UsuarioInsertDTO usuarioDTO)
        {
            // Al crearse un Usuario, no puede estar eliminado
            var usuario = new Usuario()
            {
                Nombre = usuarioDTO.Nombre,
                Password = usuarioDTO.Password,
                Apellido = usuarioDTO.Apellido,
                Email = usuarioDTO.Email,
                FechaNacimiento = usuarioDTO.FechaNacimiento,
                RolID = usuarioDTO.RolID,
                DomicilioID = usuarioDTO.DomicilioID,
                Eliminado = false
            };
            return usuario;
        }

        public static UsuarioDTO ConvertUsuarioToDTO(Usuario usuario)
        {
            var usuarioDTO = new UsuarioDTO()
            {
                Id = usuario.UsuarioID,
                Nombre = usuario.Nombre,
                Password = usuario.Password,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                FechaNacimiento = usuario.FechaNacimiento,
                RolID = usuario.RolID,
                DomicilioID = usuario.DomicilioID,
                Eliminado = usuario.Eliminado
            };
            return usuarioDTO;
        }
    }
}
