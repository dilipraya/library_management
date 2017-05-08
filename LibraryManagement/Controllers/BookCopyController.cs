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
using Microsoft.AspNet.Identity;

namespace LibraryManagement.Controllers
{
    public class BookCopyController : Controller
    {
        private LibraryContext db = new LibraryContext();

        // GET: /BookCopy/
        public ActionResult Index()
        {
            var bookcopies = db.BookCopies.Include(b => b.Book).Include(b => b.Rack);
            return View(bookcopies.ToList());
        }

        // GET: /BookCopy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookCopy bookCopy = db.BookCopies.Find(id);
            if (bookCopy == null)
            {
                return HttpNotFound();
            }
            return View(bookCopy);
        }

        // GET: /BookCopy/Create
        public ActionResult Create()
        {
            ViewBag.bookID = new SelectList(db.Books, "bookID", "title");
            ViewBag.rackID = new SelectList(db.Racks, "rackID", "rack_name");
            return View();
        }

        // POST: /BookCopy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="bookCopyID,bookID,rackID")] BookCopy bookCopy)
        {
            if (ModelState.IsValid)
            {
                Book book = db.Books.Find(bookCopy.bookID);
                int number_of_books = 0;
                if(book.BookCopies.Count() > 0)
                {
                    number_of_books = book.BookCopies.Max(bc => bc.bookCopyNo);
                }
                bookCopy.bookCopyNo = number_of_books + 1;
                bookCopy.added_date = DateTime.Today;
                db.BookCopies.Add(bookCopy);

                db.SaveChanges();
                TempData["Success"] = "Success, Book copy has been added.";
                return RedirectToAction("Index");
            }

            ViewBag.bookID = new SelectList(db.Books, "bookID", "title", bookCopy.bookID);
            ViewBag.rackID = new SelectList(db.Racks, "rackID", "rack_name", bookCopy.rackID);
            TempData["Error"] = "Sorry, Book copy could not be added.";
            return View(bookCopy);
        }

        // GET: /BookCopy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookCopy bookCopy = db.BookCopies.Find(id);
            if (bookCopy == null)
            {
                return HttpNotFound();
            }
            ViewBag.bookID = new SelectList(db.Books, "bookID", "title", bookCopy.bookID);
            ViewBag.rackID = new SelectList(db.Racks, "rackID", "rack_name", bookCopy.rackID);
            return View(bookCopy);
        }

        // POST: /BookCopy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="bookCopyID,bookID,rackID")] BookCopy bookCopy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookCopy).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "Success, Book copy has been updated.";
                return RedirectToAction("Index");
            }
            ViewBag.bookID = new SelectList(db.Books, "bookID", "title", bookCopy.bookID);
            ViewBag.rackID = new SelectList(db.Racks, "rackID", "rack_name", bookCopy.rackID);
            TempData["Error"] = "Sorry, Book copy could not be updated.";
            return View(bookCopy);
        }

        // GET: /BookCopy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookCopy bookCopy = db.BookCopies.Find(id);
            if (bookCopy == null)
            {
                return HttpNotFound();
            }
            return View(bookCopy);
        }

        // POST: /BookCopy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookCopy bookCopy = db.BookCopies.Find(id);
            db.BookCopies.Remove(bookCopy);
            db.SaveChanges();
            TempData["Success"] = "Success, Book copy has been deleted.";
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

        // GET: BookCopy/Loan/1
        public ActionResult Loan(int? id)
        {
            var members = db.Members.Select(s => new
            {
                Text = s.first_name + " " + s.last_name,
                Value = s.memberID
            }).ToList();
            ViewBag.loanTypeID = new SelectList(db.LoanTypes, "loanTypeID", "loan_type_name");
            ViewBag.MembersList = new SelectList(members, "Value", "Text");
            return View();
        }

        // POST: BookCopy/Loan/1
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Loan([Bind(Include = "loanID,date_out,memberID,loanTypeID")] Loan loan, int? id)
        {

            if (ModelState.IsValid)
            {
                Member member = db.Members.Find(loan.memberID);
                BookCopy bookCopy = db.BookCopies.Find(id);
                Book book = bookCopy.Book;

                // Check Member Age Restricition
                if (this.checkAgeRestriction(member, book) == false)
                {
                    TempData["Error"] = "Sorry, the book is not avaliable for members below 18.";
                    return RedirectToAction("Index");
                } 
                
                // Check Allowed books by Membership Type
                if (this.checkAllowedBooks(member) == false)
                {
                    TempData["Error"] = "Sorry, the member has already loaned allowed number of books as per his/her membership type plan.";
                    return RedirectToAction("Index");
                }

                LoanType loanType = db.LoanTypes.Find(loan.loanTypeID);

                loan.date_due = loan.date_out.AddDays(loanType.duration);
                loan.BookCopy = bookCopy;
                db.Loans.Add(loan);

                // Change Book Copy Loan Status
                bookCopy.loan_status = true;

                // Calculate Standard Charge for the Book
                decimal loan_duration = (decimal)loanType.duration;  // Casting duration to decimal
                decimal total_charge = book.standard_charge * loan_duration;
                db.SaveChanges();

                // Add Loan Log with Staff
                LoanLog loanLog = new LoanLog();
                loanLog.LoanID = loan.loanID;
                loanLog.username = User.Identity.GetUserName();
                loanLog.loan_type = "Out";
                db.LoanLogs.Add(loanLog);
                db.SaveChanges();
                TempData["Success"] = "Success, The book copy has been loaned. The estimated cost for the loan is "+total_charge;
                return RedirectToAction("Index");
            }
            
                TempData["Error"] = "Sorry, the could not be loaned.";
                return RedirectToAction("Index");

        }

        // GET: /BookCopy/Return/5
        public ActionResult Return(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookCopy bookCopy = db.BookCopies.Find(id);
            if (bookCopy == null)
            {
                return HttpNotFound();
            }
            Loan loan = bookCopy.Loans.SingleOrDefault(l => l.date_returned == null);
            loan.date_returned = DateTime.Now;
            bookCopy.loan_status = false;
            db.SaveChanges();


            // Calculate Charge for the Book
            int total_loan_days = (DateTime.Today - loan.date_out).Days;
            // For book returned in same day
            if (total_loan_days == 0)
            {
                total_loan_days = 1;
            }

            decimal standard_charge = 0;
            decimal penalty_charge = 0;
            decimal total_loan_charge = 0;
            if (total_loan_days > loan.LoanType.duration)
            {
                standard_charge = loan.LoanType.duration * bookCopy.Book.standard_charge;
                penalty_charge = (total_loan_days - loan.LoanType.duration) * bookCopy.Book.penalty_charge;
                total_loan_charge = standard_charge + penalty_charge;
            }
            else
            {
                standard_charge = total_loan_days * bookCopy.Book.standard_charge;
                total_loan_charge = standard_charge;
            }

            // Add Loan Log with Staff
            LoanLog loanLog = new LoanLog();
            loanLog.LoanID = loan.loanID;
            loanLog.username = User.Identity.GetUserName();
            loanLog.loan_type = "Return";
            db.LoanLogs.Add(loanLog);
            db.SaveChanges();
            TempData["Success"] = "Success, The book has been returned. The total charge for the book is " + total_loan_charge + " for " + total_loan_days + " days. Where, standard charge is " + standard_charge + " and penalty chanrge is " + penalty_charge;
            return RedirectToAction("Index");
        }

        /**
         * This method checks if the member age above 18 to loan a book of age restricted category.
         * @param member
         * @param book
         * 
         * returns bool
         */
        private bool checkAgeRestriction(Member member, Book book)
        {
            int member_age = DateTime.Today.Year - member.date_of_birth.Year;
            int category_with_age_restriction = book.BookCategories.Where(bc => bc.age_restriction == true).Count();

            if(member_age < 18)
            {
                if(category_with_age_restriction > 0)
                {
                    return false;
                }
            }
            return true;
        }

        /**
         * This method checks if the member exceeds the book loan number by membership type.
         * @param member
         * 
         * return bool
         */ 
        private bool checkAllowedBooks(Member member)
        {
            MembershipType membershipType = db.MembershipTypes.Find(member.membershipTypeID);
            int number_of_books_allowed = membershipType.books_allowed;
            int number_of_books_loaned = member.Loans.Where(l => l.date_returned == null).Count();

            if(number_of_books_loaned >= number_of_books_allowed)
            {
                return false;
            }
            return true;
        }

        // GET: /BookCopy/Old
        public ActionResult Old()
        {
            DateTime book_expiry_date = DateTime.Today.AddDays(-365); //
            var bookcopies = db.BookCopies.Where(b => b.added_date < book_expiry_date && b.loan_status == false).Include(b => b.Book).Include(b => b.Rack);
            return View(bookcopies.ToList());
        }

        // GET: /BookCopy/DeleteOld
        public ActionResult DeleteOld()
        {
            DateTime book_expiry_date = DateTime.Today.AddDays(-365);
            db.BookCopies.RemoveRange(db.BookCopies.Where(b => b.added_date < book_expiry_date && b.loan_status == false));
            db.SaveChanges();

            TempData["Success"] = "Success, The book copies older than 365 days have been deleted ";
            return RedirectToAction("Old");
        }

        // GET: /BookCopy/OnLoan
        public ActionResult Onloan()
        {
           var loans = db.Loans.Where(lo => lo.date_returned == null).Include(lo => lo.BookCopy).Include(lo => lo.Member).OrderByDescending(lo => lo.date_out);
           //var loans = db.Loans.Where(lo => lo.date_returned == null).Include(lo => lo.BookCopy).Include(lo => lo.Member).OrderByDescending(lo => lo.date_out).GroupBy(lo => lo.date_out).Select(m => new {date_out = m.Key, count = m.Count() });
           return View(loans.ToList());
        }

    }
}
