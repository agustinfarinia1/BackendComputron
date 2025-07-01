using BackendProyectoFinal.DTOs;
using BackendProyectoFinal.Models;
using BackendProyectoFinal.Repositories;
using BackendProyectoFinal.Services;
using BackendProyectoFinal.Utils;
using BackendProyectoFinal.Validators.CategoriaProducto;
using BackendProyectoFinal.Validators.Domicilio;
using BackendProyectoFinal.Validators.Rol;
using BackendProyectoFinal.Validators.Usuario;
using DotNetEnv;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Se agrega una config para que pueda convertir de JSON a DATEONLY
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// SERVICIOS
builder.Services.AddKeyedScoped<ICommonService<CategoriaProductoDTO, CategoriaProductoInsertDTO, CategoriaProductoUpdateDTO>, CategoriaProductoService>("CategoriaProductoService");
builder.Services.AddKeyedScoped<ICommonService<RolDTO, RolInsertDTO, RolUpdateDTO>, RolService>("RolService");
builder.Services.AddKeyedScoped<ICommonService<DomicilioDTO, DomicilioInsertDTO, DomicilioUpdateDTO>, DomicilioService>("DomicilioService");
builder.Services.AddKeyedScoped<ICommonService<UsuarioDTO, UsuarioInsertDTO, UsuarioUpdateDTO>, UsuarioService>("UsuarioService");

// REPOSITORY
builder.Services.AddScoped<IRepository<CategoriaProducto>, CategoriaProductoRepository>();
builder.Services.AddScoped<IRepository<Rol>, RolRepository>();
builder.Services.AddScoped<IRepository<Domicilio>, DomicilioRepository>();
builder.Services.AddScoped<IRepository<Usuario>, UsuarioRepository>();

// ENTITY FRAMEWORK
builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"));
});
// VALIDATORS
builder.Services.AddScoped<IValidator<CategoriaProductoInsertDTO>, CategoriaProductoInsertValidator>();
builder.Services.AddScoped<IValidator<CategoriaProductoUpdateDTO>, CategoriaProductoUpdateValidator>();
builder.Services.AddScoped<IValidator<RolInsertDTO>, RolInsertValidator>();
builder.Services.AddScoped<IValidator<RolUpdateDTO>, RolUpdateValidator>();
builder.Services.AddScoped<IValidator<DomicilioInsertDTO>, DomicilioInsertValidator>();
builder.Services.AddScoped<IValidator<DomicilioUpdateDTO>, DomicilioUpdateValidator>();
builder.Services.AddScoped<IValidator<UsuarioInsertDTO>, UsuarioInsertValidator>();
builder.Services.AddScoped<IValidator<UsuarioUpdateDTO>, UsuarioUpdateValidator>();

// .ENV

Env.Load();


builder.Services.Configure<EncryptConfiguration>(config =>
{
    config.PrivateKeyPassword1 = Environment.GetEnvironmentVariable("CLAVE_SECRETA1");
    config.PrivateKeyPassword2 = Environment.GetEnvironmentVariable("CLAVE_SECRETA2");
});

builder.Services.AddSingleton<Encrypt>();

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
