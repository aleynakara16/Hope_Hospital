using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_reservation_system.Migrations
{
    /// <inheritdoc />
    public partial class doktordatabasegüncelleme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PoliclincName",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PoliclincName",
                table: "Doctors");
        }
    }
}
