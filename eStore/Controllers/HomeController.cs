using BusinessObject;
using DataAccess.Repository;
using eStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace eStore.Controllers
{
    public class HomeController : Controller
    {
        
        IMemberRepository memberRepository = new MemberRepository();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Member member)
        {
            if (member.Email == "admin@estore.com" && member.Password == "admin@@")
            {
                Member admin = new Member();
                admin.Email = member.Email;
                admin.Password = member.Password;
                CookieOptions cookieAdmin = new CookieOptions();
                cookieAdmin.Expires = DateTime.Now.AddMinutes(30);
                Response.Cookies.Append("ADMIN", admin.Email);
                return RedirectToAction("Index", "Member");
            }
            else
            {
                var user = memberRepository.CheckLogin(member.Email, member.Password);
                CookieOptions cookieUser = new CookieOptions();
                cookieUser.Expires = DateTime.Now.AddMinutes(30);
                Response.Cookies.Append("USER", user.Email);
                return RedirectToAction("GetOrderByMemberID", "Order", new {id = user.MemberId});
            }
            
        }

        public ActionResult Logout()
        {
            var checkAdmin = Request.Cookies["ADMIN"];
            if (checkAdmin != null)
            {
                Response.Cookies.Delete("ADMIN");
            }
            var checkUser = Request.Cookies["USER"];
            if (checkUser != null)
            {
                Response.Cookies.Delete("USER");
            }
            return RedirectToAction("Index", "Home");
            
        }
    }
}