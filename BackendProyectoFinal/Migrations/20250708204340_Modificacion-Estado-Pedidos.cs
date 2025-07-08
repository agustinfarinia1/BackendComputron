using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendProyectoFinal.Migrations
{
    /// <inheritdoc />
    public partial class ModificacionEstadoPedidos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EsPrimero",
                table: "EstadosDePedidos");

            migrationBuilder.DropColumn(
                name: "EsUltimo",
                table: "EstadosDePedidos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EsPrimero",
                table: "EstadosDePedidos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EsUltimo",
                table: "EstadosDePedidos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
