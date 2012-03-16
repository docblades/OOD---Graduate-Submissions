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
    //CRUD - Department
    public class DepartmentController : Controller
    {
        private GraduateContext db = new GraduateContext();

        //
        // GET: /Department/

        public ViewResult Index()
        {
            return View(db.DepartmentModel.ToList());
        }

        //
        // GET: /Department/Details/5

        public ViewResult Details(int id)
        {
            DepartmentModel departmentmodel = db.DepartmentModel.Find(id);
            return View(departmentmodel);
        }

        //
        // GET: /Department/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Department/Create

        [HttpPost]
        public ActionResult Create(DepartmentModel departmentmodel)
        {
            if (ModelState.IsValid)
            {
                db.DepartmentModel.Add(departmentmodel);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(departmentmodel);
        }
        
        //
        // GET: /Department/Edit/5
 
        public ActionResult Edit(int id)
        {
            DepartmentModel departmentmodel = db.DepartmentModel.Find(id);
            return View(departmentmodel);
        }

        //
        // POST: /Department/Edit/5

        [HttpPost]
        public ActionResult Edit(DepartmentModel departmentmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(departmentmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(departmentmodel);
        }

        //
        // GET: /Department/Delete/5
 
        public ActionResult Delete(int id)
        {
            DepartmentModel departmentmodel = db.DepartmentModel.Find(id);
            return View(departmentmodel);
        }

        //
        // POST: /Department/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            DepartmentModel departmentmodel = db.DepartmentModel.Find(id);
            db.DepartmentModel.Remove(departmentmodel);
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