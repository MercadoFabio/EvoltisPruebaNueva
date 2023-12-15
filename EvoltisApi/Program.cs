using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Service.Configs;

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration.GetConnectionString("CatalogConnection ");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInternalServices();
builder.Services.AddConfig();
builder.Services.AddSwagger();


builder.Services.AddDbContext<CatalogContext>(options =>
{
    options.UseMySql(connection, Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.2.0-mysql"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
