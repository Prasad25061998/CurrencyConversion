using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurrencyConversion.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TwoTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurrencyConversionAudit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InputAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InputCurrency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConvertedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OutputCurrency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyConversionAudit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyData",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    currencyCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    currencyRate = table.Column<long>(type: "bigint", nullable: false),
                    createdDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyData", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrencyConversionAudit");

            migrationBuilder.DropTable(
                name: "CurrencyData");
        }
    }
}
