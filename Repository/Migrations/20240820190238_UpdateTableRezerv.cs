using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class UpdateTableRezerv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ToDate",
                table: "Reservations",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "FromDate",
                table: "Reservations",
                newName: "EndDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Reservations",
                newName: "ToDate");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Reservations",
                newName: "FromDate");
        }
    }
}
