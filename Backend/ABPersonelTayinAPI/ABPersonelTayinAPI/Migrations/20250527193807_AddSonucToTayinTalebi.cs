using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ABPersonelTayinAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddSonucToTayinTalebi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sonuc",
                table: "Talepler",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Değerlendirilmedi");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sonuc",
                table: "Talepler");
        }
    }
}
