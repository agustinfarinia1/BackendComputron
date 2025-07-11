using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using BackendProyectoFinal.DTOs.CartDTO;
using BackendProyectoFinal.Services;

namespace BackendProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private ICommonService<CartDTO, CartInsertDTO, CartUpdateDTO> _cartService;
        private IValidator<CartInsertDTO> _cartInsertValidator;
        private IValidator<CartUpdateDTO> _cartUpdateValidator;

        public CartController(
            [FromKeyedServices("CartService")] ICommonService<CartDTO, CartInsertDTO, CartUpdateDTO> cartService,
            IValidator<CartInsertDTO> cartInsertValidator,
            IValidator<CartUpdateDTO> cartUpdateValidator)
        {
            _cartService = cartService;
            _cartInsertValidator = cartInsertValidator;
            _cartUpdateValidator = cartUpdateValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<CartDTO>> Get()
            => await _cartService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<CartDTO>> GetById(int id)
        {
            var cart = await _cartService.GetById(id);
            return cart == null ? NotFound() : Ok(cart);
        }

        [HttpPost]
        public async Task<ActionResult<CartDTO>> Add(CartInsertDTO cartInsertDTO)
        {
            // REALIZA VALIDACION DE INSERT DTO
            var validationResult = await _cartInsertValidator.ValidateAsync(cartInsertDTO);
            // SI LA VALIDACION ES ERRONEA, SE PARA
            // Y DEVUELVE LOS ERRRORES LISTADOS
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_cartService.Validate(cartInsertDTO))
            {
                return BadRequest(_cartService.Errors);
            }
            var cartDTO = await _cartService.Add(cartInsertDTO);
            // CreatedAtAction otorga el metodo para la consulta del objeto generado
            // el campo por el cual se puede buscar y el objeto generado en esta ejecucion
            return CreatedAtAction(nameof(GetById), new { id = cartDTO.Id }, cartDTO);
        }

        [HttpPut]
        public async Task<ActionResult<CartDTO>> Update(CartUpdateDTO cartUpdateDTO)
        {
            var validationResult = await _cartUpdateValidator.ValidateAsync(cartUpdateDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_cartService.Validate(cartUpdateDTO))
            {
                return BadRequest(_cartService.Errors);
            }
            var cartDTO = await _cartService.Update(cartUpdateDTO);

            return cartDTO == null ? NotFound() : Ok(cartDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var cartDTO = await _cartService.Delete(id);

            return cartDTO == null ? NotFound() : Ok(cartDTO);
        }
    }
}
