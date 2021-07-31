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

        // GET: api/Invoices/FindInvoice/5
        /// <summary>
        ///     Returns the data of a specific Invoice based on the Invoice ID
        /// </summary>
        /// <param name="id"> Invoice ID </param>
        /// <returns>
        ///     HEADER: 200 (OK)
        ///     CONTENT: A Invoice related to Invoice ID
        ///     or
        ///     HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        ///     GET: api/Invoices/FindInvoice/5 -> 
        /// </example>
        [HttpGet]
        [ResponseType(typeof(Invoice))]
        public IHttpActionResult FindInvoice(int id)
        {
            Invoice Invoice = db.Invoices.Find(id);
            InvoiceDto InvoiceDto = new InvoiceDto()
            {
                InvoiceID = Invoice.InvoiceID,
                InvoiceNumber = Invoice.InvoiceNumber,
                InvoiceDate = Invoice.InvoiceDate,
                Amount = Invoice.Amount,
                Currency = Invoice.Currency
            };

            if (Invoice == null)
            {
                return NotFound();
            }

            return Ok(Invoice);
        }

        // POST: api/Invoices/UpdateInvoice/5
        /// <summary>
        ///     Updates an Invoice's information in the invoices table within the database
        /// </summary>
        /// <param name="id"> Invoice ID </param>
        /// <param name="Invoice"> json Form data of an Invoice </param>
        /// <returns>
        ///     HEADER: 200 (Success, No content)
        ///     or
        ///     HEADER: 400 (BAD REQUEST)
        ///     or
        ///     HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        ///     POST: api/Invoices/UpdateInvoice/5
        /// </example>
        [HttpPost]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateInvoice(int id, Invoice Invoice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Invoice.InvoiceID)
            {
                return BadRequest();
            }

            db.Entry(Invoice).State = EntityState.Modified;

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

        // POST: api/Invoices/AddInvoice
        /// <summary>
        ///     Adds a new Invoice to the Invoices table within the database
        /// </summary>
        /// <param name="Invoice"> json Form Data of a Invoice </param>
        /// <returns>
        ///     HEADER: 201 (Created)
        ///     CONTENT: Invoice data
        ///     or
        ///     HEADER: 400 (BAD REQUEST)
        /// </returns>
        /// <example>
        ///     POST: api/Invoices/AddInvoice
        /// </example>
        [HttpPost]
        [ResponseType(typeof(Invoice))]
        public IHttpActionResult AddInvoice(Invoice Invoice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Invoices.Add(Invoice);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Invoice.InvoiceID }, Invoice);
        }

        // POST: api/Invoices/DeleteInvoice/5
        /// <summary>
        ///     Deletes an Invoice from the Invoices table within the database
        /// </summary>
        /// <param name="id"> Invoice ID </param>
        /// <returns>
        ///     HEADER: 200 (OK)
        ///     or
        ///     HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        ///     POST: api/Invoices/DeleteInvoice/5
        /// </example>
        [HttpPost]
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