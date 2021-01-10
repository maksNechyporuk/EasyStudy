using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class deleteageaddDayOfbirthday : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "tblTeacher");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "tblStudent");

            migrationBuilder.AddColumn<DateTime>(
                name: "DayOfbirthday",
                table: "tblTeacher",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DayOfbirthday",
                table: "tblStudent",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOfbirthday",
                table: "tblTeacher");

            migrationBuilder.DropColumn(
                name: "DayOfbirthday",
                table: "tblStudent");

            migrationBuilder.AddColumn<double>(
                name: "Age",
                table: "tblTeacher",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Age",
                table: "tblStudent",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
