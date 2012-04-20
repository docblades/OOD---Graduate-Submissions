using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GraduateSubmissionsMVC.Models.General;

namespace GraduateSubmissionsMVC.Models
{
    public class ReviewViewModel
    {
        GraduateContext db = new GraduateContext();
        public Application Application { get; set; }

        public bool exists { get; set; }

        public TermModel Term { get; set; }

        public List<TransitionCourses> TransitionCoursesList { get; set; }

        public List<TransitionCourses> TransitionCoursesExist { get; set; }

        public List<TransitionCourses> TransitionCourseList(int departmentID) 
        {
                var grabTransitionCourse = (from a in db.TransitionCourse
                                           where a.DepartmentID == departmentID
                                           select a).ToList();

                List<TransitionCourses> tList = new List<TransitionCourses>();

                var grabTransitionOptions = from a in db.TransitionOption
                                            select a;

                List<SelectListItem> transitionOptionNames = new List<SelectListItem>();

                foreach (var transitionOption in grabTransitionOptions)
                {
                    transitionOptionNames.Add(new SelectListItem() { Value = transitionOption.ID.ToString(), Text = transitionOption.Name });
                }

                foreach (var t in grabTransitionCourse)
                {
                    tList.Add(new TransitionCourses() { TransitionCourseModel = t, TransitionOptionSelectListItem = transitionOptionNames });
                }

                TransitionCoursesList = tList;

                return tList;
        }
        public List<PDFurlModel> PDFurlList { get; set; }

        
        //list of Departments
        public List<DepartmentModel> DepartmentList { get; set; }

        //number of elements in DepartmentNamesList
        public int NumberofElementsDepartmentNamesList
        {
            get
            {
                return DepartmentNamesList.Count();
            }
        }

        public List<SelectListItem> DepartmentNamesList
        {
            get
            {
                var grabDepartments = from a in db.DepartmentModel
                                      select a;

                List<SelectListItem> departmentNames = new List<SelectListItem>();

                foreach (var department in grabDepartments)
                {
                    departmentNames.Add(new SelectListItem() { Value = department.ID.ToString(), Text = department.Name });
                }

                return departmentNames; //datasource
            }
        }

        public List<SelectList> TransitionOptionList(int count)
        {
                var grabTransitionOptions = from a in db.TransitionOption
                                            select a;

                List<SelectListItem> transitionOptionNames = new List<SelectListItem>();

                foreach (var transitionOption in grabTransitionOptions)
                {
                    transitionOptionNames.Add(new SelectListItem() { Value = transitionOption.ID.ToString(), Text = transitionOption.Name });
                }

                List<SelectList> list = new List<SelectList>();
                for (int i = 0; i < count; ++i)
                {
                    list.Add(new SelectList(transitionOptionNames));
                }


                return list; //datasource
        }

        public List<TransitionCourses> TransitionOptionListExists(int count, int reviewID, int applicationID)
        {
            var grabTransitionOptions = from a in db.TransitionOption
                                        select a;

            List<SelectListItem> transitionOptionNames = new List<SelectListItem>();

            List<TransitionOptionModel> grabTransitionOptionsExists = (from a in db.TransitionReview
                                                                       join b in db.TransitionOption on a.TransitionOptionModelID equals b.ID
                                                                       where (a.ApplicationID == applicationID) && (a.ReviewModelID == reviewID)
                                                                       select b).ToList();

            List<TransitionReviewModel> transitionReviewList = (from a in db.TransitionReview
                                                                where (a.ApplicationID == applicationID) && (a.ReviewModelID == reviewID)
                                                                select a).ToList();

            List<TransitionCourseModel> transitionCourseList = (from a in db.TransitionReview
                                                            join b in db.TransitionCourse on a.TransitionCourseModelID equals b.ID
                                                            where (a.ApplicationID == applicationID) && (a.ReviewModelID == reviewID)
                                                            select b).ToList();

            List<TransitionCourses> tList = new List<TransitionCourses>();

            int k = 0;
            foreach (var a in transitionReviewList)
            {
                for (int i=0; i < grabTransitionOptions.ToList().Count; ++i)
                {
                    if (a.answered == false)
                    {
                        transitionOptionNames.Add(new SelectListItem() { Value = grabTransitionOptions.ToList()[i].ID.ToString(), Text = grabTransitionOptions.ToList()[i].Name });
                    }
                    else
                    {
                        transitionOptionNames.Add(new SelectListItem() { Value = grabTransitionOptions.ToList()[i].ID.ToString(), Text = grabTransitionOptions.ToList()[i].Name, Selected = grabTransitionOptions.ToList()[i].ID.ToString().Equals(a.TransitionOptionModelID.ToString())});
                    }
                }

                tList.Add(new TransitionCourses() { TransitionCourseModel = transitionCourseList[k], TransitionOptionSelectListItem = transitionOptionNames });
                transitionOptionNames = new List<SelectListItem>();
                k++;
            }

            TransitionCoursesExist = tList;

            return tList; //datasource
        }

        public int reviewerDepartmentID { get; set; }

        public ReviewerModel ReviewerModel { get; set; }

        public string[] Transition { get; set; }
    }

    public class TransitionCourses
    {
        public TransitionCourseModel TransitionCourseModel { get; set; }
        public List<SelectListItem> TransitionOptionSelectListItem { get; set; }
    }
}