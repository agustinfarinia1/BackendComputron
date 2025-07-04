using BackendProyectoFinal.DTOs;
using BackendProyectoFinal.Models;

namespace BackendProyectoFinal.Utils.Mappers
{
    public static class UsuarioMapper
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

        public static void ActualizarUsuario(Usuario usuario, UsuarioUpdateDTO usuarioDTO)
        {
            if (!string.IsNullOrWhiteSpace(usuarioDTO.Nombre))
                usuario.Nombre = usuarioDTO.Nombre;

            if (!string.IsNullOrWhiteSpace(usuarioDTO.Password))
                usuario.Password = usuarioDTO.Password;

            if (!string.IsNullOrWhiteSpace(usuarioDTO.Apellido))
                usuario.Apellido = usuarioDTO.Apellido;

            if (!string.IsNullOrWhiteSpace(usuarioDTO.Email))
                usuario.Email = usuarioDTO.Email;

            if (usuarioDTO.FechaNacimiento > DateOnly.MinValue)
                usuario.FechaNacimiento = usuarioDTO.FechaNacimiento;

            if (usuarioDTO.RolID > 0)
                usuario.RolID = usuarioDTO.RolID;

            if (usuarioDTO.DomicilioID > 0)
                usuario.DomicilioID = usuarioDTO.DomicilioID;

            if (usuarioDTO.Eliminado.HasValue)
            {
                usuario.Eliminado = (bool)usuarioDTO.Eliminado;
            }
        }
    }
}
