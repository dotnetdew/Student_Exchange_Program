using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentExchange.Wiut.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOneToOneRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Submissions_StudentId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_PersonalDetails_StudentId",
                table: "PersonalDetails");

            migrationBuilder.DropIndex(
                name: "IX_Housings_StudentId",
                table: "Housings");

            migrationBuilder.DropIndex(
                name: "IX_EducationalDetails_StudentId",
                table: "EducationalDetails");

            migrationBuilder.DropIndex(
                name: "IX_DisabilityLearningSupports_StudentId",
                table: "DisabilityLearningSupports");

            migrationBuilder.DropIndex(
                name: "IX_ContactDetails_StudentId",
                table: "ContactDetails");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_StudentId",
                table: "Submissions",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDetails_StudentId",
                table: "PersonalDetails",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Housings_StudentId",
                table: "Housings",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EducationalDetails_StudentId",
                table: "EducationalDetails",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DisabilityLearningSupports_StudentId",
                table: "DisabilityLearningSupports",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetails_StudentId",
                table: "ContactDetails",
                column: "StudentId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Submissions_StudentId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_PersonalDetails_StudentId",
                table: "PersonalDetails");

            migrationBuilder.DropIndex(
                name: "IX_Housings_StudentId",
                table: "Housings");

            migrationBuilder.DropIndex(
                name: "IX_EducationalDetails_StudentId",
                table: "EducationalDetails");

            migrationBuilder.DropIndex(
                name: "IX_DisabilityLearningSupports_StudentId",
                table: "DisabilityLearningSupports");

            migrationBuilder.DropIndex(
                name: "IX_ContactDetails_StudentId",
                table: "ContactDetails");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_StudentId",
                table: "Submissions",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDetails_StudentId",
                table: "PersonalDetails",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Housings_StudentId",
                table: "Housings",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalDetails_StudentId",
                table: "EducationalDetails",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_DisabilityLearningSupports_StudentId",
                table: "DisabilityLearningSupports",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetails_StudentId",
                table: "ContactDetails",
                column: "StudentId");
        }
    }
}
