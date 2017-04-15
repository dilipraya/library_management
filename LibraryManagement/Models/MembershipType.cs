using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class MembershipType
    {
        public MembershipType()
        {
            this.Members = new HashSet<Member>();
        }
        [Key, DisplayName("Membership Type ID")]
        public int membershipTypeID { get; set; }
        [DisplayName("Membership Type Name"), Required]
        public string membership_type_name { get; set; }
        [DisplayName("Number of Books allowed"), Required]
        public int books_allowed { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }
}