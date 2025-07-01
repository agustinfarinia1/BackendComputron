using BackendProyectoFinal.DTOs;
using BackendProyectoFinal.Models;
using BackendProyectoFinal.Repositories;
using BackendProyectoFinal.Utils.Mappers;

namespace BackendProyectoFinal.Services
{
    public class UsuarioService : ICommonService<UsuarioDTO, UsuarioInsertDTO, UsuarioUpdateDTO>
    {
        private IRepository<Usuario> _repository;
        public List<string> Errors { get; }
        public UsuarioService(
            IRepository<Usuario> repository)
        {
            _repository = repository;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<UsuarioDTO>> Get()
        {
            var categorias = await _repository.Get();
            // CONVIERTE LOS Usuarios A DTO
            return categorias.Select(u =>
            UsuarioMapper.ConvertUsuarioToDTO(u)
            );
        }

        public async Task<UsuarioDTO> GetById(int id)
        {
            var usuario = await _repository.GetById(id);
            if (usuario != null)
            {
                return UsuarioMapper.ConvertUsuarioToDTO(usuario);
            }
            return null;
        }

        public async Task<UsuarioDTO> Add(UsuarioInsertDTO usuarioInsertDTO)
        {
            var usuario = UsuarioMapper.ConvertDTOToUsuario(usuarioInsertDTO);
            await _repository.Add(usuario);
            await _repository.Save();

            return UsuarioMapper.ConvertUsuarioToDTO(usuario);
        }

        public async Task<UsuarioDTO> Update(UsuarioUpdateDTO usuarioUpdateDTO)
        {
            var usuario = await _repository.GetById(usuarioUpdateDTO.Id);
            if (usuario != null)
            {
                usuario.Nombre = usuarioUpdateDTO.Nombre;
                usuario.Password = usuarioUpdateDTO.Password;
                usuario.Apellido = usuarioUpdateDTO.Apellido;
                usuario.FechaNacimiento = usuarioUpdateDTO.FechaNacimiento;
                usuario.Eliminado = usuarioUpdateDTO.Eliminado;

                _repository.Update(usuario);
                await _repository.Save();

                return UsuarioMapper.ConvertUsuarioToDTO(usuario);
            }
            return null;
        }

        public async Task<UsuarioDTO> Delete(int id)
        {
            var usuario = await _repository.GetById(id);
            if (usuario != null)
            {
                var usuarioDTO = UsuarioMapper.ConvertUsuarioToDTO(usuario);
                usuario.Eliminado = true;

                _repository.Delete(usuario);
                await _repository.Save();
                return usuarioDTO;
            }
            return null;
        }

        public bool Validate(UsuarioInsertDTO usuarioDTO)
        {
            if (_repository.Search(u => u.Email == usuarioDTO.Email).Count() > 0)
            {
                Errors.Add("No puede existir un usuario con un email ya existente");
                return false;
            }
            return true;
        }

        public bool Validate(UsuarioUpdateDTO usuarioDTO)
        {
            if (_repository.Search(
                u => u.Email == usuarioDTO.Email
                && usuarioDTO.Id != u.UsuarioID).Count() > 0)
            {
                Errors.Add("No puede existir un usuario con un email ya existente");
                return false;
            }
            return true;
        }
    }
}
