using HealthChecks.UI.Client;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks()
    .AddUrlGroup(new Uri("https://jsonplaceholder.typicode.com/posts"), "posts")
    .AddUrlGroup(new Uri("https://jsonplaceholder.typicode.com/comments"), "comments")
    .AddUrlGroup(new Uri("https://jsonplaceholder.typicode.com/albums"), "albums")
    .AddUrlGroup(new Uri("https://jsonplaceholder.typicode.com/photos"), "photos")
    .AddUrlGroup(new Uri("https://jsonplaceholder.typicode.com/todos"), "todos")
    .AddUrlGroup(new Uri("https://jsonplaceholder.typicode.com/users"), "users");

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
