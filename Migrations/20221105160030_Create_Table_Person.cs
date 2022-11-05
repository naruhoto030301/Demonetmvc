using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NguyenVietPhuongBTH2.Migrations
{
    public partial class Create_Table_Person : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PersonAddress",
                table: "Person",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonAddress",
                table: "Person");
        }
    }
}
