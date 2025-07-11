using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendProyectoFinal.Migrations
{
    /// <inheritdoc />
    public partial class StoreContextUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemCart_Carts_CartID",
                table: "ItemCart");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemCart_Products_ProductID",
                table: "ItemCart");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemOrder_Orders_OrderID",
                table: "ItemOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemOrder_Products_ProductID",
                table: "ItemOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemOrder",
                table: "ItemOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemCart",
                table: "ItemCart");

            migrationBuilder.RenameTable(
                name: "ItemOrder",
                newName: "ItemsOrders");

            migrationBuilder.RenameTable(
                name: "ItemCart",
                newName: "ItemsCarts");

            migrationBuilder.RenameIndex(
                name: "IX_ItemOrder_ProductID",
                table: "ItemsOrders",
                newName: "IX_ItemsOrders_ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_ItemOrder_OrderID",
                table: "ItemsOrders",
                newName: "IX_ItemsOrders_OrderID");

            migrationBuilder.RenameIndex(
                name: "IX_ItemCart_ProductID",
                table: "ItemsCarts",
                newName: "IX_ItemsCarts_ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_ItemCart_CartID",
                table: "ItemsCarts",
                newName: "IX_ItemsCarts_CartID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemsOrders",
                table: "ItemsOrders",
                column: "ItemOrderID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemsCarts",
                table: "ItemsCarts",
                column: "ItemCartID");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsCarts_Carts_CartID",
                table: "ItemsCarts",
                column: "CartID",
                principalTable: "Carts",
                principalColumn: "CartID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsCarts_Products_ProductID",
                table: "ItemsCarts",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsOrders_Orders_OrderID",
                table: "ItemsOrders",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsOrders_Products_ProductID",
                table: "ItemsOrders",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemsCarts_Carts_CartID",
                table: "ItemsCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemsCarts_Products_ProductID",
                table: "ItemsCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemsOrders_Orders_OrderID",
                table: "ItemsOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemsOrders_Products_ProductID",
                table: "ItemsOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemsOrders",
                table: "ItemsOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemsCarts",
                table: "ItemsCarts");

            migrationBuilder.RenameTable(
                name: "ItemsOrders",
                newName: "ItemOrder");

            migrationBuilder.RenameTable(
                name: "ItemsCarts",
                newName: "ItemCart");

            migrationBuilder.RenameIndex(
                name: "IX_ItemsOrders_ProductID",
                table: "ItemOrder",
                newName: "IX_ItemOrder_ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_ItemsOrders_OrderID",
                table: "ItemOrder",
                newName: "IX_ItemOrder_OrderID");

            migrationBuilder.RenameIndex(
                name: "IX_ItemsCarts_ProductID",
                table: "ItemCart",
                newName: "IX_ItemCart_ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_ItemsCarts_CartID",
                table: "ItemCart",
                newName: "IX_ItemCart_CartID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemOrder",
                table: "ItemOrder",
                column: "ItemOrderID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemCart",
                table: "ItemCart",
                column: "ItemCartID");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemCart_Carts_CartID",
                table: "ItemCart",
                column: "CartID",
                principalTable: "Carts",
                principalColumn: "CartID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemCart_Products_ProductID",
                table: "ItemCart",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemOrder_Orders_OrderID",
                table: "ItemOrder",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemOrder_Products_ProductID",
                table: "ItemOrder",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
