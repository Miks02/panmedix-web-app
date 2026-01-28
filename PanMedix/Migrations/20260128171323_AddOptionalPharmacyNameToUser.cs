using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PanMedix.Migrations
{
    /// <inheritdoc />
    public partial class AddOptionalPharmacyNameToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PharmacyName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PharmacyName",
                table: "Users");
        }
    }
}
