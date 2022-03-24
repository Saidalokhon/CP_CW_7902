using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CP_CW_7902_DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Swipes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SwipeId = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    Direction = table.Column<string>(nullable: true),
                    TerminalIp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Swipes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Swipes");
        }
    }
}
