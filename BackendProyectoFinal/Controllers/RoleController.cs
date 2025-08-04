using BackendProyectoFinal.DTOs.User.Role;
using BackendProyectoFinal.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BackendProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private ICommonService<RoleDTO,RoleInsertDTO, RoleUpdateDTO> _roleService;
        private IValidator<RoleInsertDTO> _roleInsertValidator;
        private IValidator<RoleUpdateDTO> _roleUpdateValidator;

        public RoleController(
            [FromKeyedServices("RoleService")] ICommonService<RoleDTO, RoleInsertDTO, RoleUpdateDTO> roleService,
            IValidator<RoleInsertDTO> roleInsertValidator,
            IValidator<RoleUpdateDTO> roleUpdateValidator) 
        {
            _roleService = roleService;
            _roleInsertValidator = roleInsertValidator;
            _roleUpdateValidator = roleUpdateValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<RoleDTO>> Get() 
            => await _roleService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDTO>> GetById(int id)
        {
            var role = await _roleService.GetById(id);
            return role == null ? NotFound() : Ok(role);
        }

        [HttpPost]
        public async Task<ActionResult<RoleDTO>> Add(RoleInsertDTO RoleInsertDTO)
        {
            // REALIZA VALIDACION DE INSERT DTO (QUE NOMBRE NO ESTE VACIO)
            var validationResult = await _roleInsertValidator.ValidateAsync(RoleInsertDTO);
            // SI LA VALIDACION ES ERRONEA, SE PARA
            // Y DEVUELVE LOS ERRRORES LISTADOS
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_roleService.Validate(RoleInsertDTO))
            {
                return BadRequest(_roleService.Errors);
            }
            var RoleDTO = await _roleService.Add(RoleInsertDTO);
            // CreatedAtAction otorga el metodo para la consulta del objeto generado
            // el campo por el cual se puede buscar y el objeto generado en esta ejecucion
            return CreatedAtAction(nameof(GetById), new { id = RoleDTO.Id }, RoleDTO);
        }

        [HttpPut]
        public async Task<ActionResult<RoleDTO>> Update(RoleUpdateDTO RoleUpdateDTO)
        {
            var validationResult = await _roleUpdateValidator.ValidateAsync(RoleUpdateDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_roleService.Validate(RoleUpdateDTO))
            {
                return BadRequest(_roleService.Errors);
            }
            var RoleDTO = await _roleService.Update(RoleUpdateDTO);

            return RoleDTO == null ? NotFound() : Ok(RoleDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var RoleDTO = await _roleService.Delete(id);

            return RoleDTO == null ? NotFound() : Ok(RoleDTO);
        }
    }
}
