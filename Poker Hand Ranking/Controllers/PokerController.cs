using Poker_Hand_Ranking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Poker_Hand_Ranking.Controllers
{
    public class PokerController : Controller
    {
        // GET: Poker
        public ActionResult Index()
        {
            // This is not in any way complete or realistic
            // Just being used to generate a somewhat random poker hand
            PokerPlayer playerOne = new PokerPlayer("John");

            // User the PokerPlayer's hand we'll create an instance of the
            // PokerHand to send out to the view
            PokerHand playerOneHand = new PokerHand(playerOne.PlayerHand);

            // This was done just to be quick
            ViewBag.Message = playerOneHand.ToString();

            return View();

        }
        protected override void HandleUnknownAction(string actionName)
        {
            RedirectToAction("Index").ExecuteResult(this.ControllerContext);
        }

    }
}