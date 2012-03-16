using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraduateSubmissionsMVC.Models.SysAdmin
{
    //major roles used by the system
    public class RolesList
    {
       public List<string> Roles { get; set; }

        public RolesList() 
        {
            Roles = new List<string>();
            Roles.Add("Admin Assist");
            Roles.Add("Reviewer");
            Roles.Add("Decider");
            Roles.Add("Sys Admin");
        }
    }
}