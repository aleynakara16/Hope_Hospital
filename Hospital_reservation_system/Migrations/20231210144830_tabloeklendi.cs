using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_reservation_system.Migrations
{
    /// <inheritdoc />
    public partial class tabloeklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Department_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Department_Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Department_Id);
                });

            migrationBuilder.CreateTable(
                name: "Policlinics",
                columns: table => new
                {
                    Policlinic_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Policlinic_Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policlinics", x => x.Policlinic_Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Policlinics");
        }
    }
}
