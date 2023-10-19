using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MountainHoneyApp.Models;

namespace MountainHoneyApp.Controllers
{
    public class AccountController : Controller
    {

        private readonly TestingContext _context;

        public AccountController(TestingContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userdetail = await _context.Userdetails
                .SingleOrDefaultAsync(m => m.Email == model.Email && m.Password == model.Password);
                if (userdetail == null)
                {
                    ModelState.AddModelError("Password", "Invalid login attempt.");
                    return View("Index");
                }
                return RedirectToAction("Index", "Sunrises");
                //  HttpContext.Session.SetString("userId", userdetail.Name);

            }
            else
            {
                return RedirectToAction("Index", "Account");
            }
            // return RedirectToAction("Index", "Account");
        }
        [HttpPost]
        public async Task<ActionResult> Registar(RegistrationViewModel model)
        {

            if (ModelState.IsValid)
            {
                Userdetail user = new Userdetail
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password,
                    Mobile = model.Mobile

                };
                _context.Add(user);
                await _context.SaveChangesAsync();

            }
            else
            {
                return View("Registration");
            }
            return RedirectToAction("Index", "Account");
        }
        // registration Page load
        public IActionResult Registration()
        {
            ViewData["Message"] = "Registration Page";

            return View();
        }

    }
}
