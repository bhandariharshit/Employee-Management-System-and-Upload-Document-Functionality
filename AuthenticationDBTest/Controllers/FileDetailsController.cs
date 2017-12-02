using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AuthenticationDBTest.Data;
using System.IO;

namespace AuthenticationDBTest.Controllers
{
    [Authorize(Roles ="banker")]
    public class FileDetailsController : Controller
    {
        private SecondDBEntities db = new SecondDBEntities();

        // GET: FileDetails
        public ActionResult Index()
        {
            return View(db.FileDetails.ToList());
        }

        // GET: FileDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileDetail fileDetail = db.FileDetails.Find(id);
            if (fileDetail == null)
            {
                return HttpNotFound();
            }
            return View(fileDetail);
        }

        // GET: FileDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FileDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FileDetail fileDetails)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (fileDetails != null && fileDetails.FileName.Length > 0)
                    {
                        string fileName = "TestFile_" + DateTime.Now.ToString("yymmdd_hhmmss").Replace('/','.')+".txt";
                        string absoluteFilePath = Path.Combine(Server.MapPath("/TextFiles"), fileName);
                        string relativePath = "/TextFiles/" + fileName;

                        if (!System.IO.File.Exists(absoluteFilePath))
                        {
                            StreamWriter sw = new StreamWriter(absoluteFilePath);
                            sw.WriteLine(fileDetails.FileName);
                            sw.Close();
                            sw.Dispose();
                        }
                        else if (System.IO.File.Exists(absoluteFilePath))
                        {
                            TextWriter tw = new StreamWriter(absoluteFilePath);
                            tw.WriteLine("The next line!");
                            tw.Close();
                        }

                        FileDetail objFileDetail = new FileDetail();
                        objFileDetail.FileName = fileName;
                        objFileDetail.FilePath = relativePath;
                        objFileDetail.CreateDate = DateTime.Now;
                        db.FileDetails.Add(objFileDetail);
                        db.SaveChanges();
                        return RedirectToAction("Index");

                    }

                }
            }
            catch (Exception ex)
            {
                //TODO: Log exception
                throw;
            }
            finally
            {


            }
            return View();
        }

        // GET: FileDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileDetail fileDetail = db.FileDetails.Find(id);
            if (fileDetail == null)
            {
                return HttpNotFound();
            }
            return View(fileDetail);
        }

        // POST: FileDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FileName,FilePath,CreateDate")] FileDetail fileDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fileDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fileDetail);
        }

        // GET: FileDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileDetail fileDetail = db.FileDetails.Find(id);
            if (fileDetail == null)
            {
                return HttpNotFound();
            }
            return View(fileDetail);
        }

        // POST: FileDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FileDetail fileDetail = db.FileDetails.Find(id);
            db.FileDetails.Remove(fileDetail);
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

        [HttpGet]
        public ActionResult CheckFileName(string FileName)
        {
             return Json(false, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        [HttpGet]
        public JsonResult useralready(string FileName)
        {
            return Json(!FileName.Equals("selvakumar"),
                                         JsonRequestBehavior.AllowGet);
        }
    }
}
