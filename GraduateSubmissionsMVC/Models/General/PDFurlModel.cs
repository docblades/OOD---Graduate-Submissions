using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GraduateSubmissionsMVC.Models.General
{
    public class PDFurlModel
    {
        [Key]
        public int ID { get; set; }
        public int ApplicationID { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public virtual Application Application { get; set; }
    }
}