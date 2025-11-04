using CardValidator.Api.Models;
using CardValidator.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CardValidator.Api.Controllers
{
    [ApiController]
    [Route("api/v1/cards")]
    public class CreditCardValidationController : ControllerBase {
        private readonly ILuhnAlgorithmValidator _luhnAlgorithmValidator;
        private readonly ILogger<CreditCardValidationController> _logger;

        public CreditCardValidationController(ILuhnAlgorithmValidator luhnAlgorithmValidator, ILogger<CreditCardValidationController> logger) {
            _luhnAlgorithmValidator = luhnAlgorithmValidator;
            _logger = logger;
        }

        [HttpPost("validate")]
        [ProducesResponseType(typeof(CreditCardValidationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Validate([FromBody] CreditCardValidationRequest request) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (request.CardNumber == "force-exception") {
                throw new Exception("Forced exception for testing purposes.");
            }

            var result = _luhnAlgorithmValidator.IsValid(request.CardNumber);

            var response = new CreditCardValidationResponse {
                IsValid = result,
                Message = result ? "Card number is valid." : "Card number is invalid.",
            };

            _logger.LogInformation("Validation requested, valid={IsValid}", response.IsValid);
            return Ok(response);
        }
    }
}
