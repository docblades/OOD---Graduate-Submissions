using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraduateSubmissionsMVC.Models
{
    public class Users
    {
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string[] Roles { get; set; }

        //profile
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
    }
}