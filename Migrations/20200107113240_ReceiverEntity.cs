using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC.Migrations
{
    public partial class ReceiverEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Receiver",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    TaxNumber = table.Column<string>(nullable: false),
                    DestinationLocation = table.Column<string>(nullable: false),
                    AuthorizationNumber = table.Column<string>(nullable: false),
                    AuthorizationDate = table.Column<DateTime>(nullable: false),
                    ReceiverCategoryId = table.Column<int>(nullable: false),
                    DestinatarioCategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receiver", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receiver_DestinatarioCategory_DestinatarioCategoryId",
                        column: x => x.DestinatarioCategoryId,
                        principalTable: "DestinatarioCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Receiver_DestinatarioCategoryId",
                table: "Receiver",
                column: "DestinatarioCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Receiver");
        }
    }
}
