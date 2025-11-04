using System.ComponentModel.DataAnnotations;

namespace CardValidator.Api.Models {
    public class CardValidationRequest {
        [Required]
        [StringLength(32)]
        public string CardNumber { get; set; } = string.Empty;
    }
}
