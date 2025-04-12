using Persistence.EventStore;
using Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IEventStore, InMemoryEventStore>();
builder.Services.AddTransient<OrderRepository>();
builder.Services.AddTransient<ShippingRepository>();

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
