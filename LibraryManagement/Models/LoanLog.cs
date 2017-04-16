using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class LoanLog
    {
        public enum Type{
            Out, Returned
        }

        [key, DisplayName("Loan Log ID")]
        public int LoanLogID { get; set; }

        [DisplayName("Loan ID")]
        public int LoanID { get; set; }

        [DisplayName("User ID")]
        public int userID { get; set; }

        [DisplayName("Loan Type"), Required]
        public Type loan_type { get; set; }

        public Loan Loan { get; set; }
    }
}