using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyPhongTro.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTablesTypeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NguoiDungID",
                table: "chiTietDatPhongs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "NguoiDungID",
                table: "baiDangs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NguoiDungID",
                table: "chiTietDatPhongs",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "NguoiDungID",
                table: "baiDangs",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
