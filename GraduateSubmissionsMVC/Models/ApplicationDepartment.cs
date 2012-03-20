using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GraduateSubmissionsMVC.Models
{
    public class ApplicationDepartment
    {
        [Key]
        public int ID { get; set; }
        public int DepartmentID { get; set; }
        public int ApplicationID { get; set; }
    }
}