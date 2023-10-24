using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyPhongTro.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableForekigUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_baiDangs_AspNetUsers_ApplicationUserId1",
                table: "baiDangs");

            migrationBuilder.DropForeignKey(
                name: "FK_chiTietDatPhongs_AspNetUsers_ApplicationUserId1",
                table: "chiTietDatPhongs");

            migrationBuilder.DropForeignKey(
                name: "FK_phongTros_AspNetUsers_ApplicationUserId1",
                table: "phongTros");

            migrationBuilder.DropIndex(
                name: "IX_phongTros_ApplicationUserId1",
                table: "phongTros");

            migrationBuilder.DropIndex(
                name: "IX_chiTietDatPhongs_ApplicationUserId1",
                table: "chiTietDatPhongs");

            migrationBuilder.DropIndex(
                name: "IX_baiDangs_ApplicationUserId1",
                table: "baiDangs");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "phongTros");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "chiTietDatPhongs");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "baiDangs");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "phongTros",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "NguoiDungID",
                table: "phongTros",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "chiTietDatPhongs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "NguoiDungID",
                table: "chiTietDatPhongs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "baiDangs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "NguoiDungID",
                table: "baiDangs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_phongTros_ApplicationUserId",
                table: "phongTros",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_chiTietDatPhongs_ApplicationUserId",
                table: "chiTietDatPhongs",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_baiDangs_ApplicationUserId",
                table: "baiDangs",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_baiDangs_AspNetUsers_ApplicationUserId",
                table: "baiDangs",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_chiTietDatPhongs_AspNetUsers_ApplicationUserId",
                table: "chiTietDatPhongs",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_phongTros_AspNetUsers_ApplicationUserId",
                table: "phongTros",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_baiDangs_AspNetUsers_ApplicationUserId",
                table: "baiDangs");

            migrationBuilder.DropForeignKey(
                name: "FK_chiTietDatPhongs_AspNetUsers_ApplicationUserId",
                table: "chiTietDatPhongs");

            migrationBuilder.DropForeignKey(
                name: "FK_phongTros_AspNetUsers_ApplicationUserId",
                table: "phongTros");

            migrationBuilder.DropIndex(
                name: "IX_phongTros_ApplicationUserId",
                table: "phongTros");

            migrationBuilder.DropIndex(
                name: "IX_chiTietDatPhongs_ApplicationUserId",
                table: "chiTietDatPhongs");

            migrationBuilder.DropIndex(
                name: "IX_baiDangs_ApplicationUserId",
                table: "baiDangs");

            migrationBuilder.DropColumn(
                name: "NguoiDungID",
                table: "phongTros");

            migrationBuilder.DropColumn(
                name: "NguoiDungID",
                table: "chiTietDatPhongs");

            migrationBuilder.DropColumn(
                name: "NguoiDungID",
                table: "baiDangs");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                table: "phongTros",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "phongTros",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                table: "chiTietDatPhongs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "chiTietDatPhongs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                table: "baiDangs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "baiDangs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_phongTros_ApplicationUserId1",
                table: "phongTros",
                column: "ApplicationUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_chiTietDatPhongs_ApplicationUserId1",
                table: "chiTietDatPhongs",
                column: "ApplicationUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_baiDangs_ApplicationUserId1",
                table: "baiDangs",
                column: "ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_baiDangs_AspNetUsers_ApplicationUserId1",
                table: "baiDangs",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_chiTietDatPhongs_AspNetUsers_ApplicationUserId1",
                table: "chiTietDatPhongs",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_phongTros_AspNetUsers_ApplicationUserId1",
                table: "phongTros",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
