using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class Author
    {
        public Author()
        {
            this.Books = new HashSet<Book>();
        }

        public enum AuthorGender
        {
            male, female, other
        }

        [Key, DisplayName("Author ID")]
        public int authorID { get; set; }

        [DisplayName("First Name"), Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Please enter the first name using 3-15 characters.")]
        public string first_name { get; set; }

        [DisplayName("Last Name"), Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Please enter the last name using 3-15 characters.")]
        public string last_name { get; set; }
        public AuthorGender? gender { get; set; }

        [DisplayName("Email"), Required]
        [StringLength(40, MinimumLength = 5, ErrorMessage = "Please enter the email using 5-40 characters.")]
        public string email { get; set; }

        [DisplayName("Website URL"), Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Please enter the website using 5-50 characters.")]
        public string website { get; set; }

        public virtual ICollection<Book> Books { get; set; }

    }
}