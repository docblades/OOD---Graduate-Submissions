using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GraduateSubmissionsMVC.Models.General
{
    public class ApplicantModel
    {
        [Key()]
        public int ID { get; set; }

        [Required()]
        public string FirstName { get; set; }
        [Required()]
        public string LastName { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string StudentID { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<GraduateApplication> Applications { get; set; }

        public String FormattedName
        {
            get
            {
                return String.Format("{0}, {1} ({2})", LastName, FirstName, StudentID);
            }
        }

    }
}