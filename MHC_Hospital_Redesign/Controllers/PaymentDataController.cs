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

        // GET: api/PaymentData
        public IQueryable<Payment> GetPayments()
        {
            return db.Payments;
        }

        // GET: api/PaymentData/5
        [ResponseType(typeof(Payment))]
        public IHttpActionResult GetPayment(int id)
        {
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return NotFound();
            }

            return Ok(payment);
        }

        // PUT: api/PaymentData/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPayment(int id, Payment payment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != payment.PaymentID)
            {
                return BadRequest();
            }

            db.Entry(payment).State = EntityState.Modified;

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

        // POST: api/PaymentData
        [ResponseType(typeof(Payment))]
        public IHttpActionResult PostPayment(Payment payment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Payments.Add(payment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = payment.PaymentID }, payment);
        }

        // DELETE: api/PaymentData/5
        [ResponseType(typeof(Payment))]
        public IHttpActionResult DeletePayment(int id)
        {
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return NotFound();
            }

            db.Payments.Remove(payment);
            db.SaveChanges();

            return Ok(payment);
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