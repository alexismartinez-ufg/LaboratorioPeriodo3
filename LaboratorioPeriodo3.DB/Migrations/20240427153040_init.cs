using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaboratorioPeriodo3.DB.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autores",
                columns: table => new
                {
                    AutorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Apellido = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Autores__F58AE909E94B3F36", x => x.AutorID);
                });

            migrationBuilder.CreateTable(
                name: "Libros",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ISBN = table.Column<string>(type: "varchar(13)", unicode: false, maxLength: 13, nullable: true),
                    AnioEdicion = table.Column<int>(type: "int", nullable: true),
                    Editorial = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    NacionalidadAutor = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Libros__06370DAD6C9D3516", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "LibrosAutores",
                columns: table => new
                {
                    CodigoLibro = table.Column<int>(type: "int", nullable: false),
                    AutorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibrosAutores", x => new { x.CodigoLibro, x.AutorID });
                    table.ForeignKey(
                        name: "FK_LibrosAutores_Autores_AutorID",
                        column: x => x.AutorID,
                        principalTable: "Autores",
                        principalColumn: "AutorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LibrosAutores_Libros_CodigoLibro",
                        column: x => x.CodigoLibro,
                        principalTable: "Libros",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LibrosAutores_AutorID",
                table: "LibrosAutores",
                column: "AutorID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LibrosAutores");

            migrationBuilder.DropTable(
                name: "Autores");

            migrationBuilder.DropTable(
                name: "Libros");
        }
    }
}
