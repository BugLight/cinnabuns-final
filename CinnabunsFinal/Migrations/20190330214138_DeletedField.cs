using Microsoft.EntityFrameworkCore.Migrations;

namespace CinnabunsFinal.Migrations
{
    public partial class DeletedField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Partners",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Events",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Events");
        }
    }
}
