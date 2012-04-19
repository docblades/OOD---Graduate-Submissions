﻿using System;
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
		[Authorize]
        public ViewResult Index()
        {
            return View(db.DepartmentModel.ToList());
        }

        //
        // GET: /Department/Details/5
		[Authorize]
        public ViewResult Details(int id)
        {
            DepartmentModel departmentmodel = db.DepartmentModel.Find(id);
            return View(departmentmodel);
        }

        //
        // GET: /Department/Create
		[Authorize]
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Department/Create
		[Authorize]
        [HttpPost]
        public ActionResult Create(DepartmentModel departmentmodel)
        {
            //departments must be unique
            var query = from a in db.DepartmentModel
                        where departmentmodel.Name.Equals(a.Name)
                        select a;

            if (ModelState.IsValid)
            {
                if (query.Count() != 0)
                {
                    ModelState.AddModelError(string.Empty, "Department name already exists!");
                }
                else
                {
                    db.DepartmentModel.Add(departmentmodel);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(departmentmodel);
        }
        
        //
        // GET: /Department/Edit/5
		[Authorize]
        public ActionResult Edit(int id)
        {
            DepartmentModel departmentmodel = db.DepartmentModel.Find(id);
            return View(departmentmodel);
        }

        //
        // POST: /Department/Edit/5
		[Authorize]
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
		[Authorize]
        public ActionResult Delete(int id)
        {
            DepartmentModel departmentmodel = db.DepartmentModel.Find(id);
            return View(departmentmodel);
        }

        //
        // POST: /Department/Delete/5
		[Authorize]
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