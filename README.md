# CardValidator
How to Run the API:
1. Clone the repository:
   git clone https://github.com/sirarthur2479/CardValidator.git
   cd .\CardValidator\CardValidator.Api\

2. Prepare packages:
   dotnet restore
   dotnet build
   dotnet run

3. Visit Swagger UI:Port number fixed on 5097. Can be changed in \CardValidator\CardValidator.Api\Properties\launchSettings.json

   http://localhost:5097/swagger

Send a POST request to /api/v1/cards/validate in swagger to test credit card number validtion

Example valid credit card number requests
{
  "cardNumber": "4111 1111 1111 1111"
}
{
  "cardNumber": "5431 1111 1111 1111"
}

Example Invalid credit card number request
{
  "cardNumber": "5431 1111 1111 1112"
}

Example request to force and exception response
{
  "cardNumber": "force-exception"
}
