using Microsoft.EntityFrameworkCore.Migrations;

namespace VoiceSocialNetworks.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    RealName = table.Column<string>(nullable: true),
                    Login = table.Column<string>(nullable: true),
                    Sex = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VkUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    YandexUserId = table.Column<string>(nullable: true),
                    AccessToken = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VkUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VkUsers_Users_YandexUserId",
                        column: x => x.YandexUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VkUsers_YandexUserId",
                table: "VkUsers",
                column: "YandexUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VkUsers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
