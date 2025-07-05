using DotNetEnv;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
// Local
using BackendProyectoFinal.Configurations;
using BackendProyectoFinal.Models;
using BackendProyectoFinal.Repositories;
using BackendProyectoFinal.Services;
// DTOs
using BackendProyectoFinal.DTOs.RolDTO;
using BackendProyectoFinal.DTOs.DomicilioDTO;
using BackendProyectoFinal.DTOs.UsuarioDTO;
using BackendProyectoFinal.DTOs.CategoriaProductoDTO;
using BackendProyectoFinal.DTOs.MarcaDTO;
using BackendProyectoFinal.DTOs.ProductoDTO;
using BackendProyectoFinal.DTOs.CarritoDTO;
using BackendProyectoFinal.DTOs.EstadoPedidoDTO;
using BackendProyectoFinal.DTOs.PedidoDTO;
//Validators
using BackendProyectoFinal.Validators.Domicilio;
using BackendProyectoFinal.Validators.Rol;
using BackendProyectoFinal.Validators.Usuario;
using BackendProyectoFinal.Validators.CategoriaProducto;
using BackendProyectoFinal.Validators.Marca;
using BackendProyectoFinal.Validators.Producto;
using BackendProyectoFinal.Validators.Carrito;
using BackendProyectoFinal.Validators.EstadoPedido;
using BackendProyectoFinal.Validators.Pedido;

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
builder.Services.AddKeyedScoped<ICommonService<CarritoDTO, CarritoInsertDTO, CarritoUpdateDTO>, CarritoService>("CarritoService");
builder.Services.AddKeyedScoped<ICommonService<EstadoPedidoDTO, EstadoPedidoInsertDTO, EstadoPedidoUpdateDTO>, EstadoPedidoService>("EstadoPedidoService");
builder.Services.AddKeyedScoped<ICommonService<PedidoDTO, PedidoInsertDTO, PedidoUpdateDTO>, PedidoService>("PedidoService");
builder.Services.AddKeyedScoped<EncryptService>("EncryptService");

// REPOSITORY
builder.Services.AddScoped<IRepository<CategoriaProducto>, CategoriaProductoRepository>();
builder.Services.AddScoped<IRepository<Rol>, RolRepository>();
builder.Services.AddScoped<IRepository<Domicilio>, DomicilioRepository>();
builder.Services.AddScoped<IRepository<Usuario>, UsuarioRepository>();
builder.Services.AddScoped<IRepository<Marca>, MarcaRepository>();
builder.Services.AddScoped<IRepository<Producto>, ProductoRepository>();
builder.Services.AddScoped<IRepository<Carrito>, CarritoRepository>();
builder.Services.AddScoped<IRepository<EstadoPedido>, EstadoPedidoRepository>();
builder.Services.AddScoped<IRepository<Pedido>, PedidoRepository>();

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
builder.Services.AddScoped<IValidator<CarritoInsertDTO>, CarritoInsertValidator>();
builder.Services.AddScoped<IValidator<CarritoUpdateDTO>, CarritoUpdateValidator>();
builder.Services.AddScoped<IValidator<EstadoPedidoInsertDTO>, EstadoPedidoInsertValidator>();
builder.Services.AddScoped<IValidator<EstadoPedidoUpdateDTO>, EstadoPedidoUpdateValidator>();
builder.Services.AddScoped<IValidator<PedidoInsertDTO>, PedidoInsertValidator>();
builder.Services.AddScoped<IValidator<PedidoUpdateDTO>, PedidoUpdateValidator>();


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
