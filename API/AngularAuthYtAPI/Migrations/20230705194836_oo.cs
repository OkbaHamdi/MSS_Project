using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AngularAuthYtAPI.Migrations
{
    public partial class oo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "users",
                newName: "Organisation");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "users",
                newName: "Lockout_enable");

            migrationBuilder.AddColumn<string>(
                name: "Bankcode",
                table: "users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "bankinfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bankcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeApp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Organisation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SMSid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SMSpwd = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bankinfo", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bankinfo");

            migrationBuilder.DropColumn(
                name: "Bankcode",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "Organisation",
                table: "users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Lockout_enable",
                table: "users",
                newName: "FirstName");
        }
    }
}
