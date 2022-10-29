using Multiple.Enums;
using Multiple.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace Multiple.Migrations.TableStructure.User
{
    [DbContext(typeof(SharedDbContext))]
    [Migration("02001-CreateUser_Table")]
    public class CreateUser : Migration
    {
        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(name: "User", schema: "dbo", columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                NameSurname = table.Column<string>(nullable: false, type: "varchar(150)"),
                UserName = table.Column<string>(nullable: false, type: "varchar(150)"),
                Password = table.Column<string>(nullable: false, type: "varchar(150)"),
                ConnectionString = table.Column<string>(type: "nvarchar(max)", nullable: false),
                TenantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Status = table.Column<short>(nullable: true, defaultValue: Status.Enabled, defaultValueSql: "1"),
            }, constraints: table =>
            {
                table.PrimaryKey("PK_UserId", x => x.Id);
            });
        }
    }
}
