using BackendProyectoFinal.DTOs;
using BackendProyectoFinal.Models;
using BackendProyectoFinal.Repositories;
using BackendProyectoFinal.Utils.Mappers;
using Microsoft.IdentityModel.Tokens;

namespace BackendProyectoFinal.Services
{
    public class RolService : ICommonService<RolDTO, RolInsertDTO, RolUpdateDTO>
    {
        private IRepository<Rol> _repository;
        public List<string> Errors { get; }
        public RolService(IRepository<Rol> repository)
        {
            _repository = repository;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<RolDTO>> Get() 
        {
            var roles = await _repository.Get();
            // CONVIERTE LOS ROLES A DTO
            return roles.Select(rol => 
            RolMapper.ConvertRolToDTO(rol)
            );
        }

        public async Task<RolDTO> GetById(int id)
        {
            var rol = await _repository.GetById(id);
            if (rol != null)
            {
                return RolMapper.ConvertRolToDTO(rol);
            }
            return null;
        }

        public async Task<RolDTO> GetByField(string field)
        {
            var rol = _repository.Search(r => r.RolID == int.Parse(field)).FirstOrDefault();
            if (rol != null)
            {
                return RolMapper.ConvertRolToDTO(rol);
            }
            return null;
        }

        public async Task<RolDTO> Add(RolInsertDTO rolInsertDTO)
        {
            var rol = new Rol()
            {
                Nombre = rolInsertDTO.Nombre
            };
            await _repository.Add(rol);
            await _repository.Save();

            return RolMapper.ConvertRolToDTO(rol) ;
        }

        public async Task<RolDTO> Update(RolUpdateDTO rolUpdateDTO)
        {
            var rol = await _repository.GetById(rolUpdateDTO.Id);
            if (rol != null)
            {
                rol.Nombre = rolUpdateDTO.Nombre;

                _repository.Update(rol);
                await _repository.Save();

                return RolMapper.ConvertRolToDTO(rol);
            }
            return null;
        }

        public async Task<RolDTO> Delete(int id)
        {
            var rol = await _repository.GetById(id);
            if (rol != null)
            {
                var rolDTO = RolMapper.ConvertRolToDTO(rol);

                _repository.Delete(rol);
                await _repository.Save();
                return rolDTO;
            }
            return null;
        }

        public bool Validate(RolInsertDTO rolDTO)
        {
            if (_repository.Search(r => r.Nombre.ToUpper() == rolDTO.Nombre.ToUpper()).Count() > 0)
            {
                Errors.Add("No puede existir un rol con un nombre ya existente");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }

        public bool Validate(RolUpdateDTO rolDTO)
        {
            if (_repository.Search(
                r => r.Nombre.ToUpper() == rolDTO.Nombre.ToUpper()
                && rolDTO.Id != r.RolID).Count() > 0)
            {
                Errors.Add("No puede existir un rol con un nombre ya existente");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }
    }
}
