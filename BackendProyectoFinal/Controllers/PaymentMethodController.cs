using BackendProyectoFinal.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using BackendProyectoFinal.DTOs.Payment.PaymentMethod;

namespace BackendProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private ICommonService<PaymentMethodDTO, PaymentMethodInsertDTO, PaymentMethodUpdateDTO> _paymentMethodService;
        private IValidator<PaymentMethodInsertDTO> _paymentMethodInsertValidator;
        private IValidator<PaymentMethodUpdateDTO> _paymentMethodUpdateValidator;

        public PaymentMethodController(
            [FromKeyedServices("PaymentMethodService")] ICommonService<PaymentMethodDTO, PaymentMethodInsertDTO, PaymentMethodUpdateDTO> paymentMethodService,
            IValidator<PaymentMethodInsertDTO> paymentMethodInsertValidator,
            IValidator<PaymentMethodUpdateDTO> paymentMethodUpdateValidator)
        {
            _paymentMethodService = paymentMethodService;
            _paymentMethodInsertValidator = paymentMethodInsertValidator;
            _paymentMethodUpdateValidator = paymentMethodUpdateValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<PaymentMethodDTO>> Get()
            => await _paymentMethodService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentMethodDTO>> GetById(int id)
        {
            var paymentMethod = await _paymentMethodService.GetById(id);
            return paymentMethod == null ? NotFound() : Ok(paymentMethod);
        }

        [HttpPost]
        public async Task<ActionResult<PaymentMethodDTO>> Add(PaymentMethodInsertDTO paymentMethodInsertDTO)
        {
            // REALIZA VALIDACION DE INSERT DTO (QUE NOMBRE NO ESTE VACIO)
            var validationResult = await _paymentMethodInsertValidator.ValidateAsync(paymentMethodInsertDTO);
            // SI LA VALIDACION ES ERRONEA, SE PARA
            // Y DEVUELVE LOS ERRRORES LISTADOS
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_paymentMethodService.Validate(paymentMethodInsertDTO))
            {
                return BadRequest(_paymentMethodService.Errors);
            }
            var paymentMethodDTO = await _paymentMethodService.Add(paymentMethodInsertDTO);
            // CreatedAtAction otorga el metodo para la consulta del objeto generado
            // el campo por el cual se puede buscar y el objeto generado en esta ejecucion
            return CreatedAtAction(nameof(GetById), new { id = paymentMethodDTO.Id }, paymentMethodDTO);
        }

        [HttpPut]
        public async Task<ActionResult<PaymentMethodDTO>> Update(PaymentMethodUpdateDTO paymentMethodUpdateDTO)
        {
            var validationResult = await _paymentMethodUpdateValidator.ValidateAsync(paymentMethodUpdateDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_paymentMethodService.Validate(paymentMethodUpdateDTO))
            {
                return BadRequest(_paymentMethodService.Errors);
            }
            var paymentMethodDTO = await _paymentMethodService.Update(paymentMethodUpdateDTO);

            return paymentMethodDTO == null ? NotFound() : Ok(paymentMethodDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var paymentMethodDTO = await _paymentMethodService.Delete(id);

            return paymentMethodDTO == null ? NotFound() : Ok(paymentMethodDTO);
        }
    }
}
