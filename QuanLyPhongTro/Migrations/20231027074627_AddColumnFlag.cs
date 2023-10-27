using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyPhongTro.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnFlag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "flag",
                table: "phongTros",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "flag",
                table: "chiTietDatPhongs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "flag",
                table: "baiDangs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "flag",
                table: "phongTros");

            migrationBuilder.DropColumn(
                name: "flag",
                table: "chiTietDatPhongs");

            migrationBuilder.DropColumn(
                name: "flag",
                table: "baiDangs");
        }
    }
}
