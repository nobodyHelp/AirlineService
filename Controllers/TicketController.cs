using Microsoft.AspNetCore.Mvc;

namespace AirlineService.Controllers
{
    public class TicketController : Controller
    {
        private readonly DataContext _context;
        public TicketController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Ticket> ticketsList = _context.Tickets.ToList();

            return View(ticketsList);
        }

        public IActionResult MoreInfo(int id)
        {
            var ticket = _context.Tickets.Find(id);
            if (ticket == null)
            {
                return NotFound();
            }

            ticket = _context.Tickets
                .Where(ticket => ticket.Id == id)
                .Include(p => p.Passengers)
                .ThenInclude(d => d.Documents)
                .FirstOrDefault();

            return View(ticket);
        }

        //GET
        public IActionResult Passengers(int id)
        {
            var ticket = _context.Tickets.Find(id);
            if (ticket == null)
            {
                return NotFound();
            }

            ticket = _context.Tickets
                .Where(ticket => ticket.Id == id)
                .Include(p => p.Passengers)                
                .FirstOrDefault();

            return View(ticket);
        }

        //GET
        public IActionResult Create()
        {            
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var ticketFromDB = _context.Tickets.Find(id);

            if(ticketFromDB == null)
            {
                return NotFound();
            }

            return View(ticketFromDB);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var ticketFromDB = _context.Tickets.Find(id);

            if (ticketFromDB == null)
            {
                return NotFound();
            }

            return View(ticketFromDB);
        }

        //POST
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var ticketFromDB = _context.Tickets.Find(id);

            if (ticketFromDB == null)
            {
                return NotFound();
            }

            _context.Tickets.Remove(ticketFromDB);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
