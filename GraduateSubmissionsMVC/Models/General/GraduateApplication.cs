using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GraduateSubmissionsMVC.Models.General
{
    public class GraduateApplication
    {
        public GraduateApplication()
        {
            this.EntryDate = DateTime.Now;
        }

        [Key]
        public int ID { get; set; }

        [Required()]
        public ApplicantModel Applicant { get; set; }

        [Required()]
        public TermModel Term { get; set; }

        [Required()]
        public DateTime EntryDate { get; set; }

        public ICollection<PDFDocModel> PDFDocs { get; set; }

    }
}