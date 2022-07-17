namespace AirlineService.Data.Entities
{
    public class Passenger
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public Ticket Ticket { get; set; }
        public int TicketId { get; set; }
        public List<Document> Documents { get; set; }
    }
}
