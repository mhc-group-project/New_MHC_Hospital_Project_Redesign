using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using MHC_Hospital_Redesign.Models;
using System.Web.Script.Serialization;

namespace MHC_Hospital_Redesign.Controllers
{

   
    public class TemplateController : Controller

    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static TemplateController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44338/api/");
        }


        // GET: Template/List
        public ActionResult List()
        {
            //goal: to communicate with template data api to retrieve a list of templates
            //curl https://localhost:44338/api/templatedata/listtemplates


            string url = "templatedata/listtemplates";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<TemplateDto> templates = response.Content.ReadAsAsync<IEnumerable<TemplateDto>>().Result;
            Debug.WriteLine("Recieived #" + templates.Count() + " templates");

            return View(templates);
        }

        // GET: Template/Details/5
        public ActionResult Details(int id)
        {
            

            //goal: to communicate with template data api to retrieve a one template
            //curl https://localhost:44338/api/templatedata/findtemplate/{id}


            string url = "templatedata/findtemplate/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            TemplateDto selectedtemplate =  response.Content.ReadAsAsync<TemplateDto>().Result;
            Debug.WriteLine("template recieved " + selectedtemplate.TemplateName);

           

            return View(selectedtemplate);


        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Template/New
        public ActionResult New()
        {
            string url = "ecarddata/listecards";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<EcardDto> EcardOptions = response.Content.ReadAsAsync<IEnumerable<EcardDto>>().Result;

            return View(EcardOptions);
        }

        // POST: Template/Create
        [HttpPost]
        public ActionResult Create(Template template)
        {
            Debug.WriteLine(template.TemplateName);
            //objective: add new template into our sstem using the API
            //curl -H "Content-type:application/json" -d@template.json https://localhost:44338/api/templatedata/addtemplate
            string url = "templatedata/addtemplate";

            string jsonpayload = jss.Serialize(template);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

           HttpResponseMessage response =  client.PostAsync(url, content).Result;
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            } else
            {
                return RedirectToAction("Error");
            }


    
        }

        // GET: Template/Edit/5
        public ActionResult Edit(int id)
        {

            string url = "templatedata/findtemplate/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            TemplateDto selectedtemplate = response.Content.ReadAsAsync<TemplateDto>().Result;
            return View(selectedtemplate);
        } 

        // POST: Template/Update/5
        [HttpPost]
        public ActionResult Update(int id, Template template, HttpPostedFileBase TemplatePic)
        {
            string url = "templatedata/updatetemplate/" + id;

            string jsonpayload = jss.Serialize(template);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);


            // update request is succesful, and image data is recieved
            if (response.IsSuccessStatusCode && TemplatePic != null)
            {
                //Updating the template picture as a seperate request
                Debug.WriteLine("Calling Update Image method");
                //Send over image data
                url = "TemplateData/UploadTemplatePic/" + id;
                Debug.WriteLine("Recieved template picture " + TemplatePic.FileName);

                MultipartFormDataContent requestcontent = new MultipartFormDataContent();
                HttpContent imagecontent = new StreamContent(TemplatePic.InputStream);
                requestcontent.Add(imagecontent, "TemplatePic", TemplatePic.FileName);
                response = client.PostAsync(url, requestcontent).Result;

                return RedirectToAction("List");
            }

            else if (response.IsSuccessStatusCode)
            {
                //No image uploaded, but the update was still succesful
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Errors");
            }
        }

        // GET: Template/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "templatedata/findtemplate/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            TemplateDto selectedtemplate = response.Content.ReadAsAsync<TemplateDto>().Result;
            return View(selectedtemplate);
        }

        // POST: Template/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "templatedata/deletetemplate/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);

            if (response.IsSuccessStatusCode)
            {
                // TODO: Add delete logic here

                return RedirectToAction("List");
            }
            else
            {
                return View("Error");
            }
        }
    }
}
