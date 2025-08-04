using BackendProyectoFinal.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using BackendProyectoFinal.DTOs.Payment;

namespace BackendProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private ICommonService<PaymentDTO, PaymentInsertDTO, PaymentUpdateDTO> _paymentService;
        private IValidator<PaymentInsertDTO> _paymentInsertValidator;
        private IValidator<PaymentUpdateDTO> _paymentUpdateValidator;

        public PaymentController(
            [FromKeyedServices("PaymentService")] ICommonService<PaymentDTO, PaymentInsertDTO, PaymentUpdateDTO> paymentService,
            IValidator<PaymentInsertDTO> paymentInsertValidator,
            IValidator<PaymentUpdateDTO> paymentUpdateValidator)
        {
            _paymentService = paymentService;
            _paymentInsertValidator = paymentInsertValidator;
            _paymentUpdateValidator = paymentUpdateValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<PaymentDTO>> Get()
            => await _paymentService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDTO>> GetById(int id)
        {
            var role = await _paymentService.GetById(id);
            return role == null ? NotFound() : Ok(role);
        }

        [HttpPost]
        public async Task<ActionResult<PaymentDTO>> Add(PaymentInsertDTO paymentInsertDTO)
        {
            // REALIZA VALIDACION DE INSERT DTO
            var validationResult = await _paymentInsertValidator.ValidateAsync(paymentInsertDTO);
            // SI LA VALIDACION ES ERRONEA, SE PARA
            // Y DEVUELVE LOS ERRRORES LISTADOS
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_paymentService.Validate(paymentInsertDTO))
            {
                return BadRequest(_paymentService.Errors);
            }
            var paymentDTO = await _paymentService.Add(paymentInsertDTO);
            // CreatedAtAction otorga el metodo para la consulta del objeto generado
            // el campo por el cual se puede buscar y el objeto generado en esta ejecucion
            return CreatedAtAction(nameof(GetById), new { id = paymentDTO.Id }, paymentDTO);
        }

        [HttpPut]
        public async Task<ActionResult<PaymentDTO>> Update(PaymentUpdateDTO paymentUpdateDTO)
        {
            var validationResult = await _paymentUpdateValidator.ValidateAsync(paymentUpdateDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_paymentService.Validate(paymentUpdateDTO))
            {
                return BadRequest(_paymentService.Errors);
            }
            var paymentDTO = await _paymentService.Update(paymentUpdateDTO);

            return paymentDTO == null ? NotFound() : Ok(paymentDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var paymentDTO = await _paymentService.Delete(id);

            return paymentDTO == null ? NotFound() : Ok(paymentDTO);
        }
    }
}
