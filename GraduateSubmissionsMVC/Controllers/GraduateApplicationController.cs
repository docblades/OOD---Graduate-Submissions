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
    public class GraduateApplicationController : Controller
    {
        private GraduateContext db = new GraduateContext();

        //
        // GET: /GraduateApplication/

        public ViewResult Index()
        {
            return View(db.GraduateApplicationV1.ToList());
        }

        //
        // GET: /GraduateApplication/Details/5

        public ViewResult Details(int id)
        {
            GraduateApplicationV1 graduateapplicationv1 = db.GraduateApplicationV1.Find(id);
            return View(graduateapplicationv1);
        }

        //
        // GET: /GraduateApplication/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /GraduateApplication/Create

        [HttpPost]
        public ActionResult Create(GraduateApplicationV1 graduateapplicationv1)
        {
            if (ModelState.IsValid)
            {
                db.GraduateApplicationV1.Add(graduateapplicationv1);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(graduateapplicationv1);
        }
        
        //
        // GET: /GraduateApplication/Edit/5
 
        public ActionResult Edit(int id)
        {
            GraduateApplicationV1 graduateapplicationv1 = db.GraduateApplicationV1.Find(id);
            return View(graduateapplicationv1);
        }

        //
        // POST: /GraduateApplication/Edit/5

        [HttpPost]
        public ActionResult Edit(GraduateApplicationV1 graduateapplicationv1)
        {
            if (ModelState.IsValid)
            {
                db.Entry(graduateapplicationv1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(graduateapplicationv1);
        }

        //
        // GET: /GraduateApplication/Delete/5
 
        public ActionResult Delete(int id)
        {
            GraduateApplicationV1 graduateapplicationv1 = db.GraduateApplicationV1.Find(id);
            return View(graduateapplicationv1);
        }

        //
        // POST: /GraduateApplication/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            GraduateApplicationV1 graduateapplicationv1 = db.GraduateApplicationV1.Find(id);
            db.GraduateApplicationV1.Remove(graduateapplicationv1);
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