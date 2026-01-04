using Hi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hi.Controllers
{
    public class RegistrationController : Controller
    {
        private static List<Users> _user = new List<Users>();

        public static Users currentUsers = null;

        public RegistrationController()
        {
            Users u1 = new Users { Id = 1, Email = "Admin@gmail.com", Username = "Admin", Password = "123" , key="Admin"};
            _user.Add(u1);

            Users u2 = new Users { Id = 2, Email = "Mustafa@gmail.com", Username = "Mustafa", Password = "1969" , key="Users"};
            _user.Add(u2);
            
        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LogIn(Login log)
        {
            if(ModelState.IsValid)
            {
                var Username = _user.Find(x => x.Username.ToLower() == log.Username.ToLower());

                if(Username != null)
                {
                    if (Username.Password == log.Password)
                    {
                        
                        currentUsers = Username;

                        if (currentUsers.key == "Admin")
                        {
                            return RedirectToAction("Show" , "He");
                        }
                        else
                        {
                            return RedirectToAction("Profile" , "He");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Password is incorrect");
                    }
                }
                else 
                {
                    ModelState.AddModelError("", "Username is incorrect");
                }
                return View(log);
            }
            return View();
        }
        public IActionResult Welcome()
        {
            return View();
        }
    }
}
