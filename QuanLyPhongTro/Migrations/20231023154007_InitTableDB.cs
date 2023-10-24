using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyPhongTro.Migrations
{
    /// <inheritdoc />
    public partial class InitTableDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "TrangThai",
                table: "AspNetUsers",
                type: "tinyint",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "GioiTinh",
                table: "AspNetUsers",
                type: "tinyint",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "VungMiens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenMien = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VungMiens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tinhThanhs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VungId = table.Column<int>(type: "int", nullable: false),
                    VungMienId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tinhThanhs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tinhThanhs_VungMiens_VungMienId",
                        column: x => x.VungMienId,
                        principalTable: "VungMiens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "phongTros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gia = table.Column<decimal>(type: "decimal(6,2)", nullable: true),
                    DienTich = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YeuThich = table.Column<int>(type: "int", nullable: false),
                    TinhTrang = table.Column<byte>(type: "tinyint", nullable: false),
                    TinhId = table.Column<int>(type: "int", nullable: false),
                    TinhThanhId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phongTros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_phongTros_tinhThanhs_TinhThanhId",
                        column: x => x.TinhThanhId,
                        principalTable: "tinhThanhs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "baiDangs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TieuDe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Anh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<byte>(type: "tinyint", nullable: true),
                    NguoiDungId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PhongId = table.Column<int>(type: "int", nullable: false),
                    PhongTroId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_baiDangs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_baiDangs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_baiDangs_phongTros_PhongTroId",
                        column: x => x.PhongTroId,
                        principalTable: "phongTros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chiTietDatPhongs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThoiGianBD = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ThoiGianKT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoNguoi = table.Column<int>(type: "int", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhongId = table.Column<int>(type: "int", nullable: false),
                    PhongTroId = table.Column<int>(type: "int", nullable: false),
                    NguoiDungId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chiTietDatPhongs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_chiTietDatPhongs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_chiTietDatPhongs_phongTros_PhongTroId",
                        column: x => x.PhongTroId,
                        principalTable: "phongTros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_baiDangs_PhongTroId",
                table: "baiDangs",
                column: "PhongTroId");

            migrationBuilder.CreateIndex(
                name: "IX_baiDangs_UserId",
                table: "baiDangs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_chiTietDatPhongs_PhongTroId",
                table: "chiTietDatPhongs",
                column: "PhongTroId");

            migrationBuilder.CreateIndex(
                name: "IX_chiTietDatPhongs_UserId",
                table: "chiTietDatPhongs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_phongTros_TinhThanhId",
                table: "phongTros",
                column: "TinhThanhId");

            migrationBuilder.CreateIndex(
                name: "IX_tinhThanhs_VungMienId",
                table: "tinhThanhs",
                column: "VungMienId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "baiDangs");

            migrationBuilder.DropTable(
                name: "chiTietDatPhongs");

            migrationBuilder.DropTable(
                name: "phongTros");

            migrationBuilder.DropTable(
                name: "tinhThanhs");

            migrationBuilder.DropTable(
                name: "VungMiens");

            migrationBuilder.AlterColumn<bool>(
                name: "TrangThai",
                table: "AspNetUsers",
                type: "bit",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "GioiTinh",
                table: "AspNetUsers",
                type: "bit",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldNullable: true);
        }
    }
}
