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

namespace MHC_Hospital_Redesign.Controllers
{
    public class UserDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all the users in the system
        /// </summary>
        /// <returns>
        ///  HEADER: 200 OK
        /// CONTENT: all listings in the database
        /// </returns>
        /// <example>
        /// GET: api/UserData/ListApplicationUsers
        /// </example>
        [HttpGet]
        public IEnumerable<ApplicationUserDto> ListApplicationUsers()
        {
            List<ApplicationUser> ApplicationUsers = db.Users.ToList();
            List<ApplicationUserDto> ApplicationUserDtos = new List<ApplicationUserDto>();

            ApplicationUsers.ForEach(
                a => ApplicationUserDtos.Add(new ApplicationUserDto()
                {
                    UserID = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName
                }));


            return ApplicationUserDtos;
        }

        /// <summary>
        /// Gathers information about users related to a particular listing
        /// </summary>
        /// <returns>
        /// HEADER: 200 OK
        /// CONTENT: all users in the database that match to a particular listing
        /// </returns>
        /// <param name="id">Listing ID</param>
        /// <example>
        /// GET: api/UserData/ListUsersForListing/5
        /// </example>
        [HttpGet]
        public IEnumerable<ApplicationUserDto> ListUsersForListing(int id)
        {
            // all users that match with listing id
            List<ApplicationUser> Users = db.Users.Where(
                u => u.Listings.Any(
                l => l.ListID == id
                )).ToList();
            List<ApplicationUserDto> ApplicationUserDtos = new List<ApplicationUserDto>();

            Users.ForEach(a => ApplicationUserDtos.Add(new ApplicationUserDto()
            {
                UserID = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName

            }));

            return ApplicationUserDtos;
        }

        /// <summary>
        /// Returns all users in the system that are not assigned to a listing
        /// </summary>
        /// <returns>
        /// HEADER: 200 OK
        /// CONTENT: all users in the database not associated with a particular listing
        /// </returns>
        /// <param name="id">Listing ID primary key</param>
        /// <example>
        /// GET: api/UserData/ListUsersNotForListing/5
        /// </example>
        [HttpGet]
        public IEnumerable<ApplicationUserDto> ListUsersNotForListing(int id)
        {
            // all users that match with listing id
            List<ApplicationUser> Users = db.Users.Where(
                u => !u.Listings.Any(
                l => l.ListID == id
                )).ToList();
            List<ApplicationUserDto> ApplicationUserDtos = new List<ApplicationUserDto>();

            Users.ForEach(a => ApplicationUserDtos.Add(new ApplicationUserDto()
            {
                UserID = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName

            }));

            return ApplicationUserDtos;
        }

        /*
        // GET: api/UserData/5
        [ResponseType(typeof(ApplicationUser))]
        public IHttpActionResult GetApplicationUser(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return Ok(applicationUser);
        }

        // PUT: api/UserData/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutApplicationUser(string id, ApplicationUser applicationUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != applicationUser.Id)
            {
                return BadRequest();
            }

            db.Entry(applicationUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationUserExists(id))
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

        // POST: api/UserData
        [ResponseType(typeof(ApplicationUser))]
        public IHttpActionResult PostApplicationUser(ApplicationUser applicationUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(applicationUser);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ApplicationUserExists(applicationUser.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = applicationUser.Id }, applicationUser);
        }

        // DELETE: api/UserData/5
        [ResponseType(typeof(ApplicationUser))]
        public IHttpActionResult DeleteApplicationUser(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            db.Users.Remove(applicationUser);
            db.SaveChanges();

            return Ok(applicationUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ApplicationUserExists(string id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
        */

    }
}