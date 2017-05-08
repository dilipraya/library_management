//controller
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagement.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Net;

namespace LibraryManagement.Controllers
{
    public class RoleController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        

        // GET: Roles
        public ActionResult Index()
        {
            return View(context.Roles.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                context.Roles.Add(new IdentityRole()
                {
                    Name = collection["RoleName"]
                });
                context.SaveChanges();
                TempData["Success"] = "Role created successfully !";
                return View("Index", context.Roles.ToList());
            }
            catch
            {
                TempData["Error"] = "Sorry, Role could not be created.";
                return View();
            }
        }

        // GET: /Role/Details/Admin
        public ActionResult Details(string roleName)
        {
            var thisRole = context.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            return View(thisRole);
        }




        // GET: /Rack/Delete/5
        public ActionResult Delete(string roleName)
        {
            if (roleName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var thisRole = context.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            
            if (thisRole == null)
            {
                return HttpNotFound();
            }
            return View(thisRole);
        }

        // POST: /Rack/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string RoleName)
        {
            var thisRole = context.Roles.Where(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            context.Roles.Remove(thisRole);
            TempData["Success"] = "Role deleted successfully !";
            return View("Index", context.Roles.ToList());
        }

        //
        // GET: /Roles/Edit/5
        public ActionResult Edit(string roleName)
        {
            var thisRole = context.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            return View(thisRole);
        }

        //
        // POST: /Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Microsoft.AspNet.Identity.EntityFramework.IdentityRole role)
        {
            try
            {
                context.Entry(role).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

                ViewBag.ResultMessage = "Role updated successfully !";
                return View("Index", context.Roles.ToList());
            }
            catch
            {
                TempData["Error"] = "Sorry, Role could not be updated.";
                return View();
            }
        }

        //Controller code for above
        public ActionResult ManageUserRoles()
        {
            // prepopulat roles for the view dropdown
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr =>

            new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;
            return View();
        }


        public ActionResult ManageUsers()
        {
            // prepopulat roles for the view dropdown
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr =>
            new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string UserName, string RoleName)
        {
            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            manager.AddToRole(user.Id, RoleName);

            //ViewBag.ResultMessage = "Role created successfully !";

            // prepopulat roles for the view dropdown
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;
            TempData["Success"] = "Role "+ RoleName +" added to the user "+UserName;

            return View("ManageUsers");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetRoles(string UserName)
        {
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                ViewBag.RolesForThisUser = manager.GetRoles(user.Id);

                // prepopulat roles for the view dropdown
                var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = list;
            }

            return View("ManageUsers");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleForUser(string UserName, string RoleName)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            if (manager.IsInRole(user.Id, RoleName))
            {
                manager.RemoveFromRole(user.Id, RoleName);
                TempData["Success"] = "Role " + RoleName + " removed from the user " + UserName;
            }
            else
            {
                TempData["Error"] = "This user doesn't belong to selected role.";
            }
            // prepopulat roles for the view dropdown
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            return View("ManageUsers");
        }

    }
}