using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagement.Controllers
{
    [Authorize]
    public class HomeController : ApplicationBaseController
    {
        public ActionResult Index()
        {
            ViewData["SubTitle"] = "Welcome to Library Management System ";
            ViewData["Message"] = "It is a library management system which can be used by school, colleges and organizational libraries.";

            return View();
        }
    }
}