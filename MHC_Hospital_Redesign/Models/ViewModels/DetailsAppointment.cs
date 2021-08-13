using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MHC_Hospital_Redesign.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

using static MHC_Hospital_Redesign.Models.Appointment;

namespace MHC_Hospital_Redesign.Models.ViewModels
{
    public class CreateAppointment
    {
        [Required]
        [DisplayName("Patient:")]
        public string PatientId { get; set; }

        [Required]
        [DisplayName("Doctor:")]
        public string DoctorId { get; set; }

        [Required]
        public string DateTime { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
        public IEnumerable<ApplicationUser> UsersInRole { get; set; }
    }

    public class UpdateAppointment
    {
        [Required]
        public int AId { get; set; }
        [Required]
        [DisplayName("Patient:")]
        public string PatientId { get; set; }
        public virtual ApplicationUser PatientUser { get; set; }

        [Required]
        [DisplayName("Doctor:")]
        public string DoctorId { get; set; }
        public virtual ApplicationUser DoctorUser { get; set; }

        [Required]
        public string DateTime { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public AppointmentStatus Status { get; set; }
        [Required]
        public IEnumerable<ApplicationUser> UsersInRole { get; set; }
    }
}