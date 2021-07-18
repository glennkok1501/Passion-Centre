using Microsoft.EntityFrameworkCore.Migrations;

namespace PassionCentre.Migrations
{
    public partial class AddAudit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FieldEdited",
                table: "AuditRecords");

            migrationBuilder.DropColumn(
                name: "NewValue",
                table: "AuditRecords");

            migrationBuilder.DropColumn(
                name: "OldValue",
                table: "AuditRecords");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FieldEdited",
                table: "AuditRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NewValue",
                table: "AuditRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OldValue",
                table: "AuditRecords",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
