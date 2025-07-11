using BackendProyectoFinal.DTOs.AddressDTO;
using BackendProyectoFinal.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BackendProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private ICommonService<AddressDTO,AddressInsertDTO, AddressUpdateDTO> _addressService;
        private IValidator<AddressInsertDTO> _addressInsertValidator;
        private IValidator<AddressUpdateDTO> _addressUpdateValidator;

        public AddressController(
            [FromKeyedServices("AddressService")] ICommonService<AddressDTO, AddressInsertDTO, AddressUpdateDTO> domicilioService,
            IValidator<AddressInsertDTO> addressInsertValidator,
            IValidator<AddressUpdateDTO> addressUpdateValidator) 
        {
            _addressService = domicilioService;
            _addressInsertValidator = addressInsertValidator;
            _addressUpdateValidator = addressUpdateValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<AddressDTO>> Get() 
            => await _addressService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<AddressDTO>> GetById(int id)
        {
            var address = await _addressService.GetById(id);
            return address == null ? NotFound() : Ok(address);
        }

        [HttpPost]
        public async Task<ActionResult<AddressDTO>> Add(AddressInsertDTO addressInsertDTO)
        {
            // REALIZA VALIDACION DE INSERT DTO (QUE NOMBRE NO ESTE VACIO)
            var validationResult = await _addressInsertValidator.ValidateAsync(addressInsertDTO);
            // SI LA VALIDACION ES ERRONEA, SE PARA
            // Y DEVUELVE LOS ERRRORES LISTADOS
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_addressService.Validate(addressInsertDTO))
            {
                return BadRequest(_addressService.Errors);
            }
            var addressDTO = await _addressService.Add(addressInsertDTO);
            // CreatedAtAction otorga el metodo para la consulta del objeto generado
            // el campo por el cual se puede buscar y el objeto generado en esta ejecucion
            return CreatedAtAction(nameof(GetById), new { id = addressDTO.Id }, addressDTO);
        }

        [HttpPut]
        public async Task<ActionResult<AddressDTO>> Update(AddressUpdateDTO addressUpdateDTO)
        {
            var validationResult = await _addressUpdateValidator.ValidateAsync(addressUpdateDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_addressService.Validate(addressUpdateDTO))
            {
                return BadRequest(_addressService.Errors);
            }
            var AddressDTO = await _addressService.Update(addressUpdateDTO);

            return AddressDTO == null ? NotFound() : Ok(AddressDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var AddressDTO = await _addressService.Delete(id);

            return AddressDTO == null ? NotFound() : Ok(AddressDTO);
        }
    }
}
