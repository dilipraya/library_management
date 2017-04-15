using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagement.Models.ViewModels
{
    public class BookViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<SelectListItem> AllAuthors { get; set; }
        public IEnumerable<SelectListItem> AllBookCategories { get; set; }

        private List<int> _selectedAuthors;
        private List<int> _selectedBookCategories;

        public BookViewModel()
        {
            AllAuthors = new List<SelectListItem>();
            AllBookCategories = new List<SelectListItem>();
        }

        public List<int> SelectedAuthors
        {
            get
            {
                if (_selectedAuthors == null)
                {
                    _selectedAuthors = Book.Authors.Select(m => m.authorID).ToList();
                }
                return _selectedAuthors;
            }
            set { _selectedAuthors = value; }
        }

        public List<int> SelectedBookCategories
        {
            get
            {
                if (_selectedBookCategories == null)
                {
                    _selectedBookCategories = Book.BookCategories.Select(m => m.bookCategoryID).ToList();
                }
                return _selectedBookCategories;
            }
            set { _selectedBookCategories = value; }
        }
       
    }
}