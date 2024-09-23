using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class BookController : Controller
{
    private readonly ApplicationDbContext _context;

    public BookController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult List()
    {
        var books = _context.Books.Include(b => b.Author).ToList();
        return View(books);
    }

    public IActionResult Details(int id)
    {
        var book = _context.Books.Include(b => b.Author).FirstOrDefault(b => b.Id == id);
        return View(book);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(BookViewModel model)
    {
        if (ModelState.IsValid)
        {
            var newBook = new Book
            {
                Title = model.Title,
                Genre = model.Genre,
                PublishDate = model.PublishDate,
                ISBN = model.ISBN,
                CopiesAvailable = model.CopiesAvailable
                // AuthorId'yi belirlemelisin
            };
            _context.Books.Add(newBook);
            _context.SaveChanges();
            return RedirectToAction("List");
        }
        return View(model);
    }

    public IActionResult Edit(int id)
    {
        var book = _context.Books.Find(id);
        return View(book);
    }

    [HttpPost]
    public IActionResult Edit(BookViewModel model)
    {
        if (ModelState.IsValid)
        {
            var updatedBook = _context.Books.Find(model.Id);
            if (updatedBook != null)
            {
                updatedBook.Title = model.Title;
                updatedBook.Genre = model.Genre;
                updatedBook.PublishDate = model.PublishDate;
                updatedBook.ISBN = model.ISBN;
                updatedBook.CopiesAvailable = model.CopiesAvailable;
                _context.Books.Update(updatedBook);
                _context.SaveChanges();
            }
            return RedirectToAction("List");
        }
        return View(model);
    }

    public IActionResult Delete(int id)
    {
        var book = _context.Books.Find(id);
        return View(book);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var book = _context.Books.Find(id);
        if (book != null)
        {
            _context.Books.Remove(book);
            _context.SaveChanges();
        }
        return RedirectToAction("List");
    }
}
