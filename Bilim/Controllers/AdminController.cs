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
            ViewBag.Count = db.Users.Where(x => x.UserName != "Admin").Count();
            ViewBag.Page = page;

            var pager = new Pager(ViewBag.Count, page);

            UsersView uv = new UsersView
            {
                Users = await db.Users.Where(x => x.UserName != "Admin").Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToListAsync(),
                Pager = pager
            };

            return View(uv);
        }

        public async Task<IActionResult> AboutUser(string id)
        {
            AboutUserView auv = new AboutUserView()
            {
                User = db.Users.FirstOrDefault(x => x.Id == id),
                Kurs = await db.Kurs.ToListAsync(),
                UserKurs = await db.UserKurs.Where(x => x.UserId == id).ToListAsync()
            };

            return View(auv);
        }
        [HttpPost]
        public async Task<IActionResult> AboutUser(string userId, int kursId)
        {
            if (userId != null && kursId != 0)
            {
                var have = await db.UserKurs.FirstOrDefaultAsync(x => x.UserId == userId && x.KursId == kursId);
                if(have != null)
                {
                    db.Remove(have);
                    await db.SaveChangesAsync();
                }
                else
                {
                    UserKurs uk = new UserKurs { UserId = userId, KursId = kursId };

                    await db.AddAsync(uk);
                    await db.SaveChangesAsync();
                }
                AboutUserView auv = new AboutUserView()
                {
                    User = db.Users.FirstOrDefault(x => x.Id == userId),
                    Kurs = await db.Kurs.ToListAsync(),
                    UserKurs = await db.UserKurs.Where(x => x.UserId == userId).ToListAsync()
                };
                return View(auv);
            }

            return RedirectToAction("UserList");
        }

        //Category Actions

        public async Task<IActionResult> CategoryList()
        {
            CategoryView cv = new CategoryView
            {
                Categories = await db.Categories.ToListAsync(),
                Categories2 = await db.Categories2.ToListAsync(),
                Categories3 = await db.Categories3.ToListAsync(),
            };

            return View(cv);
        }
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(string ctmodel, string ctName)
        {
            if (ctmodel == "Category1")
            {
                Category ct1 = new Category
                {
                    Name = ctName
                };
                await db.Categories.AddAsync(ct1);
            }
            else if (ctmodel == "Category2")
            {
                Category2 ct2 = new Category2
                {
                    Name = ctName
                };
                await db.Categories2.AddAsync(ct2);
            }
            else if (ctmodel == "Category3")
            {
                Category3 ct3 = new Category3
                {
                    Name = ctName
                };
                await db.Categories3.AddAsync(ct3);
            }
            await db.SaveChangesAsync();
            return RedirectToAction("CategoryList");
        }

        public async Task<IActionResult> EditCategory(string ct)
        {
            ViewBag.ctmodel = ct;
            if (ct == "category1")
            {
                ViewBag.ctList = await db.Categories.ToListAsync();
            }
            else if (ct == "category2")
            {
                ViewBag.ctList = await db.Categories2.ToListAsync();
            }
            else if (ct == "category3")
            {
                ViewBag.ctList = await db.Categories3.ToListAsync();
            }
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditCategory(string ctmodel, int[] ctId, string[] ctNames)
        {
            if (ctmodel == "category1")
            {
                int counter = 0;
                foreach(var ctName in ctNames)
                {
                    var ct = await db.Categories.FirstOrDefaultAsync(x => x.Id == ctId[counter]);
                    ct.Name = ctName;
                    counter++;
                }
            }
            else if (ctmodel == "category2")
            {
                int counter = 0;
                foreach (var ctName in ctNames)
                {
                    var ct2 = await db.Categories2.FirstOrDefaultAsync(x => x.Id == ctId[counter]);
                    ct2.Name = ctName;
                    counter++;
                }
            }
            else if (ctmodel == "category3")
            {
                int counter = 0;
                foreach (var ctName in ctNames)
                {
                    var ct3 = await db.Categories3.FirstOrDefaultAsync(x => x.Id == ctId[counter]);
                    ct3.Name = ctName;
                    counter++;
                }
            }

            await db.SaveChangesAsync();
            return RedirectToAction("CategoryList");
        }
        public IActionResult DeleteCategory(int id, string ctmodel)
        {
            if (id != 0)
            {
                if(ctmodel == "category1")
                {
                    var ct = db.Categories.FirstOrDefault(x => x.Id == id);
                    db.Categories.Remove(ct);
                }
                else if (ctmodel == "category2")
                {
                    var ct = db.Categories2.FirstOrDefault(x => x.Id == id);
                    db.Categories2.Remove(ct);
                }
                else if (ctmodel == "category3")
                {
                    var ct = db.Categories3.FirstOrDefault(x => x.Id == id);
                    db.Categories3.Remove(ct);
                }
                db.SaveChanges();
            }

            return RedirectToAction("CategoryList", "Admin");
        }
        //Category Actions



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
        public async Task<IActionResult> KursCreate(Kurs kurs, IFormFileCollection files)
        {
            foreach(var file in files)
            {
                if (file == null) return Content("Файл не найден!");

                var img1 = DateTime.Now.ToString("MMddHHmmss") + file.FileName;
                using (var stream = new FileStream(env.WebRootPath + "/kurs/" + img1, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                };
            }

            kurs.CreatDate = DateTime.Now;
            kurs.BannerUrl = DateTime.Now.ToString("MMddHHmmss") + files[0].FileName;
            kurs.PhotoUrl = DateTime.Now.ToString("MMddHHmmss") + files[1].FileName;
            kurs.AvtorImgUrl = DateTime.Now.ToString("MMddHHmmss") + files[2].FileName;

            db.Kurs.Add(kurs);
            db.SaveChanges();

            return RedirectToAction("KursList", "Admin");
        }

        public IActionResult KursEdit(int Id)
        {
            var kurs = db.Kurs.FirstOrDefault(p => p.Id == Id);

            ViewBag.ctList1 = db.Categories.ToList();
            ViewBag.ctList2 = db.Categories2.ToList();
            ViewBag.ctList3 = db.Categories3.ToList();

            ViewBag.KursVideos = db.KursVideos.Where(x => x.KursId == Id);

            return View(kurs);
        }

        [HttpPost]
        public async Task<IActionResult> KursEdit(Kurs kurs, IFormFile banner, IFormFile fon, IFormFile avtor, string ctName1, string ctName2, string ctName3)
        {
            if (banner != null)
            {
                var imgname = DateTime.Now.ToString("MMddHHmmss") + banner.FileName;
                using (var stream = new FileStream(env.WebRootPath + "/kurs/" + imgname, FileMode.Create))
                {
                    await banner.CopyToAsync(stream);
                };
                kurs.BannerUrl = imgname;
            }
            //photo
            if (fon != null)
            {
                var imgname = DateTime.Now.ToString("MMddHHmmss") + fon.FileName;
                using (var stream = new FileStream(env.WebRootPath + "/kurs/" + imgname, FileMode.Create))
                {
                    await fon.CopyToAsync(stream);
                };
                kurs.PhotoUrl = imgname;
            }
            //avatar
            if (avtor != null)
            {
                var imgname = DateTime.Now.ToString("MMddHHmmss") + avtor.FileName;
                using (var stream = new FileStream(env.WebRootPath + "/kurs/" + imgname, FileMode.Create))
                {
                    await avtor.CopyToAsync(stream);
                };
                kurs.AvtorImgUrl = imgname;
            }
            int ctId1 = db.Categories.FirstOrDefault(x => x.Name == ctName1).Id;
            int ctId2 = db.Categories2.FirstOrDefault(x => x.Name == ctName2).Id;
            int ctId3 = db.Categories3.FirstOrDefault(x => x.Name == ctName3).Id;
            kurs.CategoryId1 = ctId1;
            kurs.CategoryId2 = ctId2;
            kurs.CategoryId3 = ctId3;

            db.Kurs.Update(kurs);
            await db.SaveChangesAsync();

            return RedirectToAction("KursList");
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
        public async Task<IActionResult> AddVideo(int kursId, string VideoName, string Info, string VideoUrl, IFormFile PhotoUrl, bool Free)
        {
            if(kursId != 0)
            {
                KursVideo kv = new KursVideo { KursId = kursId, VideoName = VideoName, Info = Info, VideoUrl = VideoUrl };

                if(Free == true)
                {
                    kv.Free = true;
                }
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

                thisVideo.Free = video.Free;
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




        public IActionResult FreeVideoList()
        {
            return View(db.Categories.ToList());
        }



        public IActionResult FreeVideoAdd()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> FreeVideoAdd(Category model)
        {
            Category c = new Category { Name = model.Name };

            db.Categories.Add(c);

            await db.SaveChangesAsync();

            return RedirectToAction("FreeVideoList");


        }
    }
}