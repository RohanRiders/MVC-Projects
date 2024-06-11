using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsletterAppMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //The SignUp method is passing in three input parameters from the index.cshtml. 
        [HttpPost] //Anytime you are creating a post method you are needing to put this tag above it. 
        public ActionResult SignUp(string firstName, string lastName, string emailAddress)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(emailAddress))
            {
                //if there is a null value passsed into the method we will send the user to this page. 
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                //Here is the connection string to the local DB. 
                string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Newsletter;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                //if the user passes all filled out fields then we will do the below logic.

                //remember parameterization is a way to prevent SQL injection. 
                string queryString = @"INSERT INTO SignUps (FirstName, LastName, EmailAddress) VALUES (@FirstName, @Lastname, @EmailAddress)";

                //remember a "using" statement allows you to get rid of connections when your done with them so you don't risk having memory leaks. 
                using (SqlConnection connection = new SqlConnection(connectionString)) 
                {
                    //This logic below allows you to insert whatever the user adds to the form into the database, as long as it matches the expected paramaters. 

                    //Here is how you add the parameters to the database in ADO.NET. 
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.Add("@FirstName", SqlDbType.VarChar);
                    command.Parameters.Add("@LastName", SqlDbType.VarChar);
                    command.Parameters.Add("@EmailAddress", SqlDbType.VarChar);

                    //This is how you add the values to the database in ADO.NET. 
                    command.Parameters["@FirstName"].Value = firstName;
                    command.Parameters["@LastName"].Value = lastName;
                    command.Parameters["@EmailAddress"].Value = emailAddress;

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                }
                return View("Success");
            }
        }

        public ActionResult Admin()
        {
            return View();
        }
    }
}