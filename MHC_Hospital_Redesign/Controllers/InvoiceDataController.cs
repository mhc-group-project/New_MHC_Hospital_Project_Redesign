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
    public class InvoiceDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Invoices/ListInvoices
        /// <summary>
        ///     Returns a list of data in the invoices table within the database
        /// </summary>
        /// <returns> List of Invoices </returns>
        /// <example>
        ///     GET: api/Invoices/ListInvoices -> Invoice Object, Invoice Object...
        /// </example>
        [HttpGet]
        public IEnumerable<InvoiceDto> ListInvoices()
        {
            List<Invoice> Invoices = db.Invoices.ToList();
            List<InvoiceDto> InvoiceDtos = new List<InvoiceDto>();

            Invoices.ForEach(Invoice => InvoiceDtos.Add(new InvoiceDto()
            {
                InvoiceID = Invoice.InvoiceID,
                InvoiceNumber = Invoice.InvoiceNumber,
                InvoiceDate = Invoice.InvoiceDate,
                Amount = Invoice.Amount,
                Currency = Invoice.Currency
            }));

            return InvoiceDtos;
        }

        // GET: api/Invoices/5
        [ResponseType(typeof(Invoice))]
        public IHttpActionResult GetInvoice(int id)
        {
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return NotFound();
            }

            return Ok(invoice);
        }

        // PUT: api/Invoices/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInvoice(int id, Invoice invoice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != invoice.InvoiceID)
            {
                return BadRequest();
            }

            db.Entry(invoice).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(id))
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

        // POST: api/Invoices
        [ResponseType(typeof(Invoice))]
        public IHttpActionResult PostInvoice(Invoice invoice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Invoices.Add(invoice);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = invoice.InvoiceID }, invoice);
        }

        // DELETE: api/Invoices/5
        [ResponseType(typeof(Invoice))]
        public IHttpActionResult DeleteInvoice(int id)
        {
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return NotFound();
            }

            db.Invoices.Remove(invoice);
            db.SaveChanges();

            return Ok(invoice);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InvoiceExists(int id)
        {
            return db.Invoices.Count(e => e.InvoiceID == id) > 0;
        }
    }
}