using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagement.Models.ViewModels
{
    public class BookSearchModel
    {
        public Book Book { get; set; }

        public string search_name
        {
            get; set;
        }
        [DefaultValue(false)]
        public string book_in_shelf
        {
            get; set;
        }
    }
}