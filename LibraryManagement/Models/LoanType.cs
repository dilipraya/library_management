using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class LoanType
    {
        public LoanType(){

            this.Loans = new HashSet<Loan>();
        }

        [Key, DisplayName("Loan Type ID")]
        public int loanTypeID { get; set; }
    
        [DisplayName("Loan Type Name"), Required]
        public string loan_type_name { get; set; }

        [DisplayName("Duration")]
        public int duration { get; set; }

        public virtual ICollection<Loan> Loans { get; set; }
    }
}