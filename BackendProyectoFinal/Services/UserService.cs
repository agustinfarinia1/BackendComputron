using BackendProyectoFinal.Repositories;
using Microsoft.IdentityModel.Tokens;
using BackendProyectoFinal.Utils.Mappers;
using BackendProyectoFinal.Models;
using BackendProyectoFinal.DTOs.User;

namespace BackendProyectoFinal.Services
{
    public class UserService : ICommonService<UserDTO, UserInsertDTO, UserUpdateDTO>
    {
        private IRepository<User> _repository;
        public List<string> Errors { get; }
        private readonly EncryptService _encryptService;
        public UserService(
            IRepository<User> repository,
            [FromKeyedServices("EncryptService")] EncryptService encryptService)
        {
            _repository = repository;
            _encryptService = encryptService;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<UserDTO>> Get()
        {
            var categorias = await _repository.Get();
            // Convierte los Users A DTO
            return categorias.Select(u =>
                UserMapper.ConvertUserToDTO(u)
            );
        }

        public async Task<UserDTO?> GetById(int id)
        {
            var user = await _repository.GetById(id);
            if (user != null)
            {
                return UserMapper.ConvertUserToDTO(user);
            }
            return null;
        }

        public async Task<UserDTO?> GetByField(string field)
        {
            var user = _repository.Search(u => u.Email == field).FirstOrDefault(); ;
            if (user != null)
            {
                return UserMapper.ConvertUserToDTO(user);
            }
            return null;
        }

        public async Task<UserDTO> Add(UserInsertDTO userInsertDTO)
        {
            var encrypt = _encryptService.EncryptData(userInsertDTO.Password);
            userInsertDTO.Password = encrypt;
            var user = UserMapper.ConvertDTOToUser(userInsertDTO);
            await _repository.Add(user);
            await _repository.Save();

            return UserMapper.ConvertUserToDTO(user);
        }

        public async Task<UserDTO> Update(UserUpdateDTO userUpdateDTO)
        {
            var user = await _repository.GetById(userUpdateDTO.Id);
            if (user != null)
            {
                UserMapper.UpdateUser(user, userUpdateDTO);

                _repository.Update(user);
                await _repository.Save();

                return UserMapper.ConvertUserToDTO(user);
            }
            return null;
        }

        public async Task<UserDTO> Delete(int id)
        {
            var user = await _repository.GetById(id);
            if (user != null)
            {
                user.Eliminated = true;
                var userDTO = UserMapper.ConvertUserToDTO(user);

                _repository.Delete(user);
                await _repository.Save();
                return userDTO;
            }
            return null;
        }

        public bool Validate(UserInsertDTO userDTO)
        {
            if (_repository.Search(u => u.Email == userDTO.Email).Count() > 0)
            {
                Errors.Add("No puede existir un usuario con un email ya existente");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }

        public bool Validate(UserUpdateDTO userDTO)
        {
            if (_repository.Search(
                u => u.Email == userDTO.Email
                && userDTO.Id != u.UserID).Count() > 0)
            {
                Errors.Add("No puede existir un usuario con un email ya existente");
            }
            return Errors.IsNullOrEmpty() == true ? true : false;
        }
    }
}
