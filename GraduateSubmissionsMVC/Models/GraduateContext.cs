using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace GraduateSubmissionsMVC.Models
{
    //create the relational - oo mapping

    public class GraduateContext : DbContext
    {
        public DbSet<DepartmentModel> DepartmentModel { get; set; }
        public DbSet<TransitionCourseModel> TransitionCourse { get; set; }
    }
}