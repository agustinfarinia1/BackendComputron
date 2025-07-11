using BackendProyectoFinal.DTOs.UserDTO;
using BackendProyectoFinal.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BackendProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private ICommonService<UserDTO, UserInsertDTO, UserUpdateDTO> _userService;
        private IValidator<UserInsertDTO> _userInsertValidator;
        private IValidator<UserUpdateDTO> _userUpdateValidator;

        public UsuarioController(
            [FromKeyedServices("UserService")] ICommonService<UserDTO, UserInsertDTO, UserUpdateDTO> usuarioService,
            IValidator<UserInsertDTO> userInsertValidator,
            IValidator<UserUpdateDTO> userUpdateValidator)
        {
            _userService = usuarioService;
            _userInsertValidator = userInsertValidator;
            _userUpdateValidator = userUpdateValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<UserDTO>> Get()
            => await _userService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetById(int id)
        {
            var user = await _userService.GetById(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> Add(UserInsertDTO userInsertDTO)
        {
            // REALIZA VALIDACION DE INSERT DTO
            var validationResult = await _userInsertValidator.ValidateAsync(userInsertDTO);
            // SI LA VALIDACION ES ERRONEA, SE PARA
            // Y DEVUELVE LOS ERRRORES LISTADOS
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_userService.Validate(userInsertDTO))
            {
                return BadRequest(_userService.Errors);
            }
            var userDTO = await _userService.Add(userInsertDTO);
            // CreatedAtAction otorga el metodo para la consulta del objeto generado
            // el campo por el cual se puede buscar y el objeto generado en esta ejecucion
            return CreatedAtAction(nameof(GetById), new { id = userDTO.Id }, userDTO);
        }

        [HttpPut]
        public async Task<ActionResult<UserDTO>> Update(UserUpdateDTO userUpdateDTO)
        {
            var validationResult = await _userUpdateValidator.ValidateAsync(userUpdateDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_userService.Validate(userUpdateDTO))
            {
                return BadRequest(_userService.Errors);
            }
            var userDTO = await _userService.Update(userUpdateDTO);

            return userDTO == null ? NotFound() : Ok(userDTO);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var userDTO = await _userService.Delete(id);

            return userDTO == null ? NotFound() : Ok(userDTO);
        }
    }
}
