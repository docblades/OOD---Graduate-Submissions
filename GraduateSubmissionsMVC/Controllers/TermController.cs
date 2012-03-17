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
    public class TermController : Controller
    {
        private GraduateContext db = new GraduateContext();

        //
        // GET: /Term/

        public ViewResult Index()
        {
            return View(db.Term.ToList());
        }

        //
        // GET: /Term/Details/5

        public ViewResult Details(int id)
        {
            TermModel termmodel = db.Term.Find(id);
            return View(termmodel);
        }

        //
        // GET: /Term/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Term/Create

        [HttpPost]
        public ActionResult Create(TermModel termmodel)
        {
            if (ModelState.IsValid)
            {
                db.Term.Add(termmodel);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(termmodel);
        }
        
        //
        // GET: /Term/Edit/5
 
        public ActionResult Edit(int id)
        {
            TermModel termmodel = db.Term.Find(id);
            return View(termmodel);
        }

        //
        // POST: /Term/Edit/5

        [HttpPost]
        public ActionResult Edit(TermModel termmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(termmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(termmodel);
        }

        //
        // GET: /Term/Delete/5
 
        public ActionResult Delete(int id)
        {
            TermModel termmodel = db.Term.Find(id);
            return View(termmodel);
        }

        //
        // POST: /Term/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            TermModel termmodel = db.Term.Find(id);
            db.Term.Remove(termmodel);
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