using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using GraduateSubmissionsMVC.Models.General;

namespace GraduateSubmissionsMVC.Models
{
    //represents a school department
 
    public class DepartmentModel
    {
        [Key]
        public int ID { get; set; }
        [Required(), DisplayName("Department Name")]
        public string Name { get; set; }

        //A department has many courses
        public virtual ICollection<TransitionCourseModel> TransitionCourse { get; set; }
        public virtual ICollection<Application> Application { get; set; }
    }
}