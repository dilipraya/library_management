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
    public class RackController : ApplicationBaseController
    {
        private LibraryContext db = new LibraryContext();

        // GET: /Rack/
        public ActionResult Index()
        {
            var racks = db.Racks.Include(r => r.Shelf);
            return View(racks.ToList());
        }

        // GET: /Rack/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rack rack = db.Racks.Find(id);
            if (rack == null)
            {
                return HttpNotFound();
            }
            return View(rack);
        }

        // GET: /Rack/Create
        public ActionResult Create()
        {
            ViewBag.shelfID = new SelectList(db.Shelves, "shelfID", "shelf_name");
            return View();
        }

        // POST: /Rack/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="rackID,rack_name,shelfID")] Rack rack)
        {
            if (ModelState.IsValid)
            {
                db.Racks.Add(rack);
                db.SaveChanges();
                TempData["Success"] = "Success, Rack has been added.";
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Sorry, Rack could not be added.";
            ViewBag.shelfID = new SelectList(db.Shelves, "shelfID", "shelf_name", rack.shelfID);
            return View(rack);
        }

        // GET: /Rack/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rack rack = db.Racks.Find(id);
            if (rack == null)
            {
                return HttpNotFound();
            }
            ViewBag.shelfID = new SelectList(db.Shelves, "shelfID", "shelf_name", rack.shelfID);
            return View(rack);
        }

        // POST: /Rack/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="rackID,rack_name,shelfID")] Rack rack)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rack).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "Success, Rack has been updated.";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Sorry, Rack could not be updated.";
            ViewBag.shelfID = new SelectList(db.Shelves, "shelfID", "shelf_name", rack.shelfID);
            return View(rack);
        }

        // GET: /Rack/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rack rack = db.Racks.Find(id);
            if (rack == null)
            {
                return HttpNotFound();
            }
            return View(rack);
        }

        // POST: /Rack/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rack rack = db.Racks.Find(id);
            db.Racks.Remove(rack);
            db.SaveChanges();
            TempData["Success"] = "Success, Rack has been deleted.";
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
