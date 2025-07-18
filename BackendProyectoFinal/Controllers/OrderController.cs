using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using BackendProyectoFinal.DTOs.Order;
using BackendProyectoFinal.Services;

namespace BackendProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;
        private IValidator<OrderInsertDTO> _orderInsertValidator;
        private IValidator<OrderUpdateDTO> _orderUpdateValidator;

        public OrderController(
            [FromKeyedServices("OrderService")] IOrderService pedidoService,
            IValidator<OrderInsertDTO> orderInsertValidator,
            IValidator<OrderUpdateDTO> orderUpdateValidator)
        {
            _orderService = pedidoService;
            _orderInsertValidator = orderInsertValidator;
            _orderUpdateValidator = orderUpdateValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<OrderDTO>> Get()
            => await _orderService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetById(int id)
        {
            var order = await _orderService.GetById(id);
            return order == null ? NotFound() : Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDTO>> Add(OrderInsertDTO orderInsertDTO)
        {
            // REALIZA VALIDACION DE INSERT DTO
            var validationResult = await _orderInsertValidator.ValidateAsync(orderInsertDTO);
            // SI LA VALIDACION ES ERRONEA, SE PARA
            // Y DEVUELVE LOS ERRRORES LISTADOS
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_orderService.Validate(orderInsertDTO))
            {
                return BadRequest(_orderService.Errors);
            }
            var orderDTO = await _orderService.Add(orderInsertDTO);
            // CreatedAtAction otorga el metodo para la consulta del objeto generado
            // el campo por el cual se puede buscar y el objeto generado en esta ejecucion
            return CreatedAtAction(nameof(GetById), new { id = orderDTO.Id }, orderDTO);
        }

        [HttpPut]
        public async Task<ActionResult<OrderDTO>> Update(OrderUpdateDTO orderUpdateDTO)
        {
            var validationResult = await _orderUpdateValidator.ValidateAsync(orderUpdateDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_orderService.Validate(orderUpdateDTO))
            {
                return BadRequest(_orderService.Errors);
            }
            var orderDTO = await _orderService.Update(orderUpdateDTO);

            return orderDTO == null ? NotFound() : Ok(orderDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var orderDTO = await _orderService.Delete(id);

            return orderDTO == null ? NotFound() : Ok(orderDTO);
        }
    }
}
