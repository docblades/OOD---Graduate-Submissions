using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GraduateSubmissionsMVC.Models.General
{
    public class TransitionOptionModel
    {
        [Key]
        public int ID { get; set; }
        [Required(), DisplayName("Transition Option")]
        public string Name { get; set; }

        public virtual ICollection<TransitionReviewModel> TransitionReviewModel { get; set; }
    }
}