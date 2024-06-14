using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsletterAppMVC.ViewModels
{
    //Welcome to your View Model. This is a way for you to filter what data is sent to the view. It is a way of protecting sensitive data as well as only sending what the view needs. See NewsletterSignUp as reference. 
    public class SignupVm
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
    }
}