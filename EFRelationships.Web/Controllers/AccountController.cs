using EFRelationships.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EFRelationships.Web.Controllers
{
    public class AccountController : Controller
    {
        private string _connectionString;
        public AccountController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Signup(User user, string passwordhash)
        {
            var db = new UserRepsitory(_connectionString);
            db.AddUser(user, passwordhash);
            return Redirect("/account/login");
        }
        public IActionResult Login()
        {
            if (TempData["Error"] != null)
            {
                ViewBag.Message = TempData["Error"];
            }
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var repo = new UserRepsitory(_connectionString);
            var user = repo.Login(email, password);
            if (user == null)
            {
                TempData["Error"] = "Invalid login!";
                return Redirect("/account/login");
            }
            var claims = new List<Claim>
            {
                new Claim("user", email)
            };

            HttpContext.SignInAsync(new ClaimsPrincipal(
                new ClaimsIdentity(claims, "Cookies", "user", "role"))).Wait();

            return Redirect("/home/Index");

           
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync().Wait();
            return Redirect("/home/index");


        }
    }
}
