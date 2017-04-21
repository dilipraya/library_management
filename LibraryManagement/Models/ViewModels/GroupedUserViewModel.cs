using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models.ViewModels
{
    public class GroupedUserViewModel
    {
        public List<UserViewModel> Users { get; set; }
    }

    public class UserViewModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}