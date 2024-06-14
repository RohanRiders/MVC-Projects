using NewsletterAppMVC.Models;
using NewsletterAppMVC.ViewModels;
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
        //Here is the connection string to the local DB. We want to make it private readonly to protect it a bit.
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
                //********************************************************************************Entity Framework Example*********************************************************************
                //The NewsletterEntities has the connection string built in when you set up Entity Framework. 
                using (NewsletterEntities db = new NewsletterEntities())
                {
                    var signup = new SignUp();
                    signup.FirstName = firstName;
                    signup.LastName = lastName;
                    signup.EmailAddress = emailAddress;

                    //When the sign up properties have been filled, add it to the database. 
                    db.SignUps.Add(signup);
                    //nothing is saved to the db until we run this method.
                    db.SaveChanges();
                 //*******************************************************************************End of Entity Framework Example***************************************************************
                }

                //********************************************************************************ADO.NET Example*******************************************************************************
                ////if the user passes all filled out fields then we will do the below logic.

                ////remember parameterization is a way to prevent SQL injection. 
                //string queryString = @"INSERT INTO SignUps (FirstName, LastName, EmailAddress) VALUES (@FirstName, @Lastname, @EmailAddress)";

                ////remember a "using" statement allows you to get rid of connections when your done with them so you don't risk having memory leaks. 
                //using (SqlConnection connection = new SqlConnection(connectionString)) 
                //{
                //    //This logic below allows you to insert whatever the user adds to the form into the database, as long as it matches the expected paramaters. 

                //    //Here is how you add the parameters to the database in ADO.NET. 
                //    SqlCommand command = new SqlCommand(queryString, connection);
                //    command.Parameters.Add("@FirstName", SqlDbType.VarChar);
                //    command.Parameters.Add("@LastName", SqlDbType.VarChar);
                //    command.Parameters.Add("@EmailAddress", SqlDbType.VarChar);

                //    //This is how you add the values to the database in ADO.NET. 
                //    command.Parameters["@FirstName"].Value = firstName;
                //    command.Parameters["@LastName"].Value = lastName;
                //    command.Parameters["@EmailAddress"].Value = emailAddress;

                //    connection.Open();
                //    command.ExecuteNonQuery();
                //    connection.Close();

                //}
                //******************************************************************************End of ADO.NET Example*******************************************************************************
                return View("Success");
            }
        }

        //public ActionResult Admin() We moved this logic into its own controller file called AdminController. 
        //{
        //    //********************************************************************************Entity Framework Example*********************************************************************
        //    //It is considered best practice to wrap instanciated entity objects in a using statement so that the database connection is closed when done. 
        //    using (NewsletterEntities db = new NewsletterEntities())
        //    {
        //        //We are going to use this logic to map our database object to our view model. signups is a variable that represents all the records in the database. 
        //        var signups = db.SignUps;
        //        var signupVms = new List<SignupVm>();
        //        foreach (var signup in signups)
        //        {
        //            //Here we are mapping properties between two objects. There are libraries (auto mapper) that do this for you which are called "Reflecting" which can have a lot of overhead. 
        //            var signupVm = new SignupVm();
        //            signupVm.FirstName = signup.FirstName;
        //            signupVm.LastName = signup.LastName;
        //            signupVm.EmailAddress = signup.EmailAddress;
        //            signupVms.Add(signupVm);
        //        }
        //        return View(signupVms);
        //    }
        //    //*******************************************************************************End of Entity Framework Example***************************************************************





        //    //********************************************************************************ADO.NET Example*******************************************************************************
        //    ////we want to reach into the database and present the data to the user front end using ADO.NET
        //    ////Here we are defining the sql query
        //    //string queryString = @"SELECT Id, FirstName, LastName, EmailAddress, SocialSecurityNumber from SignUps";
        //    ////Here we are creating a list called "signups" with a data type of NewsLetterSignUp
        //    //List<NewsletterSignUp> signups = new List<NewsletterSignUp>();

        //    ////Here we are establishing a connection and executing instructions. Specifically we are creating a connection object that passes in the connection string.
        //    //using (SqlConnection connection = new SqlConnection(connectionString))
        //    //{
        //    //    //We are then creating a command object that needs the query string and connection details. 
        //    //    SqlCommand command = new SqlCommand(queryString, connection);

        //    //    //With the connection details we are then executing the Open() method to open the connection
        //    //    connection.Open();

        //    //    //with the connection open we are then taking the command variable and running the Execute Reader method on it and storing it in the reader variable of data type SqlDataReader. 
        //    //    SqlDataReader reader = command.ExecuteReader();

        //    //    //The .Read() method comes from the SqlDataReader data type which is a bool and reads a record from the target data and will continue to read if there is another record available. In this case there is two records.
        //    //    while (reader.Read())
        //    //    {
        //    //        //While the condition is true we will be creating a NewsletterSignUp object and labling it as the signup variable. 
        //    //        var signup = new NewsletterSignUp();
        //    //        //We will be reading the "Id" field and assigning it to the signup.id property and converting it into a C# int. 
        //    //        signup.Id = Convert.ToInt32(reader["Id"]);
        //    //        //we will be reading the Firstname field in SQL Server and converting it to a string and assinging it to the FirstName property in the signup class object. 
        //    //        signup.FirstName = reader["FirstName"].ToString();
        //    //        //The same thing is being done here but for LastName
        //    //        signup.LastName = reader["LastName"].ToString();
        //    //        //Same here
        //    //        signup.EmailAddress = reader["EmailAddress"].ToString();
        //    //        //Same applies here
        //    //        signup.SocialSecurityNumber = reader["SocialSecurityNumber"].ToString();
        //    //        //Once all the properties have been set we are adding those to the signup list object. 
        //    //        signups.Add(signup);
        //    //******************************************************************************End of ADO.NET Example*******************************************************************************
        //}
    }
}