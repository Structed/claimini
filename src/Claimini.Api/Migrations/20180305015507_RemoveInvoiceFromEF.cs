using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Claimini.Api.Migrations
{
    public partial class RemoveInvoiceFromEF : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItem_Invoice_InvoiceId",
                table: "InvoiceItem");

            migrationBuilder.RenameColumn(
                name: "InvoiceId",
                table: "InvoiceItem",
                newName: "InvoiceIdSql");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceItem_InvoiceId",
                table: "InvoiceItem",
                newName: "IX_InvoiceItem_InvoiceIdSql");

            migrationBuilder.RenameColumn(
                name: "PaidInstant",
                table: "Invoice",
                newName: "PaidTimestamp");

            migrationBuilder.RenameColumn(
                name: "CreatedInstant",
                table: "Invoice",
                newName: "CreatedTimestamp");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Invoice",
                newName: "IdSql");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItem_Invoice_InvoiceIdSql",
                table: "InvoiceItem",
                column: "InvoiceIdSql",
                principalTable: "Invoice",
                principalColumn: "IdSql",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItem_Invoice_InvoiceIdSql",
                table: "InvoiceItem");

            migrationBuilder.RenameColumn(
                name: "InvoiceIdSql",
                table: "InvoiceItem",
                newName: "InvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceItem_InvoiceIdSql",
                table: "InvoiceItem",
                newName: "IX_InvoiceItem_InvoiceId");

            migrationBuilder.RenameColumn(
                name: "PaidTimestamp",
                table: "Invoice",
                newName: "PaidInstant");

            migrationBuilder.RenameColumn(
                name: "CreatedTimestamp",
                table: "Invoice",
                newName: "CreatedInstant");

            migrationBuilder.RenameColumn(
                name: "IdSql",
                table: "Invoice",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItem_Invoice_InvoiceId",
                table: "InvoiceItem",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
