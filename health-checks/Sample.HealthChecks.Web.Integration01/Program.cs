using HealthChecks.UI.Client;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks()
    .AddUrlGroup(new Uri("https://dummyjson.com/products"), "products")
    .AddUrlGroup(new Uri("https://dummyjson.com/carts"), "carts")
    .AddUrlGroup(new Uri("https://dummyjson.com/users"), "users")
    .AddUrlGroup(new Uri("https://dummyjson.com/posts"), "posts")
    .AddUrlGroup(new Uri("https://dummyjson.com/comments"), "comments")
    .AddUrlGroup(new Uri("https://dummyjson.com/quotes"), "quotes")
    .AddUrlGroup(new Uri("https://dummyjson.com/todos"), "todos");

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
