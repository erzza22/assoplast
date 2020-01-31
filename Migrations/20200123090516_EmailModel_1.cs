using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC.Migrations
{
    public partial class EmailModel_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SenderMail",
                table: "Email",
                newName: "Sender");

            migrationBuilder.RenameColumn(
                name: "SenderEmail",
                table: "Email",
                newName: "EmailAddress");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sender",
                table: "Email",
                newName: "SenderMail");

            migrationBuilder.RenameColumn(
                name: "EmailAddress",
                table: "Email",
                newName: "SenderEmail");
        }
    }
}
