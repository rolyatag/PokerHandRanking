using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Poker_Hand_Ranking.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Poker Hand Ranking Exercise.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Gregg Taylor";

            return View();
        }
    }
}