using HealthChecks.UI.Client;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks()
    .AddUrlGroup(new Uri("https://pokeapi.co/api/v2/ability"), "ability")
    .AddUrlGroup(new Uri("https://pokeapi.co/api/v2/characteristic"), "characteristic")
    .AddUrlGroup(new Uri("https://pokeapi.co/api/v2/egg-group"), "egg-group")
    .AddUrlGroup(new Uri("https://pokeapi.co/api/v2/gender"), "gender")
    .AddUrlGroup(new Uri("https://pokeapi.co/api/v2/pokemon"), "pokemon")
    .AddUrlGroup(new Uri("https://pokeapi.co/api/v2/generation"), "generation");

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

app.Run();
