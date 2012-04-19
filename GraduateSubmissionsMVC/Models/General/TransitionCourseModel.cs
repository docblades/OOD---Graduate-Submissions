using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using GraduateSubmissionsMVC.Models.General;

namespace GraduateSubmissionsMVC.Models
{
    //a typical school course model

    public class TransitionCourseModel
    {
        [Key]
        public int ID { get; set; }
        public int DepartmentID { get; set; }
        [Required(), DisplayName("Course Name")]
        public string Name { get; set; }
        [Required(), DisplayName("Course Number")]
        public int Number { get; set; }
        [Required(), DisplayName("Credit Hours")]
        public int CreditHour { get; set; }
        [Required(), DisplayName("Course Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        //a course belongs to one department
        public virtual DepartmentModel Department { get; set; }

        public virtual ICollection<TransitionReviewModel> TransitionReviewModel { get; set; }
    }
}