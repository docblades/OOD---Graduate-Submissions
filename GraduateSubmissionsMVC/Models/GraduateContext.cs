using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using GraduateSubmissionsMVC.Models.General;

namespace GraduateSubmissionsMVC.Models
{
    //create the relational - oo mapping

    public class GraduateContext : DbContext
    {
        public DbSet<DepartmentModel> DepartmentModel { get; set; }
        public DbSet<TransitionCourseModel> TransitionCourse { get; set; }
        public DbSet<TermModel> Term { get; set; }
        public DbSet<PDFurlModel> PDFurlModel { get; set; }
        public DbSet<Application> Application { get; set; }
        public DbSet<ApplicationDepartment> ApplicationDepartment { get; set; }
    }
}