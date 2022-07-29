using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KysectAcademyTask.DataAccess.EfStructures.Migrations
{
    public partial class Change1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true, computedColumnSql: "[FirstName] + ' ' + [LastName]"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Submits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Homework = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Submits_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Path = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubmitId = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Path);
                    table.ForeignKey(
                        name: "FK_Files_Submits_SubmitId",
                        column: x => x.SubmitId,
                        principalTable: "Submits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComparisonResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName1 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FileName2 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Metrics = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SimilarityRate = table.Column<double>(type: "float", nullable: false),
                    TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComparisonResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComparisonResults_Files_FileName1",
                        column: x => x.FileName1,
                        principalTable: "Files",
                        principalColumn: "Path");
                    table.ForeignKey(
                        name: "FK_ComparisonResults_Files_FileName2",
                        column: x => x.FileName2,
                        principalTable: "Files",
                        principalColumn: "Path");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComparisonResults_FileName1",
                table: "ComparisonResults",
                column: "FileName1");

            migrationBuilder.CreateIndex(
                name: "IX_ComparisonResults_FileName2",
                table: "ComparisonResults",
                column: "FileName2");

            migrationBuilder.CreateIndex(
                name: "IX_Files_SubmitId",
                table: "Files",
                column: "SubmitId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_GroupId",
                table: "Students",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Submits_StudentId",
                table: "Submits",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComparisonResults");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Submits");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Groups");
        }
    }
}
