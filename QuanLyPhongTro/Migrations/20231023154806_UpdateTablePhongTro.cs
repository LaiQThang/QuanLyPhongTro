using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyPhongTro.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTablePhongTro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NguoiDungId",
                table: "phongTros",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "phongTros",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_phongTros_UserId",
                table: "phongTros",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_phongTros_AspNetUsers_UserId",
                table: "phongTros",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_phongTros_AspNetUsers_UserId",
                table: "phongTros");

            migrationBuilder.DropIndex(
                name: "IX_phongTros_UserId",
                table: "phongTros");

            migrationBuilder.DropColumn(
                name: "NguoiDungId",
                table: "phongTros");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "phongTros");
        }
    }
}
