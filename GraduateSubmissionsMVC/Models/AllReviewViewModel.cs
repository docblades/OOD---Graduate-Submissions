using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GraduateSubmissionsMVC.Models.General;

namespace GraduateSubmissionsMVC.Models
{
    public class AllReviewViewModel
    {
        GraduateContext db = new GraduateContext();

        public Application Application { get; set; }
        public List<ReviewStateWrapper> ReviewList { get; set; }

        public Application grabApplication(int appId)
        {
            Application = (from a in db.Application where a.ID == appId select a).ToList()[0];
            return Application;
        }

        public List<ReviewStateWrapper> grabReviewers(int appId, string user)
        {
            List<ReviewerModel> reviews = (from a in db.Reviewer
                          where a.ApplicationID == appId
                          select a).ToList();

            ReviewList = new List<ReviewStateWrapper>();

            foreach (var a in reviews)
            {
                 float percentagePart = (from t in db.TransitionReview
                                where (t.answered == true) && (t.ApplicationID == appId) && (t.ReviewModelID == a.ID)
                                select t).ToList().Count;
                 float percentageWhole = (from t in db.TransitionReview
                                          where (t.ApplicationID == appId) && (t.ReviewModelID == a.ID)
                                          select t).ToList().Count;

                 var profile = Profile.GetProfile(user);

                 ReviewList.Add(new ReviewStateWrapper() { user = a.User, FirstName = profile.FirstName, LastName = profile.LastName, date = a.Date, percentage = (percentagePart / percentageWhole) * 100 }); 
            }

            return ReviewList;
        }
    }

    public class ReviewStateWrapper
    {
        public string user { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime date { get; set; }
        public float percentage { get; set; }
    }
}