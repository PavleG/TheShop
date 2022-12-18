using Shop.WebApi.Configurations;
using Shop.WebApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<Dealer1Settings>(builder.Configuration.GetSection(nameof(Dealer1Settings)));
builder.Services.Configure<Dealer2Settings>(builder.Configuration.GetSection(nameof(Dealer2Settings)));

builder.Services.AddScoped<Db>();
builder.Services.AddHttpClient<IArticleRepository, Dealer1>();
builder.Services.AddHttpClient<IArticleRepository, Dealer2>();
builder.Services.AddScoped<IArticleRepository, Warehouse>();
builder.Services.Decorate<IArticleRepository, CachedSupplier>();

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
