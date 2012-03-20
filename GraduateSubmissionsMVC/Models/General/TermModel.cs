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
        public string Name { get; set; }
        public string Date { get; set; }

        public ICollection<Application> Application { get; set; }
    }
}