namespace InvoiceAndStorage.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class DatabaseОwnersProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DatabaseОwnerId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DatabaseОwnersProducts",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DatabaseОwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatabaseОwnersProducts", x => new { x.DatabaseОwnerId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_DatabaseОwnersProducts_DatabaseОwners_ProductId",
                        column: x => x.ProductId,
                        principalTable: "DatabaseОwners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DatabaseОwnersProducts_Products_DatabaseОwnerId",
                        column: x => x.DatabaseОwnerId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_DatabaseОwnerId",
                table: "Products",
                column: "DatabaseОwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_DatabaseОwnersProducts_IsDeleted",
                table: "DatabaseОwnersProducts",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_DatabaseОwnersProducts_ProductId",
                table: "DatabaseОwnersProducts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_DatabaseОwners_DatabaseОwnerId",
                table: "Products",
                column: "DatabaseОwnerId",
                principalTable: "DatabaseОwners",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_DatabaseОwners_DatabaseОwnerId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "DatabaseОwnersProducts");

            migrationBuilder.DropIndex(
                name: "IX_Products_DatabaseОwnerId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DatabaseОwnerId",
                table: "Products");
        }
    }
}
