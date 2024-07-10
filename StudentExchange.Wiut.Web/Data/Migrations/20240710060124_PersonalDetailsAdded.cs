using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentExchange.Wiut.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class PersonalDetailsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonalDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ForeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondForeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThirdForeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrefferedName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreviousFamilyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasWestminster8DigitStudentID = table.Column<bool>(type: "bit", nullable: false),
                    CountryOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryOfResidence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassportNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfIssue = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfExpiry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NationalityAsShownOnPassport = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassportFile = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PassportFileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassportFileType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AreYouAbleToAttendAllLessons = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudiedInUKPreviouslyOnStudentVisa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalDetails_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDetails_StudentId",
                table: "PersonalDetails",
                column: "StudentId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonalDetails");
        }
    }
}
