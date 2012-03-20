using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GraduateSubmissionsMVC.Models.General
{
    public class PDFDocModel
    {
        [Key]
        public int ID { get; set; }

        [Required(), DisplayName("Document Name")]
        public string DocumentName { get; set; }

        [Required()]
        public string PhysicalPath { get; set; }

    }
}