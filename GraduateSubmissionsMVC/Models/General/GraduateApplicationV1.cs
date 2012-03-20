using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GraduateSubmissionsMVC.Models.General
{
    public class GraduateApplicationV1
    {
        [Key()]
        public int Id { get; set; }

        [Required(), Display(Name="Applicant Name", GroupName="Applicant")]
        public string ApplicantName { get; set; }
        [Display(Name="Student ID", GroupName="Applicant")]
        public string ApplicantStudentId { get; set; }
        [Display(Name = "Phone Number", GroupName = "Applicant")]
        public string PhoneNumber { get; set; }
        
        [Required()]
        public string Term { get; set; }

        [Required(), Display(Name="Application Date")]
        public DateTime EntryDate { get; set; }

        [Required()]
        public string ApplicationContent { get; set; }

        [Required()]
        public string Department { get; set; }

        public GraduateApplicationV1()
        {
            EntryDate = DateTime.Now;
        }
        
    }
}