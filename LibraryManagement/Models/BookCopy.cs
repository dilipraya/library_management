using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class BookCopy
    {
        public BookCopy()
        {
            this.Loans = new HashSet<Loan>();
        }

        [Key, DisplayName("Book Copy ID")]
        public int bookCopyID { get; set; }

        [DisplayName("Copy Number")]
        public int bookCopyNo { get; set; }

        [DisplayName("Book"), Required]
        public int bookID { get; set; }

        [DisplayName("Rack"), Required]
        public int rackID { get; set; }
        public virtual Book Book { get; set; }
        public virtual Rack Rack { get; set; }

        public virtual ICollection<Loan> Loans { get; set; }
    }
}