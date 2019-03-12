using Microsoft.EntityFrameworkCore.Migrations;

namespace MMTP_LMS.Migrations
{
    public partial class nullebale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "LmsActivityId",
                table: "LmsActivityType",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "LmsActivityId",
                table: "LmsActivityType",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
