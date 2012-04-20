using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GraduateSubmissionsMVC.Models;
using GraduateSubmissionsMVC.Models.General;
using System.Data;

namespace GraduateSubmissionsMVC.Controllers
{
    public class ReviewerController : Controller
    {

        GraduateContext db = new GraduateContext();

        //
        // GET: /Reviewer/
		[Authorize]
        public ActionResult Index()
        {
            List<ReviewViewModel> rvmList = new List<ReviewViewModel>();
            var grabApplication = from a in db.Application
                                  where a.IsDecided == false
                                  select a;
            foreach (var app in grabApplication)
            {
                rvmList.Add(new ReviewViewModel()
                {
                    Application = app,
                    DepartmentList = (from c in db.DepartmentModel
                                      join a in db.ApplicationDepartment on app.ID equals a.ApplicationID
                                      where c.ID == a.DepartmentID
                                      select c).ToList(),
                    Term = (from a in db.Term
                            join b in db.Application on a.ID equals b.ID
                            where a.ID == b.ID
                            select a).ToList()[0]
                });
            }
            return View(rvmList);
        }

		[Authorize]
        public ActionResult Review(int id)
        {
            ReviewViewModel rvm = new ReviewViewModel();

            var grabReviews = from a in db.Reviewer
                              where (a.ApplicationID == id) && (User.Identity.Name == a.User)
                              select a;
            if (grabReviews.Count() > 0)
            {
                rvm.exists = true;
                var grabApplication = from a in db.Application
                                      where a.ID == id
                                      select a;

                var profile = Profile.GetProfile(User.Identity.Name);

                int reviewerDepartmentID = (from a in db.DepartmentModel
                                                where a.Name == profile.Department
                                                select a.ID).ToList()[0];

                foreach (var app in grabApplication)
                {
                    rvm = new ReviewViewModel()
                    {
                        Application = app,
                        DepartmentList = (from c in db.DepartmentModel
                                          join a in db.ApplicationDepartment on app.ID equals a.ApplicationID
                                          where c.ID == a.DepartmentID
                                          select c).ToList(),
                        Term = (from a in db.Term
                                join b in db.Application on a.ID equals b.ID
                                where a.ID == b.ID
                                select a).ToList()[0],

                        PDFurlList = (from a in db.PDFurlModel
                                      join b in db.Application on a.ApplicationID equals b.ID
                                      where a.ApplicationID == id
                                      select a).ToList(),

                        reviewerDepartmentID = (from a in db.DepartmentModel
                                                where a.Name == profile.Department
                                                select a.ID).ToList()[0],

                        ReviewerModel = (from a in db.Reviewer
                                        where (a.User == User.Identity.Name) && (a.ApplicationID == id)
                                        select a).ToList()[0],

                        TransitionCoursesList = rvm.TransitionOptionListExists(rvm.TransitionCourseList(reviewerDepartmentID).Count, grabReviews.ToList()[0].ID, app.ID)
                    };
                }
            }
            else
            {
                rvm = new ReviewViewModel();

                var grabApplication = from a in db.Application
                                      where a.ID == id
                                      select a;

                var profile = Profile.GetProfile(User.Identity.Name);

                foreach (var app in grabApplication)
                {
                    rvm = new ReviewViewModel()
                    {
                        Application = app,
                        DepartmentList = (from c in db.DepartmentModel
                                          join a in db.ApplicationDepartment on app.ID equals a.ApplicationID
                                          where c.ID == a.DepartmentID
                                          select c).ToList(),
                        Term = (from a in db.Term
                                join b in db.Application on a.ID equals b.ID
                                where a.ID == b.ID
                                select a).ToList()[0],

                        PDFurlList = (from a in db.PDFurlModel
                                      join b in db.Application on a.ApplicationID equals b.ID
                                      where a.ApplicationID == id
                                      select a).ToList(),

                        reviewerDepartmentID = (from a in db.DepartmentModel
                                                where a.Name == profile.Department
                                                select a.ID).ToList()[0],

                        ReviewerModel = new Models.General.ReviewerModel(),

                        TransitionCoursesList = rvm.TransitionCourseList(rvm.reviewerDepartmentID)
                    };

                    int reviewerDepartmentID = (from a in db.DepartmentModel
                                                where a.Name == profile.Department
                                                select a.ID).ToList()[0];

                    rvm.TransitionOptionList(rvm.TransitionCourseList(reviewerDepartmentID).Count);
                }
            }

            return View(rvm);
        }

		[Authorize]
        [HttpPost]
        public ActionResult Review(ReviewViewModel rvm)
        {

            var grabReviews = from a in db.Reviewer
                              where (a.ApplicationID == rvm.Application.ID) && (User.Identity.Name == a.User)
                              select a;
            if (grabReviews.Count() > 0)
            {
                int rmExists = (from a in db.Reviewer
                                          where a.User.Equals(User.Identity.Name) && a.ApplicationID == rvm.Application.ID
                                          select a.ID).ToList()[0];
                //rmExists = new ReviewerModel() { Comment = rvm.ReviewerModel.Comment, Date = DateTime.Now, User = User.Identity.Name, ApplicationID = rvm.Application.ID };
                ReviewerModel rm = db.Reviewer.Find(rmExists);
                rm.Comment = rvm.ReviewerModel.Comment;
                rm.Date = DateTime.Now;
                db.Entry(rm).State = EntityState.Modified;
                db.SaveChanges();

                List<TransitionCourses> transitionCourseList = rvm.TransitionCourseList(Int32.Parse(rvm.DepartmentNamesList[0].Value));

                List<int> transitionReviewList = (from a in db.TransitionReview
                                                 where (a.ApplicationID == rvm.Application.ID) && (a.ReviewModelID == rmExists)
                                                 select a.ID).ToList();
                int i = 0;
                foreach (var a in transitionReviewList)
                {
                    TransitionReviewModel t = db.TransitionReview.Find(a);

                    if (rvm.Transition[i].Equals(""))
                    {
                        //t.ID = transitionCourseList[i].TransitionCourseModel.ID;
                        t.TransitionOptionModelID = 1;
                        t.answered = false;
                        t.ReviewModelID = rm.ID;
                        t.ApplicationID = rvm.Application.ID;
                    }
                    else
                    {
                        int temp = Int32.Parse(rvm.Transition[i]);
                        //t.ID = transitionCourseList[i].TransitionCourseModel.ID;
                        t.TransitionOptionModelID = (from x in db.TransitionOption
                                                     where x.ID == temp
                                                     select x.ID).ToList()[0];
                        t.answered = true;
                        t.ReviewModelID = rm.ID;
                        t.ApplicationID = rvm.Application.ID;
                    }
                    i++;
                    db.Entry(t).State = EntityState.Modified;
                    db.SaveChanges();
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {

                ReviewerModel rm = new ReviewerModel() { Comment = rvm.ReviewerModel.Comment, Date = DateTime.Now, User = User.Identity.Name, ApplicationID = rvm.Application.ID };
                db.Reviewer.Add(rm);
                db.SaveChanges();
                List<TransitionCourses> transitionCourseList = rvm.TransitionCourseList(Int32.Parse(rvm.DepartmentNamesList[0].Value));

                for (int i = 0; i < rvm.Transition.Length; ++i)
                {
                    if (rvm.Transition[i].Equals(""))
                    {
                        db.TransitionReview.Add(new TransitionReviewModel()
                        {
                            TransitionCourseModelID = transitionCourseList[i].TransitionCourseModel.ID,
                            TransitionOptionModelID = 1,
                            answered = false,
                            ReviewModelID = rm.ID,
                            ApplicationID = rvm.Application.ID
                        });
                    }
                    else
                    {
                        int temp = Int32.Parse(rvm.Transition[i]);
                        db.TransitionReview.Add(new TransitionReviewModel()
                        {
                            TransitionCourseModelID = transitionCourseList[i].TransitionCourseModel.ID,
                            TransitionOptionModelID = (from a in db.TransitionOption
                                                       where a.ID == temp
                                                       select a.ID).ToList()[0],
                            ReviewModelID = rm.ID,
                            ApplicationID = rvm.Application.ID,
                            answered = true
                        });
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
