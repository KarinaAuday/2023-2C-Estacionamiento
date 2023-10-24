using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _2023_2C_F_Estacionamiento.Migrations
{
    public partial class indexUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Vehiculo_Patente",
                table: "Vehiculo",
                column: "Patente",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vehiculo_Patente",
                table: "Vehiculo");
        }
    }
}
