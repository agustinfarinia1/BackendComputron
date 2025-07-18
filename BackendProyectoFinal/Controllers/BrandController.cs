using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using BackendProyectoFinal.Services;
using BackendProyectoFinal.DTOs.Brand;

namespace BackendProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private ICommonService<BrandDTO, BrandInsertDTO, BrandUpdateDTO> _brandService;
        private IValidator<BrandInsertDTO> _brandInsertValidator;
        private IValidator<BrandUpdateDTO> _brandUpdateValidator;

        public BrandController(
            [FromKeyedServices("BrandService")] ICommonService<BrandDTO, BrandInsertDTO, BrandUpdateDTO> marcaService,
            IValidator<BrandInsertDTO> brandInsertValidator,
            IValidator<BrandUpdateDTO> brandUpdateValidator)
        {
            _brandService = marcaService;
            _brandInsertValidator = brandInsertValidator;
            _brandUpdateValidator = brandUpdateValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<BrandDTO>> Get()
            => await _brandService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<BrandDTO>> GetById(int id)
        {
            var brand = await _brandService.GetById(id);
            return brand == null ? NotFound() : Ok(brand);
        }

        [HttpPost]
        public async Task<ActionResult<BrandDTO>> Add(BrandInsertDTO brandInsertDTO)
        {
            // REALIZA VALIDACION DE INSERT DTO
            var validationResult = await _brandInsertValidator.ValidateAsync(brandInsertDTO);
            // SI LA VALIDACION ES ERRONEA, SE PARA
            // Y DEVUELVE LOS ERRRORES LISTADOS
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_brandService.Validate(brandInsertDTO))
            {
                return BadRequest(_brandService.Errors);
            }
            var brandDTO = await _brandService.Add(brandInsertDTO);
            // CreatedAtAction otorga el metodo para la consulta del objeto generado
            // el campo por el cual se puede buscar y el objeto generado en esta ejecucion
            return CreatedAtAction(nameof(GetById), new { id = brandDTO.Id }, brandDTO);
        }

        [HttpPut]
        public async Task<ActionResult<BrandDTO>> Update(BrandUpdateDTO brandUpdateDTO)
        {
            var validationResult = await _brandUpdateValidator.ValidateAsync(brandUpdateDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_brandService.Validate(brandUpdateDTO))
            {
                return BadRequest(_brandService.Errors);
            }
            var brandDTO = await _brandService.Update(brandUpdateDTO);

            return brandDTO == null ? NotFound() : Ok(brandDTO);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var brandDTO = await _brandService.Delete(id);

            return brandDTO == null ? NotFound() : Ok(brandDTO);
        }
    }
}
