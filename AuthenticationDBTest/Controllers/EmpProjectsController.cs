using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AuthenticationDBTest.Data;

namespace AuthenticationDBTest.Controllers
{
    public class EmpProjectsController : Controller
    {
        private SecondDBEntities db = new SecondDBEntities();

        // GET: EmpProjects
        [OutputCache(Duration =10,VaryByParam ="none",Location =System.Web.UI.OutputCacheLocation.Client)]
        public ActionResult Index()
        {
            var empProjects = db.EmpProjects.Include(e => e.Employee).Include(e => e.Project);
            //var empProjectsQuery = (from d in db.EmpProjects select d).AsQueryable() ;
            return View(empProjects);
        }

        // GET: EmpProjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpProject empProject = db.EmpProjects.Find(id);
            if (empProject == null)
            {
                return HttpNotFound();
            }
            return View(empProject);
        }

        // GET: EmpProjects/Create
        public ActionResult Create()
        {
            ViewBag.EId = new SelectList(db.Employees, "Eid", "Ename");
            ViewBag.PId = new SelectList(db.Projects, "PId", "PName");
            return View();
        }

        // POST: EmpProjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EPId,PId,EId,Date")] EmpProject empProject)
        {
            if (ModelState.IsValid)
            {
                int pId = empProject.PId;
                var project = (from db in db.EmpProjects where db.PId == pId select db).FirstOrDefault();
                if (project != null)
                {
                    ViewBag.Message = "Employee has already been assigned a project";
                }
                else
                {
                    db.EmpProjects.Add(empProject);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.EId = new SelectList(db.Employees, "Eid", "Ename", empProject.EId);
            ViewBag.PId = new SelectList(db.Projects, "PId", "PName", empProject.PId);
            return View(empProject);
        }

        // GET: EmpProjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpProject empProject = db.EmpProjects.Find(id);
            if (empProject == null)
            {
                return HttpNotFound();
            }
            ViewBag.EId = new SelectList(db.Employees, "Eid", "Ename", empProject.EId);
            ViewBag.PId = new SelectList(db.Projects, "PId", "PName", empProject.PId);
            return View(empProject);
        }

        // POST: EmpProjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EPId,PId,EId,Date")] EmpProject empProject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empProject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EId = new SelectList(db.Employees, "Eid", "Ename", empProject.EId);
            ViewBag.PId = new SelectList(db.Projects, "PId", "PName", empProject.PId);
            return View(empProject);
        }

        // GET: EmpProjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpProject empProject = db.EmpProjects.Find(id);
            if (empProject == null)
            {
                return HttpNotFound();
            }
            return View(empProject);
        }

        // POST: EmpProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmpProject empProject = db.EmpProjects.Find(id);
            db.EmpProjects.Remove(empProject);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
