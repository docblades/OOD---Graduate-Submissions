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

        public virtual TransitionCourseModel TransitionCourseModel{ get; set; }
        public virtual TransitionOptionModel TransitionOptionModel { get; set; }
        public virtual ICollection<ReviewerModel> ReviewerModel { get; set; }
    }
}