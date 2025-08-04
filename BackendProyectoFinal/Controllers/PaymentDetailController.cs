using BackendProyectoFinal.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using BackendProyectoFinal.DTOs.Payment.PaymentDetail;

namespace BackendProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentDetailController : ControllerBase
    {
        private ICommonService<PaymentDetailDTO, PaymentDetailInsertDTO, PaymentDetailUpdateDTO> _paymentDetailService;
        private IValidator<PaymentDetailInsertDTO> _paymentDetailInsertValidator;
        private IValidator<PaymentDetailUpdateDTO> _paymentDetailUpdateValidator;

        public PaymentDetailController(
            [FromKeyedServices("PaymentDetailService")] ICommonService<PaymentDetailDTO, PaymentDetailInsertDTO, PaymentDetailUpdateDTO> paymentDetailService,
            IValidator<PaymentDetailInsertDTO> paymentDetailInsertValidator,
            IValidator<PaymentDetailUpdateDTO> paymentDetailUpdateValidator)
        {
            _paymentDetailService = paymentDetailService;
            _paymentDetailInsertValidator = paymentDetailInsertValidator;
            _paymentDetailUpdateValidator = paymentDetailUpdateValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<PaymentDetailDTO>> Get()
            => await _paymentDetailService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDetailDTO>> GetById(int id)
        {
            var paymentDetail = await _paymentDetailService.GetById(id);
            return paymentDetail == null ? NotFound() : Ok(paymentDetail);
        }

        [HttpPost]
        public async Task<ActionResult<PaymentDetailDTO>> Add(PaymentDetailInsertDTO paymentDetailInsertDTO)
        {
            // REALIZA VALIDACION DE INSERT DTO
            var validationResult = await _paymentDetailInsertValidator.ValidateAsync(paymentDetailInsertDTO);
            // SI LA VALIDACION ES ERRONEA, SE PARA
            // Y DEVUELVE LOS ERRRORES LISTADOS
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_paymentDetailService.Validate(paymentDetailInsertDTO))
            {
                return BadRequest(_paymentDetailService.Errors);
            }
            var paymentDetailDTO = await _paymentDetailService.Add(paymentDetailInsertDTO);
            // CreatedAtAction otorga el metodo para la consulta del objeto generado
            // el campo por el cual se puede buscar y el objeto generado en esta ejecucion
            return CreatedAtAction(nameof(GetById), new { id = paymentDetailDTO.Id }, paymentDetailDTO);
        }

        [HttpPut]
        public async Task<ActionResult<PaymentDetailDTO>> Update(PaymentDetailUpdateDTO paymentDetailUpdateDTO)
        {
            var validationResult = await _paymentDetailUpdateValidator.ValidateAsync(paymentDetailUpdateDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_paymentDetailService.Validate(paymentDetailUpdateDTO))
            {
                return BadRequest(_paymentDetailService.Errors);
            }
            var paymentDetailDTO = await _paymentDetailService.Update(paymentDetailUpdateDTO);

            return paymentDetailDTO == null ? NotFound() : Ok(paymentDetailDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var paymentDetailDTO = await _paymentDetailService.Delete(id);

            return paymentDetailDTO == null ? NotFound() : Ok(paymentDetailDTO);
        }
    }
}
