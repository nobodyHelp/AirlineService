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

        //GET
        public IActionResult Delete(int? id)
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
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var passengerFromDB = _context.Passengers.Find(id);

            if (passengerFromDB == null)
            {
                return NotFound();
            }

            _context.Passengers.Remove(passengerFromDB);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
