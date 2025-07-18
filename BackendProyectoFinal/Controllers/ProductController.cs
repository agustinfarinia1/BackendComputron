using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using BackendProyectoFinal.Services;
using BackendProyectoFinal.DTOs.Product;

namespace BackendProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ICommonService<ProductDTO, ProductInsertDTO, ProductUpdateDTO> _productService;
        private IValidator<ProductInsertDTO> _productInsertValidator;
        private IValidator<ProductUpdateDTO> _productUpdateValidator;

        public ProductController(
            [FromKeyedServices("ProductService")] ICommonService<ProductDTO, ProductInsertDTO, ProductUpdateDTO> productoService,
            IValidator<ProductInsertDTO> productInsertValidator,
            IValidator<ProductUpdateDTO> productUpdateValidator)
        {
            _productService = productoService;
            _productInsertValidator = productInsertValidator;
            _productUpdateValidator = productUpdateValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductDTO>> Get()
            => await _productService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetById(int id)
        {
            var product = await _productService.GetById(id);
            return product == null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Add(ProductInsertDTO productInsertDTO)
        {
            // REALIZA VALIDACION DE INSERT DTO
            var validationResult = await _productInsertValidator.ValidateAsync(productInsertDTO);
            // SI LA VALIDACION ES ERRONEA, SE PARA
            // Y DEVUELVE LOS ERRRORES LISTADOS
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_productService.Validate(productInsertDTO))
            {
                return BadRequest(_productService.Errors);
            }
            var productDTO = await _productService.Add(productInsertDTO);
            // CreatedAtAction otorga el metodo para la consulta del objeto generado
            // el campo por el cual se puede buscar y el objeto generado en esta ejecucion
            return CreatedAtAction(nameof(GetById), new { id = productDTO.Id }, productDTO);
        }

        [HttpPut]
        public async Task<ActionResult<ProductDTO>> Update(ProductUpdateDTO productUpdateDTO)
        {
            var validationResult = await _productUpdateValidator.ValidateAsync(productUpdateDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_productService.Validate(productUpdateDTO))
            {
                return BadRequest(_productService.Errors);
            }
            var productDTO = await _productService.Update(productUpdateDTO);

            return productDTO == null ? NotFound() : Ok(productDTO);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var productDTO = await _productService.Delete(id);

            return productDTO == null ? NotFound() : Ok(productDTO);
        }
    }
}
