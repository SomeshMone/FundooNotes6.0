using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    public partial class AddColumnNameToTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "UserNotes",
                nullable: true);
        }

        
    }
}
