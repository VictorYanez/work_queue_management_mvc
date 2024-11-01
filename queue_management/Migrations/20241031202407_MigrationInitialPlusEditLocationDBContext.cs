using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace queue_management.Migrations
{
    /// <inheritdoc />
    public partial class MigrationInitialPlusEditLocationDBContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Municipality",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "Locations");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Locations",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountryID",
                table: "Locations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentID",
                table: "Locations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MunicipalityID",
                table: "Locations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RegionID",
                table: "Locations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_CountryID",
                table: "Locations",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_DepartmentID",
                table: "Locations",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_MunicipalityID",
                table: "Locations",
                column: "MunicipalityID");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_RegionID",
                table: "Locations",
                column: "RegionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Countries_CountryID",
                table: "Locations",
                column: "CountryID",
                principalTable: "Countries",
                principalColumn: "CountryID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Departments_DepartmentID",
                table: "Locations",
                column: "DepartmentID",
                principalTable: "Departments",
                principalColumn: "DepartmentID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Municipalities_MunicipalityID",
                table: "Locations",
                column: "MunicipalityID",
                principalTable: "Municipalities",
                principalColumn: "MunicipalityID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Regions_RegionID",
                table: "Locations",
                column: "RegionID",
                principalTable: "Regions",
                principalColumn: "RegionID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Countries_CountryID",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Departments_DepartmentID",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Municipalities_MunicipalityID",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Regions_RegionID",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_CountryID",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_DepartmentID",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_MunicipalityID",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_RegionID",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "CountryID",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "DepartmentID",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "MunicipalityID",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "RegionID",
                table: "Locations");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Locations",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Locations",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Locations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Locations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Municipality",
                table: "Locations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "Locations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
