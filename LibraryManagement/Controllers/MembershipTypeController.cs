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
    public class MembershipTypeController : ApplicationBaseController
    {
        private LibraryContext db = new LibraryContext();

        // GET: /MembershipType/
        public ActionResult Index()
        {
            return View(db.MembershipTypes.ToList());
        }

        // GET: /MembershipType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MembershipType membershipType = db.MembershipTypes.Find(id);
            if (membershipType == null)
            {
                return HttpNotFound();
            }
            return View(membershipType);
        }

        // GET: /MembershipType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /MembershipType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="membershipTypeID,membership_type_name,books_allowed")] MembershipType membershipType)
        {
            if (ModelState.IsValid)
            {
                db.MembershipTypes.Add(membershipType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(membershipType);
        }

        // GET: /MembershipType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MembershipType membershipType = db.MembershipTypes.Find(id);
            if (membershipType == null)
            {
                return HttpNotFound();
            }
            return View(membershipType);
        }

        // POST: /MembershipType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="membershipTypeID,membership_type_name,books_allowed")] MembershipType membershipType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(membershipType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(membershipType);
        }

        // GET: /MembershipType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MembershipType membershipType = db.MembershipTypes.Find(id);
            if (membershipType == null)
            {
                return HttpNotFound();
            }
            return View(membershipType);
        }

        // POST: /MembershipType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MembershipType membershipType = db.MembershipTypes.Find(id);
            db.MembershipTypes.Remove(membershipType);
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
