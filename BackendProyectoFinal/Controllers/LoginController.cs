using BackendProyectoFinal.DTOs.UserDTO;
using BackendProyectoFinal.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly EncryptService _encryptService;
        private ICommonService<UserDTO, UserInsertDTO, UserUpdateDTO> _userService;

        public LoginController(
            [FromKeyedServices("EncryptService")] EncryptService encryptService,
            [FromKeyedServices("UsuarioService")] ICommonService<UserDTO, UserInsertDTO, UserUpdateDTO> usuarioService)
        {
            _encryptService = encryptService;
            _userService = usuarioService;
        }

        // Prueba de logueo con encriptacion
        [HttpGet]
        public async Task<string> IniciarSesion(UserInsertDTO userInsertDTO) {
            var logueoMensaje = "NoLogueo";
            var busqueda = await _userService.GetByField(userInsertDTO.Email);
            if(busqueda != null)
            {
                var contrasenia = _encryptService.EncryptData(userInsertDTO.Password);
                if(busqueda.Email == userInsertDTO.Email && busqueda.Password == contrasenia)
                {
                    logueoMensaje = "LogueoExitoso";
                }
            }
            return logueoMensaje;
        }

        [HttpPost]
        public void CrearCuenta()
        {
        }

        [HttpPatch("{id}")]
        public void BorrarCuenta()
        {
        }
    }
}
