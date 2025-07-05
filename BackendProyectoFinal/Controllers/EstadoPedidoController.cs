
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using BackendProyectoFinal.Services;
using BackendProyectoFinal.DTOs.EstadoPedidoDTO;

namespace BackendProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoPedidoController : ControllerBase
    {
        private ICommonService<EstadoPedidoDTO, EstadoPedidoInsertDTO, EstadoPedidoUpdateDTO> _estadoPedidoService;
        private IValidator<EstadoPedidoInsertDTO> _estadoPedidoInsertValidator;
        private IValidator<EstadoPedidoUpdateDTO> _estadoPedidoUpdateValidator;

        public EstadoPedidoController(
            [FromKeyedServices("EstadoPedidoService")] ICommonService<EstadoPedidoDTO, EstadoPedidoInsertDTO, EstadoPedidoUpdateDTO> estadoPedidoService,
            IValidator<EstadoPedidoInsertDTO> estadoPedidoInsertValidator,
            IValidator<EstadoPedidoUpdateDTO> estadoPedidoUpdateValidator)
        {
            _estadoPedidoService = estadoPedidoService;
            _estadoPedidoInsertValidator = estadoPedidoInsertValidator;
            _estadoPedidoUpdateValidator = estadoPedidoUpdateValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<EstadoPedidoDTO>> Get()
            => await _estadoPedidoService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<EstadoPedidoDTO>> GetById(int id)
        {
            var rol = await _estadoPedidoService.GetById(id);
            return rol == null ? NotFound() : Ok(rol);
        }

        [HttpPost]
        public async Task<ActionResult<EstadoPedidoDTO>> Add(EstadoPedidoInsertDTO estadoPedidoInsertDTO)
        {
            // REALIZA VALIDACION DE INSERT DTO (QUE NOMBRE NO ESTE VACIO)
            var validationResult = await _estadoPedidoInsertValidator.ValidateAsync(estadoPedidoInsertDTO);
            // SI LA VALIDACION ES ERRONEA, SE PARA
            // Y DEVUELVE LOS ERRRORES LISTADOS
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_estadoPedidoService.Validate(estadoPedidoInsertDTO))
            {
                return BadRequest(_estadoPedidoService.Errors);
            }
            var estadoPedidoDTO = await _estadoPedidoService.Add(estadoPedidoInsertDTO);
            // CreatedAtAction otorga el metodo para la consulta del objeto generado
            // el campo por el cual se puede buscar y el objeto generado en esta ejecucion
            return CreatedAtAction(nameof(GetById), new { id = estadoPedidoDTO.Id }, estadoPedidoDTO);
        }

        [HttpPut]
        public async Task<ActionResult<EstadoPedidoDTO>> Update(EstadoPedidoUpdateDTO estadoPedidoUpdateDTO)
        {
            var validationResult = await _estadoPedidoUpdateValidator.ValidateAsync(estadoPedidoUpdateDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_estadoPedidoService.Validate(estadoPedidoUpdateDTO))
            {
                return BadRequest(_estadoPedidoService.Errors);
            }
            var estadoPedidoDTO = await _estadoPedidoService.Update(estadoPedidoUpdateDTO);

            return estadoPedidoDTO == null ? NotFound() : Ok(estadoPedidoDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var estadoPedidoDTO = await _estadoPedidoService.Delete(id);

            return estadoPedidoDTO == null ? NotFound() : Ok(estadoPedidoDTO);
        }
    }
}
