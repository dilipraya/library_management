using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class Loan
    {
        public Loan()
        {
            this.LoanLogs = new HashSet<LoanLog>();
        }

        [key, DisplayName("Loan ID")]
        public int loanID { get; set; }

        [DisplayName("Loan Date"), Required, DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime date_out { get; set; }

        [DisplayName("Due Date"), Required, DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime date_due { get; set; }

        [DisplayName("Return Date"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime date_returned { get; set; }

        [DisplayName("Book Copy ID"), Required]
        public int bookCopyID { get; set; }

        [DisplayName("Member ID"), Required]
        public int memberID { get; set; }

        [DisplayName("Loan Type ID"), Required]
        public int loanTypeID { get; set; }

        public virtual BookCopy BookCopy { get; set; }
        public virtual Member Member { get; set; }
        public virtual LoanType LoanType { get; set; }

        public virtual ICollection<LoanLog> LoanLogs { get; set; }
    }
}