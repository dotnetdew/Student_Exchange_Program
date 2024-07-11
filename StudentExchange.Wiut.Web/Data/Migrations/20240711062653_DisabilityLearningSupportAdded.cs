using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentExchange.Wiut.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class DisabilityLearningSupportAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DisabilityLearningSupport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HaveADisablity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisabilityLearningSupport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisabilityLearningSupport_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DisabilityLearningSupport_StudentId",
                table: "DisabilityLearningSupport",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DisabilityLearningSupport");
        }
    }
}
