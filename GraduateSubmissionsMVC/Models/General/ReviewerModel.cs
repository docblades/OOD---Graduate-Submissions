using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GraduateSubmissionsMVC.Models.General
{
    public class ReviewerModel
    {
        [Key]
        public int ID { get; set; }
        public DateTime Date { get; set; }
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }
        public string User { get; set; }
        public int ApplicationID { get; set; }

        public virtual ICollection<Application> Application { get; set; }
        public virtual RegisterModel RegisterModel { get; set; }
        public virtual ICollection<TransitionReviewModel> TransitionReview { get; set; }
    }
}