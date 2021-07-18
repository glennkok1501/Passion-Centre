using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PassionCentre.Migrations
{
    public partial class AddAudit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.CreateTable(
                name: "AuditRecords",
                columns: table => new
                {
                    Audit_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditActionType = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    DateStamp = table.Column<DateTime>(nullable: false),
                    TimeStamp = table.Column<string>(nullable: true),
                    KeyCourseFieldID = table.Column<int>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditRecords", x => x.Audit_ID);
                });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropTable(
                name: "AuditRecords");

           
        }
    }
}
