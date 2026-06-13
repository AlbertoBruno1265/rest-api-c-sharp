using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebServiceFiap.Migrations
{
    /// <inheritdoc />
    public partial class V1__Cria_Estrutura_Banco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_catador",
                columns: table => new
                {
                    id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    capacidade_volume_total = table.Column<float>(type: "BINARY_FLOAT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_catador", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_centros_coleta",
                columns: table => new
                {
                    id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    endereco = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    volume_itens_total = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    volume_itens_atual = table.Column<float>(type: "BINARY_FLOAT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_centros_coleta", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_descartador",
                columns: table => new
                {
                    id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    endereco = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_descartador", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_itens",
                columns: table => new
                {
                    id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    volume = table.Column<float>(type: "BINARY_FLOAT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_itens", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_usuario",
                columns: table => new
                {
                    id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    senha = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    funcao = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_usuario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_coletas",
                columns: table => new
                {
                    id_coleta = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    data = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    id_catador = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    id_descartador = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    id_centro = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    foi_finalizada = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_coletas", x => x.id_coleta);
                    table.ForeignKey(
                        name: "FK_tb_coletas_tb_catador_id_catador",
                        column: x => x.id_catador,
                        principalTable: "tb_catador",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_coletas_tb_centros_coleta_id_centro",
                        column: x => x.id_centro,
                        principalTable: "tb_centros_coleta",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_coletas_tb_descartador_id_descartador",
                        column: x => x.id_descartador,
                        principalTable: "tb_descartador",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_catador_item",
                columns: table => new
                {
                    id_catador = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    id_item = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    foi_entregue = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_catador_item", x => new { x.id_catador, x.id_item });
                    table.ForeignKey(
                        name: "FK_tb_catador_item_tb_catador_id_catador",
                        column: x => x.id_catador,
                        principalTable: "tb_catador",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_catador_item_tb_itens_id_item",
                        column: x => x.id_item,
                        principalTable: "tb_itens",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_coleta_itens",
                columns: table => new
                {
                    id_coleta = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    id_item = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_coleta_itens", x => new { x.id_coleta, x.id_item });
                    table.ForeignKey(
                        name: "FK_tb_coleta_itens_tb_coletas_id_coleta",
                        column: x => x.id_coleta,
                        principalTable: "tb_coletas",
                        principalColumn: "id_coleta",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_coleta_itens_tb_itens_id_item",
                        column: x => x.id_item,
                        principalTable: "tb_itens",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_catador_item_id_item",
                table: "tb_catador_item",
                column: "id_item");

            migrationBuilder.CreateIndex(
                name: "IX_tb_coleta_itens_id_item",
                table: "tb_coleta_itens",
                column: "id_item");

            migrationBuilder.CreateIndex(
                name: "IX_tb_coletas_id_catador",
                table: "tb_coletas",
                column: "id_catador");

            migrationBuilder.CreateIndex(
                name: "IX_tb_coletas_id_centro",
                table: "tb_coletas",
                column: "id_centro");

            migrationBuilder.CreateIndex(
                name: "IX_tb_coletas_id_descartador",
                table: "tb_coletas",
                column: "id_descartador");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_catador_item");

            migrationBuilder.DropTable(
                name: "tb_coleta_itens");

            migrationBuilder.DropTable(
                name: "tb_usuario");

            migrationBuilder.DropTable(
                name: "tb_coletas");

            migrationBuilder.DropTable(
                name: "tb_itens");

            migrationBuilder.DropTable(
                name: "tb_catador");

            migrationBuilder.DropTable(
                name: "tb_centros_coleta");

            migrationBuilder.DropTable(
                name: "tb_descartador");
        }
    }
}
