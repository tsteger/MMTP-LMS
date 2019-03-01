using Microsoft.EntityFrameworkCore.Migrations;

namespace MMTP_LMS.Migrations
{
    public partial class lagttillfelt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "LmsActivity",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "LmsActivity");
        }
    }
}
