using Microsoft.AspNetCore.Mvc;
using ToDO.Data;
using ToDO.Models;

namespace ToDO.Controllers
{
    public class UserController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        #region Loin
        public IActionResult Login()
        {
            
            return View(new User());
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            var founduser = context.Users
                        .FirstOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);
            
            if(founduser != null)
            {
                Response.Cookies.Append("name", founduser.Name, new CookieOptions { Expires = DateTime.Now.AddDays(1) });

                return RedirectToAction("Index", "ToDoList", new { id = founduser.UserId });
            }

            TempData["ErrorMessage"] = "Incorrect username or password.";

            return View(user);
        }
        #endregion

        #region Register
        public IActionResult Register()
        {
            return View(new User());
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                context.Users.Add(user);
                context.SaveChanges();
                return RedirectToAction("Index","ToDoList",new {id = user.UserId});
            }
            return View(user);
        }
        #endregion
    }
}
