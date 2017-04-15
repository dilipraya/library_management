using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class BookCategory
    {
        public BookCategory()
        {
            this.Books = new HashSet<Book>();
        }

        [Key, DisplayName("Book Category ID")]
        public int bookCategoryID { get; set; }

        [DisplayName("Category Name"), Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Please enter the book category name using 3-20 characters.")]
        public string category_name { get; set; }

        [DisplayName("Age Restriction"), Required, DefaultValue(true)]
        public bool age_restriction { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}