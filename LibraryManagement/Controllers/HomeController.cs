using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using LibraryManagement.DAL;
using LibraryManagement.Models;

namespace LibraryManagement.Controllers
{
    [Authorize]
    public class HomeController : ApplicationBaseController
    {
        private LibraryContext db = new LibraryContext();
        public ActionResult Index()
        {
            ViewData["SubTitle"] = "Welcome to Library Management System ";
            ViewData["Message"] = "It is a library management system which can be used by school, colleges and organizational libraries.";

            ViewData["BookCopiesOnLoan"] = db.BookCopies.Where(bc => bc.loan_status == true).Count();
            ViewData["BookCopies"] = db.BookCopies.Count();
            ViewData["Books"] = db.Books.Count();
            ViewData["Members"] = db.Members.Count();
            ViewData["Authors"] = db.Authors.Count();
            ViewData["Publishers"] = db.Publishers.Count();
            ViewData["Racks"] = db.Racks.Count();
            return View();
        }
    }
}