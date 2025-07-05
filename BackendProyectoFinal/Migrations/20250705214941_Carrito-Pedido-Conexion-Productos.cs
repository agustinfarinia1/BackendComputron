using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendProyectoFinal.Migrations
{
    /// <inheritdoc />
    public partial class CarritoPedidoConexionProductos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriaProductos",
                columns: table => new
                {
                    CategoriaProductoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaProductos", x => x.CategoriaProductoID);
                });

            migrationBuilder.CreateTable(
                name: "Domicilios",
                columns: table => new
                {
                    DomicilioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Piso = table.Column<int>(type: "int", nullable: true),
                    Departamento = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domicilios", x => x.DomicilioID);
                });

            migrationBuilder.CreateTable(
                name: "EstadosDePedido",
                columns: table => new
                {
                    EstadoPedidoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosDePedido", x => x.EstadoPedidoID);
                });

            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    MarcaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.MarcaID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RolID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RolID);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    ProductoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoML = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarcaID = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateOnly>(type: "date", nullable: false),
                    CategoriaProductoID = table.Column<int>(type: "int", nullable: false),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.ProductoID);
                    table.ForeignKey(
                        name: "FK_Productos_CategoriaProductos_CategoriaProductoID",
                        column: x => x.CategoriaProductoID,
                        principalTable: "CategoriaProductos",
                        principalColumn: "CategoriaProductoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Productos_Marcas_MarcaID",
                        column: x => x.MarcaID,
                        principalTable: "Marcas",
                        principalColumn: "MarcaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaNacimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    RolID = table.Column<int>(type: "int", nullable: false),
                    DomicilioID = table.Column<int>(type: "int", nullable: false),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioID);
                    table.ForeignKey(
                        name: "FK_Usuarios_Domicilios_DomicilioID",
                        column: x => x.DomicilioID,
                        principalTable: "Domicilios",
                        principalColumn: "DomicilioID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_RolID",
                        column: x => x.RolID,
                        principalTable: "Roles",
                        principalColumn: "RolID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carritos",
                columns: table => new
                {
                    CarritoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carritos", x => x.CarritoID);
                    table.ForeignKey(
                        name: "FK_Carritos_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    PedidoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioID = table.Column<int>(type: "int", nullable: false),
                    EstadoPedidoID = table.Column<int>(type: "int", nullable: false),
                    DomicilioID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.PedidoID);
                    table.ForeignKey(
                        name: "FK_Pedidos_Domicilios_DomicilioID",
                        column: x => x.DomicilioID,
                        principalTable: "Domicilios",
                        principalColumn: "DomicilioID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pedidos_EstadosDePedido_EstadoPedidoID",
                        column: x => x.EstadoPedidoID,
                        principalTable: "EstadosDePedido",
                        principalColumn: "EstadoPedidoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pedidos_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemCarrito",
                columns: table => new
                {
                    ItemCarritoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    CarritoID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCarrito", x => x.ItemCarritoID);
                    table.ForeignKey(
                        name: "FK_ItemCarrito_Carritos_CarritoID",
                        column: x => x.CarritoID,
                        principalTable: "Carritos",
                        principalColumn: "CarritoID");
                    table.ForeignKey(
                        name: "FK_ItemCarrito_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "ProductoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemPedido",
                columns: table => new
                {
                    ItemPedidoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PedidoID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPedido", x => x.ItemPedidoID);
                    table.ForeignKey(
                        name: "FK_ItemPedido_Pedidos_PedidoID",
                        column: x => x.PedidoID,
                        principalTable: "Pedidos",
                        principalColumn: "PedidoID");
                    table.ForeignKey(
                        name: "FK_ItemPedido_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "ProductoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carritos_UsuarioID",
                table: "Carritos",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCarrito_CarritoID",
                table: "ItemCarrito",
                column: "CarritoID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCarrito_ProductoId",
                table: "ItemCarrito",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedido_PedidoID",
                table: "ItemPedido",
                column: "PedidoID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedido_ProductoId",
                table: "ItemPedido",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_DomicilioID",
                table: "Pedidos",
                column: "DomicilioID");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_EstadoPedidoID",
                table: "Pedidos",
                column: "EstadoPedidoID");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_UsuarioID",
                table: "Pedidos",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CategoriaProductoID",
                table: "Productos",
                column: "CategoriaProductoID");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_MarcaID",
                table: "Productos",
                column: "MarcaID");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_DomicilioID",
                table: "Usuarios",
                column: "DomicilioID");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_RolID",
                table: "Usuarios",
                column: "RolID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemCarrito");

            migrationBuilder.DropTable(
                name: "ItemPedido");

            migrationBuilder.DropTable(
                name: "Carritos");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "EstadosDePedido");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "CategoriaProductos");

            migrationBuilder.DropTable(
                name: "Marcas");

            migrationBuilder.DropTable(
                name: "Domicilios");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
