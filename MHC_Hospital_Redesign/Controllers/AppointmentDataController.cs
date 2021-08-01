using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MHC_Hospital_Redesign.Models;
using System.Diagnostics;

namespace MHC_Hospital_Redesign.Controllers
{
    public class AppointmentDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// fetch the list of appointments in the database
        /// </summary>
        /// <param name="searchStatus">appointment status to search for</param>
        /// <returns>The list of appointments available in the database</returns>
        /// <example>GET: api/AppointmentData/GetAppointments?searchStatus=all</example>
        [ResponseType(typeof(IEnumerable<Appointment>))]
        public IHttpActionResult GetAppointments(string searchStatus = "all")
        {
            if (string.Equals(searchStatus, "all"))
                return Ok(db.Appointments.ToList());

            int status = Int32.Parse(searchStatus);
            return Ok(db.Appointments.OrderByDescending(a => a.DateTime).ToList());

        }
        /// <summary>
        /// It'll return a list of appointments in the database alongside a status code (200 OK). Skips the first {startindex} records and takes {nberPerPage} records.
        /// </summary>
        /// <param name="startIndex">The number of records to skip through</param>
        /// <param name="nberPerPage"> The number of records for each</param>
        /// <param name="searchStatus">appointment status to search for</param>
        /// <returns> a list of appointements</returns>
        /// <example>GET: api/AppointmentData/GetAppointmentsPage/5/10?searchStatus=all</example>
        [ResponseType(typeof(IEnumerable<Appointment>))]
        [Route("api/AppointmentData/GetAppointmentsPage/{startIndex}/{nberPerPage}")]

        public IHttpActionResult GetAppointmentsPage(int startIndex, int nberPerPage, string searchStatus = "all")
        {
            if (string.Equals(searchStatus, "all"))
                return Ok(db.Appointments.OrderByDescending(a => a.DateTime).Skip(startIndex).Take(nberPerPage).ToList());
            int status = Int32.Parse(searchStatus);
            return Ok(db.Appointments.OrderByDescending(a => a.DateTime).Skip(startIndex).Take(nberPerPage).ToList());
        }


        /// <summary>
        /// gets a user's list of appointments in the database
        /// </summary>
        /// <param name="userId">a user id</param>
        /// <param name="searchStatus">appointment status to search for</param>
        /// <returns>The list of a user's appointments in the database</returns>
        /// <example>GET: api/AppointmentData/FindUserAppointments/4?searchStatus=all</example>
        [ResponseType(typeof(IEnumerable<Appointment>))]
        [Route("api/AppointmentData/FindUserAppointments/{userId}")]
        [HttpGet]
        public IHttpActionResult FindUserAppointments(string userId, string searchStatus = "all")
        {
            if (string.Equals(searchStatus, "all"))
                return Ok(db.Appointments.Where(a => a.PatientId == userId || a.DoctorId == userId).OrderByDescending(a => a.DateTime).ToList());
            int status = Int32.Parse(searchStatus);
            return Ok(db.Appointments.Where(a => (a.PatientId == userId || a.DoctorId == userId)).OrderByDescending(a => a.DateTime).ToList());
        }
        /// <summary>
        /// Gets a list of appointments associated with a user in the database alongside a status code (200 OK). Skips the first {startindex} records and takes {nberPerPage} records.
        /// </summary>
        /// <param name="userId">a user id</param>
        /// <param name="searchStatus">appointment status to search for</param>
        /// <param name="startIndex">The number of records to skip through</param>
        /// <param name="nberPerPage"> The number of records for each</param>
        /// <returns> a list of appointements</returns>
        /// <example>GET: api/AppointmentData/FindUserAppointmentsPage/{userId}/{startIndex}/{nberPerPage}?searchStatus=all</example>
        [HttpGet]
        [Route("api/AppointmentData/FindUserAppointmentsPage/{userId}/{startIndex}/{nberPerPage}")]
        public IHttpActionResult FindUserAppointmentsPage(string userId, int startIndex, int nberPerPage, string searchStatus = "all")
        {
            if (string.Equals(searchStatus, "all"))
                return Ok(db.Appointments.Where(a => a.PatientId == userId || a.DoctorId == userId).OrderByDescending(a => a.DateTime)
                .Skip(startIndex).Take(nberPerPage).ToList());

            int status = Int32.Parse(searchStatus);
            return Ok(db.Appointments.Where(a => (a.PatientId == userId || a.DoctorId == userId)).OrderByDescending(a => a.DateTime)
                .Skip(startIndex).Take(nberPerPage).ToList());
        }

        // GET: api/AppointmentData/2
        /// <summary>
        /// gets an appointment associated with a prodided id
        /// </summary>
        /// <param name="id"> an appointment id</param>
        /// <returns> an appoinment associated with the id provided</returns>
        /// <example>GET: api/AppointmentData/GetAppointment/8</example>
        [ResponseType(typeof(Appointment))]
        public IHttpActionResult GetAppointment(int id)
        {
            Debug.WriteLine(id);
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                Debug.WriteLine("nf");
                return NotFound();
            }

            return Ok(appointment);
        }


        /// <summary>
        /// updates an appointmemnt in the database
        /// </summary>
        /// <param name="id">an appointment id</param>
        /// <param name="appointment">the appointment to update</param>
        /// <returns>status code 200 if successful.</returns>
        /// <example>POST: api/AppointmentData/UpdateAppointment/5</example>
        [ResponseType(typeof(void))]
        [HttpPost]
        [Authorize(Roles = "Patient,Doctor")]

        public IHttpActionResult UpdateAppointment(int id, Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != appointment.AId)
            {
                return BadRequest();
            }

            db.Entry(appointment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        /// <summary>
        /// adds a new appointment in the  database
        /// </summary>
        /// <param name="appointment"> a new appointment</param>
        /// <returns> the newly added appointment's id</returns>
        /// <example>POST: api/AppointmentData/AddAppointment</example>
        [ResponseType(typeof(Appointment))]
        [HttpPost]
        [Authorize(Roles = "Patient,Doctor")]

        public IHttpActionResult AddAppointment(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Appointments.Add(appointment);
            db.SaveChanges();

            return Ok(appointment.AId);
        }


        /// <summary>
        /// deletes an appointment in the database
        /// </summary>
        /// <param name="id"> an appointment id</param>
        /// <returns>status code 200 if successful.</returns>
        /// <example>DELETE: api/AppointmentData/DeleteAppointment/5</example>
        [ResponseType(typeof(Appointment))]
        [HttpPost]
        [Authorize(Roles = "Patient,Doctor")]

        public IHttpActionResult DeleteAppointment(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return NotFound();
            }

            db.Appointments.Remove(appointment);
            db.SaveChanges();

            return Ok(appointment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AppointmentExists(int id)
        {
            return db.Appointments.Count(e => e.AId == id) > 0;
        }



    }
}