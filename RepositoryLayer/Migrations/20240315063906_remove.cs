using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    public partial class remove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
    name: "Image",
    table: "UserNotes");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
