using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InterviewWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Vacancies",
                columns: new[] { "VacancyId", "Description", "ProposalCompensation", "Title" },
                values: new object[,]
                {
                    { 1, "John", 30000m, "Developer" },
                    { 2, "Chris", 50000m, "Manager" },
                    { 3, "Mukesh", 20000m, "Consultant" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Vacancies",
                keyColumn: "VacancyId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Vacancies",
                keyColumn: "VacancyId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Vacancies",
                keyColumn: "VacancyId",
                keyValue: 3);
        }
    }
}
