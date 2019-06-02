using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CinemaApp.Data.Migrations
{
    public partial class ShowTimeUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CinemaOwnerId",
                table: "ShowTimes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "MovieCategoryId",
                table: "ShowTimes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ShowTimes_CinemaOwnerId",
                table: "ShowTimes",
                column: "CinemaOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ShowTimes_MovieCategoryId",
                table: "ShowTimes",
                column: "MovieCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShowTimes_CinemaOwners_CinemaOwnerId",
                table: "ShowTimes",
                column: "CinemaOwnerId",
                principalTable: "CinemaOwners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShowTimes_MovieCategories_MovieCategoryId",
                table: "ShowTimes",
                column: "MovieCategoryId",
                principalTable: "MovieCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShowTimes_CinemaOwners_CinemaOwnerId",
                table: "ShowTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_ShowTimes_MovieCategories_MovieCategoryId",
                table: "ShowTimes");

            migrationBuilder.DropIndex(
                name: "IX_ShowTimes_CinemaOwnerId",
                table: "ShowTimes");

            migrationBuilder.DropIndex(
                name: "IX_ShowTimes_MovieCategoryId",
                table: "ShowTimes");

            migrationBuilder.DropColumn(
                name: "CinemaOwnerId",
                table: "ShowTimes");

            migrationBuilder.DropColumn(
                name: "MovieCategoryId",
                table: "ShowTimes");
        }
    }
}
