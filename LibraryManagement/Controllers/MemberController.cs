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
    public class MemberController : ApplicationBaseController
    {
        private LibraryContext db = new LibraryContext();

        // GET: /Member/
        public ActionResult Index()
        {
            // Sort member by their first name alphabetically
            var members = db.Members.Include(m => m.MembershipType).OrderBy(m => m.first_name.Length).ThenBy(m => m.first_name);
            return View(members.ToList());
        }

        // GET: /Member/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: /Member/Create
        public ActionResult Create()
        {
            ViewBag.MembershipTypeID = new SelectList(db.MembershipTypes, "membershipTypeID", "membership_type_name");
            return View();
        }

        // POST: /Member/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="memberID,first_name,last_name,gender,date_of_birth,email,contact_number,address,MembershipTypeID")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Members.Add(member);
                TempData["Success"] = "Success, Member has been added.";
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Sorry, Member could not be added.";
            ViewBag.membershipTypeID = new SelectList(db.MembershipTypes, "membershipTypeID", "membership_type_name", member.membershipTypeID);
            return View(member);
        }

        // GET: /Member/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            ViewBag.membershipTypeID = new SelectList(db.MembershipTypes, "membershipTypeID", "membership_type_name", member.membershipTypeID);
            return View(member);
        }

        // POST: /Member/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="memberID,first_name,last_name,gender,date_of_birth,email,contact_number,address,MembershipTypeID")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                TempData["Success"] = "Success, Member has been updated.";
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Sorry, Member could not be updated.";
            ViewBag.MembershipTypeID = new SelectList(db.MembershipTypes, "membershipTypeID", "membership_type_name", member.membershipTypeID);
            return View(member);
        }

        // GET: /Member/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: /Member/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
            db.SaveChanges();
            TempData["Success"] = "Success, Member has been deleted.";
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


        // GET: /Member/Inactive
        public ActionResult Inactive()
        {
            DateTime last_loan_date = DateTime.Today.AddDays(-31); // 31 Days before today
            var members = db.Members.Where(m => m.Loans.Count() > 0).Where(m => m.Loans.All(lo => lo.date_out < last_loan_date));
            return View(members.ToList());
        }

    }
}
