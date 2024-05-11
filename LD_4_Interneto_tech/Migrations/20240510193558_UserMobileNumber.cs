using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LD_4_Interneto_tech.Migrations
{
    /// <inheritdoc />
    public partial class UserMobileNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ResetTokenHash",
                table: "Users",
                newName: "MobileNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MobileNumber",
                table: "Users",
                newName: "ResetTokenHash");
        }
    }
}
