using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Profile;

namespace GraduateSubmissionsMVC.Models
{
    public class Profile : ProfileBase
    {
        [Display(Name = "First Name")]
        public virtual string FirstName
        {
            get
            {
                return (this.GetPropertyValue("FirstName").ToString());
            }
            set
            {
                this.SetPropertyValue("FirstName", value);
            }
        }

        [Display(Name = "Last Name")]
        public virtual string LastName
        {
            get
            {
                return (this.GetPropertyValue("LastName").ToString());
            }
            set
            {
                this.SetPropertyValue("LastName", value);
            }
        }

        [Display(Name = "Department")]
        public virtual string Department
        {
            get
            {
                return (this.GetPropertyValue("Department").ToString());
            }
            set
            {
                this.SetPropertyValue("Department", value);
            }
        }

        public static Profile GetProfile(string username)
        {
            return Create(username) as Profile;
        }
    }
}