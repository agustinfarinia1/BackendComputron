using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using BackendProyectoFinal.Services;
using BackendProyectoFinal.DTOs.ItemOrder;

namespace BackendProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemOrderController : ControllerBase
    {
        private IItemListService<ItemOrderDTO, ItemOrderInsertDTO, ItemOrderUpdateDTO> _itemOrderService;
        private IValidator<ItemOrderInsertDTO> _itemOrderInsertValidator;
        private IValidator<ItemOrderUpdateDTO> _itemOrderUpdateValidator;

        public ItemOrderController(
        [FromKeyedServices("ItemOrderService")] IItemListService<ItemOrderDTO, ItemOrderInsertDTO, ItemOrderUpdateDTO> itemOrderService,
        IValidator<ItemOrderInsertDTO> itemOrderInsertValidator,
        IValidator<ItemOrderUpdateDTO> itemOrderUpdateValidator)
        {
            _itemOrderService = itemOrderService;
            _itemOrderInsertValidator = itemOrderInsertValidator;
            _itemOrderUpdateValidator = itemOrderUpdateValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemOrderDTO>> Get()
            => await _itemOrderService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemOrderDTO>> GetById(int id)
        {
            var itemCart = await _itemOrderService.GetById(id);
            return itemCart == null ? NotFound() : Ok(itemCart);
        }

        [HttpPost]
        public async Task<ActionResult<ItemOrderDTO>> Add(ItemOrderInsertDTO itemOrderInsertDTO)
        {
            // REALIZA VALIDACION DE INSERT DTO
            var validationResult = await _itemOrderInsertValidator.ValidateAsync(itemOrderInsertDTO);
            // SI LA VALIDACION ES ERRONEA, SE PARA
            // Y DEVUELVE LOS ERRRORES LISTADOS
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_itemOrderService.Validate(itemOrderInsertDTO))
            {
                return BadRequest(_itemOrderService.Errors);
            }
            var itemOrderDTO = await _itemOrderService.Add(itemOrderInsertDTO);
            // CreatedAtAction otorga el metodo para la consulta del objeto generado
            // el campo por el cual se puede buscar y el objeto generado en esta ejecucion
            return CreatedAtAction(nameof(GetById), new { id = itemOrderDTO.Id }, itemOrderDTO);
        }

        // Cuando el usuario modifica un dato de su Cart, se modifica el ItemCart
        // Podria implementarse que el dato que recibe sea un List<ItemCartDTO>
        [HttpPut]
        public async Task<ActionResult<ItemOrderDTO>> Update(ItemOrderUpdateDTO itemOrderUpdateDTO)
        {
            var validationResult = await _itemOrderUpdateValidator.ValidateAsync(itemOrderUpdateDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_itemOrderService.Validate(itemOrderUpdateDTO))
            {
                return BadRequest(_itemOrderService.Errors);
            }
            var itemCartDTO = await _itemOrderService.Update(itemOrderUpdateDTO);

            return itemCartDTO == null ? NotFound() : Ok(itemCartDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var itemCartDTO = await _itemOrderService.Delete(id);

            return itemCartDTO == null ? NotFound() : Ok(itemCartDTO);
        }
    }
}
