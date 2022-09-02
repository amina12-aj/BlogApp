using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogProject.Migrations
{
    public partial class Third_Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConfirmPassword",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "ArticleId", "Description", "Title" },
                values: new object[] { 1, "Totally", "John Doe" });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "ArticleId", "Description", "Title" },
                values: new object[] { 2, "Totally Brave", "John Silver" });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "ArticleId", "Description", "Title" },
                values: new object[] { 3, "Totally Awesome", "Rita Silver" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "ArticleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "ArticleId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "ArticleId",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "ConfirmPassword",
                table: "Accounts");
        }
    }
}
