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
        public int TermID { get; set; }
        [Required]
        [Display(Name = "First name")]
        public string firstname { get; set; }
        [Required]
        [Display(Name = "Last name")]
        public string lastname { get; set; }
        [Display(Name = "Phone number")]
        public string number { get; set; }
        [Display(Name = "E-mail")]
        [RegularExpression(@"^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$", ErrorMessage = "Enter a valid e-mail address!")]
        public string email { get; set; }
		[Display(Name = "Personal description"),DataType(DataType.MultilineText)]
        public string PersonalInfo { get; set; }
        [Display(Name = "Has applied before?")]
        public bool AppliedBefore { get; set; }
        [Display(Name = "If yes, date applied before")]
        [RegularExpression(@"^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$", ErrorMessage = "Format mm/dd/yyyy")]
        public string DateAppliedBefore { get; set; }

        [Display(Name = "Student ID")]
        public string studentNumber { get; set; }

        public DateTime Date { get; set; }
        public bool IsDecided { get; set; }

        public virtual TermModel Term { get; set; }
        public virtual ICollection<DepartmentModel> Department { get; set; }
        public virtual ICollection<PDFurlModel> PDFurlModel { get; set; }

        public virtual ICollection<ReviewerModel> ReviewerModel { get; set; }

        
    }
}