using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventHub.Migrations
{
    /// <inheritdoc />
    public partial class AddSessionsSelfRelationShip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentEventId",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_ParentEventId",
                table: "Events",
                column: "ParentEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Events_ParentEventId",
                table: "Events",
                column: "ParentEventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Events_ParentEventId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_ParentEventId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ParentEventId",
                table: "Events");
        }
    }
}
