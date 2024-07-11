using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentExchange.Wiut.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class EducationalDetailsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EducationalDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExchangePartnerInstitution = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExactNameOfDegreeProgramme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalLengthOfDegreeProgrammeInYears = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpectedMonthOfGraduation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpectedYearOfGraduation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsEnglishFirstLanguage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationalDetails_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EducationalDetails_StudentId",
                table: "EducationalDetails",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EducationalDetails");
        }
    }
}
