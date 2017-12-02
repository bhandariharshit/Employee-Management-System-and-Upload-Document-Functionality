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
    public class ManageAccountController : Controller
    {
        private SecondDBEntities db = new SecondDBEntities();

        // GET: ManageAccount
        public ActionResult Index()
        {
            var accounts = db.Accounts.Include(a => a.AccountType);

            var testJoin = (from dba in db.Accounts join dh in db.AccountTypes on dba.AccountTypeId equals dh.AccountTypeId select new { dba.AccountId, dh.AccountTypeId });

            foreach (var test in testJoin)
            {
             //int testid=test.   
            }
            return View(accounts.ToList());
        }

        // GET: ManageAccount/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: ManageAccount/Create
        public ActionResult Create()
        {
            ViewBag.AccountTypeId = new SelectList(db.AccountTypes, "AccountTypeId", "AccountType1");
            return View();
        }

        // POST: ManageAccount/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccountId,AccountCreationDate,AccountTypeId")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountTypeId = new SelectList(db.AccountTypes, "AccountTypeId", "AccountType1", account.AccountTypeId);
            return View(account);
        }

        // GET: ManageAccount/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountTypeId = new SelectList(db.AccountTypes, "AccountTypeId", "AccountType1", account.AccountTypeId);
            return View(account);
        }

        // POST: ManageAccount/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountId,AccountCreationDate,AccountTypeId")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountTypeId = new SelectList(db.AccountTypes, "AccountTypeId", "AccountType1", account.AccountTypeId);
            return View(account);
        }

        // GET: ManageAccount/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: ManageAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
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
