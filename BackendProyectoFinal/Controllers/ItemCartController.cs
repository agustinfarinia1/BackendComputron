using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using BackendProyectoFinal.Services;
using BackendProyectoFinal.DTOs.ItemCart;

namespace BackendProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemCartController : ControllerBase
    {
        private IItemCartService _itemCartService;
        private IValidator<ItemCartInsertDTO> _itemCartInsertValidator;
        private IValidator<ItemCartUpdateDTO> _itemCartUpdateValidator;

        public ItemCartController(
        [FromKeyedServices("ItemCartService")] IItemCartService itemCartService,
        IValidator<ItemCartInsertDTO> itemCartInsertValidator,
        IValidator<ItemCartUpdateDTO> itemCartUpdateValidator)
        {
            _itemCartService = itemCartService;
            _itemCartInsertValidator = itemCartInsertValidator;
            _itemCartUpdateValidator = itemCartUpdateValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemCartDTO>> Get()
            => await _itemCartService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemCartDTO>> GetById(int id)
        {
            var itemCart = await _itemCartService.GetById(id);
            return itemCart == null ? NotFound() : Ok(itemCart);
        }

        [HttpPost]
        public async Task<ActionResult<ItemCartDTO>> Add(ItemCartInsertDTO itemCartInsertDTO)
        {
            // REALIZA VALIDACION DE INSERT DTO
            var validationResult = await _itemCartInsertValidator.ValidateAsync(itemCartInsertDTO);
            // SI LA VALIDACION ES ERRONEA, SE PARA
            // Y DEVUELVE LOS ERRRORES LISTADOS
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_itemCartService.Validate(itemCartInsertDTO))
            {
                return BadRequest(_itemCartService.Errors);
            }
            var itemCartDTO = await _itemCartService.Add(itemCartInsertDTO);
            // CreatedAtAction otorga el metodo para la consulta del objeto generado
            // el campo por el cual se puede buscar y el objeto generado en esta ejecucion
            return CreatedAtAction(nameof(GetById), new { id = itemCartDTO.Id }, itemCartDTO);
        }

        // Cuando el usuario modifica un dato de su Cart, se modifica el ItemCart
        // Podria implementarse que el dato que recibe sea un List<ItemCartDTO>
        [HttpPut]
        public async Task<ActionResult<ItemCartDTO>> Update(ItemCartUpdateDTO itemCartUpdateDTO)
        {
            var validationResult = await _itemCartUpdateValidator.ValidateAsync(itemCartUpdateDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_itemCartService.Validate(itemCartUpdateDTO))
            {
                return BadRequest(_itemCartService.Errors);
            }
            var itemCartDTO = await _itemCartService.Update(itemCartUpdateDTO);

            return itemCartDTO == null ? NotFound() : Ok(itemCartDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var itemCartDTO = await _itemCartService.Delete(id);

            return itemCartDTO == null ? NotFound() : Ok(itemCartDTO);
        }
    }
}
