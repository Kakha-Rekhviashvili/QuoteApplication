using Microsoft.AspNetCore.Mvc;
using QuoteApplication.Data;
using QuoteApplication.Models;



namespace QuoteApplication.Controllers
{
    public class QuoteController : Controller
    {
        private readonly ApplicationDbContext _db;


        public QuoteController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult AllQuotes(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var Quotes = from x in _db.Quotes select x;
            if (!String.IsNullOrEmpty(searchString))
            {
                Quotes = _db.Quotes.Where(q => q.QuoteContent.Contains(searchString) || q.Author.Contains(searchString));
            }
            return View(Quotes.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Quote quote)
        {
            var _quoteContent = _db.Quotes.Any(q => q.QuoteContent == quote.QuoteContent);
            if (_quoteContent)
            {
                ModelState.AddModelError("", "Already exist.");
                return View("Create");
            }
            else
            {
                _db.Quotes.Add(quote);
                _db.SaveChanges();
                return RedirectToAction("AllQuotes");
            }
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var editableId = _db.Quotes.Find(id);

            return View(editableId);
        }

        [HttpPost]
        public IActionResult Edit(int id, Quote quote)
        {
            var _quoteContent = _db.Quotes.Any(q => q.QuoteContent == quote.QuoteContent);
            if (_quoteContent)
            {
                ModelState.AddModelError("", "Unchanged or Already exist.");
                return View("Edit");
            }
            else
            {
                _db.Update(quote);
                _db.SaveChanges();
                return RedirectToAction("AllQuotes");
            }
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var quoteFromDb = _db.Quotes.Find(id);

            if (quoteFromDb == null)
            {
                return NotFound();
            }

            return View(quoteFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Quotes.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Quotes.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("AllQuotes");
        }

        public IActionResult AddQuoteToFav(int id)
        {
            var _quote = _db.Quotes.Find(id);
            _quote.IsFavorite = true;

            _db.Update(_quote);
            _db.SaveChanges();
            return RedirectToAction("AllQuotes");
        }

        public IActionResult DeleteQuoteFromFav(int id)
        {
            var _quote = _db.Quotes.Find(id);
            _quote.IsFavorite = false;

            _db.Update(_quote);
            _db.SaveChanges();
            return RedirectToAction("AllQuotes");
        }

        public IActionResult AllFavQuotes()
        {
            List<Quote> allFavQuotes = _db.Quotes.Where(q => q.IsFavorite == true).ToList();
            return View(allFavQuotes);
        }
    }
}
