using Microsoft.AspNetCore.Mvc;

namespace AirlineService.Controllers
{
    public class PassengerController : Controller
    {
        private readonly DataContext _context;
        public PassengerController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Passenger> passengerList = _context.Passengers.ToList();

            return View(passengerList);
        }

        //GET
        public IActionResult GetDocument(int id)
        {
            var passenger = _context.Passengers.Find(id);
            if (passenger == null)
            {
                return NotFound();
            }

            passenger = _context.Passengers
                .Where(passenger => passenger.Id == id)
                .Include(p => p.Documents)
                .FirstOrDefault();

            return View(passenger);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Passenger passenger)
        {
            _context.Passengers.Add(passenger);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var passengerFromDB = _context.Passengers.Find(id);

            if (passengerFromDB == null)
            {
                return NotFound();
            }

            return View(passengerFromDB);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Passenger passenger)
        {
            _context.Passengers.Update(passenger);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Passenger/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Passengers == null)
            {
                return NotFound();
            }

            var passenger = await _context.Passengers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (passenger == null)
            {
                return NotFound();
            }

            return View(passenger);
        }

        // POST: Passenger/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Passengers == null)
            {
                return Problem("Entity set 'DataContext.Tickets'  is null.");
            }
            var passenger = await _context.Passengers.FindAsync(id);
            if (passenger != null)
            {
                _context.Passengers.Remove(passenger);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Report(int id, DateTime tripStart, DateTime tripEnd)
        {
            List<Ticket> tickets = new List<Ticket>();

            var passenger = _context.Passengers.Find(id);
            if (passenger == null)
            {
                return NotFound();
            }

            passenger = _context.Passengers
                .Where(passenger => passenger.Id == id)
                .Include(p => p.Ticket)
                .FirstOrDefault();

            foreach (var item in passenger.Ticket)
            {
                if ((item.DateOfService <= tripStart) && (item.DateOfDeparture >= tripStart) && (item.DateOfArrival <= tripEnd))
                {
                   tickets.Add(item);
                }

                else if ((item.DateOfService >= tripStart) && (item.DateOfService <= tripEnd))
                {
                    tickets.Add(item);
                }
            }

            ReportViewModel reportViewModel = new ReportViewModel
            {
                Tickets = tickets,
                ServiceRendered = "yes"
            };
            
            return View(reportViewModel);
        }
    }
}
