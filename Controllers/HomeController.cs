using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeBankApplication.Models;

namespace WeBankApplication.Controllers
{
    public class HomeController : Controller
    {
  
        public ActionResult Index()
        {
            //string Email = Session["Id"].ToString();
            //ViewBag.Message = Email;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Savings and Investment are made simple.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}