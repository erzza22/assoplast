using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC.Migrations
{
    public partial class TransportatoreTableMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transportatore",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    EstablishmentName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    TaxNumber = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    AuthorizationNumber = table.Column<string>(nullable: false),
                    AuthorizationDate = table.Column<DateTime>(nullable: false),
                    TransportatoreCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transportatore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transportatore_TransportatoreCategory_TransportatoreCategoryId",
                        column: x => x.TransportatoreCategoryId,
                        principalTable: "TransportatoreCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transportatore_TransportatoreCategoryId",
                table: "Transportatore",
                column: "TransportatoreCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transportatore");
        }
    }
}
