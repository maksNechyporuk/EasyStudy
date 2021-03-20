using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class addedschoolfromgroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SchoolId",
                table: "Groups",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_SchoolId",
                table: "Groups",
                column: "SchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Schools_SchoolId",
                table: "Groups",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Schools_SchoolId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_SchoolId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "Groups");
        }
    }
}
