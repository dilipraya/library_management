using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryManagement.DAL;
using LibraryManagement.Models;

namespace LibraryManagement.Controllers
{
    public class LoanTypeController : ApplicationBaseController
    {
        private LibraryContext db = new LibraryContext();

        // GET: /LoanType/
        public ActionResult Index()
        {
            return View(db.LoanTypes.ToList());
        }

        // GET: /LoanType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanType loanType = db.LoanTypes.Find(id);
            if (loanType == null)
            {
                return HttpNotFound();
            }
            return View(loanType);
        }

        // GET: /LoanType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /LoanType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="loanTypeID,loan_type_name,duration")] LoanType loanType)
        {
            if (ModelState.IsValid)
            {
                db.LoanTypes.Add(loanType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loanType);
        }

        // GET: /LoanType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanType loanType = db.LoanTypes.Find(id);
            if (loanType == null)
            {
                return HttpNotFound();
            }
            return View(loanType);
        }

        // POST: /LoanType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="loanTypeID,loan_type_name,duration")] LoanType loanType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loanType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loanType);
        }

        // GET: /LoanType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanType loanType = db.LoanTypes.Find(id);
            if (loanType == null)
            {
                return HttpNotFound();
            }
            return View(loanType);
        }

        // POST: /LoanType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoanType loanType = db.LoanTypes.Find(id);
            db.LoanTypes.Remove(loanType);
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
