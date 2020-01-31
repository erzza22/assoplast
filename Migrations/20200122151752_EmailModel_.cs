using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC.Migrations
{
    public partial class EmailModel_ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Email",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Email");
        }
    }
}
