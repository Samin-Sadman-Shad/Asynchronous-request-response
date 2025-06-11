using AsyncProductAPI.Dtos;
using AsyncProductAPI.Models;
using AsyncProductAPI.Persistance;
using AsyncProductAPI.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using AsyncProductAPI.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnectionString")));
builder.Services.AddScoped<ProductRepository, ProductRepository>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
#region old

/*var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")do
.WithOpenApi();*/
#endregion

//start endpoint
app.MapPost("api/v1/products", async (ProductRepository repository, ListingRequest request) =>
{
    if (request == null)
    {
        return Results.BadRequest();
    }

    ListProduct product = new ListProduct
    {
        ProductDetails = request.RequestBody
    };

    await repository.AddProducts(product);

    Guid requestId = Guid.NewGuid();

    ListingResponse response = new ListingResponse
    {
        RequestStatus = "ACCEPTED",
        EstimatedCompletionTime =  DateTime.Now.AddMinutes(60),
        RequestId = requestId
    };

    return Results.Accepted($"api/v1/product-status/{requestId}", response);

});

//status endpoint
app.MapGet("api/v1/product-status/{requestId}", async (ProductRepository repository, string requestId) =>
{
    var product = await repository.GetProductsByRequestId(Guid.Parse(requestId));
    if(product == null)
    {
        return Results.NotFound();
    }

    ListingResponse response = new ListingResponse
    {
        RequestStatus = product.RequestStatus,
        RequestId = Guid.Parse(requestId)
    };

    if(product.RequestStatus == Constants.COMPLETED)
    {
        response.RequestStatus = Constants.COMPLETED;
        response.FinalEndpoint = new Uri($"{Constants.finalEndpoint}/{product.Id}");
        return Results.Created(response.FinalEndpoint, response);
    }

    return Results.Ok(response);
});

app.Run();

/*internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}*/
