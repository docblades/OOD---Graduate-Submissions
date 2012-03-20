using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GraduateSubmissionsMVC.Models.General
{

    //Represents a school term

    public class TermModel
    {
        [Key]
        public int ID { get; set; }
        [Required(), DisplayName("Name")]
        public string Name { get; set; }
        [Required(), DisplayName("Year"),
         RegularExpression(@"^\d{4}$", ErrorMessage = "Valid 4 digit year!")]
        public string Date { get; set; }

        public ICollection<Application> Application { get; set; }
    }
}