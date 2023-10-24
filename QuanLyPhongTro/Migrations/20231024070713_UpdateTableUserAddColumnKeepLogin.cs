using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyPhongTro.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableUserAddColumnKeepLogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "KeepLogin",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KeepLogin",
                table: "AspNetUsers");
        }
    }
}
