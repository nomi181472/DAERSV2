using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAERS.API.Migrations
{
    public partial class AddingNutritionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NutritionFacts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    Carbohydrate = table.Column<double>(nullable: false),
                    Protein = table.Column<double>(nullable: false),
                    Fats = table.Column<double>(nullable: false),
                    Calorie = table.Column<double>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutritionFacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhotosNF",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Url = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    IsMain = table.Column<bool>(nullable: false),
                    PublicNId = table.Column<string>(nullable: true),
                    NutritionFactId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotosNF", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotosNF_NutritionFacts_NutritionFactId",
                        column: x => x.NutritionFactId,
                        principalTable: "NutritionFacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhotosNF_NutritionFactId",
                table: "PhotosNF",
                column: "NutritionFactId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhotosNF");

            migrationBuilder.DropTable(
                name: "NutritionFacts");
        }
    }
}
