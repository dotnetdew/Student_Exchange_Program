using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentExchange.Wiut.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class PersonalStudentPropertyChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PersonalDetails_StudentId",
                table: "PersonalDetails");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDetails_StudentId",
                table: "PersonalDetails",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PersonalDetails_StudentId",
                table: "PersonalDetails");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDetails_StudentId",
                table: "PersonalDetails",
                column: "StudentId",
                unique: true);
        }
    }
}
