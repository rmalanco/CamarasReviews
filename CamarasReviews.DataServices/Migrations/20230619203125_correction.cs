﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CamarasReviews.Data.Migrations
{
    /// <inheritdoc />
    public partial class correction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ReviewId",
                table: "Ratings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ReviewId",
                table: "Ratings",
                column: "ReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Reviews_ReviewId",
                table: "Ratings",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "ReviewId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Reviews_ReviewId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_ReviewId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "ReviewId",
                table: "Ratings");
        }
    }
}
