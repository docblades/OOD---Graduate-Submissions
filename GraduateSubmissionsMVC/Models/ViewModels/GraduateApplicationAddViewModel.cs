using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GraduateSubmissionsMVC.Models.General;

namespace GraduateSubmissionsMVC.Models.ViewModels
{
    public class GraduateApplicationAddViewModel
    {
        public GraduateApplication Application { get; set; }

        public IDictionary<int, string> Applicants { get; set; }
        public IDictionary<int, string> Terms { get; set; }

        public GraduateApplicationAddViewModel() 
        {
            GraduateContext context = new GraduateContext();

            Application = new GraduateApplication();

            instantiateApplicants(context);
            instantiateTerms(context);
        }

        public GraduateApplicationAddViewModel(int id)
        {
            GraduateContext context = new GraduateContext();

            Application = context.Application.Where(app => app.ID == id).SingleOrDefault();

            instantiateApplicants(context);
            instantiateTerms(context);
        }

        private void instantiateTerms(GraduateContext context)
        {
            Terms = new Dictionary<int, string>();
            foreach (var term in context.Term.Where(term => term.Date >= System.Data.Objects.SqlClient.SqlFunctions.DateAdd("year", -1, DateTime.Now)))
            {               
                Terms.Add(term.ID, term.Name);
            }
        }

        private void instantiateApplicants(GraduateContext context)
        {
            Applicants = new Dictionary<int, string>();
            foreach (var applicant in context.Applicant)
            {
                Applicants.Add(applicant.ID, applicant.FormattedName);
            }
        }

    }
}