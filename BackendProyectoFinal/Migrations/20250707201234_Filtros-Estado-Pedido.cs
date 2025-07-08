using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendProyectoFinal.Migrations
{
    /// <inheritdoc />
    public partial class FiltrosEstadoPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EsPrimero",
                table: "EstadosDePedido",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EsUltimo",
                table: "EstadosDePedido",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "EstadoSiguienteID",
                table: "EstadosDePedido",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EsPrimero",
                table: "EstadosDePedido");

            migrationBuilder.DropColumn(
                name: "EsUltimo",
                table: "EstadosDePedido");

            migrationBuilder.DropColumn(
                name: "EstadoSiguienteID",
                table: "EstadosDePedido");
        }
    }
}
