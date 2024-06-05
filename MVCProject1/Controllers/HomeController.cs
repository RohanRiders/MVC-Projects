using MVCProject1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProject1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            User user = new User();
            user.Id = 1;
            user.FirstName = "Trason";
            user.LastName = "Carver";
            user.Age = 32;

            return View(user);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            //Just another example of what you can do with MVC
            throw new Exception("Invalid page"); 

            return View();
        }

        //This is an example of passing a paramter which can be used for user ids. If a user logs in you can display their account for example. 
        public ActionResult Contact(int id=0)
        {
            ViewBag.Message = id;

            return View();
        }
    }
}