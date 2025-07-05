using BackendProyectoFinal.DTOs.RolDTO;
using BackendProyectoFinal.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BackendProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private ICommonService<RolDTO,RolInsertDTO, RolUpdateDTO> _rolService;
        private IValidator<RolInsertDTO> _rolInsertValidator;
        private IValidator<RolUpdateDTO> _rolUpdateValidator;

        public RolController(
            [FromKeyedServices("RolService")] ICommonService<RolDTO, RolInsertDTO, RolUpdateDTO> rolService,
            IValidator<RolInsertDTO> rolInsertValidator,
            IValidator<RolUpdateDTO> rolUpdateValidator) 
        {
            _rolService = rolService;
            _rolInsertValidator = rolInsertValidator;
            _rolUpdateValidator = rolUpdateValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<RolDTO>> Get() 
            => await _rolService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<RolDTO>> GetById(int id)
        {
            var rol = await _rolService.GetById(id);
            return rol == null ? NotFound() : Ok(rol);
        }

        [HttpPost]
        public async Task<ActionResult<RolDTO>> Add(RolInsertDTO rolInsertDTO)
        {
            // REALIZA VALIDACION DE INSERT DTO (QUE NOMBRE NO ESTE VACIO)
            var validationResult = await _rolInsertValidator.ValidateAsync(rolInsertDTO);
            // SI LA VALIDACION ES ERRONEA, SE PARA
            // Y DEVUELVE LOS ERRRORES LISTADOS
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_rolService.Validate(rolInsertDTO))
            {
                return BadRequest(_rolService.Errors);
            }
            var rolDTO = await _rolService.Add(rolInsertDTO);
            // CreatedAtAction otorga el metodo para la consulta del objeto generado
            // el campo por el cual se puede buscar y el objeto generado en esta ejecucion
            return CreatedAtAction(nameof(GetById), new { id = rolDTO.Id }, rolDTO);
        }

        [HttpPut]
        public async Task<ActionResult<RolDTO>> Update(RolUpdateDTO rolUpdateDTO)
        {
            var validationResult = await _rolUpdateValidator.ValidateAsync(rolUpdateDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_rolService.Validate(rolUpdateDTO))
            {
                return BadRequest(_rolService.Errors);
            }
            var rolDTO = await _rolService.Update(rolUpdateDTO);

            return rolDTO == null ? NotFound() : Ok(rolDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var rolDTO = await _rolService.Delete(id);

            return rolDTO == null ? NotFound() : Ok(rolDTO);
        }
    }
}
