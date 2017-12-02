using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AuthenticationDBTest.Data;
using System.IO;
using Microsoft.AspNet.Identity;
using System;

namespace AuthenticationDBTest.Controllers
{
    public class UserDocumentsController : Controller
    {
        private SecondDBEntities db = new SecondDBEntities();

        // GET: UserDocuments
        public ActionResult Index()
        {
            return View(db.UserDocuments.ToList());
        }

        // GET: UserDocuments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDocument userDocument = db.UserDocuments.Find(id);
            if (userDocument == null)
            {
                return HttpNotFound();
            }
            return View(userDocument);
        }

        // GET: UserDocuments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserDocuments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase file)
        {
            UserDocument userDocument = new UserDocument();
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    string filePath = Path.Combine(Server.MapPath("/Uploaded Files"), file.FileName);
                    string fileExtension = Path.GetExtension(file.FileName);

                    if (!System.IO.File.Exists(filePath))
                    {
                        userDocument.FileName = file.FileName;
                        userDocument.FilePath = "/Uploaded Files/" + file.FileName;
                        userDocument.UserId = User.Identity.GetUserId();
                        userDocument.CreateDate = DateTime.Now;
                        
                        file.SaveAs(filePath);
                        db.UserDocuments.Add(userDocument);
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                // Log Exception
                throw;
            }

            return View(userDocument);
        }

        // GET: UserDocuments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDocument userDocument = db.UserDocuments.Find(id);
            if (userDocument == null)
            {
                return HttpNotFound();
            }
            return View(userDocument);
        }

        // POST: UserDocuments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FileName,FilePath,UserId,CreateDate")] UserDocument userDocument)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userDocument).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userDocument);
        }

        // GET: UserDocuments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDocument userDocument = db.UserDocuments.Find(id);
            string filePath = Path.Combine(Server.MapPath("~/Uploaded Files"), userDocument.FileName);
            System.IO.File.Delete(filePath);
            if (userDocument == null)
            {
                return HttpNotFound();
            }
            return View(userDocument);
        }

        // POST: UserDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserDocument userDocument = db.UserDocuments.Find(id);
            db.UserDocuments.Remove(userDocument);
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
