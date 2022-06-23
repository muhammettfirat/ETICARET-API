
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplicationA.Models;
using System.Data.Entity;

namespace WebApplicationA.Controllers
{
    public class KategoriLController : ApiController
    {
        ETicaret2022Entities1 db = new ETicaret2022Entities1();
        public List<Kategori> Get()
        {
            var data= db.Kategori.ToList();
            return data;
        }
    }
}
