using System.Net;
using System.Net.Http.Json;
using CardValidator.Api;
using CardValidator.Api.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CardValidator.Tests
{
    public class IntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public IntegrationTests(WebApplicationFactory<Program> factory) {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task PostValidate_ReturnsOk_AndValidTrue_ForCorrectCardNumber() {
            // Arrange
            var request = new CreditCardValidationRequest { CardNumber = "5431 1111 1111 1111" };

            // Act
            var response = await _client.PostAsJsonAsync("/api/v1/cards/validate", request);
            response.EnsureSuccessStatusCode();

            // Assert
            var body = await response.Content.ReadFromJsonAsync<CreditCardValidationResponse>();
            Assert.NotNull(body);
            Assert.True(body.IsValid, "Expected card to be valid.");
            Assert.Contains("valid", body.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task PostValidate_ReturnsBadRequest_ForInvalidModel() {
            // Arrange
            var request = new { CardNumber = "" };
            // Act
            var response = await _client.PostAsJsonAsync("/api/v1/cards/validate", request);
            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PostValidate_ReturnsInternalServerError_WhenExceptionIsForced() {
            // Arrange
            var request = new { CardNumber = "force-exception" };
            // Act
            var response = await _client.PostAsJsonAsync("/api/v1/cards/validate", request);
            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }
    }
}
