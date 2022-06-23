using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ETicaret2022.Models;

namespace ETicaret2022.Controllers
{
    [Authorize(Roles = "Admin")]
    public class KategoriController : Controller
    {
        private ETicaret2022Entities1 db = new ETicaret2022Entities1();
        HttpClient client = new HttpClient();
  
        public ActionResult Index()
        {
            List<Kategori> kategoriler = null;
           
            client.BaseAddress = new Uri("https://localhost:44390/api/");

            var response = client.GetAsync("Kategoriler");
            response.Wait();

            var getresult = response.Result;
            if(getresult.IsSuccessStatusCode)
            {
                var data = getresult.Content.ReadAsAsync<List<Kategori>>();
                data.Wait();
                kategoriler = data.Result;
            }

            return View(kategoriler);
        }

        // GET: Kategori/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Kategori kategori = null;

       
            client.BaseAddress = new Uri("https://localhost:44390/api/");

            var response = client.GetAsync("Kategoriler/"+id.ToString());
            response.Wait();

            var getresult = response.Result;
            if (getresult.IsSuccessStatusCode)
            {
                var data = getresult.Content.ReadAsAsync<Kategori>();
                data.Wait();
                kategori = data.Result;
            }

            //Kategori kategori = db.Kategori.Find(id);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }

        // GET: Kategori/Create
        public ActionResult Create()
        {
            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KategoriID,KategoriAdi")] Kategori kategori)
        {       

            if (ModelState.IsValid)
            {
             
                client.BaseAddress = new Uri("https://localhost:44390/api/Kategoriler");

                var result = client.PostAsJsonAsync<Kategori>("Kategoriler", kategori);
                result.Wait();

                var postResult = result.Result;
                if(postResult.IsSuccessStatusCode)
                  return RedirectToAction("Index");
            }

            return View(kategori);
        }

        // GET: Kategori/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //   Kategori kategori = db.Kategori.Find(id);

            Kategori kategori = null;

           
            client.BaseAddress = new Uri("https://localhost:44390/api/");

            var response = client.GetAsync("Kategoriler/" + id.ToString());
            response.Wait();

            var getresult = response.Result;
            if (getresult.IsSuccessStatusCode)
            {
                var data = getresult.Content.ReadAsAsync<Kategori>();
                data.Wait();
                kategori = data.Result;
            }


            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KategoriID,KategoriAdi")] Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(kategori).State = EntityState.Modified;
                //db.SaveChanges();

             
                client.BaseAddress = new Uri("https://localhost:44390/api/");

                var response = client.PutAsJsonAsync<Kategori>("Kategoriler",kategori);
                response.Wait();

                var putresult = response.Result;
                if (putresult.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                
            }
            return View(kategori);
        }

        // GET: Kategori/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Kategori kategori = db.Kategori.Find(id);

            Kategori kategori = null;

      
            client.BaseAddress = new Uri("https://localhost:44390/api/");

            var response = client.GetAsync("Kategoriler/" + id.ToString());
            response.Wait();

            var getresult = response.Result;
            if (getresult.IsSuccessStatusCode)
            {
                var data = getresult.Content.ReadAsAsync<Kategori>();
                data.Wait();
                kategori = data.Result;
            }

            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }

        // POST: Kategori/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Kategori kategori = db.Kategori.Find(id);
            //db.Kategori.Remove(kategori);
            //db.SaveChanges();

       
            client.BaseAddress = new Uri("https://localhost:44390/api/");

            var response = client.DeleteAsync("Kategoriler/" + id);


            var getresult = response.Result;
            if (getresult.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }


            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
