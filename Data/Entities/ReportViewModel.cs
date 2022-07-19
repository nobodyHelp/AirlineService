using Microsoft.AspNetCore.Mvc.Rendering;

namespace AirlineService.Data.Entities
{
    public class ReportViewModel
    {
        public List<Ticket> Tickets { get; set; }
        public string ServiceRendered { get; set; } = "yes";
    }
}
