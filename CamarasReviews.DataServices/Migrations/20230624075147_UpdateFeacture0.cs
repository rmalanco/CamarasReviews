using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CamarasReviews.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFeacture0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeatureModelProductModel");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductModelProductId",
                table: "Features",
                type: "uniqueidentifier",
                nullable: true);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Features_Products_ProductModelProductId",
                table: "Features");

            migrationBuilder.DropIndex(
                name: "IX_Features_ProductModelProductId",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "ProductModelProductId",
                table: "Features");

            migrationBuilder.CreateTable(
                name: "FeatureModelProductModel",
                columns: table => new
                {
                    FeaturesFeatureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductsProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureModelProductModel", x => new { x.FeaturesFeatureId, x.ProductsProductId });
                    table.ForeignKey(
                        name: "FK_FeatureModelProductModel_Features_FeaturesFeatureId",
                        column: x => x.FeaturesFeatureId,
                        principalTable: "Features",
                        principalColumn: "FeatureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeatureModelProductModel_Products_ProductsProductId",
                        column: x => x.ProductsProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeatureModelProductModel_ProductsProductId",
                table: "FeatureModelProductModel",
                column: "ProductsProductId");
        }
    }
}
