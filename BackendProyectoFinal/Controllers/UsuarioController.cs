using BackendProyectoFinal.DTOs;
using BackendProyectoFinal.Services;
using BackendProyectoFinal.Utils;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BackendProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private ICommonService<UsuarioDTO, UsuarioInsertDTO, UsuarioUpdateDTO> _usuarioService;
        private IValidator<UsuarioInsertDTO> _usuarioInsertValidator;
        private IValidator<UsuarioUpdateDTO> _usuarioUpdateValidator;

        public UsuarioController(
            [FromKeyedServices("UsuarioService")] ICommonService<UsuarioDTO, UsuarioInsertDTO, UsuarioUpdateDTO> usuarioService,
            IValidator<UsuarioInsertDTO> usuarioInsertValidator,
            IValidator<UsuarioUpdateDTO> usuarioUpdateValidator)
        {
            _usuarioService = usuarioService;
            _usuarioInsertValidator = usuarioInsertValidator;
            _usuarioUpdateValidator = usuarioUpdateValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<UsuarioDTO>> Get()
            => await _usuarioService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetById(int id)
        {
            var usuario = await _usuarioService.GetById(id);
            return usuario == null ? NotFound() : Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> Add(UsuarioInsertDTO usuarioInsertDTO)
        {
            Console.WriteLine("Usuario " +usuarioInsertDTO);
            // REALIZA VALIDACION DE INSERT DTO
            var validationResult = await _usuarioInsertValidator.ValidateAsync(usuarioInsertDTO);
            // SI LA VALIDACION ES ERRONEA, SE PARA
            // Y DEVUELVE LOS ERRRORES LISTADOS
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_usuarioService.Validate(usuarioInsertDTO))
            {
                return BadRequest(_usuarioService.Errors);
            }
            var usuarioDTO = await _usuarioService.Add(usuarioInsertDTO);
            // CreatedAtAction otorga el metodo para la consulta del objeto generado
            // el campo por el cual se puede buscar y el objeto generado en esta ejecucion
            return CreatedAtAction(nameof(GetById), new { id = usuarioDTO.Id }, usuarioDTO);
        }

        [HttpPut]
        public async Task<ActionResult<UsuarioDTO>> Update(UsuarioUpdateDTO usuarioUpdateDTO)
        {
            var validationResult = await _usuarioUpdateValidator.ValidateAsync(usuarioUpdateDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_usuarioService.Validate(usuarioUpdateDTO))
            {
                return BadRequest(_usuarioService.Errors);
            }
            var usuarioDTO = await _usuarioService.Update(usuarioUpdateDTO);

            return usuarioDTO == null ? NotFound() : Ok(usuarioDTO);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var usuarioDTO = await _usuarioService.Delete(id);

            return usuarioDTO == null ? NotFound() : Ok(usuarioDTO);
        }
    }
}
