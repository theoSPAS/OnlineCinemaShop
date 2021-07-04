using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineCinema.Repository.Migrations
{
    public partial class AddedNewProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MovieGenre",
                table: "Tickets",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovieGenre",
                table: "Tickets");
        }
    }
}
