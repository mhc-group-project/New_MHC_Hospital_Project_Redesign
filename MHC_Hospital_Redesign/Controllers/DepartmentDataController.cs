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
    public class DepartmentDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Returns all the Departments in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Departments in the database
        /// </returns>
        /// <example>
        /// GET: api/DepartmentData/ListDepartments
        /// </example>
        
        [ResponseType(typeof(IEnumerable<DepartmentDto>))]       
        [HttpGet]
        public IHttpActionResult ListDepartments()
        {
            List<Department> Department = db.Departments.ToList();
            List<DepartmentDto> departmentDtos = new List<DepartmentDto> { };
            foreach (var Departments in Department)
            {
                DepartmentDto Dept = new DepartmentDto
                {
                    DId = Departments.DId,
                    DepartmentName = Departments.DepartmentName,
                    PhoneNumber = Departments.PhoneNumber,
                };
                departmentDtos.Add(Dept);
            }
            
            return Ok(departmentDtos);
        }
        /// <summary>
        /// Returns all Departments in the system associated with a particular Job Listing.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Departments the database which includes a particular Department
        /// </returns>
        /// <param name="DId">Department Primary Key</param>
        /// <example>
        /// GET: api/DepartmentData/FindListingsForDepartment/1
        /// </example>
        [HttpGet]
        [ResponseType(typeof(IEnumerable<ListingDto>))]
        public IHttpActionResult FindListingsForDepartment(int id)
        {

            List<Listing> Listings = db.Listings
                .Where(l => l.DepartmentID == id)
                .ToList();

            List<ListingDto> ListingDtos = new List<ListingDto> { };

            foreach (var Listing in Listings)
            {
                ListingDto listing = new ListingDto
                {
                    ListID = Listing.ListID,
                    ListTitle = Listing.ListTitle,
                    ListDate = Listing.ListDate,
                    ListDescription = Listing.ListDescription,
                    ListRequirements = Listing.ListRequirements,
                    ListLocation = Listing.ListLocation,
                    DepartmentID = Listing.DepartmentID
                };
                ListingDtos.Add(listing);
            }
            return Ok(ListingDtos);
        }
        // GET: api/DepartmentData/2
        [ResponseType(typeof(Department))]
        [HttpGet]
        public IHttpActionResult FindDepartment(int id)
        {
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }
            DepartmentDto DepartmentDto = new DepartmentDto
            {
                DId = department.DId,
                DepartmentName = department.DepartmentName,
                PhoneNumber = department.PhoneNumber
            };
            return Ok(DepartmentDto);
        }
        /// <summary>
        /// Updates a particular Department in the system with POST Data input
        /// </summary>
        /// <param name="DId">Represents the Department ID primary key</param>
        /// <param name="Department">JSON FORM DATA of an Department</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/DepartmentData/UpdateDepartment/2
        /// FORM DATA:Department JSON Object
        /// </example>
       
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateDepartment(int id, [FromBody] Department Department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Department.DId)
            {
                return BadRequest();
            }

            db.Entry(Department).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
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
        /// Adds Department to the system
        /// </summary>
        /// <param name="Department">JSON FORM DATA of a Category</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Department ID, Department Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/DepartmentData/AddDepartment
        /// FORM DATA:Department JSON Object
        /// </example>
        [ResponseType(typeof(Department))]
        [HttpPost]
        public IHttpActionResult AddDepartment(Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Departments.Add(department);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = department.DId }, department);
        }

        // DELETE: api/DepartmentData/2
        [ResponseType(typeof(Department))]
        public IHttpActionResult DeleteDepartment(int id)
        {
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }

            db.Departments.Remove(department);
            db.SaveChanges();
            
            return Ok(department);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DepartmentExists(int id)
        {
            return db.Departments.Count(e => e.DId == id) > 0;
        }
    }
}