using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PanMedix.Migrations
{
    /// <inheritdoc />
    public partial class AddGuardianStatusToUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GuardianStatus",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuardianStatus",
                table: "Users");
        }
    }
}
