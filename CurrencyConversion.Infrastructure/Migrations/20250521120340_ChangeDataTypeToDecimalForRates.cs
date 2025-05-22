using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurrencyConversion.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDataTypeToDecimalForRates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "currencyRate",
                table: "CurrencyData",
                type: "decimal(18,6)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "currencyRate",
                table: "CurrencyData",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)");
        }
    }
}
