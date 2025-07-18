using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using BackendProyectoFinal.Services;
using BackendProyectoFinal.DTOs.Cart;

namespace BackendProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private ICartService _cartService;
        private IValidator<CartInsertDTO> _cartInsertValidator;
        private IValidator<CartUpdateDTO> _cartUpdateValidator;

        public CartController(
            [FromKeyedServices("CartService")] ICartService cartService,
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

        [HttpGet("{cartID}")]
        public async Task<ActionResult<CartDTO>> GetById(int cartID)
        {
            var cart = await _cartService.GetById(cartID);
            return cart == null ? NotFound() : Ok(cart);
        }

        [HttpGet("user/{userID}")]
        public async Task<ActionResult<CartDTO>> GetByUserID(int userID)
        {
            var cart = await _cartService.GetByField(userID.ToString());
            return cart == null ? NotFound() : Ok(cart);
        }

        // Se utiliza en el UserController -> Add
        // Ya que el usuario no deberia existir sin un Cart
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

        // Update y Delete no se utilizaran directamente.
        // Cart se elimina de manera Cascade con el UserID
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
