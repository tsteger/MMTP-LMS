using Microsoft.EntityFrameworkCore.Migrations;

namespace MMTP_LMS.Migrations
{
    public partial class null_activityTyp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LmsActivity_LmsActivityType_LmsActivityTypeId",
                table: "LmsActivity");

            migrationBuilder.AlterColumn<int>(
                name: "LmsActivityTypeId",
                table: "LmsActivity",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_LmsActivity_LmsActivityType_LmsActivityTypeId",
                table: "LmsActivity",
                column: "LmsActivityTypeId",
                principalTable: "LmsActivityType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LmsActivity_LmsActivityType_LmsActivityTypeId",
                table: "LmsActivity");

            migrationBuilder.AlterColumn<int>(
                name: "LmsActivityTypeId",
                table: "LmsActivity",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LmsActivity_LmsActivityType_LmsActivityTypeId",
                table: "LmsActivity",
                column: "LmsActivityTypeId",
                principalTable: "LmsActivityType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
