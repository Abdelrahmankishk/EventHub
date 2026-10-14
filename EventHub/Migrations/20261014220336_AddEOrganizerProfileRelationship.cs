using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventHub.Migrations
{
    /// <inheritdoc />
    public partial class AddEOrganizerProfileRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganizerId",
                table: "OrganizerProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizerProfiles_OrganizerId",
                table: "OrganizerProfiles",
                column: "OrganizerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizerProfiles_Organizers_OrganizerId",
                table: "OrganizerProfiles",
                column: "OrganizerId",
                principalTable: "Organizers",
                principalColumn: "OrganizerId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizerProfiles_Organizers_OrganizerId",
                table: "OrganizerProfiles");

            migrationBuilder.DropIndex(
                name: "IX_OrganizerProfiles_OrganizerId",
                table: "OrganizerProfiles");

            migrationBuilder.DropColumn(
                name: "OrganizerId",
                table: "OrganizerProfiles");
        }
    }
}
