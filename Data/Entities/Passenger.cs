namespace AirlineService.Data.Entities
{
    public class Passenger
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public List<Ticket> Ticket { get; set; }
        public List<Document> Documents { get; set; }
    }
}
