using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LD_4_Interneto_tech.Migrations
{
    /// <inheritdoc />
    public partial class noname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Properties");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
