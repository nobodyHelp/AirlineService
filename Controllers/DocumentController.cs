using Microsoft.AspNetCore.Mvc;

namespace AirlineService.Controllers
{
    public class DocumentController : Controller
    {
        private readonly DataContext _context;
        public DocumentController(DataContext context)
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

        
        //GET
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Создаем новый документ
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Document document)
        {
            _context.Documents.Add(document);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Открываем форму редактирования
        /// документа по его идентификатору
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
            var documentFromDb = _context.Documents.Find(id);

            if (documentFromDb == null)
            {
                return NotFound();
            }

            return View(documentFromDb);
        }

        /// <summary>
        /// Редактируем документ
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Document document)
        {
            _context.Documents.Update(document);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Открываем форму удаления
        /// документа по его идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Passenger/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Documents == null)
            {
                return NotFound();
            }

            var document = await _context.Documents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }


        /// <summary>
        /// Удаляем документ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // POST: Passenger/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Documents == null)
            {
                return Problem("Entity set 'DataContext.Tickets'  is null.");
            }
            var document = await _context.Documents.FindAsync(id);
            if (document != null)
            {
                _context.Documents.Remove(document);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(PassengerController.Index));
        }
    }
}
