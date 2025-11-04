namespace CardValidator.Api.Models {
    public class CardValidationResponse {
        public bool IsValid { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
