using System.Diagnostics;
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
            var kurs = db.Kurs.ToList();

            return View(kurs);
        }



        public IActionResult Kurs(int? Id)
        {
            if(Id != null)
            {
                var kurs = db.Kurs.FirstOrDefault(p => p.Id == Id);

                return View(kurs);
            }
            return View();
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
