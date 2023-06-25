using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CamarasReviews.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFeacture4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Features_Products_ProductModelProductId",
                table: "Features");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_ProductModelProductId",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_ProductModelProductId",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_Features_ProductModelProductId",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "ProductModelProductId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "ProductModelProductId",
                table: "Features");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "ProductImages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Features",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Features_ProductId",
                table: "Features",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Features_Products_ProductId",
                table: "Features",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Features_Products_ProductId",
                table: "Features");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_Features_ProductId",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Features");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductModelProductId",
                table: "ProductImages",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductModelProductId",
                table: "Features",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductModelProductId",
                table: "ProductImages",
                column: "ProductModelProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Features_ProductModelProductId",
                table: "Features",
                column: "ProductModelProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Features_Products_ProductModelProductId",
                table: "Features",
                column: "ProductModelProductId",
                principalTable: "Products",
                principalColumn: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_ProductModelProductId",
                table: "ProductImages",
                column: "ProductModelProductId",
                principalTable: "Products",
                principalColumn: "ProductId");
        }
    }
}
