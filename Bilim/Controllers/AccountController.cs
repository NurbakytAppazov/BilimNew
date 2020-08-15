using System.Linq;
using System.Threading.Tasks;
using Bilim.DbFolder;
using Bilim.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bilim.Controllers
{
    public class AccountController : Controller
    {
        AppDb db;
        UserManager<User> _userManager;
        SignInManager<User> _signInManager;


        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, AppDb _db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            db = _db;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { UserName = model.PhoneNumber, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName, PhoneNumber = model.PhoneNumber };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Profil", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            if(User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("UserList", "Admin");
                    //return RedirectToAction("UserList", "Admin", new { page = 1 });
                }
                else
                {
                    return RedirectToAction("Profil", "Account");
                }
            }
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.PhoneNumber, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Profil", "Account");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Құпиясөз және(немесе) логин қате!");
                }
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Visitor", "Home");
        }



        [Authorize]
        public IActionResult Profil()
        {
            if(User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("UserList", "Admin");
                    //return RedirectToAction("UserList", "Admin", new { page = 1 });
                }
                else
                {
                    var kurs = db.Kurs.ToList();
                    return View(kurs);
                }
            }
            return RedirectToAction("Login");
        }
        [Authorize]
        public async Task<IActionResult> Video(int id, int videoId = 1)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("UserList", "Admin");
                    //return RedirectToAction("UserList", "Admin", new { page = 1 });
                }
                else
                {
                    var kurs = await db.Kurs.FirstOrDefaultAsync(x => x.Id == id);
                    var video = await db.KursVideos.FirstOrDefaultAsync(x => x.Id == videoId);

                    ViewBag.OtherVideos = await db.KursVideos.Where(x => x.Id != videoId).ToListAsync();

                    return View(video);
                }
            }
            return RedirectToAction("Login");
        }
    }
}