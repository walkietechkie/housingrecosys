using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using CLIPSNET;

namespace HousingRecommendationSystem.Controllers
{
    public class HomeController : Controller
    {
        private CLIPSNET.Environment clips = new CLIPSNET.Environment();

        public ActionResult Index()
        {
            InitializeClips();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private void InitializeClips()
        {
            clips.LoadFromResource(Properties.Resources.recommendation_engine);
            clips.Reset();
        }
    }
}