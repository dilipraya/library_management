using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class Shelf
    {
        public Shelf()
        {
            this.Racks = new HashSet<Rack>();
        }

        [Key, DisplayName("Shelf ID")]
        public int shelfID { get; set; }

        [DisplayName("Shelf Name"), Required]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Please enter shelf name using 5-30 characters.")]
        public string shelf_name { get; set; }

        [DisplayName("Room Name"), Required]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Please enter room name using 5-30 characters.")]
        public string room_name { get; set; }

        public virtual ICollection<Rack> Racks { get; set; }

    }
}