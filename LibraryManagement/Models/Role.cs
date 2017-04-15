using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class Role
    {
        public Role()
        {
            this.Users = new HashSet<User>();
        }

        [Key, DisplayName("Role ID")]
        public int RoleID { get; set; }

        [DisplayName("Role Name"), Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Please enter the role name using 3-15 characters.")]
        public string role_name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}