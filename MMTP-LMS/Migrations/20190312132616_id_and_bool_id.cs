using Microsoft.EntityFrameworkCore.Migrations;

namespace MMTP_LMS.Migrations
{
    public partial class id_and_bool_id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LmsActivity_LmsActivityType_LmsActivityTypeId",
                table: "LmsActivity");

            migrationBuilder.DropIndex(
                name: "IX_LmsActivity_LmsActivityTypeId",
                table: "LmsActivity");

            migrationBuilder.AddColumn<int>(
                name: "LmsActivityId",
                table: "LmsActivityType",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsSubmission",
                table: "LmsActivity",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LmsActivityTypeId1",
                table: "LmsActivity",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LmsActivity_LmsActivityTypeId1",
                table: "LmsActivity",
                column: "LmsActivityTypeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_LmsActivity_LmsActivityType_LmsActivityTypeId1",
                table: "LmsActivity",
                column: "LmsActivityTypeId1",
                principalTable: "LmsActivityType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LmsActivity_LmsActivityType_LmsActivityTypeId1",
                table: "LmsActivity");

            migrationBuilder.DropIndex(
                name: "IX_LmsActivity_LmsActivityTypeId1",
                table: "LmsActivity");

            migrationBuilder.DropColumn(
                name: "LmsActivityId",
                table: "LmsActivityType");

            migrationBuilder.DropColumn(
                name: "IsSubmission",
                table: "LmsActivity");

            migrationBuilder.DropColumn(
                name: "LmsActivityTypeId1",
                table: "LmsActivity");

            migrationBuilder.CreateIndex(
                name: "IX_LmsActivity_LmsActivityTypeId",
                table: "LmsActivity",
                column: "LmsActivityTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_LmsActivity_LmsActivityType_LmsActivityTypeId",
                table: "LmsActivity",
                column: "LmsActivityTypeId",
                principalTable: "LmsActivityType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
