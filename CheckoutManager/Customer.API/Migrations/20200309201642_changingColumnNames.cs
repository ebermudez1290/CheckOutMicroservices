using Microsoft.EntityFrameworkCore.Migrations;

namespace Customer.API.Migrations
{
    public partial class changingColumnNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DocumentType",
                table: "Document",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "DocumentNumber",
                table: "Document",
                newName: "Number");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Document",
                newName: "DocumentType");

            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Document",
                newName: "DocumentNumber");
        }
    }
}
