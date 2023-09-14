using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AppsServices.Models;


namespace AppsServices.Controllers
{


    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(login Login)
        {
            if (LoginUser(Login.AdminName, Login.Password))
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Login.AdminName)
            };

                var userIdentity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);

                //Just redirect to our index after logging in. 
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index");
        }

        private bool LoginUser(string AdminName, string Password)
        {
            if (AdminName == "admin" && Password == "123")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }


}
