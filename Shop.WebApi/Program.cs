using Shop.WebApi.Configurations;
using Shop.WebApi.Repositories;
using Shop.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddOptions<Vendor1Settings>()
    .BindConfiguration(nameof(Vendor1Settings))
    .ValidateDataAnnotations();
builder.Services.AddOptions<Vendor2Settings>()
    .BindConfiguration(nameof(Vendor2Settings))
    .ValidateDataAnnotations();

builder.Services.AddScoped<ISalesRepository, SalesRepository>();
builder.Services.AddScoped<IArticleProvider, Warehouse>();

builder.Services.AddHttpClient<IArticleProvider, VendorClient<Vendor1Settings>>();
builder.Services.AddHttpClient<IArticleProvider, VendorClient<Vendor2Settings>>();

builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.Decorate<ISupplierService, CachedSupplier>();
builder.Services.AddMemoryCache();
builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
