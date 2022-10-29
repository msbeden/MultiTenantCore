using Multiple.Enums;
using Multiple.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace Multiple.Migrations.TableStructure.Product
{
    [DbContext(typeof(MultipleDbContext))]
    [Migration("06001-CreateProduct_Table")]
    public partial class CreateProduct_Table : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(name: "Product", schema: "dbo", columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Rate = table.Column<int>(type: "int", nullable: false),
                TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Status = table.Column<short>(nullable: true, defaultValue: Status.Enabled, defaultValueSql: "1"),
            }, constraints: table =>
            {
                table.PrimaryKey("PK_Product", x => x.Id);
            });
        }
    }
}
