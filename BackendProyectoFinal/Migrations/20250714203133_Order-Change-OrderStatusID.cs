using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendProyectoFinal.Migrations
{
    /// <inheritdoc />
    public partial class OrderChangeOrderStatusID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderStatuses_StatusdOrderID",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "StatusdOrderID",
                table: "Orders",
                newName: "OrderStatusID");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_StatusdOrderID",
                table: "Orders",
                newName: "IX_Orders_OrderStatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderStatuses_OrderStatusID",
                table: "Orders",
                column: "OrderStatusID",
                principalTable: "OrderStatuses",
                principalColumn: "OrderStatusID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderStatuses_OrderStatusID",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "OrderStatusID",
                table: "Orders",
                newName: "StatusdOrderID");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_OrderStatusID",
                table: "Orders",
                newName: "IX_Orders_StatusdOrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderStatuses_StatusdOrderID",
                table: "Orders",
                column: "StatusdOrderID",
                principalTable: "OrderStatuses",
                principalColumn: "OrderStatusID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
