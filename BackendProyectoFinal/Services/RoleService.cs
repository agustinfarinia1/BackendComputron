using BackendProyectoFinal.Repositories;
using BackendProyectoFinal.Utils.Mappers;
using Microsoft.IdentityModel.Tokens;
using BackendProyectoFinal.DTOs.Role;
using BackendProyectoFinal.Models;

namespace BackendProyectoFinal.Services
{
    public class RoleService : ICommonService<RoleDTO, RoleInsertDTO, RoleUpdateDTO>
    {
        private IRepository<Role> _repository;
        public List<string> Errors { get; }
        public RoleService(IRepository<Role> repository)
        {
            _repository = repository;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<RoleDTO>> Get() 
        {
            var roles = await _repository.Get();
            // Convierte los Roles A DTO
            return roles.Select(role => 
            RoleMapper.ConvertRoleToDTO(role)
            );
        }

        public async Task<RoleDTO?> GetById(int id)
        {
            var role = await _repository.GetById(id);
            if (role != null)
            {
                return RoleMapper.ConvertRoleToDTO(role);
            }
            return null;
        }

        public async Task<RoleDTO?> GetByField(string field)
        {
            var role = _repository.Search(r => r.RoleID == int.Parse(field)).FirstOrDefault();
            if (role != null)
            {
                return RoleMapper.ConvertRoleToDTO(role);
            }
            return null;
        }

        public async Task<RoleDTO> Add(RoleInsertDTO RoleInsertDTO)
        {
            var role = new Role()
            {
                Name = RoleInsertDTO.Name
            };
            await _repository.Add(role);
            await _repository.Save();

            return RoleMapper.ConvertRoleToDTO(role) ;
        }

        public async Task<RoleDTO> Update(RoleUpdateDTO RoleUpdateDTO)
        {
            var role = await _repository.GetById(RoleUpdateDTO.Id);
            if (role != null)
            {
                role.Name = RoleUpdateDTO.Name;

                _repository.Update(role);
                await _repository.Save();

                return RoleMapper.ConvertRoleToDTO(role);
            }
            return null;
        }

        public async Task<RoleDTO> Delete(int id)
        {
            var role = await _repository.GetById(id);
            if (role != null)
            {
                var RoleDTO = RoleMapper.ConvertRoleToDTO(role);

                _repository.Delete(role);
                await _repository.Save();
                return RoleDTO;
            }
            return null;
        }

        public bool Validate(RoleInsertDTO RoleDTO)
        {
            if (_repository.Search(r => r.Name.ToUpper() == RoleDTO.Name.ToUpper()).Count() > 0)
            {
                Errors.Add("No puede existir un rol con un nombre ya existente");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }

        public bool Validate(RoleUpdateDTO RoleDTO)
        {
            if (_repository.Search(
                r => r.Name.ToUpper() == RoleDTO.Name.ToUpper()
                && RoleDTO.Id != r.RoleID).Count() > 0)
            {
                Errors.Add("No puede existir un rol con un nombre ya existente");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }
    }
}
