using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WeBankApplication.Models;


namespace WeBankApplication.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        WeBankEntities12 entity = new WeBankEntities12();

        // GET: Admin
        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(AdminViewModel credentials)
        {
            bool EmailExists = entity.Admins.Any(x => x.Email == credentials.Email && x.Passcode == credentials.Password);
            Admin u = entity.Admins.FirstOrDefault(x => x.Email == credentials.Email && x.Passcode == credentials.Password);

            if (EmailExists)
            {
                FormsAuthentication.SetAuthCookie(u.Email, false);
                return RedirectToAction("AdminPage");
            }

            ModelState.AddModelError("", "Username or Password is wrong");
            return View();
        }

        public ActionResult AdminPage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminPage(Admin ad)
        {
            return View();
        }
    }
}