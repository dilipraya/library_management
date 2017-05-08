using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagement.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using LibraryManagement.Models.ViewModels;
using System.Net;

namespace LibraryManagement.Controllers
{
    public class StaffController : ApplicationBaseController
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        // GET: Staff
        public ActionResult Index()
        {
            var users = context.Users.ToList();
            var userVM = users.Select(user => new UserViewModel { UserName = user.UserName, FirstName = user.FirstName, LastName = user.LastName, }).ToList();
            var model = new GroupedUserViewModel { Users = userVM};
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateUserViewModel model)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = model.UserName,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                };
                var result = manager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    TempData["Success"] = "Succes, User was added.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Error"] = "Sorry, User could not be added.";
                    return View("Index");
                }
            }
            
            TempData["Error"] = "Sorry, User could not be added.";
            return View(model);
        }


        public ActionResult Edit(string username)
        {
           return View();
        }

        [HttpPost]
        public ActionResult Edit(CreateUserViewModel models)
        {

            TempData["Success"] = "Success, User has been updated.";
            return RedirectToAction("Index");
        }

        // POST: /Shelf/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete()
        {
            TempData["Success"] = "Success, User has been deleted.";
            return RedirectToAction("Index");
        }
    }
}