using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using MHC_Hospital_Redesign.Models;
using System.Web.Script.Serialization;
using MHC_Hospital_Redesign.Models.ViewModels;

namespace MHC_Hospital_Redesign.Controllers
{
    public class EcardController : Controller
    {

        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static EcardController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44338/api/");
        }

        // GET: Ecard/List
        public ActionResult List()
        {
            //goal: to communicate with template data api to retrieve a list of ecards
            //curl https://localhost:44338/api/templatedata/listecards


            string url = "ecarddata/listecards";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<EcardDto> ecards = response.Content.ReadAsAsync<IEnumerable<EcardDto>>().Result;
            Debug.WriteLine("Recieived #" + ecards.Count() + " ecards");

            return View(ecards);
        }

        // GET: Ecard/Details/5
        public ActionResult Details(int id)
        {
    


            string url = "ecarddata/findecard/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            EcardDto selectedecard = response.Content.ReadAsAsync<EcardDto>().Result;
            Debug.WriteLine("ecard recieved ID:" + selectedecard.EcardId);

            return View(selectedecard);
        }

        public ActionResult Error()
        {
            return View();
        }


        // GET: Ecard/New
        public ActionResult New()
        {
            string url = "templatedata/listtemplates";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<TemplateDto> TemplateOptions = response.Content.ReadAsAsync<IEnumerable<TemplateDto>>().Result;

            return View(TemplateOptions); ;
        }

        // POST: Ecard/Create
        [HttpPost]
        public ActionResult Create(Ecard ecard)
        {
            Debug.WriteLine(ecard.EcardId);
            //objective: add new template into our sstem using the API
            //curl -H "Content-type:application/json" -d@ecard.json https://localhost:44338/api/ecarddata/addecard
            string url = "ecarddata/addecard";

            string jsonpayload = jss.Serialize(ecard);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }

        }

        // GET: Ecard/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateEcard ViewModel = new UpdateEcard();

            string url = "ecarddata/findecard/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            EcardDto SelectedEcard = response.Content.ReadAsAsync<EcardDto>().Result;
            ViewModel.SelectedEcard = SelectedEcard;

             url = "templatedata/listtemplates/";
            response = client.GetAsync(url).Result;
            IEnumerable<TemplateDto> TemplateOptions = response.Content.ReadAsAsync<IEnumerable<TemplateDto>>().Result;

            ViewModel.TemplateOptions = TemplateOptions;

            return View(ViewModel);
        }

        // POST: Ecard/Edit/5
        [HttpPost]
        public ActionResult Update(int id, Ecard ecard)
        {
            string url = "ecarddata/updateecard/" + id;

            string jsonpayload = jss.Serialize(ecard);

            HttpContent content = new StringContent(jsonpayload);

            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);
      
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Ecard/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "ecarddata/findecard/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            EcardDto selectedspecies = response.Content.ReadAsAsync<EcardDto>().Result;
            return View(selectedspecies);
        }

        // POST: Ecard/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "ecarddata/deleteecard/" + id;
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
