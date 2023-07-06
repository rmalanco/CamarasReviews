using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CamarasReviews.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReviews04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Categories_CategoryModelCategoryId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_CategoryModelCategoryId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CategoryModelCategoryId",
                table: "Reviews");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryModelCategoryId",
                table: "Reviews",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CategoryModelCategoryId",
                table: "Reviews",
                column: "CategoryModelCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Categories_CategoryModelCategoryId",
                table: "Reviews",
                column: "CategoryModelCategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId");
        }
    }
}
