using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ETicaret2022.Models;
using Microsoft.AspNet.Identity;

namespace ETicaret2022.Controllers
{
    [Authorize]
    public class SepetController : Controller
    {

        ETicaret2022Entities1 db = new ETicaret2022Entities1();
        public ActionResult SepeteEkle(int? adet,int id)
        {
            Urunler urun = db.Urunlers.Find(id);

            string userID = User.Identity.GetUserId();

            Sepet sepettekiurun = db.Sepets.FirstOrDefault(x => x.UrunID == id && x.UserID == userID);
            

            if(sepettekiurun==null)
            {
                Sepet yeniurun = new Sepet()
                {
                    Adet = adet??1,
                    UrunID=id,
                    ToplamTutar=urun.UrunFiyati*(adet??1),
                    UserID=userID                     
                };
                db.Sepets.Add(yeniurun);              
            }
            else
            {
                sepettekiurun.Adet = sepettekiurun.Adet + (adet??1);
                sepettekiurun.ToplamTutar = sepettekiurun.Adet * urun.UrunFiyati;
            }
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    
        public ActionResult Index()
        {
            string userID = User.Identity.GetUserId();
           
            var sepet = db.Sepets.Where(x => x.UserID == userID).Include(u => u.Urunler);

            return View(sepet.ToList());
        }

        public ActionResult SepetGuncelle(int? adet,int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Sepet sepet = db.Sepets.Find(id);

            if(sepet==null)
            {
                return HttpNotFound();
            }

            Urunler urun = db.Urunlers.Find(sepet.UrunID);

            sepet.Adet = adet ?? 1;
            sepet.ToplamTutar = sepet.Adet * urun.UrunFiyati;
            db.SaveChanges();

            return RedirectToAction("Index");

        }

        public ActionResult Sil(int id)
        {
            Sepet sepet = db.Sepets.Find(id);
            db.Sepets.Remove(sepet);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}