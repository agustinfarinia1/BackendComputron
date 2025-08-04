using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using BackendProyectoFinal.Services;
using BackendProyectoFinal.DTOs.Product.Category;

namespace BackendProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICommonService<CategoryDTO,CategoryInsertDTO, CategoryUpdateDTO> _categoryService;
        private IValidator<CategoryInsertDTO> _categoryInsertValidator;
        private IValidator<CategoryUpdateDTO> _categoryUpdateValidator;

        public CategoryController(
            [FromKeyedServices("CategoryService")]ICommonService<CategoryDTO, CategoryInsertDTO, CategoryUpdateDTO> categoryService,
            IValidator<CategoryInsertDTO> categoryInsertValidator,
            IValidator<CategoryUpdateDTO> categoryUpdateValidator) 
        {
            _categoryService = categoryService;
            _categoryInsertValidator = categoryInsertValidator;
            _categoryUpdateValidator = categoryUpdateValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryDTO>> Get() 
            => await _categoryService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetById(int id)
        {
            var category = await _categoryService.GetById(id);
            return category == null ? NotFound() : Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Add(CategoryInsertDTO categoryInsertDTO)
        {
            // REALIZA VALIDACION DE INSERT DTO (QUE NOMBRE NO ESTE VACIO)
            var validationResult = await _categoryInsertValidator.ValidateAsync(categoryInsertDTO);
            // SI LA VALIDACION ES ERRONEA, SE PARA
            // Y DEVUELVE LOS ERRRORES LISTADOS
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_categoryService.Validate(categoryInsertDTO))
            {
                return BadRequest(_categoryService.Errors);
            }
            var categoryDTO = await _categoryService.Add(categoryInsertDTO);
            // CreatedAtAction otorga el metodo para la consulta del objeto generado
            // el campo por el cual se puede buscar y el objeto generado en esta ejecucion
            return CreatedAtAction(nameof(GetById), new { id = categoryDTO.Id }, categoryDTO);
        }

        [HttpPut]
        public async Task<ActionResult<CategoryDTO>> Update(CategoryUpdateDTO categoryUpdateDTO)
        {
            var validationResult = await _categoryUpdateValidator.ValidateAsync(categoryUpdateDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_categoryService.Validate(categoryUpdateDTO))
            {
                return BadRequest(_categoryService.Errors);
            }
            var categoryDTO = await _categoryService.Update(categoryUpdateDTO);

            return categoryDTO == null ? NotFound() : Ok(categoryDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var categoryDTO = await _categoryService.Delete(id);

            return categoryDTO == null ? NotFound() : Ok(categoryDTO);
        }
    }
}
