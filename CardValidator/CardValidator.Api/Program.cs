using CardValidator.Api.Middleware;
using CardValidator.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options => {
        options.InvalidModelStateResponseFactory = context =>
        {
            var result = new BadRequestObjectResult(context.ModelState);
            result.ContentTypes.Add("application/json");
            return result;
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Card Validator API", Version = "v1" });
});

builder.Services.AddHealthChecks();

builder.Services.AddSingleton<ILuhnAlgorithmValidator, LuhnAlgorithmValidator>();

var app = builder.Build();

// Enable Swagger and exception
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Card Validator API v1");
    });
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseGlobalExceptionHandler();

app.UseRouting();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();

public partial class Program { } // For integration testing
