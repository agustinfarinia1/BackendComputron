using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendProyectoFinal.Migrations
{
    /// <inheritdoc />
    public partial class ChangeOrderStatusNextOrderStatusID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderStatuses_OrderStatuses_NextStatusOrderID",
                table: "OrderStatuses");

            migrationBuilder.RenameColumn(
                name: "NextStatusOrderID",
                table: "OrderStatuses",
                newName: "NextOrderStatusID");

            migrationBuilder.RenameIndex(
                name: "IX_OrderStatuses_NextStatusOrderID",
                table: "OrderStatuses",
                newName: "IX_OrderStatuses_NextOrderStatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderStatuses_OrderStatuses_NextOrderStatusID",
                table: "OrderStatuses",
                column: "NextOrderStatusID",
                principalTable: "OrderStatuses",
                principalColumn: "OrderStatusID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderStatuses_OrderStatuses_NextOrderStatusID",
                table: "OrderStatuses");

            migrationBuilder.RenameColumn(
                name: "NextOrderStatusID",
                table: "OrderStatuses",
                newName: "NextStatusOrderID");

            migrationBuilder.RenameIndex(
                name: "IX_OrderStatuses_NextOrderStatusID",
                table: "OrderStatuses",
                newName: "IX_OrderStatuses_NextStatusOrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderStatuses_OrderStatuses_NextStatusOrderID",
                table: "OrderStatuses",
                column: "NextStatusOrderID",
                principalTable: "OrderStatuses",
                principalColumn: "OrderStatusID");
        }
    }
}
