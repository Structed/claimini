using Microsoft.EntityFrameworkCore.Migrations;

namespace Claimini.Api.Migrations
{
    public partial class AddTaxPercentageToArticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TaxPercentage",
                table: "Article",
                nullable: false,
                defaultValue: 0.19m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxPercentage",
                table: "Article");
        }
    }
}
