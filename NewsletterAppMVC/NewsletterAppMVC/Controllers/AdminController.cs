using NewsletterAppMVC.Models;
using NewsletterAppMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsletterAppMVC.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            //********************************************************************************Entity Framework Example*********************************************************************
            //It is considered best practice to wrap instanciated entity objects in a using statement so that the database connection is closed when done. 
            using (NewsletterEntities db = new NewsletterEntities())
            {
                //We are going to use this logic to map our database object to our view model. signups is a variable that represents all the records in the database. 
                //We are also using LINQ to essentially query the db for null references. The value null in the db will indicate active users. 
                //var signups = db.SignUps.Where(x => x.Removed == null).ToList(); ****Filter Option using Lambda *****
                var signups = (from c in db.SignUps //*****Filter Option 2 using LINQ******
                               where c.Removed == null
                               select c).ToList();
                //So signups will only be those who have the removed property == to null. 
                var signupVms = new List<SignupVm>();
                foreach (var signup in signups)
                {
                    //Here we are mapping properties between two objects.
                    //There are libraries (auto mapper) that do mapping for you which are called "Reflecting" which can have a lot of overhead. 
                    var signupVm = new SignupVm();
                    signupVm.Id = signup.Id;
                    signupVm.FirstName = signup.FirstName;
                    signupVm.LastName = signup.LastName;
                    signupVm.EmailAddress = signup.EmailAddress;
                    signupVms.Add(signupVm);
                }
                return View(signupVms);
            }
            //*******************************************************************************End of Entity Framework Example***************************************************************
        }
        public ActionResult Unsubscribe(int Id)
        {
            //We will first establish our connection to the database using our db context. 
            using (NewsletterEntities db = new NewsletterEntities())
            {
                //We are reaching into our db and finding the primary key which is Id and assigning it to signup variable. 
                var signup = db.SignUps.Find(Id);
                //We are removing the value in the signup variable and assigning it the DateTime.Now value. 
                signup.Removed = DateTime.Now;
                //We then save our changes. 
                db.SaveChanges();
            }
            //We will redirect to the Index page which will then run its logic again. 
            return RedirectToAction("Index");
        }
    }
}