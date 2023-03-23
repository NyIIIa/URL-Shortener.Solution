using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace URL_Shortener.Client.Migrations
{
    /// <inheritdoc />
    public partial class AddUrlShortAlgorithm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UrlShortAlgorithms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlShortAlgorithms", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "UrlShortAlgorithms",
                columns: new[] { "Id", "Description" },
                values: new object[] { 1, "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UrlShortAlgorithms");
        }
    }
}
