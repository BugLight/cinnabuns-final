using Microsoft.EntityFrameworkCore.Migrations;

namespace CinnabunsFinal.Migrations
{
    public partial class FixTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_User",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_User",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "User",
                table: "Tasks");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AssignerId",
                table: "Tasks",
                column: "AssignerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_EventId",
                table: "Tasks",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_PartnerId",
                table: "Tasks",
                column: "PartnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_AssignerId",
                table: "Tasks",
                column: "AssignerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Events_EventId",
                table: "Tasks",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Partners_PartnerId",
                table: "Tasks",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_AssignerId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Events_EventId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Partners_PartnerId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_AssignerId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_EventId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_PartnerId",
                table: "Tasks");

            migrationBuilder.AddColumn<int>(
                name: "User",
                table: "Tasks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_User",
                table: "Tasks",
                column: "User");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_User",
                table: "Tasks",
                column: "User",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
