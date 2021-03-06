﻿using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bilim.Models;
using Bilim.DbFolder;

namespace Bilim.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private AppDb db;

        public HomeController(ILogger<HomeController> logger, AppDb _db)
        {
            _logger = logger;
            db = _db;


            
        }

        public IActionResult Visitor()
        {
            //var kurs = db.Kurs.ToList();
            var kurs = db.Kurs.Take(6);

            return View(kurs);
        }

        public IActionResult AllKurs()
        {
            var list = db.Kurs.ToList();

            ViewBag.ctList1 = db.Categories.ToList();
            ViewBag.ctList2 = db.Categories2.ToList();
            ViewBag.ctList3 = db.Categories3.ToList();

            return View(list);
        }

        [HttpPost]
        public IActionResult AllKurs(string cat1, string cat2, string cat3)
        {
            var ctId1 = db.Categories.FirstOrDefault(x => x.Name == cat1).Id;
            var ctId2 = db.Categories2.FirstOrDefault(x => x.Name == cat2).Id;
            var ctId3 = db.Categories3.FirstOrDefault(x => x.Name == cat3).Id;

            var list = db.Kurs.Where(x => x.CategoryId1 == ctId1 && x.CategoryId2 == ctId2 && x.CategoryId3 == ctId3).ToList();

            ViewBag.ctList1 = db.Categories.ToList();
            ViewBag.ctList2 = db.Categories2.ToList();
            ViewBag.ctList3 = db.Categories3.ToList();

            return View(list);
        }



        public IActionResult Kurs(int? Id)
        {
            if(Id != null)
            {
                ViewBag.Videos = db.KursVideos.Where(x => x.KursId == Id).ToList();
                ViewBag.FreeVideos = db.KursVideos.Where(x => x.KursId == Id && x.Free == true).ToList();

                var kurs = db.Kurs.FirstOrDefault(p => p.Id == Id);
                return View(kurs);
            }
            return View();
        }







        public IActionResult AllVideo()
        {
            var list = db.KursVideos.Where(p => p.Free == true).ToList();



            FreeVideoModel fvm = new FreeVideoModel { KursVideos = list, FreeVideos = db.FreeVideos.ToList() };

            return View(fvm);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
