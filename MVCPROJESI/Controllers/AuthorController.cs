using Microsoft.AspNetCore.Mvc;

public class AuthorController : Controller
{
    private readonly ApplicationDbContext _context;

    public AuthorController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult List()
    {
        var authors = _context.Authors.ToList();
        return View(authors);
    }

    public IActionResult Details(int id)
    {
        var author = _context.Authors.Find(id);
        return View(author);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(AuthorViewModel model)
    {
        if (ModelState.IsValid)
        {
            var newAuthor = new Author
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth
            };
            _context.Authors.Add(newAuthor);
            _context.SaveChanges();
            return RedirectToAction("List");
        }
        return View(model);
    }

    public IActionResult Edit(int id)
    {
        var author = _context.Authors.Find(id);
        return View(author);
    }

    [HttpPost]
    public IActionResult Edit(AuthorViewModel model)
    {
        if (ModelState.IsValid)
        {
            var updatedAuthor = _context.Authors.Find(model.Id);
            if (updatedAuthor != null)
            {
                updatedAuthor.FirstName = model.FirstName;
                updatedAuthor.LastName = model.LastName;
                updatedAuthor.DateOfBirth = model.DateOfBirth;
                _context.Authors.Update(updatedAuthor);
                _context.SaveChanges();
            }
            return RedirectToAction("List");
        }
        return View(model);
    }

    public IActionResult Delete(int id)
    {
        var author = _context.Authors.Find(id);
        return View(author);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var author = _context.Authors.Find(id);
        if (author != null)
        {
            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
        return RedirectToAction("List");
    }
}
