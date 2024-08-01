using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MasterPokemons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    Cpf = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterPokemons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pokemons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Evolutions = table.Column<string>(type: "TEXT", nullable: false),
                    Sprite = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterPokemonPokemons",
                columns: table => new
                {
                    MasterPokemonModelId = table.Column<int>(type: "INTEGER", nullable: false),
                    PokemonModelId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterPokemonPokemons", x => new { x.MasterPokemonModelId, x.PokemonModelId });
                    table.ForeignKey(
                        name: "FK_MasterPokemonPokemons_MasterPokemons_MasterPokemonModelId",
                        column: x => x.MasterPokemonModelId,
                        principalTable: "MasterPokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MasterPokemonPokemons_Pokemons_PokemonModelId",
                        column: x => x.PokemonModelId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MasterPokemonPokemons_PokemonModelId",
                table: "MasterPokemonPokemons",
                column: "PokemonModelId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterPokemons_Cpf",
                table: "MasterPokemons",
                column: "Cpf",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MasterPokemonPokemons");

            migrationBuilder.DropTable(
                name: "MasterPokemons");

            migrationBuilder.DropTable(
                name: "Pokemons");
        }
    }
}
