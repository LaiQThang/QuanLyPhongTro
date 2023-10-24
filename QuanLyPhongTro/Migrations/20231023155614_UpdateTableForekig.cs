using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyPhongTro.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableForekig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_baiDangs_AspNetUsers_UserId",
                table: "baiDangs");

            migrationBuilder.DropForeignKey(
                name: "FK_chiTietDatPhongs_AspNetUsers_UserId",
                table: "chiTietDatPhongs");

            migrationBuilder.DropForeignKey(
                name: "FK_phongTros_AspNetUsers_UserId",
                table: "phongTros");

            migrationBuilder.DropColumn(
                name: "VungId",
                table: "tinhThanhs");

            migrationBuilder.DropColumn(
                name: "NguoiDungId",
                table: "phongTros");

            migrationBuilder.DropColumn(
                name: "NguoiDungId",
                table: "chiTietDatPhongs");

            migrationBuilder.DropColumn(
                name: "NguoiDungId",
                table: "baiDangs");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "phongTros",
                newName: "ApplicationUserId1");

            migrationBuilder.RenameColumn(
                name: "TinhId",
                table: "phongTros",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_phongTros_UserId",
                table: "phongTros",
                newName: "IX_phongTros_ApplicationUserId1");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "chiTietDatPhongs",
                newName: "ApplicationUserId1");

            migrationBuilder.RenameColumn(
                name: "PhongId",
                table: "chiTietDatPhongs",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_chiTietDatPhongs_UserId",
                table: "chiTietDatPhongs",
                newName: "IX_chiTietDatPhongs_ApplicationUserId1");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "baiDangs",
                newName: "ApplicationUserId1");

            migrationBuilder.RenameColumn(
                name: "PhongId",
                table: "baiDangs",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_baiDangs_UserId",
                table: "baiDangs",
                newName: "IX_baiDangs_ApplicationUserId1");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId1",
                table: "phongTros",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "phongTros",
                newName: "TinhId");

            migrationBuilder.RenameIndex(
                name: "IX_phongTros_ApplicationUserId1",
                table: "phongTros",
                newName: "IX_phongTros_UserId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId1",
                table: "chiTietDatPhongs",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "chiTietDatPhongs",
                newName: "PhongId");

            migrationBuilder.RenameIndex(
                name: "IX_chiTietDatPhongs_ApplicationUserId1",
                table: "chiTietDatPhongs",
                newName: "IX_chiTietDatPhongs_UserId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId1",
                table: "baiDangs",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "baiDangs",
                newName: "PhongId");

            migrationBuilder.RenameIndex(
                name: "IX_baiDangs_ApplicationUserId1",
                table: "baiDangs",
                newName: "IX_baiDangs_UserId");

            migrationBuilder.AddColumn<int>(
                name: "VungId",
                table: "tinhThanhs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NguoiDungId",
                table: "phongTros",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NguoiDungId",
                table: "chiTietDatPhongs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NguoiDungId",
                table: "baiDangs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_baiDangs_AspNetUsers_UserId",
                table: "baiDangs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_chiTietDatPhongs_AspNetUsers_UserId",
                table: "chiTietDatPhongs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_phongTros_AspNetUsers_UserId",
                table: "phongTros",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
