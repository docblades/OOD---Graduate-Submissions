using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GraduateSubmissionsMVC.Models.General;
using System.Web.Mvc;

namespace GraduateSubmissionsMVC.Models
{
    public class ApplicationViewModel
    {
        GraduateContext db = new GraduateContext();

        public Application Application { get; set; }
        public DepartmentModel Department { get; set; }
        public PDFurlModel PDFurl { get; set; }
        public TermModel Term { get; set; }
        public ApplicationDepartment ApplicationDepartment { get; set; }

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


        //number of elements in TermList
        public int NumberofElementsTermList
        {
            get
            {
                return TermList.Count();
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

        public List<SelectListItem> TermList
        {
            get
            {
                var grabTerms = from a in db.Term
                                      select a;

                List<SelectListItem> termList = new List<SelectListItem>();

                foreach (var term in grabTerms)
                {
                    termList.Add(new SelectListItem() { Value = term.ID.ToString(), Text = term.Name + " " + term.Date });
                }

                return termList; //datasource
            }
        }
    }
}