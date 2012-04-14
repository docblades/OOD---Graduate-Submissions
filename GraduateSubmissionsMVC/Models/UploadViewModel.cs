using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GraduateSubmissionsMVC.Models.General;

namespace GraduateSubmissionsMVC.Models
{
    public class UploadViewModel
    {
        public Application Application { get; set; }
        public PDFurlModel PdfUrl { get; set; }
    }
}