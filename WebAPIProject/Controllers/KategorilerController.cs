using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIProject.Models;

namespace WebAPIProject.Controllers
{
    public class KategorilerController : ApiController
    {
        ETicaret2022Entities db = new ETicaret2022Entities();
        public List<Kategorim> Get()
        {

            //List<Kategori> kategoriler = (from x in db.Kategori
            //                              select new Kategori { KategoriID = x.KategoriID, KategoriAdi = x.KategoriAdi }).ToList();

            List<Kategori> kategoriler = db.Kategori.ToList();

            List<Kategorim> liste = new List<Kategorim>();
             
            foreach (var item in kategoriler)
            {
                liste.Add(new Kategorim() { KategoriID = item.KategoriID, KategoriAdi = item.KategoriAdi }) ;
            }

            return liste;
        }

        public IHttpActionResult Get(int id)
        {
            Kategori kategori = db.Kategori.Find(id);
            Kategorim kategorim = new Kategorim() { KategoriAdi = kategori.KategoriAdi, KategoriID = kategori.KategoriID };
            return Ok(kategorim);
        }
    
        public IHttpActionResult Post(Kategori kategori)
        {
            db.Kategori.Add(kategori);
            db.SaveChanges();
            return Ok();
        }

        public IHttpActionResult Put(Kategori kategori)
        {
            db.Entry(kategori).State = EntityState.Modified;
            db.SaveChanges();
            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            Kategori kategori = db.Kategori.Find(id);
            db.Kategori.Remove(kategori);
            db.SaveChanges();
            return Ok();
        }
    }
}
