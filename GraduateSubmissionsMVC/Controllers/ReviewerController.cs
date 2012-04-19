using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GraduateSubmissionsMVC.Models;

namespace GraduateSubmissionsMVC.Controllers
{
    public class ReviewerController : Controller
    {

        GraduateContext db = new GraduateContext();

        //
        // GET: /Reviewer/

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

        public ActionResult Review(int id)
        {
            ReviewViewModel rvm = new ReviewViewModel();
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

            return View(rvm);
        }

        [HttpPost]
        public ActionResult Review(ReviewViewModel rvm)
        {
            if (ModelState.IsValid)
            {

            }
            int id = 2;
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

            return View(rvm);
        }
    }
}
