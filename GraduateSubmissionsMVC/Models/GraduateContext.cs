﻿using System;
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
        public DbSet<GraduateApplication> Application { get; set; }
        public DbSet<ApplicantModel> Applicant { get; set; }
        public DbSet<PDFDocModel> PDFDocument { get; set; }

        public DbSet<GraduateApplicationV1> GraduateApplicationV1 { get; set; }
    }
}