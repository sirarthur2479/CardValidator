using System.ComponentModel.DataAnnotations;

namespace CardValidator.Api.Models {
    public class CreditCardValidationRequest {
        [Required]
        [StringLength(32)]
        public string CardNumber { get; set; } = string.Empty;
    }
}
