using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GraduateSubmissionsMVC.Models.General;
using GraduateSubmissionsMVC.Models;

namespace GraduateSubmissionsMVC.Controllers
{ 
    public class ApplicantController : Controller
    {
        private GraduateContext db = new GraduateContext();

        //
        // GET: /Applicant/

        public ViewResult Index()
        {
            return View(db.Applicant.ToList());
        }

        //
        // GET: /Applicant/Details/5

        public ViewResult Details(int id)
        {
            ApplicantModel applicantmodel = db.Applicant.Find(id);
            return View(applicantmodel);
        }

        //
        // GET: /Applicant/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Applicant/Create

        [HttpPost]
        public ActionResult Create(ApplicantModel applicantmodel)
        {
            if (ModelState.IsValid)
            {
                db.Applicant.Add(applicantmodel);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(applicantmodel);
        }
        
        //
        // GET: /Applicant/Edit/5
 
        public ActionResult Edit(int id)
        {
            ApplicantModel applicantmodel = db.Applicant.Find(id);
            return View(applicantmodel);
        }

        //
        // POST: /Applicant/Edit/5

        [HttpPost]
        public ActionResult Edit(ApplicantModel applicantmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicantmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(applicantmodel);
        }

        //
        // GET: /Applicant/Delete/5
 
        public ActionResult Delete(int id)
        {
            ApplicantModel applicantmodel = db.Applicant.Find(id);
            return View(applicantmodel);
        }

        //
        // POST: /Applicant/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            ApplicantModel applicantmodel = db.Applicant.Find(id);
            db.Applicant.Remove(applicantmodel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}