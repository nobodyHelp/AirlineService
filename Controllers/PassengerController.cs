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

        /// <summary>
        /// Получаем список всех пассажиров
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            IEnumerable<Passenger> passengerList = _context.Passengers.ToList();

            return View(passengerList);
        }


        /// <summary>
        /// Получаем список документов по идентификатору пассажира
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Создаем нового пассажира
        /// </summary>
        /// <param name="passenger"></param>
        /// <returns></returns>
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Passenger passenger)
        {
            _context.Passengers.Add(passenger);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Получеам представление редактирования
        /// пассажира по его идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Редактируем пассажиру
        /// </summary>
        /// <param name="passenger"></param>
        /// <returns></returns>
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Passenger passenger)
        {
            _context.Passengers.Update(passenger);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Получаем представление удаления пассажира
        /// по его идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Удаляем пассажира
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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



        /// <summary>
        /// Получаем отчет по пассажиру за определенный
        /// период времени. 
        /// Создаеется пустой список и заполняется билетами, используя два условия.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tripStart"></param>
        /// <param name="tripEnd"></param>
        /// <returns>Список билетов за определнный период времени</returns>
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
