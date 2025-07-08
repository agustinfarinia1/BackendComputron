using BackendProyectoFinal.DTOs.EstadoPedidoDTO;
using BackendProyectoFinal.DTOs.PedidoDTO;
using BackendProyectoFinal.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BackendProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private ICommonService<PedidoDTO, PedidoInsertDTO, PedidoUpdateDTO> _pedidoService;
        private IValidator<PedidoInsertDTO> _pedidoInsertValidator;
        private IValidator<PedidoUpdateDTO> _pedidoUpdateValidator;

        public PedidoController(
            [FromKeyedServices("PedidoService")] ICommonService<PedidoDTO, PedidoInsertDTO, PedidoUpdateDTO> pedidoService,
            IValidator<PedidoInsertDTO> pedidoInsertValidator,
            IValidator<PedidoUpdateDTO> pedidoUpdateValidator)
        {
            _pedidoService = pedidoService;
            _pedidoInsertValidator = pedidoInsertValidator;
            _pedidoUpdateValidator = pedidoUpdateValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<PedidoDTO>> Get()
            => await _pedidoService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoDTO>> GetById(int id)
        {
            var rol = await _pedidoService.GetById(id);
            return rol == null ? NotFound() : Ok(rol);
        }

        [HttpPost]
        public async Task<ActionResult<PedidoDTO>> Add(PedidoInsertDTO pedidoInsertDTO)
        {
            // REALIZA VALIDACION DE INSERT DTO
            var validationResult = await _pedidoInsertValidator.ValidateAsync(pedidoInsertDTO);
            // SI LA VALIDACION ES ERRONEA, SE PARA
            // Y DEVUELVE LOS ERRRORES LISTADOS
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_pedidoService.Validate(pedidoInsertDTO))
            {
                return BadRequest(_pedidoService.Errors);
            }
            var pedidoDTO = await _pedidoService.Add(pedidoInsertDTO);
            // CreatedAtAction otorga el metodo para la consulta del objeto generado
            // el campo por el cual se puede buscar y el objeto generado en esta ejecucion
            return CreatedAtAction(nameof(GetById), new { id = pedidoDTO.Id }, pedidoDTO);
        }

        [HttpPut]
        public async Task<ActionResult<PedidoDTO>> Update(PedidoUpdateDTO pedidoUpdateDTO)
        {
            var validationResult = await _pedidoUpdateValidator.ValidateAsync(pedidoUpdateDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_pedidoService.Validate(pedidoUpdateDTO))
            {
                return BadRequest(_pedidoService.Errors);
            }
            var pedidoDTO = await _pedidoService.Update(pedidoUpdateDTO);

            return pedidoDTO == null ? NotFound() : Ok(pedidoDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var pedidoDTO = await _pedidoService.Delete(id);

            return pedidoDTO == null ? NotFound() : Ok(pedidoDTO);
        }
    }
}
