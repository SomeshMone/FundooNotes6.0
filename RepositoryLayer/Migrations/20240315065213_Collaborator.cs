using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    public partial class Collaborator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "UserNotes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");


            migrationBuilder.CreateTable(
                name: "collaborators",
                columns: table => new
                {
                    CollaboratorsId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollaboratorsEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    NoteId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_collaborators", x => x.CollaboratorsId);
                    table.ForeignKey(
                        name: "FK_collaborators_UserNotes_NoteId",
                        column: x => x.NoteId,
                        principalTable: "UserNotes",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_collaborators_UsersTable1_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersTable1",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_collaborators_NoteId",
                table: "collaborators",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_collaborators_UserId",
                table: "collaborators",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "collaborators");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "UserNotes");

        }
    }
}
