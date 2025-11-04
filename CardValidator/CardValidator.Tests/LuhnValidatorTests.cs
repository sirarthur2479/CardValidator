using CardValidator.Api.Services;
using Xunit;

namespace CardValidator.Tests {
    public class LuhnValidatorTests {
        private readonly ILuhnAlgorithmValidator _validator = new LuhnAlgorithmValidator();

        [Theory(DisplayName = "Valid Card Numbers With Various Formats")]
        [InlineData("4111111111111111")]
        [InlineData("4111 1111 1111 1111")]
        [InlineData("4111-1111-1111-1111")]
        [InlineData(" 4111111111111111 ")]
        [InlineData("5431 1111 1111 1111")]
        [InlineData("79927398713")]
        public void LuhnAlgorithmValidation_ShouldReturnTrue_ForValidCards(string input) {
            // Act
            var result = _validator.IsValid(input);

            // Assert
            Assert.True(result, $"Expected '{input}' to be valid.");
        }

        [Theory(DisplayName = "Invalid Card Numbers")]
        [InlineData("some text")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("4111-1111-1111-1112")] // Invalid checksum
        [InlineData("4111 1111 1111 1113")] // Invalid checksum with spaces
        public void LuhnAlgorithmValidation_ShouldReturnFalse_ForInvalidCards(string? input) {
            // Act
            var result = _validator.IsValid(input);

            // Assert
            Assert.False(result, $"Expected '{input}' to be invalid.");
        }
    }
}
