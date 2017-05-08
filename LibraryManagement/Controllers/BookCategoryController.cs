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
    public class BookCategoryController : ApplicationBaseController
    {
        private LibraryContext db = new LibraryContext();

        // GET: /BookCategory/
        public ActionResult Index()
        {
            return View(db.BookCategories.ToList());
        }

        // GET: /BookCategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookCategory bookCategory = db.BookCategories.Find(id);
            if (bookCategory == null)
            {
                return HttpNotFound();
            }
            return View(bookCategory);
        }

        // GET: /BookCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /BookCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="bookCategoryID,category_name,age_restriction")] BookCategory bookCategory)
        {
            if (ModelState.IsValid)
            {
                db.BookCategories.Add(bookCategory);
                db.SaveChanges();
                TempData["Success"] = "Success, Book Category has been added.";
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Sorry, Book Category could not be added.";
            return View(bookCategory);
        }

        // GET: /BookCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookCategory bookCategory = db.BookCategories.Find(id);
            if (bookCategory == null)
            {
                return HttpNotFound();
            }
            return View(bookCategory);
        }

        // POST: /BookCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="bookCategoryID,category_name,age_restriction")] BookCategory bookCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookCategory).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "Success, Book Category has been updated.";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Sorry, Book Category could not be updated.";
            return View(bookCategory);
        }

        // GET: /BookCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookCategory bookCategory = db.BookCategories.Find(id);
            if (bookCategory == null)
            {
                return HttpNotFound();
            }
            return View(bookCategory);
        }

        // POST: /BookCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookCategory bookCategory = db.BookCategories.Find(id);
            db.BookCategories.Remove(bookCategory);
            db.SaveChanges();
            TempData["Success"] = "Success, Book Category has been deleted.";
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
