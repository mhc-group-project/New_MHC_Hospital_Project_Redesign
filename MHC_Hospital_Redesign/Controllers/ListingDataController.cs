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
using System.Web;
using System.IO;

namespace MHC_Hospital_Redesign.Controllers
{
    public class ListingDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all the listings in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 OK
        /// CONTENT: all listings in the database
        /// </returns>
        /// <example>
        /// GET: api/ListingData/ListListings
        /// </example>
        [HttpGet]
        public IEnumerable<ListingDto> ListListings()
        {
            List<Listing> Listings = db.Listings.ToList();
            List<ListingDto> ListingDtos = new List<ListingDto>();

            Listings.ForEach(a => ListingDtos.Add(new ListingDto()
            {
                ListID = a.ListID,
                ListTitle = a.ListTitle,
                ListDate = a.ListDate,
                ListDescription = a.ListDescription,
                ListRequirements = a.ListRequirements,
                ListLocation = a.ListLocation,
                DepartmentID = a.DepartmentID

            }));

            return ListingDtos;
        }

        /// <summary>
        /// Returns a listing in the system
        /// </summary>
        /// <returns>
        /// HEADER: 200 OK
        /// CONTENT: a listing in the system with a matching list id (primary key)
        /// </returns>
        /// <param name="id">Primary key of a listing</param>
        /// <example>
        /// GET: api/ListingData/FindListing/5
        /// </example>
        [ResponseType(typeof(Listing))]
        [HttpGet]
        public IHttpActionResult FindListing(int id)
        {

            Listing Listing = db.Listings.Find(id);
            ListingDto ListingDto = new ListingDto()
            {
                ListID = Listing.ListID,
                ListTitle = Listing.ListTitle,
                ListDate = Listing.ListDate,
                ListDescription = Listing.ListDescription,
                ListRequirements = Listing.ListRequirements,
                ListLocation = Listing.ListLocation,
                DepartmentID = Listing.DepartmentID

            };

            if (Listing == null)
            {
                return NotFound();
            }

            return Ok(ListingDto);
        }

        /// <summary>
        /// Updates a particular listing in the system with POST data input
        /// </summary>
        /// <param name="id">Listing ID primary key</param>
        /// <param name="listing">JSON form data of a listing</param>
        /// <returns>
        /// HEADER: 204 Success, no content response
        /// or
        /// HEADER: 400 Bad Request
        /// or
        /// HEADER: 404 Not Found
        /// </returns>
        /// <example>
        /// POST: api/ListingData/UpdateListing/5
        /// FORM DATA: Listing JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateListing(int id, Listing listing)
        {
            Debug.WriteLine("This is the update listing method");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model state is invalid");
                return BadRequest(ModelState);
            }

            if (id != listing.ListID)
            {
                Debug.WriteLine("ID does not match");
                Debug.WriteLine("GET parameter" +id);
                Debug.WriteLine("POST parameter" + listing.ListID);
                Debug.WriteLine("POST parameter" + listing.ListTitle);
                Debug.WriteLine("POST parameter" + listing.ListLocation);
                return BadRequest();
            }

            db.Entry(listing).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListingExists(id))
                {
                    Debug.WriteLine("Listing not found");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            Debug.WriteLine("None of the conditions triggered");
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Adds a listing to the system
        /// </summary>
        /// <param name="listing">JSON Form Data of a listing</param>
        /// <returns>
        /// HEADER: 204 Success, no content response
        /// or
        /// HEADER: 400 Bad Request
        /// or
        /// HEADER: 404 Not Found
        /// </returns>
        /// <example>
        /// POST: api/ListingData/AddListing
        /// </example>
        [ResponseType(typeof(Listing))]
        [HttpPost]
        public IHttpActionResult AddListing(Listing listing)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Listings.Add(listing);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = listing.ListID }, listing);
        }

        /// <summary>
        /// Deletes a listing from the system by its ID
        /// </summary>
        /// <param name="id">List ID primary key</param>
        /// <returns>
        /// HEADER: 204 Success, no content response
        /// or
        /// HEADER: 404 Not Found
        /// </returns>
        /// <example>
        /// POST: api/ListingData/DeleteListing/5
        /// FORM DATA: empty
        /// </example>
        [ResponseType(typeof(Listing))]
        [HttpPost]
        public IHttpActionResult DeleteListing(int id)
        {
            Listing listing = db.Listings.Find(id);
            if (listing == null)
            {
                return NotFound();
            }

            db.Listings.Remove(listing);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ListingExists(int id)
        {
            return db.Listings.Count(e => e.ListID == id) > 0;
        }
    }
}