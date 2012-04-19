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
    public class TransitionOptionController : Controller
    {
        private GraduateContext db = new GraduateContext();

        //
        // GET: /TransitionOption/

        public ViewResult Index()
        {
            return View(db.TransitionOption.ToList());
        }

        //
        // GET: /TransitionOption/Details/5

        public ViewResult Details(int id)
        {
            TransitionOptionModel transitionoptionmodel = db.TransitionOption.Find(id);
            return View(transitionoptionmodel);
        }

        //
        // GET: /TransitionOption/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /TransitionOption/Create

        [HttpPost]
        public ActionResult Create(TransitionOptionModel transitionoptionmodel)
        {
            if (ModelState.IsValid)
            {
                db.TransitionOption.Add(transitionoptionmodel);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(transitionoptionmodel);
        }
        
        //
        // GET: /TransitionOption/Edit/5
 
        public ActionResult Edit(int id)
        {
            TransitionOptionModel transitionoptionmodel = db.TransitionOption.Find(id);
            return View(transitionoptionmodel);
        }

        //
        // POST: /TransitionOption/Edit/5

        [HttpPost]
        public ActionResult Edit(TransitionOptionModel transitionoptionmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transitionoptionmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transitionoptionmodel);
        }

        //
        // GET: /TransitionOption/Delete/5
 
        public ActionResult Delete(int id)
        {
            TransitionOptionModel transitionoptionmodel = db.TransitionOption.Find(id);
            return View(transitionoptionmodel);
        }

        //
        // POST: /TransitionOption/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            TransitionOptionModel transitionoptionmodel = db.TransitionOption.Find(id);
            db.TransitionOption.Remove(transitionoptionmodel);
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