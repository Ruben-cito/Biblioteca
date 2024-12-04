using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteca.Migrations
{
    /// <inheritdoc />
    public partial class cambioSancion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sanciones_Prestamos_PrestamoId",
                table: "Sanciones");

            migrationBuilder.DropColumn(
                name: "Idprestamo",
                table: "Sanciones");

            migrationBuilder.AlterColumn<int>(
                name: "PrestamoId",
                table: "Sanciones",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sanciones_Prestamos_PrestamoId",
                table: "Sanciones",
                column: "PrestamoId",
                principalTable: "Prestamos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sanciones_Prestamos_PrestamoId",
                table: "Sanciones");

            migrationBuilder.AlterColumn<int>(
                name: "PrestamoId",
                table: "Sanciones",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "Idprestamo",
                table: "Sanciones",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Sanciones_Prestamos_PrestamoId",
                table: "Sanciones",
                column: "PrestamoId",
                principalTable: "Prestamos",
                principalColumn: "Id");
        }
    }
}
