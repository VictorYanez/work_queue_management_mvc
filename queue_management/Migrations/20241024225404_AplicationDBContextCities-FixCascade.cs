using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace queue_management.Migrations
{
    /// <inheritdoc />
    public partial class AplicationDBContextCitiesFixCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryID",
                table: "Cities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentID",
                table: "Cities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MunicipalityID1",
                table: "Cities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegionID",
                table: "Cities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryID",
                table: "Cities",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_DepartmentID",
                table: "Cities",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_MunicipalityID1",
                table: "Cities",
                column: "MunicipalityID1");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_RegionID",
                table: "Cities",
                column: "RegionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CountryID",
                table: "Cities",
                column: "CountryID",
                principalTable: "Countries",
                principalColumn: "CountryID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Departments_DepartmentID",
                table: "Cities",
                column: "DepartmentID",
                principalTable: "Departments",
                principalColumn: "DepartmentID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Municipalities_MunicipalityID1",
                table: "Cities",
                column: "MunicipalityID1",
                principalTable: "Municipalities",
                principalColumn: "MunicipalityID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Regions_RegionID",
                table: "Cities",
                column: "RegionID",
                principalTable: "Regions",
                principalColumn: "RegionID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CountryID",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Departments_DepartmentID",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Municipalities_MunicipalityID1",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Regions_RegionID",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_CountryID",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_DepartmentID",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_MunicipalityID1",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_RegionID",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "CountryID",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "DepartmentID",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "MunicipalityID1",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "RegionID",
                table: "Cities");
        }
    }
}
