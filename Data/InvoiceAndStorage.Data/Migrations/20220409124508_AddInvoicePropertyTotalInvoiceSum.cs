﻿#nullable disable

namespace InvoiceAndStorage.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddInvoicePropertyTotalInvoiceSum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalInvoiceSum",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalInvoiceSum",
                table: "Invoices");
        }
    }
}
