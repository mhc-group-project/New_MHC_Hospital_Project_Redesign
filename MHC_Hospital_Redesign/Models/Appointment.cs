using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace MHC_Hospital_Redesign.Models
{
    public class Appointment
    {
        [Key]
        public int AId { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public string DateTime { get; set; }
        [Required]
        public string Status { get; set; }


        [ForeignKey("PatientUser")]
        public string PatientId { get; set; }
        public virtual ApplicationUser PatientUser { get; set; }


        [ForeignKey("DoctorUser")]
        public string DoctorId { get; set; }
        public virtual ApplicationUser DoctorUser { get; set; }

    }


}