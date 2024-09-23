using Microsoft.AspNetCore.Mvc;

public class AuthController : Controller
{
    private readonly ApplicationDbContext _context;

    public AuthController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SignUp(UserViewModel model)
    {
        if (ModelState.IsValid && model.Password == model.ConfirmPassword)
        {
            var newUser = new User
            {
                FullName = model.FullName,
                Email = model.Email,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber,
                JoinDate = DateTime.Now
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return RedirectToAction("Login");
        }
        ModelState.AddModelError("", "Şifreler eşleşmiyor!");
        return View(model);
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string email, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        if (user != null)
        {
            // Kullanıcı giriş yapıldı
            return RedirectToAction("Index", "Home"); // Anasayfaya yönlendir
        }
        ModelState.AddModelError("", "Geçersiz e-posta veya şifre!");
        return View();
    }
}
