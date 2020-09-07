using Microsoft.EntityFrameworkCore.Migrations;

namespace InfiniteCalendar.Migrations
{
    public partial class addlimityear : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LimitYear",
                table: "Holidays",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LimitYear",
                table: "Holidays");
        }
    }
}
