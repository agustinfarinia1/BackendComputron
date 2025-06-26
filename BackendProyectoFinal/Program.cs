using BackendProyectoFinal.DTOs;
using BackendProyectoFinal.Models;
using BackendProyectoFinal.Repositories;
using BackendProyectoFinal.Services;
using CursoBackend01.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// SERVICIOS
builder.Services.AddKeyedScoped<ICommonService<CategoriaProductoDTO, CategoriaProductoInsertDTO, CategoriaProductoUpdateDTO>, CategoriaProductoService>("CategoriaProductoService");

// REPOSITORY
builder.Services.AddScoped<IRepository<CategoriaProducto>, CategoriaProductoRepository>();

// ENTITY FRAMEWORK
builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"));
});
// VALIDATORS
builder.Services.AddScoped<IValidator<CategoriaProductoInsertDTO>, CategoriaProductoInsertValidator>();
builder.Services.AddScoped<IValidator<CategoriaProductoUpdateDTO>, CategoriaProductoUpdateValidator>();

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
