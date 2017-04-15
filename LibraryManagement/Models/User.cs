using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class User
    {
        public User()
        {
            this.LoanLogs = new HashSet<LoanLog>();
        }

        public enum Gender
        {
            male, female, other
        }

        [Key, DisplayName("User ID")]
        public int userID { get; set; }

        [DisplayName("User Name"), Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Please enter the user name using 3-30 characters.")]
        public string username { get; set; }

        public string password { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public Gender? gender { get; set; }
        public DateTime date_of_birth { get; set; }
        public string email { get; set; }
        public string contact_number { get; set; }
        public string address { get; set; }
        public bool status { get; set; }
        public int roleID { get; set; }

        public Role Role { get; set; }

        public virtual ICollection<LoanLog> LoanLogs { get; set; }
    }
}