using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC.Migrations
{
    public partial class RequestEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    CharacteristicsDescription = table.Column<string>(nullable: true),
                    CharacteristicsCode = table.Column<string>(nullable: true),
                    CharacteristicsState = table.Column<string>(nullable: true),
                    CharacteristicsDangerDescription = table.Column<string>(nullable: true),
                    CharacteristicsNumberOfContainers = table.Column<int>(nullable: false),
                    DestinationTypeDescription = table.Column<string>(nullable: true),
                    DestinationPhysicalChemicalProprieties = table.Column<string>(nullable: true),
                    DestinationType = table.Column<int>(nullable: false),
                    RequestCategoryId = table.Column<int>(nullable: false),
                    TransporterId = table.Column<int>(nullable: false),
                    TransportatoreId = table.Column<int>(nullable: true),
                    ProducerId = table.Column<int>(nullable: false),
                    ProduttoreDetentoreId = table.Column<int>(nullable: true),
                    ReceiverId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Request_ProduttoreDetentore_ProduttoreDetentoreId",
                        column: x => x.ProduttoreDetentoreId,
                        principalTable: "ProduttoreDetentore",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Request_Receiver_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Receiver",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Request_RequestCategory_RequestCategoryId",
                        column: x => x.RequestCategoryId,
                        principalTable: "RequestCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Request_Transportatore_TransportatoreId",
                        column: x => x.TransportatoreId,
                        principalTable: "Transportatore",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Request_ProduttoreDetentoreId",
                table: "Request",
                column: "ProduttoreDetentoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_ReceiverId",
                table: "Request",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_RequestCategoryId",
                table: "Request",
                column: "RequestCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_TransportatoreId",
                table: "Request",
                column: "TransportatoreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Request");
        }
    }
}
