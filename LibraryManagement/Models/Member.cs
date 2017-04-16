using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class Member
    {
        public Member()
        {
            
            this.Loans = new HashSet<Loan>();
        }

        [Key, DisplayName("Member ID")]
        public int memberID { get; set; }

        [DisplayName("First Name"), Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Please enter the first name using 3-15 characters.")]
        public string first_name { get; set; }

        [DisplayName("Last Name"), Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Please enter the last name using 3-15 characters.")]
        public string last_name { get; set; }

        [DisplayName("Gender"), Required]
        public string gender { get; set; }

        [DisplayName("Date of Birth"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime date_of_birth { get; set; }

        [DisplayName("Email"), Required]
        [StringLength(40, MinimumLength = 5, ErrorMessage = "Please enter the email using 5-40 characters.")]
        public string email { get; set; }

        [DisplayName("Contact Number"), Required]
        [StringLength(15, MinimumLength = 9, ErrorMessage = "Please enter the website using 9-15 characters.")]
        public string contact_number { get; set; }

        [DisplayName("Address"), Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Please enter the address using 5-100 characters.")]
        public string address { get; set; }

        [DisplayName("Membership ID")]
        public int MembershipTypeID { get; set; }

        public MembershipType MembershipType { get; set; }

        public virtual ICollection<Loan> Loans { get; set; }
    }
}