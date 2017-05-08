using LibraryManagement.DAL;
using LibraryManagement.Models;
using LibraryManagement.Models.ViewModels;
using System.Data;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagement.Controllers
{
    public class BookSearchController : Controller
    {
        private LibraryContext db = new LibraryContext();

        // POST: /Book/Search
        [HttpGet]
        public ActionResult Search(BookSearchModel book)
        {
            var books = db.Books.Include(b => b.Publisher);
            if(book.book_in_shelf == "true")
            {
                //check if null or not
                if(book.search_name != null)
                {
                    books = db.Books.Where(b => b.title.Contains(book.search_name) ||
                                              b.Publisher.name.Contains(book.search_name) ||
                                              b.Authors.Any(au => au.first_name.Contains(book.search_name) ||
                                                                  au.last_name.Contains(book.search_name)))
                                                                  .Where(b => b.BookCopies.Where(x => x.loan_status == false).Count() >= 1)
                                                                  .Include(b => b.Publisher);

                }else
                {
                    books = db.Books.Where(b => b.BookCopies.Where(x => x.loan_status == false).Count() >= 1)
                                                                     .Include(b => b.Publisher);

                }
            }
            else
            {
                if(book.search_name != null)
                {
                    books = db.Books.Where(b => b.title.Contains(book.search_name) ||
                                           b.Publisher.name.Contains(book.search_name) ||
                                           b.Authors.Any(au => au.first_name.Contains(book.search_name) ||
                                                               au.last_name.Contains(book.search_name))).Include(b => b.Publisher);

                }

            }
            
            return View("Index", books.ToList());
            
        }
    }
}