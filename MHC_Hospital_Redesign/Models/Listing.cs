using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MHC_Hospital_Redesign.Models
{
    public class Listing
    {
        [Key]
        public int ListID { get; set; }
        public string ListTitle { get; set; }
        public DateTime ListDate { get; set; }
        public string ListDescription { get; set; }
        public string ListRequirements { get; set; }
        public string ListLocation { get; set; }
        // 1-M relationship with Department
        // a listing can belong to a department, a department can have many listings
        [ForeignKey("Department")]
        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; }

        // M-M relationship with Volunteer Users
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
    public class ListingDto
    {
        public int ListID { get; set; }
        public string ListTitle { get; set; }
        public DateTime ListDate { get; set; }
        public string ListDescription { get; set; }
        public string ListRequirements { get; set; }
        public string ListLocation { get; set; }
        public int DepartmentID { get; set; } //foreign key
    }
}