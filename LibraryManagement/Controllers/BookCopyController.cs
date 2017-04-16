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
    public class BookCopyController : ApplicationBaseController
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
        public ActionResult Create([Bind(Include="bookCopyID,bookCopyNo,bookID,rackID")] BookCopy bookCopy)
        {
            if (ModelState.IsValid)
            {
                db.BookCopies.Add(bookCopy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.bookID = new SelectList(db.Books, "bookID", "title", bookCopy.bookID);
            ViewBag.rackID = new SelectList(db.Racks, "rackID", "rack_name", bookCopy.rackID);
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
        public ActionResult Edit([Bind(Include="bookCopyID,bookCopyNo,bookID,rackID")] BookCopy bookCopy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookCopy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.bookID = new SelectList(db.Books, "bookID", "title", bookCopy.bookID);
            ViewBag.rackID = new SelectList(db.Racks, "rackID", "rack_name", bookCopy.rackID);
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
