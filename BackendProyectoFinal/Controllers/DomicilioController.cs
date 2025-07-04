using BackendProyectoFinal.DTOs;
using BackendProyectoFinal.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BackendProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DomicilioController : ControllerBase
    {
        private ICommonService<DomicilioDTO,DomicilioInsertDTO, DomicilioUpdateDTO> _domicilioService;
        private IValidator<DomicilioInsertDTO> _domicilioInsertValidator;
        private IValidator<DomicilioUpdateDTO> _domicilioUpdateValidator;

        public DomicilioController(
            [FromKeyedServices("DomicilioService")] ICommonService<DomicilioDTO, DomicilioInsertDTO, DomicilioUpdateDTO> domicilioService,
            IValidator<DomicilioInsertDTO> domicilioInsertValidator,
            IValidator<DomicilioUpdateDTO> domicilioUpdateValidator) 
        {
            _domicilioService = domicilioService;
            _domicilioInsertValidator = domicilioInsertValidator;
            _domicilioUpdateValidator = domicilioUpdateValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<DomicilioDTO>> Get() 
            => await _domicilioService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<DomicilioDTO>> GetById(int id)
        {
            var domicilio = await _domicilioService.GetById(id);
            return domicilio == null ? NotFound() : Ok(domicilio);
        }

        [HttpPost]
        public async Task<ActionResult<DomicilioDTO>> Add(DomicilioInsertDTO domicilioInsertDTO)
        {
            // REALIZA VALIDACION DE INSERT DTO (QUE NOMBRE NO ESTE VACIO)
            var validationResult = await _domicilioInsertValidator.ValidateAsync(domicilioInsertDTO);
            // SI LA VALIDACION ES ERRONEA, SE PARA
            // Y DEVUELVE LOS ERRRORES LISTADOS
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_domicilioService.Validate(domicilioInsertDTO))
            {
                return BadRequest(_domicilioService.Errors);
            }
            var domicilioDTO = await _domicilioService.Add(domicilioInsertDTO);
            // CreatedAtAction otorga el metodo para la consulta del objeto generado
            // el campo por el cual se puede buscar y el objeto generado en esta ejecucion
            return CreatedAtAction(nameof(GetById), new { id = domicilioDTO.Id }, domicilioDTO);
        }

        [HttpPut]
        public async Task<ActionResult<DomicilioDTO>> Update(DomicilioUpdateDTO domicilioUpdateDTO)
        {
            var validationResult = await _domicilioUpdateValidator.ValidateAsync(domicilioUpdateDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_domicilioService.Validate(domicilioUpdateDTO))
            {
                return BadRequest(_domicilioService.Errors);
            }
            var domicilioDTO = await _domicilioService.Update(domicilioUpdateDTO);

            return domicilioDTO == null ? NotFound() : Ok(domicilioDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var domicilioDTO = await _domicilioService.Delete(id);

            return domicilioDTO == null ? NotFound() : Ok(domicilioDTO);
        }
    }
}
