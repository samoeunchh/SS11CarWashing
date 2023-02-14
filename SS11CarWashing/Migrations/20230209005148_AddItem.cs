﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SS11CarWashing.Migrations
{
    /// <inheritdoc />
    public partial class AddItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModelId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrandId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OilTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsStock = table.Column<bool>(type: "bit", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Item_ItemType_ItemTypeId",
                        column: x => x.ItemTypeId,
                        principalTable: "ItemType",
                        principalColumn: "ItemTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_ItemTypeId",
                table: "Item",
                column: "ItemTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Item");
        }
    }
}
