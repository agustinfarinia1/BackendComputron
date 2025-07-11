
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using BackendProyectoFinal.Services;
using BackendProyectoFinal.DTOs.OrderStatusDTO;

namespace BackendProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusController : ControllerBase
    {
        private IOrderStatusService _orderStatusService;
        private IValidator<OrderStatusInsertDTO> _orderStatusInsertValidator;
        private IValidator<OrderStatusUpdateDTO> _orderStatusUpdateValidator;

        public OrderStatusController(
            [FromKeyedServices("OrderStatusService")] IOrderStatusService estadoPedidoService,
            IValidator<OrderStatusInsertDTO> orderStatusInsertValidator,
            IValidator<OrderStatusUpdateDTO> orderStatusUpdateValidator)
        {
            _orderStatusService = estadoPedidoService;
            _orderStatusInsertValidator = orderStatusInsertValidator;
            _orderStatusUpdateValidator = orderStatusUpdateValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<OrderStatusDTO>> Get()
            => await _orderStatusService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderStatusDTO>> GetById(int id)
        {
            var estadoPedido = await _orderStatusService.GetById(id);
            return estadoPedido == null ? NotFound() : Ok(estadoPedido);
        }

        [HttpGet("first")]
        public async Task<ActionResult<OrderStatusDTO>> GetFirstOrderStatus()
        {
            var firstStatus = await _orderStatusService.GetFirst();
            return firstStatus == null ? NotFound() : Ok(firstStatus);
        }

        [HttpGet("/last")]
        public async Task<ActionResult<OrderStatusDTO>> GetLastOrderStatus()
        {
            var lastStatus = await _orderStatusService.GetLast();
            return lastStatus == null ? NotFound() : Ok(lastStatus);
        }

        [HttpPost]
        public async Task<ActionResult<OrderStatusDTO>> Add(OrderStatusInsertDTO orderStatusInsertDTO)
        {
            // REALIZA VALIDACION DE INSERT DTO
            var validationResult = await _orderStatusInsertValidator.ValidateAsync(orderStatusInsertDTO);
            // SI LA VALIDACION ES ERRONEA, SE PARA
            // Y DEVUELVE LOS ERRRORES LISTADOS
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_orderStatusService.Validate(orderStatusInsertDTO))
            {
                return BadRequest(_orderStatusService.Errors);
            }
            var orderStatusDTO = await _orderStatusService.Add(orderStatusInsertDTO);
            // CreatedAtAction otorga el metodo para la consulta del objeto generado
            // el campo por el cual se puede buscar y el objeto generado en esta ejecucion
            return CreatedAtAction(nameof(GetById), new { id = orderStatusDTO.Id }, orderStatusDTO);
        }

        [HttpPut]
        public async Task<ActionResult<OrderStatusDTO>> Update(OrderStatusUpdateDTO orderStatusUpdateDTO)
        {
            var validationResult = await _orderStatusUpdateValidator.ValidateAsync(orderStatusUpdateDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_orderStatusService.Validate(orderStatusUpdateDTO))
            {
                return BadRequest(_orderStatusService.Errors);
            }
            var orderStatusDTO = await _orderStatusService.Update(orderStatusUpdateDTO);

            return orderStatusDTO == null ? NotFound() : Ok(orderStatusDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var orderStatusDTO = await _orderStatusService.Delete(id);

            return orderStatusDTO == null ? NotFound() : Ok(orderStatusDTO);
        }
    }
}
