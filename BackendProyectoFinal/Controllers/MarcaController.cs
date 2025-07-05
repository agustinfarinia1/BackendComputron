using BackendProyectoFinal.DTOs.MarcaDTO;
using BackendProyectoFinal.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BackendProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {
        private ICommonService<MarcaDTO, MarcaInsertDTO, MarcaUpdateDTO> _marcaService;
        private IValidator<MarcaInsertDTO> _marcaInsertValidator;
        private IValidator<MarcaUpdateDTO> _marcaUpdateValidator;

        public MarcaController(
            [FromKeyedServices("MarcaService")] ICommonService<MarcaDTO, MarcaInsertDTO, MarcaUpdateDTO> marcaService,
            IValidator<MarcaInsertDTO> marcaInsertValidator,
            IValidator<MarcaUpdateDTO> marcaUpdateValidator)
        {
            _marcaService = marcaService;
            _marcaInsertValidator = marcaInsertValidator;
            _marcaUpdateValidator = marcaUpdateValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<MarcaDTO>> Get()
            => await _marcaService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<MarcaDTO>> GetById(int id)
        {
            var producto = await _marcaService.GetById(id);
            return producto == null ? NotFound() : Ok(producto);
        }

        [HttpPost]
        public async Task<ActionResult<MarcaDTO>> Add(MarcaInsertDTO marcaInsertDTO)
        {
            // REALIZA VALIDACION DE INSERT DTO
            var validationResult = await _marcaInsertValidator.ValidateAsync(marcaInsertDTO);
            // SI LA VALIDACION ES ERRONEA, SE PARA
            // Y DEVUELVE LOS ERRRORES LISTADOS
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_marcaService.Validate(marcaInsertDTO))
            {
                return BadRequest(_marcaService.Errors);
            }
            var marcaDTO = await _marcaService.Add(marcaInsertDTO);
            // CreatedAtAction otorga el metodo para la consulta del objeto generado
            // el campo por el cual se puede buscar y el objeto generado en esta ejecucion
            return CreatedAtAction(nameof(GetById), new { id = marcaDTO.Id }, marcaDTO);
        }

        [HttpPut]
        public async Task<ActionResult<MarcaDTO>> Update(MarcaUpdateDTO marcaUpdateDTO)
        {
            var validationResult = await _marcaUpdateValidator.ValidateAsync(marcaUpdateDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_marcaService.Validate(marcaUpdateDTO))
            {
                return BadRequest(_marcaService.Errors);
            }
            var marcaDTO = await _marcaService.Update(marcaUpdateDTO);

            return marcaDTO == null ? NotFound() : Ok(marcaDTO);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var marcaDTO = await _marcaService.Delete(id);

            return marcaDTO == null ? NotFound() : Ok(marcaDTO);
        }
    }
}
