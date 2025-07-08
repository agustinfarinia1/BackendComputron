using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendProyectoFinal.Migrations
{
    /// <inheritdoc />
    public partial class EstadosDePedidos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_EstadosDePedido_EstadoPedidoID",
                table: "Pedidos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EstadosDePedido",
                table: "EstadosDePedido");

            migrationBuilder.RenameTable(
                name: "EstadosDePedido",
                newName: "EstadosDePedidos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EstadosDePedidos",
                table: "EstadosDePedidos",
                column: "EstadoPedidoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_EstadosDePedidos_EstadoPedidoID",
                table: "Pedidos",
                column: "EstadoPedidoID",
                principalTable: "EstadosDePedidos",
                principalColumn: "EstadoPedidoID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_EstadosDePedidos_EstadoPedidoID",
                table: "Pedidos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EstadosDePedidos",
                table: "EstadosDePedidos");

            migrationBuilder.RenameTable(
                name: "EstadosDePedidos",
                newName: "EstadosDePedido");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EstadosDePedido",
                table: "EstadosDePedido",
                column: "EstadoPedidoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_EstadosDePedido_EstadoPedidoID",
                table: "Pedidos",
                column: "EstadoPedidoID",
                principalTable: "EstadosDePedido",
                principalColumn: "EstadoPedidoID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
