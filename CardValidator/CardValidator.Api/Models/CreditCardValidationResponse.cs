namespace CardValidator.Api.Models {
    public class CreditCardValidationResponse {
        public bool IsValid { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
