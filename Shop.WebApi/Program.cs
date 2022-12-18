using Shop.WebApi.Configurations;
using Shop.WebApi.Repositories;
using Shop.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<Dealer1Settings>(builder.Configuration.GetSection(nameof(Dealer1Settings)));
builder.Services.Configure<Dealer2Settings>(builder.Configuration.GetSection(nameof(Dealer2Settings)));

builder.Services.AddScoped<Db>();
builder.Services.AddScoped<IArticleRepository, Warehouse>();
builder.Services.AddHttpClient<IArticleRepository, Vendor<Dealer1Settings>>();
builder.Services.AddHttpClient<IArticleRepository, Vendor<Dealer2Settings>>();

//builder.Services.Decorate<IArticleRepository, CachedSupplier>();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<ISupplierService, SupplierService>();
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
