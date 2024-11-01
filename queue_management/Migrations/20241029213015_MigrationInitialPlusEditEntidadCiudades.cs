using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace queue_management.Migrations
{
    /// <inheritdoc />
    public partial class MigrationInitialPlusEditEntidadCiudades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Municipalities_MunicipalityID1",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_MunicipalityID1",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "MunicipalityID1",
                table: "Cities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MunicipalityID1",
                table: "Cities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_MunicipalityID1",
                table: "Cities",
                column: "MunicipalityID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Municipalities_MunicipalityID1",
                table: "Cities",
                column: "MunicipalityID1",
                principalTable: "Municipalities",
                principalColumn: "MunicipalityID");
        }
    }
}
