using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CamarasReviews.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFeacture2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductImages");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductModelProductId",
                table: "ProductImages",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductModelProductId",
                table: "ProductImages",
                column: "ProductModelProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_ProductModelProductId",
                table: "ProductImages",
                column: "ProductModelProductId",
                principalTable: "Products",
                principalColumn: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_ProductModelProductId",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_ProductModelProductId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "ProductModelProductId",
                table: "ProductImages");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "ProductImages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
