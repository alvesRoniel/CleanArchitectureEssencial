using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanArchMvc.Infra.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CATEGORIES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORIES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCTS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Stok = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PRODUCTS_CATEGORIES_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CATEGORIES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CATEGORIES",
                columns: new[] { "Id", "CreatedDate", "ModifiedDate", "Name" },
                values: new object[] { 1, new DateTime(2021, 9, 9, 13, 46, 2, 143, DateTimeKind.Local).AddTicks(2146), null, "Material Escolar" });

            migrationBuilder.InsertData(
                table: "CATEGORIES",
                columns: new[] { "Id", "CreatedDate", "ModifiedDate", "Name" },
                values: new object[] { 2, new DateTime(2021, 9, 9, 13, 46, 2, 145, DateTimeKind.Local).AddTicks(5438), null, "Eletrônicos" });

            migrationBuilder.InsertData(
                table: "CATEGORIES",
                columns: new[] { "Id", "CreatedDate", "ModifiedDate", "Name" },
                values: new object[] { 3, new DateTime(2021, 9, 9, 13, 46, 2, 145, DateTimeKind.Local).AddTicks(5482), null, "Acessórios" });

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTS_CategoryId",
                table: "PRODUCTS",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PRODUCTS");

            migrationBuilder.DropTable(
                name: "CATEGORIES");
        }
    }
}
