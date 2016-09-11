using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmsManager.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    Code = table.Column<string>(maxLength: 2, nullable: false),
                    MobileCode = table.Column<string>(maxLength: 3, nullable: false),
                    Name = table.Column<string>(nullable: false),
                    SmsPrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmsMessage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    CountryId = table.Column<int>(nullable: false),
                    DateSent = table.Column<DateTime>(nullable: false),
                    From = table.Column<string>(maxLength: 50, nullable: false),
                    Message = table.Column<string>(maxLength: 256, nullable: false),
                    Status = table.Column<int>(nullable: false),
                    To = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmsMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmsMessage_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SmsMessage_CountryId",
                table: "SmsMessage",
                column: "CountryId");


            migrationBuilder.Sql("INSERT INTO Country (Name, MobileCode, Code, SmsPrice) VALUES ('Germany','262', '49', '0.055')");
            migrationBuilder.Sql("INSERT INTO Country (Name, MobileCode, Code, SmsPrice) VALUES ('Austria','232', '43', '0.053')");
            migrationBuilder.Sql("INSERT INTO Country (Name, MobileCode, Code, SmsPrice) VALUES ('Poland','260', '49', '0.032')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SmsMessage");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
