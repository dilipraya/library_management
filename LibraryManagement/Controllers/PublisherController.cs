﻿using System;
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
    public class PublisherController : ApplicationBaseController
    {
        private LibraryContext db = new LibraryContext();

        // GET: /Publisher/
        public ActionResult Index()
        {
            return View(db.Publishers.ToList());
        }

        // GET: /Publisher/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publisher publisher = db.Publishers.Find(id);
            if (publisher == null)
            {
                return HttpNotFound();
            }
            return View(publisher);
        }

        // GET: /Publisher/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Publisher/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="publisherID,name,email,contact_number,address,website,press")] Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                db.Publishers.Add(publisher);
                db.SaveChanges();
                TempData["Success"] = "Success, Publisher has been added.";
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Sorry, Publisher could not be added.";
            return View(publisher);
        }

        // GET: /Publisher/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publisher publisher = db.Publishers.Find(id);
            if (publisher == null)
            {
                return HttpNotFound();
            }
            return View(publisher);
        }

        // POST: /Publisher/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="publisherID,name,email,contact_number,address,website,press")] Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(publisher).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "Success, Publisher has been updated.";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Sorry, Publisher could not be updated.";
            return View(publisher);
        }

        // GET: /Publisher/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publisher publisher = db.Publishers.Find(id);
            if (publisher == null)
            {
                return HttpNotFound();
            }
            return View(publisher);
        }

        // POST: /Publisher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Publisher publisher = db.Publishers.Find(id);
            db.Publishers.Remove(publisher);
            db.SaveChanges();
            TempData["Success"] = "Success, Publisher has been deleted.";
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
