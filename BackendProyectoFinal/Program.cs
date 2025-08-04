using DotNetEnv;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
// Local
using BackendProyectoFinal.Configurations;
using BackendProyectoFinal.Models;
using BackendProyectoFinal.Repositories;
using BackendProyectoFinal.Services;
// DTOs
using BackendProyectoFinal.DTOs.Address;
using BackendProyectoFinal.DTOs.User;
using BackendProyectoFinal.DTOs.User.Role;
using BackendProyectoFinal.DTOs.Product;
using BackendProyectoFinal.DTOs.Product.Brand;
using BackendProyectoFinal.DTOs.Product.Category;
using BackendProyectoFinal.DTOs.Cart;
using BackendProyectoFinal.DTOs.Cart.ItemCart;
using BackendProyectoFinal.DTOs.Order;
using BackendProyectoFinal.DTOs.Order.ItemOrder;
using BackendProyectoFinal.DTOs.Order.OrderStatus;
using BackendProyectoFinal.DTOs.Payment;
using BackendProyectoFinal.DTOs.Payment.PaymentMethod;
using BackendProyectoFinal.DTOs.Payment.PaymentDetail;
//Validators
using BackendProyectoFinal.Validators.Address;
using BackendProyectoFinal.Validators.Role;
using BackendProyectoFinal.Validators.User;
using BackendProyectoFinal.Validators.Category;
using BackendProyectoFinal.Validators.Brand;
using BackendProyectoFinal.Validators.Product;
using BackendProyectoFinal.Validators.Cart;
using BackendProyectoFinal.Validators.Order;
using BackendProyectoFinal.Validators.Payment;

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
    config.PrivateKey1 = Environment.GetEnvironmentVariable("SECRET_KEY1");
    config.PrivateKey2 = Environment.GetEnvironmentVariable("SECRET_KEY2");
    config.Salt = Environment.GetEnvironmentVariable("SALT");
});

// SERVICIOS
builder.Services.AddKeyedScoped<EncryptService>("EncryptService");
builder.Services.AddKeyedScoped<ICommonService<CategoryDTO, CategoryInsertDTO, CategoryUpdateDTO>, CategoryService>("CategoryService");
builder.Services.AddKeyedScoped<ICommonService<RoleDTO, RoleInsertDTO, RoleUpdateDTO>, RoleService>("RoleService");
builder.Services.AddKeyedScoped<ICommonService<AddressDTO, AddressInsertDTO, AddressUpdateDTO>, AddressService>("AddressService");
builder.Services.AddKeyedScoped<ICommonService<UserDTO, UserInsertDTO, UserUpdateDTO>, UserService>("UserService");
builder.Services.AddKeyedScoped<ICommonService<BrandDTO, BrandInsertDTO, BrandUpdateDTO>, BrandService>("BrandService");
builder.Services.AddKeyedScoped<ICommonService<ProductDTO, ProductInsertDTO, ProductUpdateDTO>, ProductService>("ProductService");
builder.Services.AddKeyedScoped<IListService<CartDTO, CartInsertDTO, CartUpdateDTO>, CartService>("CartService");
builder.Services.AddKeyedScoped<IItemListService<ItemCartDTO,ItemCartInsertDTO,ItemCartUpdateDTO>, ItemCartService > ("ItemCartService");
builder.Services.AddKeyedScoped<IOrderStatusService, OrderStatusService>("OrderStatusService");
builder.Services.AddKeyedScoped<IListService<OrderDTO, OrderInsertDTO, OrderUpdateDTO>, OrderService>("OrderService");
builder.Services.AddKeyedScoped<IItemListService<ItemOrderDTO, ItemOrderInsertDTO, ItemOrderUpdateDTO>, ItemOrderService>("ItemOrderService");
builder.Services.AddKeyedScoped<ICommonService<PaymentMethodDTO, PaymentMethodInsertDTO, PaymentMethodUpdateDTO>, PaymentMethodService>("PaymentMethodService");
builder.Services.AddKeyedScoped<ICommonService<PaymentDTO, PaymentInsertDTO, PaymentUpdateDTO>, PaymentService>("PaymentService");
builder.Services.AddKeyedScoped<ICommonService<PaymentDetailDTO, PaymentDetailInsertDTO, PaymentDetailUpdateDTO>, PaymentDetailService>("PaymentDetailService");

// REPOSITORY
builder.Services.AddScoped<IRepository<Category>, CategoryRepository>();
builder.Services.AddScoped<IRepository<Role>, RoleRepository>();
builder.Services.AddScoped<IRepository<Address>, AddressRepository>();
builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IRepository<Brand>, BrandRepository>();
builder.Services.AddScoped<IRepository<Product>, ProductRepository>();
builder.Services.AddScoped<IRepository<Cart>, CartRepository>();
builder.Services.AddScoped<IRepository<OrderStatus>, OrderStatusRepository>();
builder.Services.AddScoped<IRepository<Order>, OrderRepository>();
builder.Services.AddScoped<IRepository<ItemCart>, ItemCartRepository>();
builder.Services.AddScoped<IRepository<ItemOrder>, ItemOrderRepository>();
builder.Services.AddScoped<IRepository<PaymentMethod>, PaymentMethodRepository>();
builder.Services.AddScoped<IRepository<Payment>, PaymentRepository>();
builder.Services.AddScoped<IRepository<PaymentDetail>, PaymentDetailRepository>();

// ENTITY FRAMEWORK
builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"));
});
// VALIDATORS
builder.Services.AddScoped<IValidator<CategoryInsertDTO>, CategoryInsertValidator>();
builder.Services.AddScoped<IValidator<CategoryUpdateDTO>, CategoryUpdateValidator>();
builder.Services.AddScoped<IValidator<RoleInsertDTO>, RoleInsertValidator>();
builder.Services.AddScoped<IValidator<RoleUpdateDTO>, RoleUpdateValidator>();
builder.Services.AddScoped<IValidator<AddressInsertDTO>, AddressInsertValidator>();
builder.Services.AddScoped<IValidator<AddressUpdateDTO>, AddressUpdateValidator>();
builder.Services.AddScoped<IValidator<UserInsertDTO>, UserInsertValidator>();
builder.Services.AddScoped<IValidator<UserUpdateDTO>, UserUpdateValidator>();
builder.Services.AddScoped<IValidator<BrandInsertDTO>, BrandInsertValidator>();
builder.Services.AddScoped<IValidator<BrandUpdateDTO>, BrandUpdateValidator>();
builder.Services.AddScoped<IValidator<ProductInsertDTO>, ProductInsertValidator>();
builder.Services.AddScoped<IValidator<ProductUpdateDTO>, ProductUpdateValidator>();
builder.Services.AddScoped<IValidator<CartInsertDTO>, CartInsertValidator>();
builder.Services.AddScoped<IValidator<CartUpdateDTO>, CartUpdateValidator>();
builder.Services.AddScoped<IValidator<OrderStatusInsertDTO>, OrderStatusInsertValidator>();
builder.Services.AddScoped<IValidator<OrderStatusUpdateDTO>, OrderStatusUpdateValidator>();
builder.Services.AddScoped<IValidator<OrderInsertDTO>, OrderInsertValidator>();
builder.Services.AddScoped<IValidator<OrderUpdateDTO>, OrderUpdateValidator>();
builder.Services.AddScoped<IValidator<ItemCartInsertDTO>, ItemCartInsertValidator>();
builder.Services.AddScoped<IValidator<ItemCartUpdateDTO>, ItemCartUpdateValidator>();
builder.Services.AddScoped<IValidator<ItemOrderInsertDTO>, ItemOrderInsertValidator>();
builder.Services.AddScoped<IValidator<ItemOrderUpdateDTO>, ItemOrderUpdateValidator>();
builder.Services.AddScoped<IValidator<PaymentMethodInsertDTO>, PaymentMethodInsertValidator>();
builder.Services.AddScoped<IValidator<PaymentMethodUpdateDTO>, PaymentMethodUpdateValidator>();
builder.Services.AddScoped<IValidator<PaymentInsertDTO>, PaymentInsertValidator>();
builder.Services.AddScoped<IValidator<PaymentUpdateDTO>, PaymentUpdateValidator>();
builder.Services.AddScoped<IValidator<PaymentDetailInsertDTO>, PaymentDetailInsertValidator>();
builder.Services.AddScoped<IValidator<PaymentDetailUpdateDTO>, PaymentDetailUpdateValidator>();


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
