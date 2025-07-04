using BackendProyectoFinal.DTOs;
using BackendProyectoFinal.Models;
using BackendProyectoFinal.Repositories;
using BackendProyectoFinal.Utils.Mappers;
using Microsoft.IdentityModel.Tokens;

namespace BackendProyectoFinal.Services
{
    public class UsuarioService : ICommonService<UsuarioDTO, UsuarioInsertDTO, UsuarioUpdateDTO>
    {
        private IRepository<Usuario> _repository;
        public List<string> Errors { get; }
        private readonly EncryptService _encryptService;
        public UsuarioService(
            IRepository<Usuario> repository,
            [FromKeyedServices("EncryptService")] EncryptService encryptService)
        {
            _repository = repository;
            _encryptService = encryptService;
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

        public async Task<UsuarioDTO> GetByField(string field)
        {
            var usuario = _repository.Search(u => u.Email == field).FirstOrDefault(); ;
            if (usuario != null)
            {
                return UsuarioMapper.ConvertUsuarioToDTO(usuario);
            }
            return null;
        }

        public async Task<UsuarioDTO> Add(UsuarioInsertDTO usuarioInsertDTO)
        {
            var claveEncriptada = _encryptService.EncryptData(usuarioInsertDTO.Password);
            usuarioInsertDTO.Password = claveEncriptada;
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
                UsuarioMapper.ActualizarUsuario(usuario,usuarioUpdateDTO);

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
                usuario.Eliminado = true;
                var usuarioDTO = UsuarioMapper.ConvertUsuarioToDTO(usuario);

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
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }

        public bool Validate(UsuarioUpdateDTO usuarioDTO)
        {
            if (_repository.Search(
                u => u.Email == usuarioDTO.Email
                && usuarioDTO.Id != u.UsuarioID).Count() > 0)
            {
                Errors.Add("No puede existir un usuario con un email ya existente");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }
    }
}
