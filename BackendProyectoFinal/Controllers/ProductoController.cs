using BackendProyectoFinal.DTOs;
using BackendProyectoFinal.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BackendProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private ICommonService<ProductoDTO, ProductoInsertDTO, ProductoUpdateDTO> _productoService;
        private IValidator<ProductoInsertDTO> _productoInsertValidator;
        private IValidator<ProductoUpdateDTO> _productoUpdateValidator;

        public ProductoController(
            [FromKeyedServices("ProductoService")] ICommonService<ProductoDTO, ProductoInsertDTO, ProductoUpdateDTO> productoService,
            IValidator<ProductoInsertDTO> productoInsertValidator,
            IValidator<ProductoUpdateDTO> productoUpdateValidator)
        {
            _productoService = productoService;
            _productoInsertValidator = productoInsertValidator;
            _productoUpdateValidator = productoUpdateValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductoDTO>> Get()
            => await _productoService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDTO>> GetById(int id)
        {
            var producto = await _productoService.GetById(id);
            return producto == null ? NotFound() : Ok(producto);
        }

        [HttpPost]
        public async Task<ActionResult<ProductoDTO>> Add(ProductoInsertDTO productoInsertDTO)
        {
            // REALIZA VALIDACION DE INSERT DTO
            var validationResult = await _productoInsertValidator.ValidateAsync(productoInsertDTO);
            // SI LA VALIDACION ES ERRONEA, SE PARA
            // Y DEVUELVE LOS ERRRORES LISTADOS
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_productoService.Validate(productoInsertDTO))
            {
                return BadRequest(_productoService.Errors);
            }
            var productoDTO = await _productoService.Add(productoInsertDTO);
            // CreatedAtAction otorga el metodo para la consulta del objeto generado
            // el campo por el cual se puede buscar y el objeto generado en esta ejecucion
            return CreatedAtAction(nameof(GetById), new { id = productoDTO.Id }, productoDTO);
        }

        [HttpPut]
        public async Task<ActionResult<ProductoDTO>> Update(ProductoUpdateDTO productoUpdateDTO)
        {
            var validationResult = await _productoUpdateValidator.ValidateAsync(productoUpdateDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_productoService.Validate(productoUpdateDTO))
            {
                return BadRequest(_productoService.Errors);
            }
            var productoDTO = await _productoService.Update(productoUpdateDTO);

            return productoDTO == null ? NotFound() : Ok(productoDTO);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var productoDTO = await _productoService.Delete(id);

            return productoDTO == null ? NotFound() : Ok(productoDTO);
        }
    }
}
