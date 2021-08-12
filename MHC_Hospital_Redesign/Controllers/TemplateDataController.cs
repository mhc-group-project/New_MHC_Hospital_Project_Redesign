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
    public class TemplateDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Retruns all templates in the system
        /// </summary>
        /// <returns>
        /// Header: 200 (OK)
        /// Content: all templates in the db
        /// </returns>
        /// <example>
        /// GET: api/TemplateData/ListTemplates 
        /// </example>
    
        [HttpGet]
        public IEnumerable<TemplateDto> ListTemplates()
        {
            List<Template> Templates =  db.Templates.ToList();
            List<TemplateDto> TemplateDtos = new List<TemplateDto>();

            Templates.ForEach(t => TemplateDtos.Add(new TemplateDto()
            {
                TemplateId = t.TemplateId,
                TemplateName = t.TemplateName,
                TemplateHasPic = t.TemplateHasPic,
                TemplatePicExtension = t.TemplatePicExtension,
                TemplateStyle = t.TemplateStyle


            }));

            return TemplateDtos;
        }

        /// <summary>
        /// Returns all templates in the system
        /// </summary>
        /// <param name="id">Primary key of a template</param>
        /// <returns><
        /// HEADER: 200 (OK)
        /// CONTENT: A template in the system matching up to the template id primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// /returns>
        /// <example>
        /// GET: api/TemplateData/FindTemplate/3
        /// </example>

        [ResponseType(typeof(Template))]
        [HttpGet]
        public IHttpActionResult FindTemplate(int id)
        {
            Template Template = db.Templates.Find(id);
            TemplateDto TemplateDto = new TemplateDto()
            {
                TemplateId = Template.TemplateId,
                TemplateName = Template.TemplateName,
                TemplatePicExtension = Template.TemplatePicExtension,
                TemplateHasPic = Template.TemplateHasPic,
                TemplateStyle = Template.TemplateStyle
            };

            if (Template == null)
            {
                return NotFound();
            }

            return Ok(TemplateDto);
        }
        /// <summary>
        /// Updates a particular template in the system with POST data input
        /// </summary>
        /// <param name="id">Represents the template id primary key</param>
        /// <param name="template">JSON form data of a template</param>
        /// <returns>
        /// Header: 204 (Success, No content response)
        /// or
        /// Header: 400 (Bad Request)
        /// or
        /// Header: 404 (Not found)
        /// </returns>
        /// <example>
        /// POST: api/TemplateData/UpdateTemplate/5
        /// </example>
        /// 
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateTemplate(int id, Template template)
        {
            Debug.WriteLine("reached update method");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }

            if (id != template.TemplateId)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET: " + id);
                Debug.WriteLine("POST: " + template.TemplateId);
                return BadRequest();
            }

            db.Entry(template).State = EntityState.Modified;

            db.Entry(template).Property(t => t.TemplateHasPic).IsModified = false;
            db.Entry(template).Property(t => t.TemplatePicExtension).IsModified = false;


            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemplateExists(id))
                {
                    Debug.WriteLine("Template not found");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            Debug.WriteLine("No conditions triggered");
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Recieves template picture data, uploads it to the web server and updates the template's HasPic option
        /// </summary>
        /// <param name="id">Template id</param>
        /// <returns>status code 200 if successful</returns>
        /// <example>
        /// POST: api/TemplateData/UpdateTemplatePic/5
        /// </example>

        [HttpPost]
        public IHttpActionResult UploadTemplatePic(int id)
        {
            bool haspic = false;
            string picextension;
            if (Request.Content.IsMimeMultipartContent())
            {
                Debug.WriteLine("recieved multipart form data.");

                int numfiles = HttpContext.Current.Request.Files.Count;
                Debug.WriteLine("Files Recieved: " + numfiles);

                //Check if file is posted

                if (numfiles == 1 && HttpContext.Current.Request.Files[0] != null)
                {
                    var templatepic = HttpContext.Current.Request.Files[0];
                    //Check if files is empty
                    if (templatepic.ContentLength > 0)
                    {
                        //establish valid file types
                        var valtypes = new[] { "jpeg", "jpg", "png", "PNG" };
                        var extension = Path.GetExtension(templatepic.FileName).Substring(1);
                        //Check the extension of the file

                        if(valtypes.Contains(extension))
                        {
                            try
                            {
                                //file name is the id of the image
                                string fn = id + "." + extension;

                                //get a direct file path to ~/Content/template{id}.{extension}
                                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/TemplatePics/"), fn);

                                //save file

                                templatepic.SaveAs(path);

                                //if successful then we can set fields

                                haspic = true;
                                Debug.WriteLine(haspic);
                                picextension = extension;

                                Template SelectedTemplate = db.Templates.Find(id);
                                SelectedTemplate.TemplateHasPic = haspic;
                                SelectedTemplate.TemplatePicExtension = extension;
                                db.Entry(SelectedTemplate).State = EntityState.Modified;

                                db.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine("template image was not saved successfully");
                                Debug.WriteLine("Exception: " + ex);
                                return BadRequest();
                            }
                        }
                    }
                }
                return Ok();
            }
            else
            {
                //not multipart form data
                return BadRequest();
            }
        }

    
    
       
        /// <summary>
        /// Add a template to the system
        /// </summary>
        /// <param name="template">JSON form data of a template</param>
        /// <returns>
        /// Header: 201 (Created)
        /// Content: Template ID, Template Data
        /// </returns>
        /// <example>
        /// POST: api/TemplateData/AddTemplate
        /// FORM DATA: Template JSON Object
        /// </example>

        
        [ResponseType(typeof(Template))]
        [HttpPost]
        public IHttpActionResult AddTemplate(Template template)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Templates.Add(template);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = template.TemplateId }, template);
        }

        /// <summary>
        /// Delete a template from the system by it's ID
        /// </summary>
        /// <param name="id">Primary key of a template</param>
        /// <returns>
        /// Header: 200 (OK)
        /// or 
        /// Header: 404 (Not found)
        /// </returns>
        /// <example>
        /// POST: api/TemplateData/DeleteTemplate/3
        /// Form data: (empty)
        /// </example>

        [ResponseType(typeof(Template))]
        [HttpPost]
        public IHttpActionResult DeleteTemplate(int id)
        {
            Template template = db.Templates.Find(id);
            if (template == null)
            {
                return NotFound();
            }

            db.Templates.Remove(template);
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

        private bool TemplateExists(int id)
        {
            return db.Templates.Count(e => e.TemplateId == id) > 0;
        }
    }
}