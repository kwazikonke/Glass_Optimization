using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlassOpt.Migrations
{
    /// <inheritdoc />
    public partial class Updating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockSheet",
                columns: table => new
                {
                    StockSheet_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockSheet", x => x.StockSheet_Id);
                });

            migrationBuilder.CreateTable(
                name: "Panel",
                columns: table => new
                {
                    Panel_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    AllocatedStockSheetStockSheet_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Panel", x => x.Panel_Id);
                    table.ForeignKey(
                        name: "FK_Panel_StockSheet_AllocatedStockSheetStockSheet_Id",
                        column: x => x.AllocatedStockSheetStockSheet_Id,
                        principalTable: "StockSheet",
                        principalColumn: "StockSheet_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Panel_AllocatedStockSheetStockSheet_Id",
                table: "Panel",
                column: "AllocatedStockSheetStockSheet_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Panel");

            migrationBuilder.DropTable(
                name: "StockSheet");
        }
    }
}
