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


        /// <summary>
        /// Получаем полную информацию о билете, включая данные
        /// пассажира и документов
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult MoreInfo(int id)
        {
            var ticket = _context.Tickets.Find(id);
            if (ticket == null)
            {
                return NotFound();
            }

            ticket = _context.Tickets
                .Where(ticket => ticket.Id == id)
                .Include(p => p.Passenger)
                .ThenInclude(d => d.Documents)
                .FirstOrDefault();

            return View(ticket);
        }

        //GET
        public IActionResult Create()
        {            
            return View();
        }


        /// <summary>
        /// Создаем новый билет
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        /// <summary>
        /// Получаем представление редактирования
        /// билета по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Редактируем билет
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Получаем представление удаления билета
        /// по его идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Удаляем билет
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Получаем пассажира по идентификатору билета
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET
        public IActionResult GetPassenger(int id)
        {
            var passenger = _context.Tickets.Find(id);
            if (passenger == null)
            {
                return NotFound();
            }

            passenger = _context.Tickets
                .Where(t => t.Id == id)
                .Include(p => p.Passenger)
                .FirstOrDefault();

            return View(passenger);
        }
    }
}
