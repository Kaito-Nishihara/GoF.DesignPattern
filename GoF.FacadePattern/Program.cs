using GoF.FacadePattern.External;
using GoF.FacadePattern.Facade;
using GoF.FacadePattern.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ˆË‘¶«’“ü
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IExternalProductService, ExternalProductService>();
builder.Services.AddScoped<IProductsFacade, ProductsFacade>();

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();

app.Run();
