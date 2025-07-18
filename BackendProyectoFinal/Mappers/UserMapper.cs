using BackendProyectoFinal.Models;
using BackendProyectoFinal.DTOs.User;

namespace BackendProyectoFinal.Utils.Mappers
{
    public static class UserMapper
    {
        public static User ConvertDTOToUser(UserInsertDTO userDTO)
        {
            // Al crearse un Usuario, no puede estar eliminado
            var user = new User()
            {
                FirstName = userDTO.FirstName,
                Password = userDTO.Password,
                SurName = userDTO.SurName,
                Email = userDTO.Email,
                DateOfBirth = userDTO.DateOfBirth,
                RoleID = userDTO.RoleId,
                AddressID = userDTO.AddressId,
                Eliminated = false
            };
            return user;
        }

        public static UserDTO ConvertUserToDTO(User user)
        {
            var userDTO = new UserDTO()
            {
                Id = user.UserID,
                FirstName = user.FirstName,
                Password = user.Password,
                SurName = user.SurName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                RoleId = user.RoleID,
                AddressId = user.AddressID,
                Eliminated = user.Eliminated
            };
            return userDTO;
        }

        public static void UpdateUser(User user, UserUpdateDTO userDTO)
        {
            if (!string.IsNullOrWhiteSpace(userDTO.FirstName))
                user.FirstName = userDTO.FirstName;

            if (!string.IsNullOrWhiteSpace(userDTO.Password))
                user.Password = userDTO.Password;

            if (!string.IsNullOrWhiteSpace(userDTO.SurName))
                user.SurName = userDTO.SurName;

            if (!string.IsNullOrWhiteSpace(userDTO.Email))
                user.Email = userDTO.Email;

            if (userDTO.DateOfBirth > DateOnly.MinValue)
                user.DateOfBirth = userDTO.DateOfBirth;

            if (userDTO.RoleId > 0)
                user.RoleID = userDTO.RoleId;

            if (userDTO.AddressId > 0)
                user.AddressID = userDTO.AddressId;

            if (userDTO.Eliminated.HasValue)
            {
                user.Eliminated = (bool)userDTO.Eliminated;
            }
        }
    }
}
