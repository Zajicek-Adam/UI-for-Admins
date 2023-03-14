using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace uiforadmins.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Build",
                columns: table => new
                {
                    BuildId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Winrate = table.Column<int>(type: "int", nullable: false),
                    Playrate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Build", x => x.BuildId);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemRarity = table.Column<int>(type: "int", nullable: false),
                    ItemCost = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemId);
                });

            migrationBuilder.CreateTable(
                name: "Champions",
                columns: table => new
                {
                    ChampionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChampionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChampionGames = table.Column<int>(type: "int", nullable: false),
                    ChampionWinrate = table.Column<int>(type: "int", nullable: false),
                    BuildId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Champions", x => x.ChampionId);
                    table.ForeignKey(
                        name: "FK_Champions_Build_BuildId",
                        column: x => x.BuildId,
                        principalTable: "Build",
                        principalColumn: "BuildId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuildItem",
                columns: table => new
                {
                    BuildId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildItem", x => new { x.ItemId, x.BuildId });
                    table.ForeignKey(
                        name: "FK_BuildItem_Build_BuildId",
                        column: x => x.BuildId,
                        principalTable: "Build",
                        principalColumn: "BuildId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuildItem_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Otps",
                columns: table => new
                {
                    OtpId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OtpName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChampionId = table.Column<int>(type: "int", nullable: false),
                    OtpRank = table.Column<int>(type: "int", nullable: false),
                    GamesPlayed = table.Column<int>(type: "int", nullable: false),
                    Winrate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Otps", x => x.OtpId);
                    table.ForeignKey(
                        name: "FK_Otps_Champions_ChampionId",
                        column: x => x.ChampionId,
                        principalTable: "Champions",
                        principalColumn: "ChampionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Build",
                columns: new[] { "BuildId", "Description", "Playrate", "Winrate" },
                values: new object[,]
                {
                    { 1, "AP roaming", 35, 55 },
                    { 2, "Off-tank CDR", 15, 53 }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "ItemId", "ItemCost", "ItemDescription", "ItemName", "ItemRarity" },
                values: new object[,]
                {
                    { 1, 3400, "Anti-Tank AD Mythic item", "Kraken Slayer", 0 },
                    { 2, 3400, "Execute AD Mythic item", "Galeforce", 0 },
                    { 3, 3400, "Survivability AD Mythic item", "Shieldbow", 0 }
                });

            migrationBuilder.InsertData(
                table: "Champions",
                columns: new[] { "ChampionId", "BuildId", "ChampionGames", "ChampionName", "ChampionWinrate" },
                values: new object[] { 1, 2, 40, "Aurelion Sol", 55 });

            migrationBuilder.InsertData(
                table: "Champions",
                columns: new[] { "ChampionId", "BuildId", "ChampionGames", "ChampionName", "ChampionWinrate" },
                values: new object[] { 2, 1, 22, "Skarner", 51 });

            migrationBuilder.InsertData(
                table: "Otps",
                columns: new[] { "OtpId", "ChampionId", "GamesPlayed", "OtpName", "OtpRank", "Winrate" },
                values: new object[] { 1, 2, 800, "Flamekite", 3, 90 });

            migrationBuilder.CreateIndex(
                name: "IX_BuildItem_BuildId",
                table: "BuildItem",
                column: "BuildId");

            migrationBuilder.CreateIndex(
                name: "IX_Champions_BuildId",
                table: "Champions",
                column: "BuildId");

            migrationBuilder.CreateIndex(
                name: "IX_Otps_ChampionId",
                table: "Otps",
                column: "ChampionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuildItem");

            migrationBuilder.DropTable(
                name: "Otps");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Champions");

            migrationBuilder.DropTable(
                name: "Build");
        }
    }
}
