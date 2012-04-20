using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GraduateSubmissionsMVC.Models.General
{
    public class TransitionReviewModel
    {
        [Key]
        public int ID { get; set; }
        public int TransitionCourseModelID { get; set; }
        public int TransitionOptionModelID { get; set; }
        public bool answered { get; set; }
        public int ReviewModelID { get; set; }
        public int ApplicationID { get; set; }

        public virtual TransitionCourseModel TransitionCourseModel{ get; set; }
        public virtual TransitionOptionModel TransitionOptionModel { get; set; }
        public virtual ReviewerModel ReviewerModel { get; set; }
    }
}