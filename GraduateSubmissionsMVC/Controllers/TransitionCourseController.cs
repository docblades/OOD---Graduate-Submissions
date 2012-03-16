using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GraduateSubmissionsMVC.Models;

namespace GraduateSubmissionsMVC.Controllers
{ 
    public class TransitionCourseController : Controller
    {
        private GraduateContext db = new GraduateContext();

        //
        // GET: /TransitionCourse/

        public ViewResult Index()
        {
            var transitioncourse = db.TransitionCourse.Include(t => t.Department);
            return View(transitioncourse.ToList());
        }

        //
        // GET: /TransitionCourse/Details/5

        public ViewResult Details(int id)
        {
            TransitionCourse transitioncourse = db.TransitionCourse.Find(id);
            return View(transitioncourse);
        }

        //
        // GET: /TransitionCourse/Create

        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.DepartmentModel, "ID", "Name");
            return View();
        } 

        //
        // POST: /TransitionCourse/Create

        [HttpPost]
        public ActionResult Create(TransitionCourse transitioncourse)
        {
            if (ModelState.IsValid)
            {
                db.TransitionCourse.Add(transitioncourse);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.DepartmentID = new SelectList(db.DepartmentModel, "ID", "Name", transitioncourse.DepartmentID);
            return View(transitioncourse);
        }
        
        //
        // GET: /TransitionCourse/Edit/5
 
        public ActionResult Edit(int id)
        {
            TransitionCourse transitioncourse = db.TransitionCourse.Find(id);
            ViewBag.DepartmentID = new SelectList(db.DepartmentModel, "ID", "Name", transitioncourse.DepartmentID);
            return View(transitioncourse);
        }

        //
        // POST: /TransitionCourse/Edit/5

        [HttpPost]
        public ActionResult Edit(TransitionCourse transitioncourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transitioncourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(db.DepartmentModel, "ID", "Name", transitioncourse.DepartmentID);
            return View(transitioncourse);
        }

        //
        // GET: /TransitionCourse/Delete/5
 
        public ActionResult Delete(int id)
        {
            TransitionCourse transitioncourse = db.TransitionCourse.Find(id);
            return View(transitioncourse);
        }

        //
        // POST: /TransitionCourse/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            TransitionCourse transitioncourse = db.TransitionCourse.Find(id);
            db.TransitionCourse.Remove(transitioncourse);
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