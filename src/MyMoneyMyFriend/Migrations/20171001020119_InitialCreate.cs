using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MyMoneyMyFriend.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Entity framework is trying to build a database schema that closely reflects my entities for the application  
            migrationBuilder.CreateTable(
                name: "Restaurants", // Name of the table 
                columns: table => new // Columns of the table
                {
                    // Note that Name, Cuisine and Id are properties of Restaurant class 
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cuisine = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 80, nullable: false) // Some of the DataAnnotations are applied here. e.g. MaxLenggth 80
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.Id);
                });

            // You could execute arbitrary SQL using migrationBuilder.Sql("INSERT")
            // You could create an index if a column is queried a lot -> migrationBuilder.CreateIndex()
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Restaurants");
        }
    }
}
