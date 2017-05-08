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

        [key, DisplayName("Loan Log ID")]
        public int LoanLogID { get; set; }

        [DisplayName("Loan ID")]
        public int LoanID { get; set; }

        [DisplayName("User Name"), Required]
        public string username{ get; set; }

        [DisplayName("Loan Type"), Required]
        public string loan_type { get; set; }

        public Loan Loan { get; set; }
    }
}