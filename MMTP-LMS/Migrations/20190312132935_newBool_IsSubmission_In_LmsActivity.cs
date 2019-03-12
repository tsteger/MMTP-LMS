using Microsoft.EntityFrameworkCore.Migrations;

namespace MMTP_LMS.Migrations
{
    public partial class newBool_IsSubmission_In_LmsActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSubmission",
                table: "LmsActivity",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSubmission",
                table: "LmsActivity");
        }
    }
}
