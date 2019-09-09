using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WideWorldImporters.API.Migrations
{
    public partial class Model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Warehouse");

            migrationBuilder.CreateTable(
                name: "StockItems",
                schema: "Warehouse",
                columns: table => new
                {
                    StockItemID = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StockItemName = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    FornecedorID = table.Column<int>(type: "int", nullable: false),
                    ColorID = table.Column<int>(type: "int", nullable: true),
                    PacoteUnidadeID = table.Column<int>(type: "int", nullable: false),
                    EmbalagemID = table.Column<int>(type: "int", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Tamanho = table.Column<string>(type: "nvarchar(40)", nullable: true),
                    LeadTimeDays = table.Column<int>(type: "int", nullable: false),
                    QuantityPerOuter = table.Column<int>(type: "int", nullable: false),
                    Refrigeracao = table.Column<bool>(type: "bit", nullable: false),
                    CodBarras = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Imposto = table.Column<decimal>(type: "decimal(18, 3)", nullable: false),
                    PrecoUnidade = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    RecommendedRetailPrice = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    TypicalWeightPerUnit = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    MarketingComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomFields = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true, computedColumnSql: "json_query([CustomFields],N'$.Tags')"),
                    SearchDetails = table.Column<string>(type: "nvarchar(max)", nullable: true, computedColumnSql: "concat([StockItemName],N' ',[MarketingComments])"),
                    LastEditedBy = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UltimaEdicao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockItems", x => x.StockItemID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockItems",
                schema: "Warehouse");
        }
    }
}
