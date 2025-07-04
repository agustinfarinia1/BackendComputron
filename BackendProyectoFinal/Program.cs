using BackendProyectoFinal.Configurations;
using BackendProyectoFinal.DTOs;
using BackendProyectoFinal.Models;
using BackendProyectoFinal.Repositories;
using BackendProyectoFinal.Services;
using BackendProyectoFinal.Validators.CategoriaProducto;
using BackendProyectoFinal.Validators.Domicilio;
using BackendProyectoFinal.Validators.Marca;
using BackendProyectoFinal.Validators.Producto;
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

Env.Load();
// Config del Encrypter, trae los valores de .Env
builder.Services.Configure<EncryptConfiguration>(config =>
{
    config.PrivateKeyPassword1 = Environment.GetEnvironmentVariable("CLAVE_SECRETA1");
    config.PrivateKeyPassword2 = Environment.GetEnvironmentVariable("CLAVE_SECRETA2");
    config.Salt = Environment.GetEnvironmentVariable("SALT");
});

// SERVICIOS
builder.Services.AddKeyedScoped<ICommonService<CategoriaProductoDTO, CategoriaProductoInsertDTO, CategoriaProductoUpdateDTO>, CategoriaProductoService>("CategoriaProductoService");
builder.Services.AddKeyedScoped<ICommonService<RolDTO, RolInsertDTO, RolUpdateDTO>, RolService>("RolService");
builder.Services.AddKeyedScoped<ICommonService<DomicilioDTO, DomicilioInsertDTO, DomicilioUpdateDTO>, DomicilioService>("DomicilioService");
builder.Services.AddKeyedScoped<ICommonService<UsuarioDTO, UsuarioInsertDTO, UsuarioUpdateDTO>, UsuarioService>("UsuarioService");
builder.Services.AddKeyedScoped<ICommonService<MarcaDTO, MarcaInsertDTO, MarcaUpdateDTO>, MarcaService>("MarcaService");
builder.Services.AddKeyedScoped<ICommonService<ProductoDTO, ProductoInsertDTO, ProductoUpdateDTO>, ProductoService>("ProductoService");
builder.Services.AddKeyedScoped<EncryptService>("EncryptService");

// REPOSITORY
builder.Services.AddScoped<IRepository<CategoriaProducto>, CategoriaProductoRepository>();
builder.Services.AddScoped<IRepository<Rol>, RolRepository>();
builder.Services.AddScoped<IRepository<Domicilio>, DomicilioRepository>();
builder.Services.AddScoped<IRepository<Usuario>, UsuarioRepository>();
builder.Services.AddScoped<IRepository<Marca>, MarcaRepository>();
builder.Services.AddScoped<IRepository<Producto>, ProductoRepository>();

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
builder.Services.AddScoped<IValidator<MarcaInsertDTO>, MarcaInsertValidator>();
builder.Services.AddScoped<IValidator<MarcaUpdateDTO>, MarcaUpdateValidator>();
builder.Services.AddScoped<IValidator<ProductoInsertDTO>, ProductoInsertValidator>();
builder.Services.AddScoped<IValidator<ProductoUpdateDTO>, ProductoUpdateValidator>();

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
