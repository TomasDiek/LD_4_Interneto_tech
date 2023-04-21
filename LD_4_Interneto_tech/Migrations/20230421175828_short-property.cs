using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LD_4_Interneto_tech.Migrations
{
    /// <inheritdoc />
    public partial class shortproperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address2",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "BHK",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "CarpetArea",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "FloorNo",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Gated",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "MainEntrance",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Maintenance",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Security",
                table: "Properties");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address2",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "BHK",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CarpetArea",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FloorNo",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Gated",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MainEntrance",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Maintenance",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Security",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
