using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MHC_Hospital_Redesign.Models
{
    public class Department
    {
        [Key]
        public int DId { get; set; }
        [Required]
        public string DepartmentName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        //A department can have many job list
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public ICollection<Listing> ListID { get; set; }
    }
    public class DepartmentDto
    {

        public int DId { get; set; }
        [Required]
        public string DepartmentName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string UserId { get; set; }
    }
}