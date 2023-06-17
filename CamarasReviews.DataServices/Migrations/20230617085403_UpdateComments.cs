using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CamarasReviews.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "ReviewComments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReviewComments_ApplicationUserId",
                table: "ReviewComments",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewComments_AspNetUsers_ApplicationUserId",
                table: "ReviewComments",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewComments_AspNetUsers_ApplicationUserId",
                table: "ReviewComments");

            migrationBuilder.DropIndex(
                name: "IX_ReviewComments_ApplicationUserId",
                table: "ReviewComments");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "ReviewComments");
        }
    }
}
