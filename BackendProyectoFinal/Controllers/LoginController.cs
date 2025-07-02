using BackendProyectoFinal.DTOs;
using BackendProyectoFinal.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly EncryptService _encryptService;
        private ICommonService<UsuarioDTO, UsuarioInsertDTO, UsuarioUpdateDTO> _usuarioService;

        public LoginController(
            [FromKeyedServices("EncryptService")] EncryptService encryptService,
            [FromKeyedServices("UsuarioService")] ICommonService<UsuarioDTO, UsuarioInsertDTO, UsuarioUpdateDTO> usuarioService)
        {
            _encryptService = encryptService;
            _usuarioService = usuarioService;
        }

        // Prueba de logueo con encriptacion
        [HttpGet]
        public async Task<string> IniciarSesion(UsuarioInsertDTO usuarioInsertDTO) {
            var logueoMensaje = "NoLogueo";
            var busqueda = await _usuarioService.GetByField(usuarioInsertDTO.Email);
            if(busqueda != null)
            {
                var contrasenia = _encryptService.EncryptData(usuarioInsertDTO.Password);
                if(busqueda.Email == usuarioInsertDTO.Email && busqueda.Password == contrasenia)
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
