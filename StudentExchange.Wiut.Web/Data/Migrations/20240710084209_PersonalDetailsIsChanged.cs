using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentExchange.Wiut.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class PersonalDetailsIsChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AreYouAbleToAttendAllLessons",
                table: "PersonalDetails");

            migrationBuilder.DropColumn(
                name: "CountryOfResidence",
                table: "PersonalDetails");

            migrationBuilder.DropColumn(
                name: "HasWestminster8DigitStudentID",
                table: "PersonalDetails");

            migrationBuilder.RenameColumn(
                name: "StudiedInUKPreviouslyOnStudentVisa",
                table: "PersonalDetails",
                newName: "UniversityStudentId");

            migrationBuilder.RenameColumn(
                name: "NationalityAsShownOnPassport",
                table: "PersonalDetails",
                newName: "FamilyName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UniversityStudentId",
                table: "PersonalDetails",
                newName: "StudiedInUKPreviouslyOnStudentVisa");

            migrationBuilder.RenameColumn(
                name: "FamilyName",
                table: "PersonalDetails",
                newName: "NationalityAsShownOnPassport");

            migrationBuilder.AddColumn<string>(
                name: "AreYouAbleToAttendAllLessons",
                table: "PersonalDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CountryOfResidence",
                table: "PersonalDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "HasWestminster8DigitStudentID",
                table: "PersonalDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
