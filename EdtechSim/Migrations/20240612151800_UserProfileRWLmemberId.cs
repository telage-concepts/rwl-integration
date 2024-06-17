using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EdtechSim.Migrations
{
    /// <inheritdoc />
    public partial class UserProfileRWLmemberId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RwlMemberId",
                table: "UserProfiles",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RwlMemberId",
                table: "UserProfiles");
        }
    }
}
