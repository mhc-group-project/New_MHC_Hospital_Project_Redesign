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
    public class PaymentDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PaymentData/ListPayments
        /// <summary>
        ///     Returns a list of data in the payments table within the database
        /// </summary>
        /// <returns> List of Payments </returns>
        /// <example>
        ///     GET: api/PaymentData/ListPayments -> Payment Object, Payment Object...
        /// </example>
        [HttpGet]
        public IEnumerable<PaymentDto> ListPayments()
        {
            List<Payment> Payments = db.Payments.ToList();
            List<PaymentDto> PaymentDtos = new List<PaymentDto>();

            Payments.ForEach(Payment => PaymentDtos.Add(new PaymentDto()
            {
                PaymentID = Payment.PaymentID,
                NameOnCard = Payment.NameOnCard,
                Address = Payment.Address,
                City = Payment.City,
                PostalCode = Payment.PostalCode,
                Province = Payment.Province,
                Country = Payment.Country
            }));

            return PaymentDtos;
        }

        // GET: api/PaymentData/FindPayment/5
        /// <summary>
        ///     Returns the data of a specific Payment based on the Payment ID
        /// </summary>
        /// <param name="id"> Payment ID </param>
        /// <returns>
        ///     HEADER: 200 (OK)
        ///     CONTENT: A Payment related to Payment ID
        ///     or
        ///     HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        ///     GET: api/PaymentData/FindPayment/5
        /// </example>
        [HttpGet]
        [ResponseType(typeof(Payment))]
        public IHttpActionResult FindPayment(int id)
        {
            Payment Payment = db.Payments.Find(id);
            PaymentDto PaymentDto = new PaymentDto()
            {
                PaymentID = Payment.PaymentID,
                NameOnCard = Payment.NameOnCard,
                Address = Payment.Address,
                City = Payment.City,
                PostalCode = Payment.PostalCode,
                Province = Payment.Province,
                Country = Payment.Country
            };

            if (Payment == null)
            {
                return NotFound();
            }

            return Ok(Payment);
        }

        // POST: api/PaymentData/UpdatePayment/5
        /// <summary>
        ///     Updates a Payment's information in the payments table within the database
        /// </summary>
        /// <param name="id"> Payment ID </param>
        /// <param name="Payment"> json Form data of a Payment </param>
        /// <returns>
        ///     HEADER: 200 (Success, No content)
        ///     or
        ///     HEADER: 400 (BAD REQUEST)
        ///     or
        ///     HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        ///     POST: api/PaymentData/UpdatePayment/5
        /// </example>
        [HttpPost]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdatePayment(int id, Payment Payment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Payment.PaymentID)
            {
                return BadRequest();
            }

            db.Entry(Payment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(id))
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

        // POST: api/PaymentData/AddPayment
        /// <summary>
        ///     Adds a new Payment to the payments table within the database
        /// </summary>
        /// <param name="Payment"> json Form data of a Payment </param>
        /// <returns>
        ///     HEADER: 201 (Created)
        ///     CONTENT: Category data
        ///     or
        ///     HEADER: 400 (BAD REQUEST)
        /// </returns>
        /// <example>
        ///     POST: api/PaymentData/AddPayment
        /// </example>
        [HttpPost]
        [ResponseType(typeof(Payment))]
        public IHttpActionResult AddPayment(Payment Payment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Payments.Add(Payment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Payment.PaymentID }, Payment);
        }

        // POST: api/PaymentData/DeletePayment/5
        /// <summary>
        ///     Deletes a Payment from the payments table within the database
        /// </summary>
        /// <param name="id"> Payment ID </param>
        /// <returns>
        ///     HEADER: 200 (OK)
        ///     or
        ///     HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        ///     POST: api/PaymentData/DeletePayment/5
        /// </example>
        [HttpPost]
        [ResponseType(typeof(Payment))]
        public IHttpActionResult DeletePayment(int id)
        {
            Payment Payment = db.Payments.Find(id);
            if (Payment == null)
            {
                return NotFound();
            }

            db.Payments.Remove(Payment);
            db.SaveChanges();

            return Ok(Payment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PaymentExists(int id)
        {
            return db.Payments.Count(e => e.PaymentID == id) > 0;
        }
    }
}