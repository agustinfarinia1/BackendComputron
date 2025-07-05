using BackendProyectoFinal.DTOs.CategoriaProductoDTO;
using BackendProyectoFinal.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BackendProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaProductoController : ControllerBase
    {
        private ICommonService<CategoriaProductoDTO,CategoriaProductoInsertDTO, CategoriaProductoUpdateDTO> _categoriaProductoService;
        private IValidator<CategoriaProductoInsertDTO> _categoriaProductoInsertValidator;
        private IValidator<CategoriaProductoUpdateDTO> _categoriaProductoUpdateValidator;

        public CategoriaProductoController(
            [FromKeyedServices("CategoriaProductoService")]ICommonService<CategoriaProductoDTO, CategoriaProductoInsertDTO, CategoriaProductoUpdateDTO> categoriaProductoService,
            IValidator<CategoriaProductoInsertDTO> categoriaProductoInsertValidator,
            IValidator<CategoriaProductoUpdateDTO> categoriaProductoUpdateValidator) 
        {
            _categoriaProductoService = categoriaProductoService;
            _categoriaProductoInsertValidator = categoriaProductoInsertValidator;
            _categoriaProductoUpdateValidator = categoriaProductoUpdateValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoriaProductoDTO>> Get() 
            => await _categoriaProductoService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaProductoDTO>> GetById(int id)
        {
            var categoria = await _categoriaProductoService.GetById(id);
            return categoria == null ? NotFound() : Ok(categoria);
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaProductoDTO>> Add(CategoriaProductoInsertDTO categoriaInsertDTO)
        {
            // REALIZA VALIDACION DE INSERT DTO (QUE NOMBRE NO ESTE VACIO)
            var validationResult = await _categoriaProductoInsertValidator.ValidateAsync(categoriaInsertDTO);
            // SI LA VALIDACION ES ERRONEA, SE PARA
            // Y DEVUELVE LOS ERRRORES LISTADOS
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_categoriaProductoService.Validate(categoriaInsertDTO))
            {
                return BadRequest(_categoriaProductoService.Errors);
            }
            var categoriaDTO = await _categoriaProductoService.Add(categoriaInsertDTO);
            // CreatedAtAction otorga el metodo para la consulta del objeto generado
            // el campo por el cual se puede buscar y el objeto generado en esta ejecucion
            return CreatedAtAction(nameof(GetById), new { id = categoriaDTO.Id }, categoriaDTO);
        }

        [HttpPut]
        public async Task<ActionResult<CategoriaProductoDTO>> Update(CategoriaProductoUpdateDTO categoriaUpdateDTO)
        {
            var validationResult = await _categoriaProductoUpdateValidator.ValidateAsync(categoriaUpdateDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_categoriaProductoService.Validate(categoriaUpdateDTO))
            {
                return BadRequest(_categoriaProductoService.Errors);
            }
            var categoriaDTO = await _categoriaProductoService.Update(categoriaUpdateDTO);

            return categoriaDTO == null ? NotFound() : Ok(categoriaDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var categoriaDTO = await _categoriaProductoService.Delete(id);

            return categoriaDTO == null ? NotFound() : Ok(categoriaDTO);
        }
    }
}
