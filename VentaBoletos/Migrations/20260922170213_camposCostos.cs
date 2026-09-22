using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VentaBoletos.Migrations
{
    /// <inheritdoc />
    public partial class camposCostos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "IVA",
                table: "Boletos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioUnitario",
                table: "Boletos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Subtotal",
                table: "Boletos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IVA",
                table: "Boletos");

            migrationBuilder.DropColumn(
                name: "PrecioUnitario",
                table: "Boletos");

            migrationBuilder.DropColumn(
                name: "Subtotal",
                table: "Boletos");
        }
    }
}
