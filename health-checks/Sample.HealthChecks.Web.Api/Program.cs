using HealthChecks.UI.Client;
using Sample.HealthChecks.Web.Api.Configurations;
using System.Reflection;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddJsonFile("environments.json")
    .Build();
var environmentSettings = configuration
    .GetSection(EnvironmentSettings.SectionName)
    .Get<EnvironmentSettings>();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddHealthChecksUI(setup => setup
    .AddHealthCheckEndpoint("dummyjson.com", new Uri(environmentSettings.Integration01BaseUrl, "health").ToString())
    .AddHealthCheckEndpoint("jsonplaceholder.typicode.com", new Uri(environmentSettings.Integration02BaseUrl, "health").ToString())
    .AddHealthCheckEndpoint("pokeapi.co", new Uri(environmentSettings.Integration03BaseUrl, "health").ToString())
    .SetEvaluationTimeInSeconds(30))
   .AddInMemoryStorage();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/system", () =>
    Assembly.GetExecutingAssembly().FullName)
.WithName("GetSystemName");
app.MapHealthChecks("/health", new()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapHealthChecksUI();

app.Run();
