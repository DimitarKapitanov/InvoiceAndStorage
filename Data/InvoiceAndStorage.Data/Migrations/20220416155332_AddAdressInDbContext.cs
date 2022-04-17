using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceAndStorage.Data.Migrations
{
    public partial class AddAdressInDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Adress_AdressId",
                table: "Companies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Adress",
                table: "Adress");

            migrationBuilder.RenameTable(
                name: "Adress",
                newName: "Adresses");

            migrationBuilder.RenameIndex(
                name: "IX_Adress_IsDeleted",
                table: "Adresses",
                newName: "IX_Adresses_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Adresses",
                table: "Adresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Adresses_AdressId",
                table: "Companies",
                column: "AdressId",
                principalTable: "Adresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Adresses_AdressId",
                table: "Companies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Adresses",
                table: "Adresses");

            migrationBuilder.RenameTable(
                name: "Adresses",
                newName: "Adress");

            migrationBuilder.RenameIndex(
                name: "IX_Adresses_IsDeleted",
                table: "Adress",
                newName: "IX_Adress_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Adress",
                table: "Adress",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Adress_AdressId",
                table: "Companies",
                column: "AdressId",
                principalTable: "Adress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
