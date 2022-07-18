namespace AirlineService.Data.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public string DeparturePoint { get; set; }
        public string DestinationPoint { get; set; }
        public string OrderNumber { get; set; }
        public string ServiceProvider { get; set; }
        public DateTime DateOfDeparture { get; set; }
        public DateTime DateOfArrival { get; set; }
        public DateTime DateOfService { get; set; }

        public Passenger Passenger { get; set; }

        public int PassengerId { get; set; }
    }
}
