using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class Rack
    {
        public Rack()
        {
            this.BookCopies = new HashSet<BookCopy>();
        }

        [Key, DisplayName("Rack ID")]
        public int rackID { get; set; }

        [DisplayName("Rack Name"), Required]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Please enter rack name using 5-30 characters.")]
        public string rack_name { get; set; }

        [DisplayName("Shelf ID"), Required]
        public int shelfID { get; set; }

        public virtual Shelf Shelf { get; set; }
        public virtual ICollection<BookCopy> BookCopies { get; set; }
    }
}