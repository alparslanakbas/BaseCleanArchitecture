using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseCleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateEmployeeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BirincilAd = table.Column<string>(type: "text", nullable: false),
                    IkincilAd = table.Column<string>(type: "text", nullable: false),
                    DogumTarihi = table.Column<DateOnly>(type: "date", nullable: false),
                    Maas = table.Column<decimal>(type: "money", nullable: false),
                    Ulke = table.Column<string>(type: "text", nullable: true),
                    Sehir = table.Column<string>(type: "text", nullable: true),
                    Ilce = table.Column<string>(type: "text", nullable: true),
                    TCNO = table.Column<string>(type: "text", nullable: false),
                    Mail = table.Column<string>(type: "text", nullable: true),
                    Telefon = table.Column<string>(type: "text", nullable: true),
                    OlusturmaTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GuncellemeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Durum = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DurumTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
