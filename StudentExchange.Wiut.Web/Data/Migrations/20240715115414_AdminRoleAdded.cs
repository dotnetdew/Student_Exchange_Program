using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentExchange.Wiut.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdminRoleAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DisabilityLearningSupport_AspNetUsers_StudentId",
                table: "DisabilityLearningSupport");

            migrationBuilder.DropForeignKey(
                name: "FK_Housing_AspNetUsers_StudentId",
                table: "Housing");

            migrationBuilder.DropForeignKey(
                name: "FK_Submission_AspNetUsers_StudentId",
                table: "Submission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Submission",
                table: "Submission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Housing",
                table: "Housing");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DisabilityLearningSupport",
                table: "DisabilityLearningSupport");

            migrationBuilder.RenameTable(
                name: "Submission",
                newName: "Submissions");

            migrationBuilder.RenameTable(
                name: "Housing",
                newName: "Housings");

            migrationBuilder.RenameTable(
                name: "DisabilityLearningSupport",
                newName: "DisabilityLearningSupports");

            migrationBuilder.RenameIndex(
                name: "IX_Submission_StudentId",
                table: "Submissions",
                newName: "IX_Submissions_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Housing_StudentId",
                table: "Housings",
                newName: "IX_Housings_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_DisabilityLearningSupport_StudentId",
                table: "DisabilityLearningSupports",
                newName: "IX_DisabilityLearningSupports_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Submissions",
                table: "Submissions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Housings",
                table: "Housings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DisabilityLearningSupports",
                table: "DisabilityLearningSupports",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DisabilityLearningSupports_AspNetUsers_StudentId",
                table: "DisabilityLearningSupports",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Housings_AspNetUsers_StudentId",
                table: "Housings",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_AspNetUsers_StudentId",
                table: "Submissions",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DisabilityLearningSupports_AspNetUsers_StudentId",
                table: "DisabilityLearningSupports");

            migrationBuilder.DropForeignKey(
                name: "FK_Housings_AspNetUsers_StudentId",
                table: "Housings");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_AspNetUsers_StudentId",
                table: "Submissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Submissions",
                table: "Submissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Housings",
                table: "Housings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DisabilityLearningSupports",
                table: "DisabilityLearningSupports");

            migrationBuilder.RenameTable(
                name: "Submissions",
                newName: "Submission");

            migrationBuilder.RenameTable(
                name: "Housings",
                newName: "Housing");

            migrationBuilder.RenameTable(
                name: "DisabilityLearningSupports",
                newName: "DisabilityLearningSupport");

            migrationBuilder.RenameIndex(
                name: "IX_Submissions_StudentId",
                table: "Submission",
                newName: "IX_Submission_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Housings_StudentId",
                table: "Housing",
                newName: "IX_Housing_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_DisabilityLearningSupports_StudentId",
                table: "DisabilityLearningSupport",
                newName: "IX_DisabilityLearningSupport_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Submission",
                table: "Submission",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Housing",
                table: "Housing",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DisabilityLearningSupport",
                table: "DisabilityLearningSupport",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DisabilityLearningSupport_AspNetUsers_StudentId",
                table: "DisabilityLearningSupport",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Housing_AspNetUsers_StudentId",
                table: "Housing",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submission_AspNetUsers_StudentId",
                table: "Submission",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
