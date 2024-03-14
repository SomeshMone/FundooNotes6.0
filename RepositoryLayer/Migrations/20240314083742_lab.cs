using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    public partial class lab : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Label",
               columns: table => new
               {
                   LabelId = table.Column<long>(type: "bigint", nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   LabelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   UserId = table.Column<long>(type: "bigint", nullable: false),
                   NoteId = table.Column<long>(type: "bigint", nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Label", x => x.LabelId);
                   table.ForeignKey(
                       name: "FK_Label_UserNotes_NoteId",
                       column: x => x.NoteId,
                       principalTable: "UserNotes",
                       principalColumn: "NoteId",
                       onDelete: ReferentialAction.NoAction);
                   table.ForeignKey(
                       name: "FK_Label_UsersTable1_UserId",
                       column: x => x.UserId,
                       principalTable: "UsersTable1",
                       principalColumn: "UserId",
                       onDelete: ReferentialAction.NoAction);
               });

            migrationBuilder.CreateIndex(
           name: "IX_Label_NoteId",
           table: "Label",
           column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_Label_UserId",
                table: "Label",
                column: "UserId");




        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropTable(
                name: "Label");

            
        }
    }
}
