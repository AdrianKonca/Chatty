using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chatty.Data.Migrations
{
    public partial class autosetMessageDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SentOn",
                table: "Message",
                type: "DATETIME",
                nullable: false,
                defaultValueSql: "Now()",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SentOn",
                table: "Message",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME",
                oldDefaultValueSql: "Now()");
        }
    }
}
