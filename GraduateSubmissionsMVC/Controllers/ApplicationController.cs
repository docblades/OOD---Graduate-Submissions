using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GraduateSubmissionsMVC.Models;
using System.IO;
using GraduateSubmissionsMVC.Models.General;

namespace GraduateSubmissionsMVC.Controllers
{ 
    [Authorize(Roles = "Admin Assist")]
    public class ApplicationController : Controller
    {
        private GraduateContext db = new GraduateContext();
        private ApplicationViewModel avm = new ApplicationViewModel();
        

        //
        // GET: /Application/
        public ViewResult Index()
        {
            //var application = db.Application.Include(a => a.Term);
            var grabApplication = from a in db.Application
                                  select a;

            List<ApplicationViewModel> avmList = new List<ApplicationViewModel>();
            foreach (var app in grabApplication)
            {
                avmList.Add(new ApplicationViewModel()
                            {
                                Application = app,
                                DepartmentList = (from c in db.DepartmentModel
                                                  join a in db.ApplicationDepartment on app.ID equals a.ApplicationID
                                                  where c.ID == a.DepartmentID
                                                  select c).ToList(),
                                Term = (from a in db.Term
                                        join b in db.Application on a.ID equals b.ID
                                        where a.ID == b.ID
                                        select a).ToList()[0]
                            });
            }
                                            

            //return View(application.ToList());
            return View(avmList);
        }

        //upload
        public ActionResult Upload(int id)
        {
            UploadViewModel avm_upload = new UploadViewModel();
            avm_upload.Application = db.Application.Find(id);
            avm_upload.PdfUrl = new PDFurlModel();
            avm_upload.ApplicationID = id;
            return View(avm_upload);
        }

	
        [HttpPost]
        public ActionResult Upload(UploadViewModel avm_upload)
        {
            foreach(string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(hpf.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/uploads/"), fileName);

                    //copy path url to db
                    PDFurlModel _upload = new PDFurlModel() { Url = path, Name = "", ApplicationID = 1};
                    db.PDFurlModel.Add(_upload);
                    hpf.SaveAs(path);
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: /Application/Details/5
        public ViewResult Details(int id)
        {
            Application application = db.Application.Find(id);
            return View(application);
        }

        //
        // GET: /Application/Create
        public ActionResult Create()
        {
            //ViewBag.TermID = new SelectList(db.Term, "ID", "Name");

            ViewBag.termList = avm.TermList;
            ViewBag.departmentList = avm.DepartmentNamesList;

            //number of elements in DepartmentList
            ViewBag.DepartmentListCounter = avm.NumberofElementsDepartmentNamesList;
            //number of elements in TermList
            ViewBag.TermListCounter = avm.NumberofElementsTermList;
            
            
            return View();
        } 

        //
        // POST: /Application/Create
        [HttpPost]
        public ActionResult Create(ApplicationViewModel _application, string [] Departments)
        {
            if (ModelState.IsValid)
            {
                if (Departments == null)
                {
                    //return RedirectToAction("Upload", new { id = _application.Application.ID });
                    ModelState.AddModelError(string.Empty, "Please choose a department.");
                }
                else
                {

                    _application.Application.Date = DateTime.Now;
                    _application.Application.IsDecided = false;

                    db.Application.Add(_application.Application);
                    db.SaveChanges();

                    foreach (string s in Departments)
                    {
                        db.ApplicationDepartment.Add(new ApplicationDepartment() { DepartmentID = Int32.Parse(s), ApplicationID = _application.Application.ID });
                    }

                    foreach (string file in Request.Files)
                    {
                        HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                        if (hpf.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(hpf.FileName);
                            var path = Path.Combine(Server.MapPath("~/Uploads/"), fileName);

                            //copy path url to db
                            PDFurlModel _upload = new PDFurlModel() { Url = path, Name = "", ApplicationID = _application.Application.ID };
                            db.PDFurlModel.Add(_upload);
                            hpf.SaveAs(path);
                        }
                    }
                    db.SaveChanges();
                    //return RedirectToAction("Upload", new { id = _application.Application.ID });
                    return RedirectToAction("Index");
                }
            }
            
            //ViewBag.TermID = new SelectList(db.Term, "ID", "Name", application.Application.TermID);
            ViewBag.termList = avm.TermList;
            ViewBag.departmentList = avm.DepartmentNamesList;

            //number of elements in DepartmentList
            ViewBag.DepartmentListCounter = avm.NumberofElementsDepartmentNamesList;
            //number of elements in TermList
            ViewBag.TermListCounter = avm.NumberofElementsTermList;
            return View(_application);
        }
        
        //
        // GET: /Application/Edit/5
        public ActionResult Edit(int id)
        {
            Application application = db.Application.Find(id);
            //ViewBag.TermID = new SelectList(db.Term, "ID", "Name", application.TermModelID);
            return View(application);
        }

        //
        // POST: /Application/Edit/5
        [HttpPost]
        public ActionResult Edit(Application application)
        {
            if (ModelState.IsValid)
            {
                db.Entry(application).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.TermID = new SelectList(db.Term, "ID", "Name", application.TermModelID);
            return View(application);
        }

        //
        // GET: /Application/Delete/5
        public ActionResult Delete(int id)
        {
            Application application = db.Application.Find(id);
            return View(application);
        }

        //
        // POST: /Application/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Application application = db.Application.Find(id);
            db.Application.Remove(application);
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