using BackendProyectoFinal.DTOs.CarritoDTO;
using BackendProyectoFinal.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BackendProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoController : ControllerBase
    {
        private ICommonService<CarritoDTO, CarritoInsertDTO, CarritoUpdateDTO> _carritoService;
        private IValidator<CarritoInsertDTO> _carritoInsertValidator;
        private IValidator<CarritoUpdateDTO> _carritoUpdateValidator;

        public CarritoController(
            [FromKeyedServices("CarritoService")] ICommonService<CarritoDTO, CarritoInsertDTO, CarritoUpdateDTO> carritoService,
            IValidator<CarritoInsertDTO> carritoInsertValidator,
            IValidator<CarritoUpdateDTO> carritoUpdateValidator)
        {
            _carritoService = carritoService;
            _carritoInsertValidator = carritoInsertValidator;
            _carritoUpdateValidator = carritoUpdateValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<CarritoDTO>> Get()
            => await _carritoService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<CarritoDTO>> GetById(int id)
        {
            var rol = await _carritoService.GetById(id);
            return rol == null ? NotFound() : Ok(rol);
        }

        [HttpPost]
        public async Task<ActionResult<CarritoDTO>> Add(CarritoInsertDTO carritoInsertDTO)
        {
            // REALIZA VALIDACION DE INSERT DTO
            var validationResult = await _carritoInsertValidator.ValidateAsync(carritoInsertDTO);
            // SI LA VALIDACION ES ERRONEA, SE PARA
            // Y DEVUELVE LOS ERRRORES LISTADOS
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_carritoService.Validate(carritoInsertDTO))
            {
                return BadRequest(_carritoService.Errors);
            }
            var carritoDTO = await _carritoService.Add(carritoInsertDTO);
            // CreatedAtAction otorga el metodo para la consulta del objeto generado
            // el campo por el cual se puede buscar y el objeto generado en esta ejecucion
            return CreatedAtAction(nameof(GetById), new { id = carritoDTO.Id }, carritoDTO);
        }

        [HttpPut]
        public async Task<ActionResult<CarritoDTO>> Update(CarritoUpdateDTO carritoUpdateDTO)
        {
            var validationResult = await _carritoUpdateValidator.ValidateAsync(carritoUpdateDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_carritoService.Validate(carritoUpdateDTO))
            {
                return BadRequest(_carritoService.Errors);
            }
            var carritoDTO = await _carritoService.Update(carritoUpdateDTO);

            return carritoDTO == null ? NotFound() : Ok(carritoDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var carritoDTO = await _carritoService.Delete(id);

            return carritoDTO == null ? NotFound() : Ok(carritoDTO);
        }
    }
}
