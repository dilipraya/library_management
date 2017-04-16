using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class Publisher
    {
        public Publisher()
        {
            this.Books = new HashSet<Book>();
        }

        [Key, DisplayName("Publisher ID")]
        public int publisherID { get; set; }

        [DisplayName("Name"), Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Please enter publisher name using 5-50 characters.")]
        public string name { get; set; }

        [DisplayName("Email"), Required]
        [StringLength(40, MinimumLength = 5, ErrorMessage = "Please enter the email using 5-40 characters.")]
        public string email { get; set; }

        [DisplayName("Contact Number"), Required]
        [StringLength(15, MinimumLength = 9, ErrorMessage = "Please enter the website using 9-15 characters.")]
        public string contact_number { get; set; }

        [DisplayName("Address"), Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Please enter the address using 5-100 characters.")]
        public string address { get; set; }

        [DisplayName("Website URL"), Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Please enter the website using 5-50 characters.")]
        public string website { get; set; }

        [DisplayName("Press Name"), Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Please enter the press name using 5-50 characters.")]
        public string press { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}