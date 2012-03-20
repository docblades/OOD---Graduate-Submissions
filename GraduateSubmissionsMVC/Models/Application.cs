using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GraduateSubmissionsMVC.Models.General;
using System.ComponentModel.DataAnnotations;

namespace GraduateSubmissionsMVC.Models
{
    public class Application
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "First name")]
        public string firstname { get; set; }
        [Display(Name = "Last name")]
        public string lastname { get; set; }
        [Display(Name = "Phone number")]
        public string number { get; set; }
        [Display(Name = "E-mail")]
        public string email { get; set; }
        [Display(Name = "Personal description")]
        public string PersonalInfo { get; set; }
        [Display(Name = "Applied before?")]
        public bool AppliedBefore { get; set; }
        [Display(Name = "If yes, date applied before")]
        public string DateAppliedBefore { get; set; }

        
        public ICollection<DepartmentModel> Department { get; set; }
        public ICollection<PDFurlModel> PDFurlModel { get; set; }
    }
}