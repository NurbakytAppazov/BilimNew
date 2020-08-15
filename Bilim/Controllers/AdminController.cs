using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bilim.DbFolder;
using Bilim.ViewModels.AdminViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bilim.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public readonly AppDb db;
        public IHostingEnvironment env;


        public AdminController(AppDb _db, IHostingEnvironment _env)
        {
            db = _db;
            env = _env;
        }

        public async Task<IActionResult> UserList(int? page = 1)
        {
            ViewBag.Count = db.Users.Where(x => x.UserName != "BeautyAdmin").Count();
            ViewBag.Page = page;

            var pager = new Pager(ViewBag.Count, page);

            UsersView uv = new UsersView
            {
                Users = await db.Users.Where(x => x.UserName != "BeautyAdmin").Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToListAsync(),
                Pager = pager
            };

            return View(uv);
        }

        public IActionResult AboutUser(string id)
        {
            var user = db.Users.FirstOrDefault(x => x.Id == id);
            ViewBag.UserKurses = db.UserKurs.Where(x => x.UserId == id);

            return View(user);
        }




        public IActionResult KursList()
        {
            var kurs = db.Kurs.ToList();

            return View(kurs);
        }

        public IActionResult KursCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> KursCreate(Kurs kurs, IFormFile file)
        {
            if (file == null || file.Length == 0) return Content("Файл не найден!");

            var imgname = DateTime.Now.ToString("MMddHHmmss") + file.FileName;

            string kursImg = "/kurs/" + imgname;
            using (var stream = new FileStream(env.WebRootPath + kursImg, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            kurs.CreatDate = DateTime.Now;
            kurs.PhotoUrl = imgname;

            db.Kurs.Add(kurs);
            db.SaveChanges();

            return RedirectToAction("KursList", "Admin");
        }

        public IActionResult KursEdit(int Id)
        {
            var kurs = db.Kurs.FirstOrDefault(p => p.Id == Id);
            ViewBag.KursVideos = db.KursVideos.Where(x => x.KursId == Id);

            return View(kurs);
        }
        [HttpPost]
        public async Task<IActionResult> KursEdit(int? kursId, Kurs kurs, IFormFile file)
        {
            if (kursId != null)
            {
                var thisKurs = db.Kurs.FirstOrDefault(x => x.Id == kursId);
                if (file == null || file.Length == 0)
                {
                    kurs.PhotoUrl = thisKurs.PhotoUrl;
                }
                else
                {
                    var imgname = DateTime.Now.ToString("MMddHHmmss") + file.FileName;

                    string kursImg = "/kurs/" + imgname;
                    using (var stream = new FileStream(env.WebRootPath + kursImg, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    kurs.PhotoUrl = imgname;
                }

                thisKurs.Name = kurs.Name;
                thisKurs.Info = kurs.Info;
                thisKurs.AvtorName = kurs.AvtorName;
                thisKurs.AvtorInfo = kurs.AvtorInfo;
                thisKurs.Price = kurs.Price;
                thisKurs.CreatDate = thisKurs.CreatDate;
                thisKurs.PhotoUrl = kurs.PhotoUrl;

                //db.Kurs.Update(kurs);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("KursList", "Admin");
        }

        public IActionResult DeleteKurs(int? Id)
        {
            if (Id != null)
            {
                var kurs = db.Kurs.FirstOrDefault(x => x.Id == Id);

                db.Kurs.Remove(kurs);
                db.SaveChanges();
            }

            return RedirectToAction("KursList", "Admin");
        }



        public IActionResult InKurs(int Id)
        {
            var kurs = db.Kurs.FirstOrDefault(p => p.Id == Id);
            var count = db.UserKurs.Where(p => p.KursId == Id).ToList().Count;
            InKursView ikv = new InKursView { Kurs = kurs, KursBuy = count };
            return View(ikv);
        }



        public IActionResult AddVideo(int? Id)
        {
            ViewBag.Kurs = db.Kurs.FirstOrDefault(x => x.Id == Id);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddVideo(int kursId, string VideoName, string Info, string VideoUrl, IFormFile PhotoUrl)
        {
            if(kursId != 0)
            {
                KursVideo kv = new KursVideo { KursId = kursId, VideoName = VideoName, Info = Info, VideoUrl = VideoUrl };

                if (PhotoUrl == null || PhotoUrl.Length == 0)
                {
                    kv.PhotoUrl = "default.jpg";
                }
                else
                {
                    var imgname = DateTime.Now.ToString("MMddHHmmss") + PhotoUrl.FileName;
                    string path_Root = env.WebRootPath;

                    string path_to_Images = path_Root + "/kursvideo/" + imgname;
                    using (var stream = new FileStream(path_to_Images, FileMode.Create))
                    {
                        await PhotoUrl.CopyToAsync(stream);
                    }

                    kv.PhotoUrl = imgname;
                }
                db.KursVideos.Add(kv);
                await db.SaveChangesAsync();

                return RedirectToAction("KursEdit", new { Id = kv.KursId });
            }
            else
            {
                return RedirectToAction("KursList", "Admin");
            }

        }

        public IActionResult EditVideo(int Id)
        {
            var video = db.KursVideos.FirstOrDefault(p => p.Id == Id);

            return View(video);
        }
        [HttpPost]
        public async Task<IActionResult> EditVideo(int? kursId, int? videoId, KursVideo video, IFormFile PhotoUrl)
        {
            if (kursId != null)
            {
                var thisVideo = db.KursVideos.FirstOrDefault(x => x.Id == videoId);
                if (PhotoUrl == null || PhotoUrl.Length == 0)
                {
                    video.PhotoUrl = thisVideo.PhotoUrl;
                }
                else
                {
                    var imgname = DateTime.Now.ToString("MMddHHmmss") + PhotoUrl.FileName;
                    string path_Root = env.WebRootPath;

                    string path_to_Images = path_Root + "/kursvideo/" + imgname;
                    using (var stream = new FileStream(path_to_Images, FileMode.Create))
                    {
                        await PhotoUrl.CopyToAsync(stream);
                    }

                    video.PhotoUrl = imgname;
                }

                thisVideo.VideoName = video.VideoName;
                thisVideo.Info = video.Info;
                thisVideo.VideoUrl = video.VideoUrl;
                thisVideo.PhotoUrl = video.PhotoUrl;
                thisVideo.KursId = (int)kursId;

                await db.SaveChangesAsync();

                return RedirectToAction("KursEdit", new { Id = video.KursId });
            }
            else
            {
                return RedirectToAction("KursList", "Admin");
            }

        }


        public IActionResult DeleteVideo(int Id)
        {
            var video = db.KursVideos.FirstOrDefault(p => p.Id == Id);

            db.KursVideos.Remove(video);
            db.SaveChanges();

            return RedirectToAction("KursEdit", new { Id = video.KursId });
        }


    }
}