namespace AirlineService.Data.Entities
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
        public Passenger Passenger { get; set; }
        public int PassengerId { get; set; }
    }
}
