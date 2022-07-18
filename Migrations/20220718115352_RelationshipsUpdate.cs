using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirlineService.Migrations
{
    public partial class RelationshipsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passengers_Tickets_TicketId",
                table: "Passengers");

            migrationBuilder.DropIndex(
                name: "IX_Passengers_TicketId",
                table: "Passengers");

            migrationBuilder.CreateTable(
                name: "PassengerTicket",
                columns: table => new
                {
                    PassengersId = table.Column<int>(type: "int", nullable: false),
                    TicketId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassengerTicket", x => new { x.PassengersId, x.TicketId });
                    table.ForeignKey(
                        name: "FK_PassengerTicket_Passengers_PassengersId",
                        column: x => x.PassengersId,
                        principalTable: "Passengers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PassengerTicket_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PassengerTicket_TicketId",
                table: "PassengerTicket",
                column: "TicketId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PassengerTicket");

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_TicketId",
                table: "Passengers",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Passengers_Tickets_TicketId",
                table: "Passengers",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
