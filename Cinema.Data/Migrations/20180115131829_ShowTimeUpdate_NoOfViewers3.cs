using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CinemaApp.Data.Migrations
{
    public partial class ShowTimeUpdate_NoOfViewers3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NoOfViewers",
                table: "ShowTimes",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NoOfViewers",
                table: "ShowTimes",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
