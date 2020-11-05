using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvccrudf.Models;
using System.Net.Http;

namespace Mvccrudf.Controllers
{
    public class CrudController : Controller
    {
        // GET: Crud

        //Fetch Records from Table using Web Api
        public ActionResult Index()
        {
            IEnumerable<RegionLocationUnitsAll> regobj = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44313/api/RegionCrud");

            var consumeapi = hc.GetAsync("RegionCrud");
            consumeapi.Wait();
            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<List<RegionLocationUnitsAll>>();
                displaydata.Wait();

                regobj = displaydata.Result;
            }
            return View(regobj);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(RegionLocationUnitsAll insertreg)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44313/api/RegionCrud");

            var insertrecord = hc.PostAsJsonAsync<RegionLocationUnitsAll>("RegionCrud", insertreg);
            insertrecord.Wait();

            var savedata = insertrecord.Result;
            if (savedata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Create");
        }


        public ActionResult Edit(Guid id)
        {
            RegClass regobj = null;

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44313/api/RegionCrud");

            var consumeapi = hc.GetAsync("RegionCrud?id=" + id.ToString());
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<RegClass>();
                displaydata.Wait();
                regobj = displaydata.Result;
            }
            return View(regobj);

        }
        [HttpPost]
        public ActionResult Edit(RegClass ec)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44313/api/RegionCrud");

            var insertrecord = hc.PutAsJsonAsync<RegClass>("RegionCrud", ec);
            insertrecord.Wait();

            var savedata = insertrecord.Result;
            if (savedata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.message = " Record Not Updated ";
            }
            return View("ec");
        }

        public ActionResult Delete(Guid id)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44313/api/RegionCrud");
            var delrecord = hc.DeleteAsync("RegionCrud/" + id.ToString());
            delrecord.Wait();

            var displaydata = delrecord.Result;
            if (displaydata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Index");
        }
    }
}