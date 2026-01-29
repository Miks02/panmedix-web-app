using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PanMedix.Migrations
{
    /// <inheritdoc />
    public partial class RenamePatientsToWardsAndRemoveIsGuardianApprovedInUsesModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsGuardianApproved",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsGuardianApproved",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
