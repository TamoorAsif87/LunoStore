using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ordering.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialOrdering : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ordering");

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "ordering",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<string>(type: "text", nullable: true),
                    BillingsAddress_AddressLine = table.Column<string>(type: "character varying(180)", maxLength: 180, nullable: false),
                    BillingsAddress_City = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BillingsAddress_Country = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BillingsAddress_EmailAddress = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BillingsAddress_FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BillingsAddress_LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BillingsAddress_State = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BillingsAddress_ZipCode = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Payment_CVV = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    Payment_CardName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Payment_CardNumber = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    Payment_Expiration = table.Column<DateOnly>(type: "date", nullable: false),
                    ShippingAddress_AddressLine = table.Column<string>(type: "character varying(180)", maxLength: 180, nullable: false),
                    ShippingAddress_City = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ShippingAddress_Country = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ShippingAddress_EmailAddress = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ShippingAddress_FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ShippingAddress_LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ShippingAddress_State = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ShippingAddress_ZipCode = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                schema: "ordering",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductImages = table.Column<List<string>>(type: "text[]", nullable: false),
                    ProductName = table.Column<string>(type: "text", nullable: false),
                    ProductColors = table.Column<List<string>>(type: "text[]", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "ordering",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                schema: "ordering",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                schema: "ordering",
                table: "Orders",
                column: "CustomerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems",
                schema: "ordering");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "ordering");
        }
    }
}
