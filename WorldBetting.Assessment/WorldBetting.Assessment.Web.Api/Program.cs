using Microsoft.OpenApi.Models;
using Serilog;
using WorldBetting.Assessment.Web.Api;
using WoldBetting.Assessment.Models;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
// Load configurations
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

builder.Configuration.AddConfiguration(configuration);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

// Add Serilog to the logging providers
builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

// Bind the HttpClientSettings section
builder.Services.Configure<HttpClientSettings>(
    builder.Configuration.GetSection("HttpClientSettings")
);

// Register the configuration object directly
builder.Services.AddSingleton(resolver =>
    resolver.GetRequiredService<IOptions<HttpClientSettings>>().Value);

// Add and configure the HttpClient
builder.Services.AddHttpClient("ExchangeRateApi", (serviceProvider, client) =>
{
    var config = serviceProvider.GetRequiredService<HttpClientSettings>().ExchangeRateSettingsConfig;

    if (config != null)
    {
        client.BaseAddress = new Uri(config.BaseAddress);
        client.DefaultRequestHeaders.Add("Accept", config.Accept);
    }
    else
    {
        throw new InvalidOperationException("CurrencyConverterConfig is not configured properly.");
    }

});


// swagger service in development mode
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = " World-Betting Assessment Currencry Converter Web API v1",
        Description = "World-Betting Web API  v1 "

    });
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddCurerencyConverterService();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        c.RouteTemplate = "/swagger/{documentName}/swagger.json";

    });
    app.UseSwaggerUI(c =>
    {

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "World-Betting Assessment Currencry Converter Web API v1");

        });

        c.RoutePrefix = string.Empty;
    });
    app.UseDeveloperExceptionPage();

}


// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();

Log.CloseAndFlush();
