using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class Book
    {
        public Book()
        {
            this.Authors = new HashSet<Author>();
            this.BookCategories = new HashSet<BookCategory>();
            this.BookCopies = new HashSet<BookCopy>();
        }

        [Key, DisplayName("Book ID")]
        public int bookID { get; set; }

        [DisplayName("Book Title"), Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Please enter the book title using 3-50 characters.")]
        public string title { get; set; }

        [DisplayName("Published Date"), Required, DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime published_date { get; set; }

        [DisplayName("Standard Charge"), Required]
        public decimal standard_charge { get; set; }

        [DisplayName("Penalty Charge"), Required]
        public decimal penalty_charge { get; set; }
        
        public int publisherID { get; set; }

        [DisplayName("Publisher ID")]
        public virtual Publisher Publisher { get; set; }

        public virtual ICollection<Author> Authors { get; set; }
        public virtual ICollection<BookCategory> BookCategories { get; set; }
        public virtual ICollection<BookCopy> BookCopies { get; set; }

        public string author_name
        {
            get; set;
        }
    }
}