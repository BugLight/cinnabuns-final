using Microsoft.EntityFrameworkCore.Migrations;

namespace CinnabunsFinal.Migrations
{
    public partial class TaskCompleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                table: "Tasks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_ContactId",
                table: "Interactions",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_ResponsibleId",
                table: "Interactions",
                column: "ResponsibleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Interactions_Contacts_ContactId",
                table: "Interactions",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Interactions_AspNetUsers_ResponsibleId",
                table: "Interactions",
                column: "ResponsibleId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interactions_Contacts_ContactId",
                table: "Interactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Interactions_AspNetUsers_ResponsibleId",
                table: "Interactions");

            migrationBuilder.DropIndex(
                name: "IX_Interactions_ContactId",
                table: "Interactions");

            migrationBuilder.DropIndex(
                name: "IX_Interactions_ResponsibleId",
                table: "Interactions");

            migrationBuilder.DropColumn(
                name: "Completed",
                table: "Tasks");
        }
    }
}
