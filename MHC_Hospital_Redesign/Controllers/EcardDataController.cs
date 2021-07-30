using System;
using System.IO;
using System.Web;
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
    public class EcardDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/EcardData/ListEcards
        [HttpGet]
        public IEnumerable<EcardDto> ListEcards()
        {
            List<Ecard> Ecards = db.Ecards.ToList();
            List<EcardDto> EcardDtos = new List<EcardDto>();

            Ecards.ForEach(e => EcardDtos.Add(new EcardDto()
            {
                EcardId = e.EcardId,
                Message = e.Message,
                SenderName = e.SenderName,
                PatientName = e.PatientName
            }));

            return EcardDtos;
        }


        /*

        /// <summary>
        /// Gathers information of sleected template for an ecard
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        // GET: api/EcardData/ListEcardSelectedTemplate/id
        [HttpGet]
        public IEnumerable<EcardDto> ListEcardSelectedTemplate(int id)
        {
            List<Ecard> Ecards = db.Ecards.Where(e=>e.TemplateId==id).ToList();
            List<EcardDto> EcardDtos = new List<EcardDto>();

            Ecards.ForEach(e => EcardDtos.Add(new EcardDto()
            {
                EcardId = e.EcardId,
                Message = e.Message,
                SenderName = e.SenderName,
                PatientName = e.PatientName
            }));

            return EcardDtos;
        }

        */

        // GET: api/EcardData/FindEcard/3
        [ResponseType(typeof(Ecard))]
        [HttpGet]
        public IHttpActionResult FindEcard(int id)
        {
            Ecard Ecard = db.Ecards.Find(id);
            EcardDto EcardDto = new EcardDto()
            {
                EcardId = Ecard.EcardId,
                Message = Ecard.Message,
                SenderName = Ecard.SenderName,
                PatientName = Ecard.PatientName
            };

            if (Ecard == null)
            {
                return NotFound();
            }

            return Ok(EcardDto);
        }

        // POST: api/EcardData/UpdateEcard/3
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateEcard(int id, Ecard ecard)
        {
            Debug.WriteLine("I have reached the update ecard method!");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }

            if (id != ecard.EcardId)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET paramter" + id);
                Debug.WriteLine("POST paramter" + ecard.EcardId);
                return BadRequest();
            }

            db.Entry(ecard).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EcardExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            Debug.WriteLine("None of the conditions trigger");
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/EcardData/AddEcard
        [ResponseType(typeof(Ecard))]
        [HttpPost]
        public IHttpActionResult AddEcard(Ecard ecard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ecards.Add(ecard);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ecard.EcardId }, ecard);
        }

        // DELETE: api/EcardData/DeleteEcard/3
        [ResponseType(typeof(Ecard))]
        [HttpPost]
        public IHttpActionResult DeleteEcard(int id)
        {
            Ecard ecard = db.Ecards.Find(id);
            if (ecard == null)
            {
                return NotFound();
            }

            db.Ecards.Remove(ecard);
            db.SaveChanges();

            return Ok(ecard);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EcardExists(int id)
        {
            return db.Ecards.Count(e => e.EcardId == id) > 0;
        }
    }
}