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
    public class LoanController : ApplicationBaseController
    {
        private LibraryContext db = new LibraryContext();

        // GET: /Loan/
        public ActionResult Index()
        {
            var loans = db.Loans.Include(l => l.BookCopy).Include(l => l.LoanType).Include(l => l.Member);
            return View(loans.ToList());
        }

        // GET: /Loan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        // GET: /Loan/Create
        public ActionResult Create()
        {
            ViewBag.bookCopyID = new SelectList(db.BookCopies, "bookCopyID", "bookCopyID");
            ViewBag.loanTypeID = new SelectList(db.LoanTypes, "loanTypeID", "loan_type_name");
            ViewBag.memberID = new SelectList(db.Members, "memberID", "first_name");
            return View();
        }

        // POST: /Loan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="loanID,date_out,date_due,date_returned,bookCopyID,memberID,loanTypeID")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                db.Loans.Add(loan);
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }

            ViewBag.bookCopyID = new SelectList(db.BookCopies, "bookCopyID", "bookCopyID", loan.bookCopyID);
            ViewBag.loanTypeID = new SelectList(db.LoanTypes, "loanTypeID", "loan_type_name", loan.loanTypeID);
            ViewBag.memberID = new SelectList(db.Members, "memberID", "first_name", loan.memberID);
            return View(loan);
        }

        // GET: /Loan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            ViewBag.bookCopyID = new SelectList(db.BookCopies, "bookCopyID", "bookCopyID", loan.bookCopyID);
            ViewBag.loanTypeID = new SelectList(db.LoanTypes, "loanTypeID", "loan_type_name", loan.loanTypeID);
            ViewBag.memberID = new SelectList(db.Members, "memberID", "first_name", loan.memberID);
            return View(loan);
        }

        // POST: /Loan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="loanID,date_out,date_due,date_returned,bookCopyID,memberID,loanTypeID")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.bookCopyID = new SelectList(db.BookCopies, "bookCopyID", "bookCopyID", loan.bookCopyID);
            ViewBag.loanTypeID = new SelectList(db.LoanTypes, "loanTypeID", "loan_type_name", loan.loanTypeID);
            ViewBag.memberID = new SelectList(db.Members, "memberID", "first_name", loan.memberID);
            return View(loan);
        }

        // GET: /Loan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        // POST: /Loan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Loan loan = db.Loans.Find(id);
            db.Loans.Remove(loan);
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
