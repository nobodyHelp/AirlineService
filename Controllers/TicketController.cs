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
            return View(_context.Tickets.ToList());
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

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tickets == null)
            {
                return Problem("Entity set 'DataContext.Tickets'  is null.");
            }
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
